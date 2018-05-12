using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using KnapsackProblem;
using Algorithm;
using CustomLogger;
namespace ExcelReport
{
    public class MeasurePerformanceReport
    {
        private Excel.Application m_excel;
        private ITask m_task;
        private int m_iterationCount;
        private int m_populationCount;
        private int m_runsCount;
        private int m_dataSize;
        private int m_instancesCount;
        private DirectoryInfo m_dir;

        public MeasurePerformanceReport(ITask task = null,
            int iterationCount = 1, int populationCount = 1, int startCount = 1, int dataSize = 15, int instancesCount = 1)
        {
            m_task = task;
            m_iterationCount = iterationCount;
            m_populationCount = populationCount;
            m_runsCount = startCount;
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
            IData[] data = { new UncorrData(m_dataSize, new Range(10, 9999)),
                new WeaklyCorrData(m_dataSize, new Range(10, 9999)),
                new StronglyCorrData(m_dataSize, new Range(10, 9999)),
                new SubsetSumData(m_dataSize, new Range(1, 9999)),
                new VeryVeryStronglyCorrData(m_dataSize, new Range(1, 9999))};

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

            for (int dataIndex = 0; dataIndex < data.Length; ++dataIndex)
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

                for (int instIndex = 0; instIndex < m_instancesCount; instIndex++)
                {
                    var taskData = m_task.Create(data[dataIndex]);

                    var gaResult = new List<long>();
                    var dpResults = new List<long>[dps.Count];
                    var bbResults = new List<long>[bbs.Count];
                    var gaElapsedTime = new List<double>();
                    var dpElapsedTime = new List<double>[dps.Count];
                    var bbElapsedTime = new List<double>[bbs.Count];

                    for (int s = 0; s < m_runsCount; s++)
                    {
                        for (var i = 0; i < dps.Count; ++i)
                        {
                            if (dpElapsedTime[i] == null) dpElapsedTime[i] = new List<double>();
                            dpElapsedTime[i].Add(Measure(() =>
                            {
                                try
                                {
                                    if (dpResults[i] == null) dpResults[i] = new List<long>();
                                    dpResults[i].Add(dps[i].Run(taskData));
                                }
                                catch (Exception e)
                                {
                                    Logger.Get().Error("ERROR DP[" + i + "], e: " + e);
                                }
                            }, TimeSpan.FromMinutes(5)));
                        }

                        for (var i = 0; i < bbs.Count; ++i)
                        {
                            if (bbElapsedTime[i] == null) bbElapsedTime[i] = new List<double>();
                            bbElapsedTime[i].Add(Measure(() =>
                            {
                                try
                                {
                                    if (bbResults[i] == null) bbResults[i] = new List<long>();
                                    bbResults[i].Add(bbs[i].Run(taskData));
                                }
                                catch (Exception e)
                                {
                                    Logger.Get().Error("ERROR BB[" + i + "], e: " + e);
                                }
                            }, TimeSpan.FromMinutes(5)));
                        }

                        if (gaElapsedTime == null) gaElapsedTime = new List<double>();
                        gaElapsedTime.Add(Measure(() =>
                        {
                            ga.SetData(taskData);
                            try
                            {
                                if (gaResult == null) gaResult = new List<long>();
                                gaResult.Add(ga.Run(m_iterationCount, m_populationCount, 2));

                            }
                            catch (Exception e)
                            {
                                Logger.Get().Error("ERROR GA, e: " + e);
                            }
                        }, TimeSpan.FromMinutes(5)));

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
                    for (var i = 0; i < dps.Count; ++i)
                    {
                        var avg = GetAvg(dpResults[i]);
                        sheet.Cells[instIndex + 3, startIndexForElapsedTime + i] = GetMedian(dpElapsedTime[i]);
                        sheet.Cells[instIndex + 3, startIndexForResult + i] = avg;
                        if (i == 0)
                        {
                            gold = avg;
                        }
                        else
                        {
                            sheet.Cells[instIndex + 3, startIndexForDeviation + i] = (gold - avg) / gold;
                        }
                    }

                    startIndexForElapsedTime += dps.Count;
                    startIndexForResult += dps.Count;
                    startIndexForDeviation += dps.Count;
                    for (var i = 0; i < bbs.Count; ++i)
                    {
                        var avg = GetAvg(bbResults[i]);
                        sheet.Cells[instIndex + 3, startIndexForElapsedTime + i] = GetMedian(bbElapsedTime[i]);
                        sheet.Cells[instIndex + 3, startIndexForResult + i] = avg;
                        sheet.Cells[instIndex + 3, startIndexForDeviation + i] = (gold - avg) / gold;
                    }

                    startIndexForElapsedTime += bbs.Count;
                    startIndexForResult += bbs.Count;
                    startIndexForDeviation += bbs.Count;

                    var gaAvg = GetAvg(gaResult);
                    sheet.Cells[instIndex + 3, startIndexForElapsedTime] = GetMedian(gaElapsedTime);
                    sheet.Cells[instIndex + 3, startIndexForResult] = gaAvg;
                    sheet.Cells[instIndex + 3, startIndexForDeviation] = (gold - gaAvg) / gold;
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
                long elapsedMs = timeSpan.Milliseconds;
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
            long result = 0;
            var count = items.Count;
            foreach (var item in items)
            {
                if (item == 0) --count;
                result += item;
            }
            return  (double)result / count;
        }

        private static double GetMedian(List<double> items)
        {
            items.Sort();
            var count = items.Count;

            if (count % 2 != 0) return items[count / 2];

            return (items[(count - 1) / 2] + items[count / 2]) / 2.0;
        }
    }
}
