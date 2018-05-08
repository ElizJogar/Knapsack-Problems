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
            m_dir = new DirectoryInfo(myDocPath + @"\gen_algorithm_doc\mp_reports_" + task.Str() + "_"
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
                dps.Add(new DynamicProgramming(new EDUK_EX(2, 2)));
                names.Add("EDUK");
                dps.Add(new DynamicProgramming(new ClassicalUKPApproach()));
                names.Add("DP");
                bbs.Add(new BranchAndBound(new DFS(), new U3Bound()));
                names.Add("BB DFS U3");
                bbs.Add(new BranchAndBound(new BFS(), new U3Bound()));
                names.Add("BB BFS U3");
                ga = new GeneticAlgorithm(new RandomPopulation(),
                new UniformCrossover(),
                new PointMutation(),
                new LinearRankSelection(new PenaltyFunction(), new RepairOperator()));
                names.Add("GA");
            }
            else if (m_task as KPTask != null)
            {
                dps.Add(new DynamicProgramming(new RecurrentApproach()));
                names.Add("DP Recurrent");
                dps.Add(new DynamicProgramming(new DirectApproach()));
                names.Add("DP Direct");
                bbs.Add(new BranchAndBound(new DFS()));
                names.Add("BB DFS");
                bbs.Add(new BranchAndBound(new BFS()));
                names.Add("BB BFS");
                ga = new GeneticAlgorithm(new RandomPopulation(),
                new UniformCrossover(),
                new Saltation(),
                new LinearRankSelection(new PenaltyFunction(), new RepairOperator()));
                names.Add("GA");
            }

            for (int dataIndex = 0; dataIndex < data.Length; ++dataIndex)
            {
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
                sheet.Cells[2, startIndexForResult + names.Count] = "GA deviation %";

                for (int instIndex = 0; instIndex < m_instancesCount; instIndex++)
                {
                    var taskData = m_task.Create(data[dataIndex]);

                    long gaResult = 0;
                    var dpResults = new long[dps.Count];
                    var bbResults = new long[bbs.Count];
                    double gaElapsedTime = 0;
                    var dpElapsedTime = new double[dps.Count];
                    var bbElapsedTime = new double[bbs.Count];

                    for (int s = 0; s < m_runsCount; s++)
                    {
                        for (var i = 0; i < dps.Count; ++i)
                        {
                            dpElapsedTime[i] += Measure(() =>
                            {
                               dpResults[i] += dps[i].Run(taskData);
                            }, TimeSpan.FromMinutes(30));
                        }

                        for (var i = 0; i < bbs.Count; ++i)
                        {
                            bbElapsedTime[i] += Measure(() =>
                            {
                               bbResults[i] += bbs[i].Run(taskData);
                            }, TimeSpan.FromMinutes(30));
                        }

                        gaElapsedTime += Measure(() =>
                        {
                            ga.SetData(taskData);
                            gaResult += ga.Run(m_iterationCount, m_populationCount);
                        }, TimeSpan.FromMinutes(30));

                    }

                    var currData = data[dataIndex];
                    Logger.Get().Info(currData.Str() + ", instance: " + instIndex);
                    Logger.Get().Info("Cost: " + string.Join(", ", currData.Cost));
                    Logger.Get().Info("Weight: " + string.Join(", ", currData.Weight));
                    Logger.Get().Info("Capacity: " + currData.Capacity);

                    startIndexForElapsedTime = 2;
                    startIndexForResult = startIndexForElapsedTime + dps.Count + bbs.Count + 2;
                    for (var i = 0; i < dps.Count; ++i)
                    {
                        sheet.Cells[instIndex + 3, startIndexForElapsedTime + i] = dpElapsedTime[i] / m_runsCount;
                        sheet.Cells[instIndex + 3, startIndexForResult + i] = dpResults[i] / m_runsCount;
                    }

                    startIndexForElapsedTime += dps.Count;
                    startIndexForResult += dps.Count;
                    for (var i = 0; i < bbs.Count; ++i)
                    {
                        sheet.Cells[instIndex + 3, startIndexForElapsedTime + i] = bbElapsedTime[i] / m_runsCount;
                        sheet.Cells[instIndex + 3, startIndexForResult + i] = bbResults[i] / m_runsCount;
                    }

                    startIndexForElapsedTime += bbs.Count;
                    startIndexForResult += bbs.Count;
                    sheet.Cells[instIndex + 3, startIndexForElapsedTime] = gaElapsedTime / m_runsCount;
                    sheet.Cells[instIndex + 3, startIndexForResult] = gaResult / m_runsCount;
                    sheet.Cells[instIndex + 3, startIndexForResult + 1] = ((double)(bbResults[0] - gaResult) / m_runsCount) / bbResults[0];
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

        private static string GetAlgorithmName(Object ob)
        {
            string res = Convert.ToString(ob);
            return res.Replace("Algorithm.", "");
        }
    }
}
