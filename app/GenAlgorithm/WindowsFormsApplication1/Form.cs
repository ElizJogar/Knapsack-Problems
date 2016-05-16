﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenAlgorithm
{
    public partial class GenAlgorithmView : Form
    {
        Logger log;
        GenAlgorithm algorithm;
        List<Individ> individs = new List<Individ>();//для множества решений
        int iterationCount = 0;
        int populationCount = 0;
        public GenAlgorithmView()
        {
            InitializeComponent();
        }

        private void runClick(object sender, EventArgs e)
        {
            if (numberOfIterationBox.Text.Length != 0)
                iterationCount = Convert.ToInt32(numberOfIterationBox.Text);
            else
                iterationCount = 20;

            if (numberOfPopulationBox.Text.Length != 0)
                populationCount = Convert.ToInt32(numberOfPopulationBox.Text);
            else
                populationCount = 20;
  
            int maxWeight = Convert.ToInt32(maxWeightBox.Text);
            if (maxWeight == 0) maxWeight = 80;
            switch (dataInstancesBox.Text)
            {
                case "No correlation":
                    algorithm = new GenAlgorithm(new UncorrDataInstances(15, maxWeight / 2));
                    break;
                case "The weak correlation":
                    algorithm = new GenAlgorithm(new WeaklyCorrDataInstances(15, maxWeight / 2));
                    break;
                case "The strong correlation":
                    algorithm = new GenAlgorithm(new StronglyCorrDataInstances(15, maxWeight / 2));
                    break;
                case "Subtotals":
                    algorithm = new GenAlgorithm(new SubsetSumDataInstances(15, maxWeight / 2));
                    break;
            }
            algorithm.LIMIT = maxWeight;
            textBox4.Text = "COST: ";
            for (int i = 0; i < algorithm.COST.Length; i++)
                textBox4.Text += algorithm.COST[i] + " ";
            textBox4.Text += "\r\n";

            textBox4.Text += "WEIGHT: ";
            for (int i = 0; i < algorithm.WEIGHT.Length; i++)
                textBox4.Text += algorithm.WEIGHT[i] + " ";
            textBox4.Text += "\r\n";

           individs.Clear();
            switch (startPopulBox.Text)
            {
                case "Danzig algorithm":
                    individs = algorithm.createPopulation(populationCount, 1);
                    break;
                case "Random algorithm":
                    individs = algorithm.createPopulation(populationCount, 2);
                    break;
            }
            textBox1.Text = " Population:            Max cost:\r\n";
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.ElementAt(i).SIZE; j++)
                {
                    textBox1.Text += individs.ElementAt(i).INDIVID[j];
                }
                textBox1.Text += "\t" + algorithm.getCost(individs.ElementAt(i)) + Environment.NewLine;
            }
            richTextBox3.Clear();

            for (int x = 0; x < iterationCount; x++)
            {
                switch (crossoverBox.Text)
                {
                    case "Single-point crossover":
                        individs = algorithm.pointCrossover(individs, 1);
                        break;
                    case "Two-point crossover":
                        individs = algorithm.pointCrossover(individs, 2);
                        break;
                    case "Uniform crossover":
                        individs = algorithm.uniformCrossover(individs);
                        break;
                }
                switch (mutationBox.Text)
                {
                    case "Point mutation":
                        individs = algorithm.pointMutation(individs);
                        break;
                    case "Inversion":
                        individs = algorithm.inversion(individs);
                        break;
                    case "Translocation":
                        individs = algorithm.translocation(individs);
                        break;
                    case "Saltation":
                        individs = algorithm.saltation(individs);
                        break;
                }
                switch (selectionBox.Text)
                {
                    case "Betta-Tournament":
                        List<Individ> population1 = new List<Individ>();
                        individs = algorithm.bettaTournamentSelection(individs, populationCount, Convert.ToInt32(bettaBox.Text));
                        break;
                    case "Linear-rank":
                        individs = algorithm.linearRankSelection(individs, populationCount);
                        break;
                }
                richTextBox3.Text += algorithm.getMaxScalledCost(individs) + "\n";
            }

            textBox2.Text = " Population:            Max cost:\r\n";
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.ElementAt(i).SIZE; j++)
                {
                    textBox2.Text += individs.ElementAt(i).INDIVID[j];
                }
                textBox2.Text += "\t" + algorithm.getCost(individs.ElementAt(i)) + Environment.NewLine;

            }
        }

        private void report_Click(object sender, EventArgs e)
        {

            int betta = 2;
            if (bettaBox.Text.Length != 0)
                betta = Convert.ToInt32(bettaBox.Text);
            else
                betta = 2;

            if (numberOfPopulationBox.Text.Length != 0)
                populationCount = Convert.ToInt32(numberOfPopulationBox.Text);
            else
                populationCount = 20;

            if (numberOfIterationBox.Text.Length != 0)
                iterationCount = Convert.ToInt32(numberOfIterationBox.Text);
            else
                iterationCount = 20;

            if (maxWeightBox.Text.Length != 0)
                algorithm.LIMIT = Convert.ToInt32(maxWeightBox.Text);
            else
                algorithm.LIMIT = 80;
            int startsCount = Convert.ToInt32(startsNumber.Text);
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;

            excel.SheetsInNewWorkbook = startsCount + 1;
            excel.Workbooks.Add(Type.Missing);
            Excel.Workbook workbook = excel.Workbooks[1]; //получам ссылку на первую открытую книгу
            Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
            string[] vsS ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                              "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
                              "AA", "AB", "AC", "AD", "AE","AF", "AG", "AH", "AI", "AJ", "AK", "AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV",
                              "AW","AX","AY","AZ","BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS",
                              "BT","BU"};
            string[] initialPopulation = { "Danzig algorithm", "Random algorithm" };
            string[] crossover = { "Single-point crossover", "Two-point crossover", "Uniform crossover" };
            string[] mutation = { "Point mutation", "Inversion", "Translocation", "Saltation" };
            string[] selection = { "Betta-Tournament", "Linear-rank" };

            for (int s = 0; s < startsCount; s++)
            {
                int count = 1;
                sheet = workbook.Worksheets.get_Item(s + 1);
                sheet.Cells[iterationCount + 4, count] = "max";
                sheet.Cells[iterationCount + 5, count] = "min";
                sheet.Cells[iterationCount + 6, count] = "i";

                for (int i = 0; i < initialPopulation.Length; i++)
                {
                    for (int j = 0; j < crossover.Length; j++)
                    {
                        for (int k = 0; k < mutation.Length; k++)
                        {
                            for (int g = 0; g < selection.Length; g++)
                            {
                                count++;
                                individs.Clear();
                                individs = algorithm.createPopulation(populationCount, i + 1);

                                for (int x = 0; x < iterationCount; x++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                        case 1:
                                            individs = algorithm.pointCrossover(individs, j + 1);
                                            break;

                                        case 2:
                                            individs = algorithm.uniformCrossover(individs);
                                            break;
                                    }
                                    switch(k)
                                    {
                                        case 0:
                                            individs = algorithm.pointMutation(individs);
                                            break;
                                        case 1:
                                            individs = algorithm.inversion(individs);
                                            break;
                                        case 2:
                                            individs = algorithm.translocation(individs);
                                            break;
                                        case 3:
                                            individs = algorithm.saltation(individs);
                                            break;

                                    }
                                    switch (g)
                                    {
                                        case 0:
                                            List<Individ> population = new List<Individ>();
                                            individs = algorithm.bettaTournamentSelection(individs, populationCount, betta);
                                            break;
                                        case 1:
                                            individs = algorithm.linearRankSelection(individs, populationCount);
                                            break;
                                    }
                                    sheet.Cells[x + 2, count] = algorithm.getMaxScalledCost(individs);
                                }
                                sheet.Cells[1, count] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                                sheet.Cells[iterationCount + 4, count].Formula = "= MAX(" + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (iterationCount + 1) + ")";
                                sheet.Cells[iterationCount + 5, count].Formula = "= MIN(" + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (iterationCount + 1) + ")";
                                sheet.Cells[iterationCount + 6, count].Formula = "= MATCH(" + vsS[count - 1] + (iterationCount + 4) + "," + vsS[count - 1] + 2 + ":" + vsS[count - 1] + (iterationCount + 1) + "," + 0 + ")";
                            }
                        }
                    }
                }
            }

            sheet = workbook.Worksheets.get_Item(startsCount + 1);
            int number = 1;
            sheet.Cells[2, number] = "max";
            sheet.Cells[3, number] = "min";
            sheet.Cells[4, number] = "i";
            sheet.Cells[5, number] = "i(среднее)";
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 4; k++)
                        for (int g = 0; g < 2; g++)
                        {
                            number++;
                            sheet.Cells[1, number] = initialPopulation[i] + Environment.NewLine + crossover[j] + Environment.NewLine + mutation[k] + Environment.NewLine + selection[g] + Environment.NewLine;
                            string max = getAFormula("= MAX(", 4, number, startsCount, vsS);
                            string min = getAFormula("= MIN(", 5, number, startsCount, vsS);
                            string average = getAFormula("= AVERAGE(", 6, number, startsCount, vsS);
                            string convergence = "=MIN(";

                            for (int v = 1; v < startsCount; v++)
                            {
                                convergence += "IF(" + vsS[number - 1] + 2 + "=Лист" + v + "!" + vsS[number - 1] + (iterationCount + 4) + "," + "Лист" + v + "!" + vsS[number - 1] + (iterationCount + 6) + "," + 100 + ")" + ",";
                            }
                            convergence += "IF(" + vsS[number - 1] + 2 + "=Лист" + startsCount + "!" + vsS[number - 1] + (iterationCount + 4) + "," + "Лист" + startsCount + "!" + vsS[number - 1] + (iterationCount + 6) + "," + 100 + "))";
                            sheet.Cells[2, number].Formula = max;
                            sheet.Cells[3, number].Formula = min;
                            sheet.Cells[4, number].Formula = convergence;
                            sheet.Cells[5, number].Formula = average;
                        }
            MessageBox.Show(@"Report created successfully!");
            excel.Visible = true;
        }
        string getAFormula(string formula, int count, int number, int startsCount, string[] vsS)
        {
            string tmp = formula;
            for (int d = 1; d < startsCount; d++)
                tmp += "Лист" + d + "!" + vsS[number - 1] + (iterationCount + count) + ",";
            tmp += "Лист" + startsCount + "!" + vsS[number - 1] + (iterationCount + count) + ")";
            return tmp;
        }
    }
}

   


