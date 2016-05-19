using System;
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
        List<Individ> individs = new List<Individ>();
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
 
            switch (dataInstancesBox.Text)
            {
                case "Test":
                    algorithm = new GenAlgorithm(new TestDataInstances(15, 100));
                    break;
                case "No correlation":
                    algorithm = new GenAlgorithm(new UncorrDataInstances(15, 100));
                    break;
                case "The weak correlation":
                    algorithm = new GenAlgorithm(new WeaklyCorrDataInstances(15, 100));
                    break;
                case "The strong correlation":
                    algorithm = new GenAlgorithm(new StronglyCorrDataInstances(15, 100));
                    break;
                case "Subtotals":
                    algorithm = new GenAlgorithm(new SubsetSumDataInstances(15, 100));
                    break;
            }
            
            textBox4.Text = "COST: ";
            for (int i = 0; i < algorithm.COST.Length; i++)
                textBox4.Text += algorithm.COST[i] + " ";
            textBox4.Text += "\r\n";

            textBox4.Text += "WEIGHT: ";
            for (int i = 0; i < algorithm.WEIGHT.Length; i++)
                textBox4.Text += algorithm.WEIGHT[i] + " ";
            textBox4.Text += "\r\n";
            textBox4.Text += "WEIGHT LIMIT: " + algorithm.LIMIT + "\r\n";
           individs.Clear();
            switch (startPopulBox.Text)
            {
                case "Danzig algorithm":
                    individs = algorithm.CreatePopulation(populationCount, 0);
                    break;
                case "Random algorithm":
                    individs = algorithm.CreatePopulation(populationCount, 1);
                    break;
            }
            textBox1.Text = " Individ:            Cost:\r\n";
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.ElementAt(i).SIZE; j++)
                {
                    textBox1.Text += individs.ElementAt(i).INDIVID[j];
                }
                textBox1.Text += "\t" + algorithm.GetCost(individs.ElementAt(i)) + Environment.NewLine;
            }
            textBox2.Clear();

            for (int x = 0; x < iterationCount; x++)
            {
                switch (crossoverBox.Text)
                {
                    case "Single-point crossover":
                        individs = algorithm.SinglePointCrossover(individs);
                        break;
                    case "Two-point crossover":
                        individs = algorithm.TwoPointCrossover(individs);
                        break;
                    case "Uniform crossover":
                        individs = algorithm.UniformCrossover(individs);
                        break;
                }
                switch (mutationBox.Text)
                {
                    case "Point mutation":
                        individs = algorithm.PointMutation(individs);
                        break;
                    case "Inversion":
                        individs = algorithm.Inversion(individs);
                        break;
                    case "Translocation":
                        individs = algorithm.Translocation(individs);
                        break;
                    case "Saltation":
                        individs = algorithm.Saltation(individs);
                        break;
                }
                switch (selectionBox.Text)
                {
                    case "Betta-Tournament":
                        individs = algorithm.BettaTournamentSelection(individs, populationCount, Convert.ToInt32(bettaBox.Text));
                        break;
                    case "Linear-rank":
                        individs = algorithm.LinearRankSelection(individs, populationCount);
                        break;
                }
                textBox2.Text +=  algorithm.GetMaxCost(individs) + Environment.NewLine;
            }
 
        }

        private void reportClick(object sender, EventArgs e)
        {

            int betta = (bettaBox.Text.Length != 0) ? Convert.ToInt32(bettaBox.Text) : 2;
            int startCount = (startsNumberBox.Text.Length != 0) ? Convert.ToInt32(startsNumberBox.Text) : 1;
            populationCount = (numberOfPopulationBox.Text.Length != 0) ? Convert.ToInt32(numberOfPopulationBox.Text) : 30;
            iterationCount = (numberOfIterationBox.Text.Length != 0) ? Convert.ToInt32(numberOfIterationBox.Text) : 20;
            switch (dataInstancesBox.Text)
            {
                case "Test":
                    algorithm = new GenAlgorithm(new TestDataInstances(15, 100));
                    break;
                case "No correlation":
                    algorithm = new GenAlgorithm(new UncorrDataInstances(15, 100));
                    break;
                case "The weak correlation":
                    algorithm = new GenAlgorithm(new WeaklyCorrDataInstances(15, 100));
                    break;
                case "The strong correlation":
                    algorithm = new GenAlgorithm(new StronglyCorrDataInstances(15, 100));
                    break;
                case "Subtotals":
                    algorithm = new GenAlgorithm(new SubsetSumDataInstances(15, 100));
                    break;
            }
            AExcelReport report = new CombinationOfStepsExcelReport(iterationCount, populationCount, betta, startCount, algorithm);
            report.Create();
        }
    }
}

   


