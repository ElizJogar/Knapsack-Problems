using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;

namespace CorrectnessTests
{
    class Parser
    {
        static string KPTestsPath = @"../../../Datasets/01KP";
        static string UKPTestsPath = @"../../../Datasets/UKP";

        public static List<ITest> Parse01KP()
        {
            var tests = new List<ITest>();
            var files = GetTestFiles(KPTestsPath);
            foreach (var file in files)
            {
                tests.Add(CreateTest(file, new KPTask()));
            }
            return tests;
        }
        public static List<ITest> ParseUKP()
        {
            var tests = new List<ITest>();
            var files = GetTestFiles(UKPTestsPath);
            foreach (var file in files)
            {
                tests.Add(CreateTest(file, new UKPTask()));
            }
            return tests;
        }
        private static ITest CreateTest(string file, ITask task)
        {
            var lines = File.ReadAllLines(file);
            var capacity = Convert.ToInt64(lines[0]);
            var cost = lines[1].Split(' ', ',', ';').Select(Int64.Parse).ToArray();
            var weight = lines[2].Split(' ', ',', ';').Select(Int64.Parse).ToArray();
            var optimum = Convert.ToInt64(lines[3]);

            return new Test(task, new TestData(new Range(0, 0), cost, weight, capacity), optimum, file);
        }
        private static string[] GetTestFiles(string path)
        {
            return Directory.GetFiles(path, "*.txt");
        }
    }
}
