using System;
using System.Collections.Generic;
using Algorithm;

namespace CorrectnessTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Correctness tests for KP are running...");
            var tests = Parser.Parse01KP();

            Console.WriteLine("BranchAndBound algorithm");
            CheckExactAlgorithm(new BranchAndBound(), tests);
            CheckExactAlgorithm(new BranchAndBound(new BFS()), tests);

            Console.WriteLine("DynamicProgramming DirectApproach algorithm");
            CheckExactAlgorithm(new DynamicProgramming(new DirectApproach()), tests);

            Console.WriteLine("DynamicProgramming RecurrentApproach algorithm");
            CheckExactAlgorithm(new DynamicProgramming(new RecurrentApproach()), tests);

            Console.WriteLine("Genetic Algorithm with default parameters");
            CheckHeuristicAlgorithm(new GeneticAlgorithm(), tests);

            Console.WriteLine("\nCorrectness tests for UKP are running...");
            tests = Parser.ParseUKP();

            Console.WriteLine("BranchAndBound algorithm with U3 Total Upper Bound");
            CheckExactAlgorithm(new BranchAndBound(new U3Bound()), tests);
            CheckExactAlgorithm(new BranchAndBound(new BFS(), new U3Bound()), tests);

            Console.WriteLine("GABBHybrid algorithm");
            CheckExactAlgorithm(new GABBHybrid(), tests);

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
                long result = -1;
                Console.WriteLine("{0}", test.Name());

                var executed = Helpers.ExecuteWithTimeLimit(TimeSpan.FromMinutes(5), () =>
                {
                    result = alg.Run(test.Data());
                });
                if (!executed)
                {
                    Console.WriteLine("FAIL on {0}. Stopped by timer. Execution time exceeded 5 minutes.");
                    continue;
                }
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
                Console.WriteLine("{0}", test.Name());
                alg.SetData(test.Data());
                var result = alg.Run(30, 2);
                var gold = test.Gold();
                var error = gold - result;
                if (error != 0)
                {
                    Console.WriteLine(" {0}. Expected: {1}, Actual: {2}, Error: {3}", test.Name(), gold, result, error);
                }
            }
        }
    }
}
