using System;
using ExcelReport;

using KnapsackProblem;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgParser parser = new ArgParser();
            var iterationCount = 30;// 40;
            var populationCount = 15;// 30;
            var betta = 5;
            var runsCount = 1;// 30; 
            var instancesCount = 1;// 5;
            parser.AddArgument("g|generation=", "Generation count", g => iterationCount = Convert.ToInt32(g));
            parser.AddArgument("p|population=", "Individ count in population", p => populationCount = Convert.ToInt32(p));
            parser.AddArgument("b|betta=", "Betta value for Betta-Tournament Selection", b => betta = Convert.ToInt32(b));
            parser.AddArgument("r|runs=", "Starts count for each test", r => runsCount = Convert.ToInt32(r));
            parser.AddArgument("inst|instances=", "Instances count for each data type", inst => instancesCount = Convert.ToInt32(inst));

            var report = new DataAnalysisReport(new UKPTask(), iterationCount, populationCount, betta, runsCount, instancesCount);
            report.Create();
            Console.WriteLine("Report created successfully! You can see reports here: {0}", report.getDir());
        }
    }
}
