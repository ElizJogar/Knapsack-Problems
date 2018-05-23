﻿using System;
using ExcelReport;
using KnapsackProblem;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new ArgParser();
            var reportType = "cc";
            var dataSize = 15; //50, 100, 500, 1000, 5000, 10000, 20000, 30000, 40000 and 50000
            var iterationCount = 30;
            var populationCount = 40;
            var betta = 14;
            var runsCount = 30;
            var instancesCount = 5;// 250;
            parser.AddArgument("rt|report=", "Type of report. 'cc' is a compinations compare report, 'mp' - is a measure performance report", rt => reportType = rt);
            parser.AddArgument("g|generation=", "Generation count", g => iterationCount = Convert.ToInt32(g));
            parser.AddArgument("p|population=", "Individ count in population", p => populationCount = Convert.ToInt32(p));
            parser.AddArgument("b|betta=", "Betta value for Betta-Tournament Selection", b => betta = Convert.ToInt32(b));
            parser.AddArgument("r|runs=", "Starts count for each test", r => runsCount = Convert.ToInt32(r));
            parser.AddArgument("ds|data_size=", "data size", ds => dataSize = Convert.ToInt32(ds));
            parser.AddArgument("inst|instances=", "Instances count for each data type", inst => instancesCount = Convert.ToInt32(inst));

            IReport report = null;
            if (reportType == "cc")
            {
                report = new CombinationsCompareReport(new KPTask(), iterationCount, populationCount, betta, runsCount, dataSize, instancesCount);
            }
            else if (reportType == "mp")
            {
                report = new MeasurePerformanceReport(new KPTask(), iterationCount, populationCount, betta, runsCount, dataSize, instancesCount);
            }
            report.Create();
            Console.WriteLine("Report created successfully! You can see reports here: {0}", report.GetDir());
        }
    }
}
