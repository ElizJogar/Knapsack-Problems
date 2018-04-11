using System;
using System.Collections.Generic;

using ExcelReport;
using Algorithm;
using KnapsackProblemData;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgParser parser = new ArgParser();
            bool debug = false;
            var iterationCount = 5;// 40;
            var populationCount = 5;// 30;
            var betta = 2;
            var runsCount = 1;// 30; 
            var instancesCount = 1;// 5;

            parser.AddArgument("debug=", "Run Genetic Algorithm for Test Data", d => debug = Convert.ToBoolean(d));
            parser.AddArgument("g|generation=", "Generation count", g => iterationCount = Convert.ToInt32(g));
            parser.AddArgument("p|population=", "Individ count in population", p => populationCount = Convert.ToInt32(p));
            parser.AddArgument("b|betta=", "Betta value for Betta-Tournament Selection", b => betta = Convert.ToInt32(b));
            parser.AddArgument("r|runs=", "Starts count for each test", r => runsCount = Convert.ToInt32(r));
            parser.AddArgument("inst|instances=", "Instances count for each task type", inst => instancesCount = Convert.ToInt32(inst));

            if (!debug)
            {
                var report = new DataAnalysisReport(iterationCount, populationCount, betta, runsCount, instancesCount);
                report.Create();
                Console.WriteLine("Report created successfully! You can see reports here: {0}", report.getDir());
            }
            else
            {
                var data = new TestData(15, 100);
                data.Fill();
                var alg = new GeneticAlgorithm(data, new DanzigAlgorithm(), new SinglePointCrossover(), new PointMutation(), new LinearRankSelection());
                Console.WriteLine("Genetic Algorithm Result: {0}", alg.Run(iterationCount, populationCount, 3));
                var exactAlg = new ExhaustiveSearchAlgorithm(data);
                Console.WriteLine("Optimum: {0}", exactAlg.Run());
            }
        }
    }
}
