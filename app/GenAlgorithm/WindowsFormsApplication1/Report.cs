﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace GenAlgorithm
{
    abstract class AExcelReport
    {
        protected Excel.Application _excel;
        public abstract void Create();
    }

    class CombinationOfStepsExcelReport : AExcelReport
    {
        private GenAlgorithm _algorithm;
        private int _iterationCount;
        private int _populationCount;
        private int _betta;
        private int _startCount;
        private List<Individ> _individs;
        
        public CombinationOfStepsExcelReport(int iterationCount, int populationCount, int betta, int startCount, GenAlgorithm algorithm)
        {
            _excel = new Excel.Application();
            _iterationCount = iterationCount;
            _populationCount = populationCount;
            _betta = betta;
            _startCount = startCount;
            _individs = new List<Individ>();
            _algorithm = algorithm;
        }

        private string getAFormula(string formula, int count, int number, int startCount, string[] vsS)
        {
            string tmp = formula;
            for (int d = 1; d < startCount; d++)
                tmp += "Sheet" + d + "!" + vsS[number - 1] + (_iterationCount + count) + ",";
            tmp += "Sheet" + startCount + "!" + vsS[number - 1] + (_iterationCount + count) + ")";
            return tmp;
        }

        public override void Create()
        {
            _excel.Visible = false;
            string[] vsS ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                              "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
                              "AA", "AB", "AC", "AD", "AE","AF", "AG", "AH", "AI", "AJ", "AK", "AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV",
                              "AW","AX","AY","AZ","BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS",
                              "BT","BU"};
            string[] initialPopulation = { "Danzig _algorithm", "Random _algorithm" };
            string[] crossover = { "Single-point crossover", "Two-point crossover", "Uniform crossover" };
            string[] mutation = { "Point mutation", "Inversion", "Translocation", "Saltation" };
            string[] selection = { "Betta-Tournament", "Linear-rank" };
            _excel.SheetsInNewWorkbook = _startCount + 1;
            _excel.Workbooks.Add(Type.Missing);
            Excel.Workbook workbook = _excel.Workbooks[1]; 
            Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
            for (int s = 0; s < _startCount; s++)
            {
                int count = 1;
                sheet = workbook.Worksheets.get_Item(s + 1);
                sheet.Cells[_iterationCount + 4, count] = "max";
                sheet.Cells[_iterationCount + 5, count] = "min";
                sheet.Cells[_iterationCount + 6, count] = "i";

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
                                sheet.Cells[_iterationCount + 4, count].Formula = "= MAX(" + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (_iterationCount + 1) + ")";
                                sheet.Cells[_iterationCount + 5, count].Formula = "= MIN(" + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (_iterationCount + 1) + ")";
                                sheet.Cells[_iterationCount + 6, count].Formula = "= MATCH(" + vsS[count - 1] + (_iterationCount + 4) + "," + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (_iterationCount + 1) + "," + 0 + ")";
                            }
                        }
                    }
                }
            }

            sheet = workbook.Worksheets.get_Item(_startCount + 1);
            int number = 1;
            sheet.Cells[2, number] = "max";
            sheet.Cells[3, number] = "min";
            sheet.Cells[4, number] = "i";
            sheet.Cells[5, number] = "i(average)";
            for (int i = 0; i < initialPopulation.Length; i++)
                for (int j = 0; j < crossover.Length; j++)
                    for (int k = 0; k < mutation.Length; k++)
                        for (int g = 0; g < selection.Length; g++)
                        {
                            number++;
                            sheet.Cells[1, number] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                            string max = getAFormula("= MAX(", 4, number, _startCount, vsS);
                            string min = getAFormula("= MIN(", 5, number, _startCount, vsS);
                            string average = getAFormula("= AVERAGE(", 6, number, _startCount, vsS);
                            string convergence = "=MIN(";

                            for (int v = 1; v < _startCount; v++)
                            {
                                convergence += "IF(" + vsS[number - 1] + 2 + "=Sheet" + v + "!" + vsS[number - 1] + (_iterationCount + 4) + "," + "Sheet" + v + "!" + vsS[number - 1] + (_iterationCount + 6) + "," + 100 + ")" + ",";
                            }
                            convergence += "IF(" + vsS[number - 1] + 2 + "=Sheet" + _startCount + "!" + vsS[number - 1] + (_iterationCount + 4) + "," + "Sheet" + _startCount + "!" + vsS[number - 1] + (_iterationCount + 6) + "," + 100 + "))";
                            sheet.Cells[2, number].Formula = max;
                            sheet.Cells[3, number].Formula = min;
                            sheet.Cells[4, number].Formula = convergence;
                            sheet.Cells[5, number].Formula = average;
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
            sheet.Cells[15, 1] = "max from the min";
            sheet.Cells[16, 1] = "min from the min";
            sheet.Cells[17, 1] = "max from the i";
            sheet.Cells[18, 1] = "min from the i";
            sheet.Cells[19, 1] = "max from the i(average)";
            sheet.Cells[20, 1] = "min from the i(average)";
            sheet.Cells[21, 1] = "the number of the best decisions A.D.";
            sheet.Cells[22, 1] = "the number of the best decisions R.A.";
       
            int length = initialPopulation.Length * crossover.Length * mutation.Length *  selection.Length;
            for (int i = 2, j = 2; i < 6; i++, j += 2)
            {
                sheet.Cells[11 + j, 4] = "= MAX(" + vsS[1] + i + ":" + vsS[length] + i + ")";
                sheet.Cells[12 + j, 4] = "= MIN(" + vsS[1] + i + ":" + vsS[length] + i + ")";
                sheet.Cells[11 + j, 5] = "= COUNTIF(" + vsS[1] + i + ":" + vsS[length] + i + "," + vsS[3] + (11 + j) + ")";
                sheet.Cells[12 + j, 5] = "= COUNTIF(" + vsS[1] + i + ":" + vsS[length] + i + "," + vsS[3] + (12 + j) + ")";
            }
            sheet.Cells[21, 5] = "= COUNTIF(" + vsS[1] + "2:" + vsS[length/2] + "2," + vsS[3] + "13)";
            sheet.Cells[22, 5] = "=COUNTIF(" + vsS[length / 2 + 1] + "2:" + vsS[length] + "2," + vsS[3] + "13)";
            MessageBox.Show(@"Report created successfully!");
            _excel.Visible = true;
        }
    }
}
