using System;
using System.Collections.Generic;
using Algorithm;
using KnapsackProblem;

namespace CorrectnessTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Correctness tests for UKP are running...");
            var tests = Parser.ParseUKP();

            Console.WriteLine("BranchAndBound algorithm");
            CheckAlgorithm(new BranchAndBound(), tests);
            CheckAlgorithm(new BranchAndBound(new BFS()), tests);

            Console.WriteLine("DynamicProgramming EDUK_EX(2, 2) algorithm");
            CheckAlgorithm(new DynamicProgramming(new EDUK_EX(2, 2)), tests);

            Console.WriteLine("DynamicProgramming ClassicalUKP algorithm");
            CheckAlgorithm(new DynamicProgramming(new ClassicalUKPApproach()), tests);
        }

        static void CheckAlgorithm(IExactAlgorithm alg, List<ITest> tests)
        {
            var pass = true;
            foreach (var test in tests)
            {
                var result = alg.Run(test.Data());
                var gold = test.Gold();

                if (result != gold)
                {
                    pass = false;
                    Console.WriteLine("FAIL on {0}. Expected: {1}, Actual: {2}.", test.Name(), gold, result);
                }
            }

            if(pass)
            {
                Console.WriteLine(" PASS");
            }
        }
    }
}
