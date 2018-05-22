using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using KnapsackProblem;
using Algorithm;

namespace ExcelReport
{
    public class CombinationsCompareReport : IReport
    {
        private Excel.Application m_excel;
        private ITask m_task;
        private List<Individ> m_individs;
        private int m_iterationCount;
        private int m_populationCount;
        private int m_betta;
        private int m_runsCount;
        private int m_dataSize;
        private int m_instancesCount;
        private DirectoryInfo m_dir;
        private string[] m_vsS ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                              "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                              "AA", "AB", "AC", "AD", "AE","AF", "AG", "AH", "AI", "AJ", "AK", "AL",
                              "AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV",
                              "AW","AX","AY","AZ","BA","BB","BC","BD","BE","BF","BG",
                              "BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS",
                              "BT","BU"};
        public DirectoryInfo GetDir()
        {
            return m_dir;
        }
        public CombinationsCompareReport(ITask task = null, int iterationCount = 1, int populationCount = 1, int betta = 1, int startCount = 1, int dataSize = 15, int instancesCount = 1)
        {
            m_task = task;
            m_iterationCount = iterationCount;
            m_populationCount = populationCount;
            m_betta = betta;
            m_runsCount = startCount;
            m_dataSize = dataSize;
            m_instancesCount = instancesCount;

            DateTime localDate = DateTime.Now;
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_dir = new DirectoryInfo(myDocPath + @"\knapsack_problems_doc\cc_reports_" + task.Str() + "_" 
                + localDate.ToShortDateString() + "-" + localDate.Hour 
                + "." + localDate.Minute + "." + localDate.Second + "." + localDate.Millisecond);
            m_dir.Create();
            m_excel = new Excel.Application();
        }

        private string CreateFormula(string formula, int count, int number, int startCount, int firstListNumber = 0)
        {
            string tmp = formula;
            for (int d = firstListNumber + 1; d < startCount + firstListNumber; d++)
                tmp += "Sheet" + d + "!" + m_vsS[number - 1] + count + ",";
            tmp += "Sheet" + (startCount + firstListNumber) + "!" + m_vsS[number - 1] + count + ")";
            return tmp;
        }

        private string GetOperatorName(IOperator op)
        {
            string res = Convert.ToString(op);
            return res.Replace("Algorithm.", "");
        }
        private string GetOperatorsStr(params IOperator[] ops)
        {
            var str = "";
            foreach (var op in ops) str += GetOperatorName(op) + Environment.NewLine;
            return str;
        }
        public void Create()
        {
            IInitialPopulation[] initialPopulation = { new DantzigAlgorithm(), new RandomPopulation() };

            ICrossover[] crossover = { new SinglePointCrossover(), new TwoPointCrossover(), new UniformCrossover() };

            IMutation[] mutation = { new PointMutation(), new Inversion(), new Translocation(), new Saltation() };

            ISelection[] selection = { new BettaTournament(new PenaltyFunction(), new EfficientRepairOperator()),
                                       new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator()) };

            IData[] data = { new UncorrData(m_dataSize, new Range(10, 9999)),
                new WeaklyCorrData(m_dataSize, new Range(10, 9999)),
                new StronglyCorrData(m_dataSize, new Range(10, 9999)),
                new SubsetSumData(m_dataSize, new Range(1, 9999)),
                new VeryVeryStronglyCorrData(m_dataSize, new Range(1, 9999))};

            int length = initialPopulation.Length * crossover.Length * mutation.Length * selection.Length;
            for (int dataIndex = 0; dataIndex < data.Length; ++dataIndex)
            {
                Excel.Workbook workbook;
                Excel.Worksheet sheet;

                m_excel.SheetsInNewWorkbook = 2;
                m_excel.Workbooks.Add(Type.Missing);
                Excel.Workbook summaryWorkbook = m_excel.Workbooks[1];
                summaryWorkbook.Saved = true;
                for (int instIndex = 0; instIndex < m_instancesCount; instIndex++)
                {
                    var taskData = m_task.Create(data[dataIndex]);
                    IDPApproach approach = null;
                    if (taskData as KPTask != null)
                    {
                        approach = new DirectApproach();
                    }
                    else if (taskData as UKPTask != null)
                    {
                        approach = new ClassicalUKPApproach();
                    }
                    var optimum = new DynamicProgramming(approach).Run(taskData);

                    m_excel.SheetsInNewWorkbook = m_runsCount + 1;
                    m_excel.Workbooks.Add(Type.Missing);
                    workbook = m_excel.Workbooks[2];
                    for (int s = 0; s < m_runsCount; s++)
                    {
                        int count = 1;
                        sheet = workbook.Worksheets.get_Item(s + 1);
                        sheet.Name = "Sheet" + (s + 1);
                        sheet.Cells.ColumnWidth = 7;
                        sheet.Cells[m_iterationCount + 4, count] = "max";
                        sheet.Cells[m_iterationCount + 5, count] = "i";

                        for (int i = 0; i < initialPopulation.Length; i++)
                        {
                            for (int j = 0; j < crossover.Length; j++)
                            {
                                for (int k = 0; k < mutation.Length; k++)
                                {
                                    for (int g = 0; g < selection.Length; g++)
                                    {
                                        ++count;
                                        var alg = new GeneticAlgorithm(initialPopulation[i], crossover[j], mutation[k], selection[g], taskData);

                                        m_individs = alg.Init(m_populationCount);
                                        for (int x = 0; x < m_iterationCount; x++)
                                        {
                                            sheet.Cells[x + 2, count] = alg.Run(m_populationCount, ref m_individs, m_betta);
                                        }
                                        sheet.Cells[1, count] = GetOperatorsStr(initialPopulation[i], crossover[j], mutation[k], selection[g]);
                                        sheet.Cells[m_iterationCount + 4, count].Formula = "= MAX(" + m_vsS[count - 1] + 2 + ":" + m_vsS[count - 1] + (m_iterationCount + 1) + ")";
                                        sheet.Cells[m_iterationCount + 5, count].Formula = "= MATCH(" + m_vsS[count - 1] + (m_iterationCount + 4) + "," + m_vsS[count - 1] + 2 + ":" + m_vsS[count - 1] + (m_iterationCount + 1) + "," + 0 + ")";
                                    }
                                }
                            }
                        }
                    }

                    sheet = workbook.Worksheets.get_Item(m_runsCount + 1);
                    sheet.Name = "Sheet" + (m_runsCount + 1);
                    int number = 1;
                    sheet.Cells[2, number] = "max";
                    sheet.Cells[3, number] = "probabil.";
                    sheet.Cells[4, number] = "i(average)";
                    for (int i = 0; i < initialPopulation.Length; i++)
                        for (int j = 0; j < crossover.Length; j++)
                            for (int k = 0; k < mutation.Length; k++)
                                for (int g = 0; g < selection.Length; g++)
                                {
                                    number++;
                                    sheet.Cells[1, number] = GetOperatorsStr(initialPopulation[i], crossover[j], mutation[k], selection[g]);
                                    string max = CreateFormula("= MAX(", m_iterationCount + 4, number, m_runsCount);
                                    string average = CreateFormula("= AVERAGE(", m_iterationCount + 5, number, m_runsCount);
                                    sheet.Cells[2, number].Formula = max;
                                    string sum = "=SUM(";
                                    for (int v = 1; v < m_runsCount; v++)
                                        sum += "IF(" + m_vsS[number - 1] + "2" + "=Sheet" + v + "!" + m_vsS[number - 1] + (m_iterationCount + 4) + ",1,0),";
                                    sum += "IF(" + m_vsS[number - 1] + "2" + "=Sheet" + m_runsCount + "!" + m_vsS[number - 1] + (m_iterationCount + 4) + ",1,0))";
                                    sheet.Cells[3, number].Formula = sum + "/" + m_runsCount;
                                    sheet.Cells[4, number].Formula = average;
                                }
                    sheet.Cells[7, 1] = "Data Instances:";
                    sheet.Cells[8, 1] = "Cost: ";
                    sheet.Cells[9, 1] = "Weight: ";
                    sheet.Cells[10, 1] = "Limit: ";
                    sheet.Cells[10, 2] = data[dataIndex].Capacity;
                    for (var i = 2; i < data[dataIndex].Cost.Length + 2; ++i)
                    {
                        sheet.Cells[8, i] = data[dataIndex].Cost[i - 2];
                        sheet.Cells[9, i] = data[dataIndex].Weight[i - 2];
                    }

                    if (m_task as UKPTask != null)
                    {
                        sheet.Cells[11, 1] = "Max X[i] Count: ";
                        var x = taskData.ItemMaxCounts;
                        for (var i = 2; i < x.Length + 2; ++i)
                        {
                            sheet.Cells[11, i] = x[i - 2];
                        }
                    }

                    sheet.Cells[12, 1] = "Results:";
                    sheet.Cells[12, 5] = "count:";
                    sheet.Cells[13, 1] = "max from the max";
                    sheet.Cells[14, 1] = "min from the max";
                    sheet.Cells[15, 1] = "max probability";
                    sheet.Cells[16, 1] = "min propability";
                    sheet.Cells[17, 1] = "max from the i(average)";
                    sheet.Cells[18, 1] = "min from the i(average)";
                    for (int i = 2, j = 2; i < 5; i++, j += 2)
                    {
                        sheet.Cells[11 + j, 4] = "= MAX(" + m_vsS[1] + i + ":" + m_vsS[length] + i + ")";
                        sheet.Cells[12 + j, 4] = "= MIN(" + m_vsS[1] + i + ":" + m_vsS[length] + i + ")";
                        sheet.Cells[11 + j, 5] = "= COUNTIF(" + m_vsS[1] + i + ":" + m_vsS[length] + i + "," + m_vsS[3] + (11 + j) + ")";
                        sheet.Cells[12 + j, 5] = "= COUNTIF(" + m_vsS[1] + i + ":" + m_vsS[length] + i + "," + m_vsS[3] + (12 + j) + ")";
                    }
                    workbook.Saved = true;
                    workbook.SaveAs(m_dir + @"\" + taskData.Str() + (instIndex + 1) + ".xlsx");
                    (sheet as Excel.Worksheet).Copy(summaryWorkbook.Worksheets[instIndex + 1]);

                    workbook.Close();
                    sheet = summaryWorkbook.Worksheets[instIndex + 1];
                    sheet.Columns.ColumnWidth = 10;
                    sheet.Name = "Sheet" + (instIndex + 3);
                    sheet.Cells[20, 1] = "Percentage data:";
                    sheet.Cells[20, 4] = "optimum: " + optimum;
                    sheet.Cells[21, 1] = "deviation%";
                    sheet.Cells[22, 1] = "probabil.%";
                    sheet.Cells[23, 1] = "i(avg) %";
                    for (int i = 2; i < length + 2; i++)
                    {
                        sheet.Cells[21, i] = 100 - Math.Round((double)(sheet.Cells[2, i].Value * 100 / optimum), 2);
                        sheet.Cells[22, i] = Math.Round(sheet.Cells[3, i].Value * 100, 2);
                        sheet.Cells[23, i] = Math.Round((double)(sheet.Cells[4, i].Value * 100 / m_iterationCount), 2);
                    }
                }
                sheet = summaryWorkbook.Worksheets.get_Item(m_instancesCount + 1);
                sheet.Name = "Sheet" + (m_instancesCount + 3);
                sheet.Columns.ColumnWidth = 14;
                int n = 2;
                sheet.Cells[2, 1] = "deviation %";
                sheet.Cells[3, 1] = "probabil.(avg)%";
                sheet.Cells[4, 1] = "i(avg avg) %";
                for (int i = 0; i < initialPopulation.Length; i++)
                    for (int j = 0; j < crossover.Length; j++)
                        for (int k = 0; k < mutation.Length; k++)
                            for (int g = 0; g < selection.Length; g++, n++)
                            {
                                sheet.Cells[1, n] = GetOperatorsStr(initialPopulation[i], crossover[j], mutation[k], selection[g]);
                                sheet.Cells[2, n].Formula = CreateFormula("= MIN(", 21, n, m_instancesCount, 2);
                                string avgPr = "=AVERAGE(";
                                string avgI = "=AVERAGE(";
                                for (int v = 3; v < m_instancesCount + 2; v++)
                                {
                                    avgPr += "IF(" + m_vsS[n - 1] + "2" + "=Sheet" + v + "!" + m_vsS[n - 1] + "21" + "," + "Sheet" + v + "!" + m_vsS[n - 1] + "22" + "," + "0),";
                                    avgI += "IF(" + m_vsS[n - 1] + "2" + "=Sheet" + v + "!" + m_vsS[n - 1] + "21" + "," + "Sheet" + v + "!" + m_vsS[n - 1] + "23" + "," + "0),";
                                }
                                avgPr += "IF(" + m_vsS[n - 1] + "2" + "=Sheet" + (m_instancesCount + 2) + "!" + m_vsS[n - 1] + "21" + "," + "Sheet" + (m_instancesCount + 2) + "!" + m_vsS[n - 1] + "22" + ",0))";
                                avgI += "IF(" + m_vsS[n - 1] + "2" + "=Sheet" + (m_instancesCount + 2) + "!" + m_vsS[n - 1] + "21" + "," + "Sheet" + (m_instancesCount + 2) + "!" + m_vsS[n - 1] + "23" + ",0))";
                                sheet.Cells[3, n].Formula = avgPr;
                                sheet.Cells[4, n].Formula = avgI;
                            }
                summaryWorkbook.SaveAs(m_dir + @"\" + data[dataIndex].Str() + "_summary.xlsx");
                summaryWorkbook.Close();
            }
            m_excel.Quit();
        }
    }
}
