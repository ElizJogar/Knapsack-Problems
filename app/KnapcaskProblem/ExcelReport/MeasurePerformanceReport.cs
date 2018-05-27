using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Linq;
using KnapsackProblem;
using Algorithm;
using CustomLogger;
namespace ExcelReport
{
    public class MeasurePerformanceReport : IReport
    {
        private Excel.Application m_excel;
        private ITask m_task;
        private int m_iterationCount;
        private int m_populationCount;
        private int m_runsCount;
        private int m_betta;
        private int m_dataSize;
        private int m_instancesCount;
        private DirectoryInfo m_dir;

        public MeasurePerformanceReport(ITask task = null,
            int iterationCount = 1, int populationCount = 1, int startCount = 1, int betta = 2, int dataSize = 15, int instancesCount = 1)
        {
            m_task = task;
            m_iterationCount = iterationCount;
            m_populationCount = populationCount;
            m_runsCount = startCount;
            m_betta = betta;
            m_dataSize = dataSize;
            m_instancesCount = instancesCount;

            DateTime localDate = DateTime.Now;
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_dir = new DirectoryInfo(myDocPath + @"\knapsack_problems_doc\mp_reports_" + task.Str() + "_"
                + localDate.ToShortDateString() + "-" + localDate.Hour
                + "." + localDate.Minute + "." + localDate.Second + "." + localDate.Millisecond);
            m_dir.Create();
            m_excel = new Excel.Application();
        }
        public DirectoryInfo GetDir()
        {
            return m_dir;
        }
        public void Create()
        {
            IData[] data = {
                new UncorrData(m_dataSize, new Range(10, 9999)),
                new WeaklyCorrData(m_dataSize, new Range(10, 9999)),
                new StronglyCorrData(m_dataSize, new Range(10, 9999)),
                new SubsetSumData(m_dataSize, new Range(1, 9999)),
                new VeryVeryStronglyCorrData(m_dataSize, new Range(1, 9999))
            };

            var dps = new List<IExactAlgorithm>();
            var bbs = new List<IExactAlgorithm>();
            IHeuristicAlgorithm ga = null;

            var names = new List<string>();

            if (m_task as UKPTask != null)
            {
                dps.Add(new DynamicProgramming(new ClassicalUKPApproach()));
                names.Add("DP");
                dps.Add(new DynamicProgramming(new EDUK_EX(2, 2)));
                names.Add("EDUK");
                bbs.Add(new BranchAndBound(new DFS(), new U3Bound()));
                names.Add("BB DFS U3");
                bbs.Add(new BranchAndBound(new BFS(), new U3Bound()));
                names.Add("BB BFS U3");
            }
            else if (m_task as KPTask != null)
            {
                dps.Add(new DynamicProgramming(new DirectApproach()));
                names.Add("DP Direct");
                dps.Add(new DynamicProgramming(new RecurrentApproach()));
                names.Add("DP Recurrent");
                bbs.Add(new BranchAndBound(new DFS()));
                names.Add("BB DFS");
                bbs.Add(new BranchAndBound(new BFS()));
                names.Add("BB BFS");
            }

            names.Add("GA");

            for (int dataIndex = 0; dataIndex < data.Length - 1; ++dataIndex)
            {
                var factory = Factory.Create(m_task, data[dataIndex]);

                ga = new GeneticAlgorithm(factory.GetInitialPopulation(),
                                           factory.GetCrossover(),
                                           factory.GetMutation(),
                                           factory.GetSelection());

                var workbook = m_excel.Workbooks.Add(Type.Missing);
                m_excel.SheetsInNewWorkbook = 1;
                var sheet = workbook.Sheets[1];
                sheet.Cells.ColumnWidth = 15;

                sheet.Cells[1, 2] = "Elapsed Time (ms)";

                var startIndexForElapsedTime = 2;
                var startIndexForResult = startIndexForElapsedTime + dps.Count + bbs.Count + 2;
                for (var i = 0; i < names.Count; ++i)
                {
                    sheet.Cells[2, startIndexForElapsedTime + i] = names[i];
                    sheet.Cells[2, startIndexForResult + i] = names[i];
                }
                for (var i = 0; i < names.Count - 1; ++i)
                {
                    sheet.Cells[2, startIndexForResult + names.Count + i] = names[i + 1] + " deviation %";
                }

                for (int instIndex = 0; instIndex < m_instancesCount; ++instIndex)
                {
                    var taskData = m_task.Create(data[dataIndex]);

                    var results = new List<long>[dps.Count + bbs.Count + 1];
                    var elapsedTime = new List<double>[dps.Count + bbs.Count + 1];
                    var elapsedTimeMedians = new List<double>();
                    long time = 0;
                    long result = 0;
                    for (int s = 0; s < m_runsCount; ++s)
                    {
                        for (var i = 0; i < dps.Count + bbs.Count; ++i)
                        {
                            time = Measure(() =>
                            {
                                try
                                {
                                    IExactAlgorithm alg = i < dps.Count ? dps[i] : bbs[i - dps.Count];
                                    result = alg.Run(taskData);
                                }
                                catch (Exception e)
                                {
                                    Logger.Get().Error("ERROR ALG[" + i + "], e: " + e);
                                }
                            }, TimeSpan.FromMinutes(25));
                            if (time != -1)
                            {
                                if (elapsedTime[i] == null) elapsedTime[i] = new List<double>();
                                if (results[i] == null) results[i] = new List<long>();

                                elapsedTime[i].Add(time);
                                results[i].Add(result);
                            }
                            else
                            {
                                Logger.Get().Info("Stopped by timer");
                            }
                        }

                        var lastIndex = elapsedTime.Length - 1;
                        time = Measure(() =>
                        {
                            ga.SetData(taskData);
                            try
                            {
                                result = ga.Run(m_iterationCount, m_populationCount, m_betta);
                            }
                            catch (Exception e)
                            {
                                Logger.Get().Error("ERROR GA, e: " + e);
                            }
                        }, TimeSpan.FromMinutes(25));
                        if (time != -1)
                        {
                            if (elapsedTime[lastIndex] == null) elapsedTime[lastIndex] = new List<double>();
                            if (results[lastIndex] == null) results[lastIndex] = new List<long>();

                            elapsedTime[lastIndex].Add(time);
                            results[lastIndex].Add(result);
                        }
                        else
                        {
                            Logger.Get().Info("Stopped by timer");
                        }
                    }

                    var currData = data[dataIndex];
                    Logger.Get().Info(currData.Str() + ", instance: " + instIndex);
                    Logger.Get().Info("Cost: " + string.Join(", ", currData.Cost));
                    Logger.Get().Info("Weight: " + string.Join(", ", currData.Weight));
                    Logger.Get().Info("Capacity: " + currData.Capacity);

                    double gold = 1;

                    startIndexForElapsedTime = 2;
                    startIndexForResult = startIndexForElapsedTime + dps.Count + bbs.Count + 2;
                    var startIndexForDeviation = startIndexForResult + dps.Count + bbs.Count;
                    for (var i = 0; i < results.Length; ++i)
                    {
                        var avg = GetAvg(results[i]);
                        var median = GetMedian(elapsedTime[i]);
                        sheet.Cells[instIndex + 3, startIndexForElapsedTime + i] = median;
                        sheet.Cells[instIndex + 3, startIndexForResult + i] = avg;
                        elapsedTimeMedians.Add(median);
                        if (i == 0)
                        {
                            gold = avg;
                        }
                        else
                        {
                            sheet.Cells[instIndex + 3, startIndexForDeviation + i] = (gold - avg) / gold;
                        }
                    }
                    sheet.Cells[instIndex + 3, startIndexForElapsedTime + results.Length] = names[elapsedTimeMedians.IndexOf(elapsedTimeMedians.Min())];
                }
                workbook.SaveAs(m_dir + @"\" + data[dataIndex].Str() + ".xlsx");
                workbook.Close();
            }
            m_excel.Quit();
        }

        private static long Measure(Action codeBlock, TimeSpan timeSpan = default(TimeSpan))
        {
            try
            {
                long elapsedMs = -1;
                Task task = Task.Factory.StartNew(() =>
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    codeBlock();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                });
                task.Wait(timeSpan);
                return elapsedMs;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }
        private static double GetAvg(List<long> items)
        {
            if (items == null || items.Count == 0)
            {
                return -1;
            }

            long result = 0;
            var count = items.Count;

            foreach (var item in items)
            {
                if (item == 0) --count;
                result += item;
            }
            return (double)result / count;
        }

        private static double GetMedian(List<double> items)
        {
            if (items == null || items.Count == 0)
            {
                return -1;
            }

            items.Sort();
            var count = items.Count;

            if (count % 2 != 0) return items[count / 2];

            return (items[(count - 1) / 2] + items[count / 2]) / 2.0;
        }
    }
}
