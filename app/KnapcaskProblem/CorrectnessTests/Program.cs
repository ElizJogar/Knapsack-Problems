﻿using System;
using System.Collections.Generic;
using Algorithm;

namespace CorrectnessTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Correctness tests for UKP are running...");
            var tests = Parser.ParseUKP();

            Console.WriteLine("BranchAndBound algorithm");
            CheckExactAlgorithm(new BranchAndBound(), tests);
            CheckExactAlgorithm(new BranchAndBound(new BFS()), tests);

            Console.WriteLine("DynamicProgramming EDUK_EX(2, 2) algorithm");
            CheckExactAlgorithm(new DynamicProgramming(new EDUK_EX(2, 2)), tests);

            Console.WriteLine("DynamicProgramming ClassicalUKP algorithm");
            CheckExactAlgorithm(new DynamicProgramming(new ClassicalUKPApproach()), tests);

            Console.WriteLine("Genetic Algorithm with default parameters");
            CheckHeuristicAlgorithm(new GeneticAlgorithm(), tests);
        }

        static void CheckExactAlgorithm(IExactAlgorithm alg, List<ITest> tests)
        {
            foreach (var test in tests)
            {
                var result = alg.Run(test.Data());
                var gold = test.Gold();

                if (result != gold)
                {
                    Console.WriteLine("FAIL on {0}. Expected: {1}, Actual: {2}.", test.Name(), gold, result);
                }
            }
        }

        static void CheckHeuristicAlgorithm(IHeuristicAlgorithm alg, List<ITest> tests)
        {
            foreach (var test in tests)
            {
                alg.SetData(test.Data());
                var result = alg.Run(30, 2);
                var gold = test.Gold();
                var error = gold - result;
                if (error < 0 || error > Math.Pow(10, (int)Math.Log10(gold) - 1))
                {
                    Console.WriteLine(" {0}. Expected: {1}, Actual: {2}, Error: {3}", test.Name(), gold, result, error);
                }
            }
        }
    }
}
