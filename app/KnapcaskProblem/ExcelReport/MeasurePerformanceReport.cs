using System;
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

            IExactAlgorithm dp = null;
            IExactAlgorithm bb = null;
            IHeuristicAlgorithm ga = null;
            if (m_task as UKPTask != null)
            {
                dp = new DynamicProgramming(new EDUK_EX(2, 2));
                bb = new BranchAndBound(new U3Bound());
                ga = new GeneticAlgorithm(new RandomPopulation(),
                new UniformCrossover(),
                new PointMutation(),
                new LinearRankSelection(new PenaltyFunction(), new RepairOperator()));
            }
            else if (m_task as KPTask != null)
            {
                dp = new DynamicProgramming(new RecurrentApproach());
                bb = new BranchAndBound();
                ga = new GeneticAlgorithm(new RandomPopulation(),
                new UniformCrossover(),
                new Saltation(),
                new LinearRankSelection(new PenaltyFunction(), new RepairOperator()));
            }

            for (int dataIndex = 0; dataIndex < data.Length; ++dataIndex)
            {
                var workbook = m_excel.Workbooks.Add(Type.Missing);
                m_excel.SheetsInNewWorkbook = 1;
                var sheet = workbook.Sheets[1];
                sheet.Cells.ColumnWidth = 15;

                sheet.Cells[1, 2] = "Elapsed Time (ms)";
                sheet.Cells[2, 2] = "Dynamic Programming";
                sheet.Cells[2, 3] = "Branch and Bound";
                sheet.Cells[2, 4] = "Genetic Algorithm";
 
                sheet.Cells[1, 7] = "Results";
                sheet.Cells[2, 7] = "Dynamic Programming";
                sheet.Cells[2, 8] = "Branch and Bound";
                sheet.Cells[2, 9] = "Genetic Algorithm";
                sheet.Cells[2, 10] = "GA deviation %";

                for (int instIndex = 0; instIndex < m_instancesCount; instIndex++)
                {
                    var taskData = m_task.Create(data[dataIndex]);

                    long gaResult = 0, dpResult = 0, bbResult = 0;
                    double gaElapsedTime = 0, dpElapsedTime = 0, bbElapsedTime = 0;
                    for (int s = 0; s < m_runsCount; s++)
                    {
                        dpElapsedTime += Measure(() =>
                        {
                            dpResult += dp.Run(taskData);
                        }, TimeSpan.FromMinutes(30));

                        bbElapsedTime += Measure(() =>
                        {
                            bbResult += bb.Run(taskData);
                        }, TimeSpan.FromMinutes(30));

                        gaElapsedTime += Measure(() =>
                        {
                            ga.SetData(taskData);
                            gaResult += ga.Run(m_iterationCount, m_populationCount);
                        }, TimeSpan.FromMinutes(30));

                    }

                    gaResult /= m_runsCount;
                    dpResult /= m_runsCount;
                    bbResult /= m_runsCount;

                    gaElapsedTime /= m_runsCount;
                    dpElapsedTime /= m_runsCount;
                    bbElapsedTime /= m_runsCount;

                    var currData = data[dataIndex];
                    Logger.Get().Info(currData.Str() + ", instance: " + instIndex);
                    Logger.Get().Info("Cost: " + string.Join(", ", currData.Cost));
                    Logger.Get().Info("Weight: " + string.Join(", ", currData.Weight));
                    Logger.Get().Info("Capacity: " + currData.Capacity);

                    sheet.Cells[instIndex + 3, 2] = dpElapsedTime;
                    sheet.Cells[instIndex + 3, 3] = bbElapsedTime;
                    sheet.Cells[instIndex + 3, 4] = gaElapsedTime;

                    sheet.Cells[instIndex + 3, 7] = dpResult;
                    sheet.Cells[instIndex + 3, 8] = bbResult;
                    sheet.Cells[instIndex + 3, 9] = gaResult;
                    sheet.Cells[instIndex + 3, 10] = (double)(dpResult - gaResult) / dpResult;
                }
                workbook.SaveAs(m_dir + @"\" + data[dataIndex].Str() + ".xlsx");
                workbook.Close();
            }
            m_excel.Quit();
        }

        public static long Measure(Action codeBlock, TimeSpan timeSpan = default(TimeSpan))
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
    }
}
