﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;

namespace CorrectnessTests
{
    interface IExtParser
    {
        ITest Parse(string file);
    }

    class TxtParser: IExtParser
    {
        private ITask m_task;
        public TxtParser(ITask task)
        {
            m_task = task;
        }

        public ITest Parse(string file)
        {
            var lines = File.ReadAllLines(file);
            var capacity = Convert.ToInt64(lines[0]);
            var cost = lines[1].Split(' ', ',', ';').Select(Int64.Parse).ToArray();
            var weight = lines[2].Split(' ', ',', ';').Select(Int64.Parse).ToArray();
            var optimum = Convert.ToInt64(lines[3]);

            return new Test(m_task, new TestData(new Range(0, 0), cost, weight, capacity), optimum, file);
        }
    }
    class UkpParser : IExtParser
    {
        public ITest Parse(string file)
        {
            var lines = File.ReadAllLines(file);

            long[] cost = null;
            long[] weight = null;
            long length = 0;
            long capacity = 0;
            bool beginData = false;
            int index = 0;
            bool optimumPresents = false;
            long optimum = 0;

            foreach (var line in lines)
            {
                if (line.IndexOf("##EDUK") != -1) continue;

                if (line.IndexOf("n:") != -1)
                {
                    length = GetInt64(line);
                    cost = new long[length];
                    weight = new long[length];
                }
                else if (line.IndexOf("c:") != -1)
                {
                    capacity = GetInt64(line);
                }
                else if (line.IndexOf("begin data") != -1)
                {
                    beginData = true;
                }
                else if (line.IndexOf("end data") != -1)
                {
                    beginData = false;
                }
                else if (beginData)
                {
                    var data = line.Split(' ', '\t');
                    weight[index] = Convert.ToInt64(data[0]);
                    cost[index] = Convert.ToInt64(data[1]);
                    ++index;
                }
                // TODO: refactor it
                else if (line.IndexOf("#The optimal value") != -1)
                {
                    optimumPresents = true;
                }
                else if (optimumPresents)
                {
                    optimumPresents = false;
                    optimum = Convert.ToInt64(line);
                }
            }
            return new Test(new UKPTask(), new TestData(new Range(0, 0), cost, weight, capacity), optimum, file);
        }
        private long GetInt64(string str)
        {
            var res = str.Split(" ").ToArray();
            return Convert.ToInt64(res[1]);
        }
    }
    class Parser
    {
        static string KPTestsPath = @"../../../Datasets/01KP";
        static string UKPTestsPath = @"../../../Datasets/UKP";

        public static List<ITest> Parse01KP()
        {
            var tests = new List<ITest>();
            var files = GetTestFiles(KPTestsPath);
            var parser = new TxtParser(new KPTask());
            foreach (var file in files)
            {
                tests.Add(parser.Parse(file));
            }
            return tests;
        }
        public static List<ITest> ParseUKP()
        {
            var tests = new List<ITest>();
            var files = GetTestFiles(UKPTestsPath);
            IExtParser parser = new TxtParser(new UKPTask());
            foreach (var file in files)
            {
                tests.Add(parser.Parse(file));
            }
            files = GetTestFiles(UKPTestsPath, "*.ukp");
            parser = new UkpParser();
            foreach (var file in files)
            {
                tests.Add(parser.Parse(file));
            }
            return tests;
        }
        private static string[] GetTestFiles(string path, string pattern = "*.txt")
        {
            return Directory.GetFiles(path, pattern);
        }
    }
}
