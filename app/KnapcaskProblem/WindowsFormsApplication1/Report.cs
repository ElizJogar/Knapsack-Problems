using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
namespace GenAlgorithm
{
    abstract class AExcelReport
    {
        protected Excel.Application _excel;
        public abstract void Create();
    }

    class DataInstancesAnalysisReport : AExcelReport
    {
        private GenAlgorithm _algorithm;

        private ADataInstances _data;
        private List<Individ> _individs;
        private int _iterationCount;
        private int _populationCount;
        private int _betta;
        private int _startCount;
        private int _instancesCount;
        private DirectoryInfo _dir;
        private string[] _vsS ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                              "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
                              "AA", "AB", "AC", "AD", "AE","AF", "AG", "AH", "AI", "AJ", "AK", "AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV",
                              "AW","AX","AY","AZ","BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS",
                              "BT","BU"};

        public DataInstancesAnalysisReport(int iterationCount, int populationCount, int betta, int startCount, int instancesCount)
        {
            _iterationCount = (iterationCount > 0) ? iterationCount : 1;
            _populationCount = (populationCount > 0) ? populationCount : 1;
            _betta = (betta > 0) ? betta: 1;
            _startCount =  (startCount > 0) ? startCount : 1 ;
            _instancesCount = (instancesCount > 0) ? instancesCount : 1;
            _individs = new List<Individ>();
            DateTime localDate = DateTime.Now;
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _dir = new DirectoryInfo(myDocPath + @"\gen_algorithm_doc\reports_" + localDate.ToShortDateString() + "-" + localDate.Hour + "." + localDate.Minute + "." + localDate.Second + "." + localDate.Millisecond);
            _dir.Create();
            _excel = new Excel.Application();
        }

        private string createFormula(string formula, int count, int number, int startCount, int firstListNumber = 0)
        {
            string tmp = formula;
            for (int d = firstListNumber + 1; d < startCount + firstListNumber; d++)
                tmp += "Sheet" + d + "!" + _vsS[number - 1] +  count + ",";
            tmp += "Sheet" + (startCount + firstListNumber) + "!" + _vsS[number - 1] + count + ")";
            return tmp;
        }

        public override void Create()
        {
            string[] initialPopulation = { "Danzig _algorithm", "Random _algorithm" };
            string[] crossover = { "Single-point crossover", "Two-point crossover", "Uniform crossover" };
            string[] mutation = { "Point mutation", "Inversion", "Translocation", "Saltation" };
            string[] selection = { "Betta-Tournament", "Linear-rank" };
            int length = initialPopulation.Length * crossover.Length * mutation.Length * selection.Length;
            for (int globIndex = 0; globIndex <4; globIndex++)
            {
                Excel.Workbook workbook;
                Excel.Worksheet sheet;
                switch (globIndex)
                {
                    case 0:
                        _data = new UncorrDataInstances(15, 100);
                        break;
                    case 1:
                        _data = new WeaklyCorrDataInstances(15, 100);
                        break;
                    case 2:
                        _data = new StronglyCorrDataInstances(15, 100);
                        break;
                    case 3:
                        _data = new SubsetSumDataInstances(15, 100);
                        break;
                }
                _excel.SheetsInNewWorkbook = 2;
                _excel.Workbooks.Add(Type.Missing);
                Excel.Workbook summaryWorkbook = _excel.Workbooks[1];
                summaryWorkbook.Saved = true;
                for (int instIndex = 0; instIndex < _instancesCount; instIndex++)
                {
                    switch (globIndex)
                    {
                        case 0:
                            _data.Fill();
                            break;
                        case 1:
                            _data.Fill();
                            break;
                        case 2:
                            _data.Fill();
                            break;
                        case 3:
                            _data.Fill();
                            break;
                    }
                    _individs.Clear();
                    _algorithm = new GenAlgorithm(_data);
                    int optimum = new ExhaustiveSearchAlgorithm(_data).Run();              
                    _excel.SheetsInNewWorkbook = _startCount + 1;
                    _excel.Workbooks.Add(Type.Missing);
                    workbook = _excel.Workbooks[2];
                    for (int s = 0; s < _startCount; s++)
                    {
                        int count = 1;
                        sheet = workbook.Worksheets.get_Item(s + 1);
                        sheet.Name = "Sheet" + (s + 1);
                        sheet.Cells.ColumnWidth = 7;
                        sheet.Cells[_iterationCount + 4, count] = "max";
                        sheet.Cells[_iterationCount + 5, count] = "i";

                        for (int i = 0; i < initialPopulation.Length; i++)
                        {
                            for (int j = 0; j < crossover.Length; j++)
                            {
                                for (int k = 0; k < mutation.Length; k++)
                                {
                                    for (int g = 0; g < selection.Length; g++)
                                    {
                                        count++;
                                        _individs.Clear();
                                        _individs = _algorithm.CreatePopulation(_populationCount, i);

                                        for (int x = 0; x < _iterationCount; x++)
                                        {
                                            switch (j)
                                            {
                                                case 0:
                                                    _individs = _algorithm.SinglePointCrossover(_individs);
                                                    break;
                                                case 1:
                                                    _individs = _algorithm.TwoPointCrossover(_individs);
                                                    break;
                                                case 2:
                                                    _individs = _algorithm.UniformCrossover(_individs);
                                                    break;
                                            }
                                            switch (k)
                                            {
                                                case 0:
                                                    _individs = _algorithm.PointMutation(_individs);
                                                    break;
                                                case 1:
                                                    _individs = _algorithm.Inversion(_individs);
                                                    break;
                                                case 2:
                                                    _individs = _algorithm.Translocation(_individs);
                                                    break;
                                                case 3:
                                                    _individs = _algorithm.Saltation(_individs);
                                                    break;

                                            }
                                            switch (g)
                                            {
                                                case 0:
                                                    _individs = _algorithm.BettaTournamentSelection(_individs, _populationCount, _betta);
                                                    break;
                                                case 1:
                                                    _individs = _algorithm.LinearRankSelection(_individs, _populationCount);
                                                    break;
                                            }
                                            sheet.Cells[x + 2, count] = _algorithm.GetMaxCost(_individs);
                                        }
                                        sheet.Cells[1, count] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                                        sheet.Cells[_iterationCount + 4, count].Formula = "= MAX(" + _vsS[count - 1] + 2 + ":" + _vsS[count - 1] + (_iterationCount + 1) + ")";
                                        sheet.Cells[_iterationCount + 5, count].Formula = "= MATCH(" + _vsS[count - 1] + (_iterationCount + 4) + "," + _vsS[count - 1] + 2 + ":" + _vsS[count - 1] + (_iterationCount + 1) + "," + 0 + ")";
                                    }
                                }
                            }
                        }
                    }

                    sheet = workbook.Worksheets.get_Item(_startCount + 1);
                    sheet.Name = "Sheet" + (_startCount + 1);
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
                                    sheet.Cells[1, number] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                                    string max = createFormula("= MAX(", _iterationCount + 4, number, _startCount);
                                    string average = createFormula("= AVERAGE(", _iterationCount + 5, number, _startCount);
                                    sheet.Cells[2, number].Formula = max;
                                    string sum = "=SUM(";
                                    for (int v = 1; v < _startCount; v++)
                                        sum += "IF(" + _vsS[number - 1] + "2" + "=Sheet" + v + "!" + _vsS[number - 1] + (_iterationCount + 4) + ",1,0),";
                                    sum += "IF(" + _vsS[number - 1] + "2" + "=Sheet" + _startCount + "!" + _vsS[number - 1] + (_iterationCount + 4) + ",1,0))";
                                    sheet.Cells[3, number].Formula = sum + "/" + _startCount;
                                    sheet.Cells[4, number].Formula = average;
                                }
                    sheet.Cells[7, 1] = "Data Instances:";
                    sheet.Cells[8, 1] = "Cost: ";
                    sheet.Cells[9, 1] = "Weight: ";
                    sheet.Cells[10, 1] = "Limit: ";
                    sheet.Cells[10, 2] = _algorithm.LIMIT;
                    for (int i = 2; i < _algorithm.INDIVID_SIZE + 2; i++)
                    {
                        sheet.Cells[8, i] = _algorithm.COST[i - 2];
                        sheet.Cells[9, i] = _algorithm.WEIGHT[i - 2];
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
                        sheet.Cells[11 + j, 4] = "= MAX(" + _vsS[1] + i + ":" + _vsS[length] + i + ")";
                        sheet.Cells[12 + j, 4] = "= MIN(" + _vsS[1] + i + ":" + _vsS[length] + i + ")";
                        sheet.Cells[11 + j, 5] = "= COUNTIF(" + _vsS[1] + i + ":" + _vsS[length] + i + "," + _vsS[3] + (11 + j) + ")";
                        sheet.Cells[12 + j, 5] = "= COUNTIF(" + _vsS[1] + i + ":" + _vsS[length] + i + "," + _vsS[3] + (12 + j) + ")";
                    }
                    workbook.Saved = true;
                    workbook.SaveAs(_dir + @"\" + _data.GetStringType() + (instIndex + 1) + ".xlsx");
                    (sheet as Excel.Worksheet).Copy(summaryWorkbook.Worksheets[instIndex + 1]);

                    workbook.Close();
                    sheet = summaryWorkbook.Worksheets[instIndex + 1];
                    sheet.Columns.ColumnWidth = 10;
                    sheet.Name = "Sheet" + (instIndex + 3);
                    sheet.Cells[20, 1] = "Percentage data:";
                    sheet.Cells[20, 4] = "Optimum: " + optimum;
                    sheet.Cells[21, 1] = "deviation%";
                    sheet.Cells[22, 1] = "probabil.%";
                    sheet.Cells[23, 1] = "i(avg) %";
                    for (int i = 2; i < length + 2; i++)
                    {
                        sheet.Cells[21, i] = 100 - Math.Round((double)(sheet.Cells[2, i].Value * 100 / optimum), 2);
                        sheet.Cells[22, i] = Math.Round(sheet.Cells[3, i].Value * 100, 2);
                        sheet.Cells[23, i] = Math.Round((double)(sheet.Cells[4, i].Value * 100 / _iterationCount), 2);
                    }
                }
                sheet = summaryWorkbook.Worksheets.get_Item(_instancesCount + 1);
                sheet.Name = "Sheet" + (_instancesCount + 3);
                sheet.Columns.ColumnWidth = 14;
                int n = 2;
                sheet.Cells[2, 1] = "deviation %";
                sheet.Cells[3, 1] = "max probabil.%";
                sheet.Cells[4, 1] = "i(avg) min %";
                for (int i = 0; i < initialPopulation.Length; i++)
                    for (int j = 0; j < crossover.Length; j++)
                        for (int k = 0; k < mutation.Length; k++)
                            for (int g = 0; g < selection.Length; g++, n++)
                            {
                                sheet.Cells[1, n] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                                sheet.Cells[2, n].Formula = createFormula("= MIN(", 21, n, _instancesCount, 2);
                                string maxIf = "=MAX(";
                                string minIf = "=MIN(";
                                for (int v = 3; v < _instancesCount + 2; v++)
                                {
                                    maxIf += "IF(" + _vsS[n - 1] + "2" + "=Sheet" + v + "!" + _vsS[n - 1] + "21" + "," + "Sheet" + v + "!" + _vsS[n - 1] + "22" + "," + "-1),";
                                    minIf += "IF(" + _vsS[n - 1] + "2" + "=Sheet" + v + "!" + _vsS[n - 1] + "21" + "," + "Sheet" + v + "!" + _vsS[n - 1] + "23" + "," + "1000000000),";
                                }
                                maxIf += "IF(" + _vsS[n - 1] + "2" + "=Sheet" + (_instancesCount + 2) + "!" + _vsS[n - 1] + "21" + "," + "Sheet" + (_instancesCount + 2) + "!" + _vsS[n - 1] + "22" + ",-1))";
                                minIf += "IF(" + _vsS[n - 1] + "2" + "=Sheet" + (_instancesCount + 2) + "!" + _vsS[n - 1] + "21" + "," + "Sheet" + (_instancesCount + 2) + "!" + _vsS[n - 1] + "23" + ",1000000000))";
                                sheet.Cells[3, n].Formula = maxIf;
                                sheet.Cells[4, n].Formula = minIf;
                            }
                summaryWorkbook.SaveAs(_dir + @"\" + _data.GetStringType() + "_summary.xlsx");
                summaryWorkbook.Close();                
            }
             MessageBox.Show(@"Report created successfully! You can see reports here: " + _dir + @"\...", "Excel Tool", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            _excel.Quit();
        } 
    }
}

