using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
   [TestClass]
    public class GATest
    {
        [TestMethod]
        public void CheckGA()
        {
            const int GOLD = 175;
            const int OFFSET = 10;
            var data = new KPTask().Create(new TestData());
            var alg = new GeneticAlgorithm(data,
            new RandomPopulation(),
            new SinglePointCrossover(),
            new PointMutation(),
            new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator()));
            Assert.IsTrue(GOLD - OFFSET <= alg.Run(30, 15));
        }

        [TestMethod]
        public void CheckOperators()
        {
            //check Danzig algorithm
            IInitialPopulation alg = new DantzigAlgorithm();
            var data = new KPTask().Create(new TestData());
            var parents = new List<Individ>
            {
                alg.Run(data)
            };
            CheckAdmissibleIndivid(parents[0], data);

            //check Random population
            alg = new RandomPopulation();
            parents.Add(alg.Run(data));
            CheckAdmissibleIndivid(parents[1], data);

            //check Single Point Crossover
            ICrossover crossover = new SinglePointCrossover();
            CheckIndivids(crossover.Run(parents), data, 4);

            //check Two Point Crossover
            crossover = new TwoPointCrossover();
            CheckIndivids(crossover.Run(parents), data, 4);

            //check Uniform Crossover
            crossover = new UniformCrossover();
            var individs = crossover.Run(parents);
            CheckIndivids(individs, data, 4);

            IMutation mutation = new PointMutation();
            //check Point Mutation
            CheckIndivids(mutation.Run(individs), data, 4);

            //check Inversion
            mutation = new Inversion();
            CheckIndivids(mutation.Run(individs), data, 4);

            //check Translocation
            mutation = new Translocation();
            CheckIndivids(mutation.Run(individs), data, 4);

            //check Saltation
            mutation = new Saltation();
            individs = mutation.Run(individs);
            CheckIndivids(individs, data, 4);

            IConstraintProcessing cp = new PenaltyFunction();
            var newIndivids = cp.Run(individs, data);
            Assert.AreEqual(4, newIndivids.Count);
            foreach (var individ in newIndivids)
            {
                Assert.IsTrue(individ.COST >= 0 && individ.WEIGHT >= 0);
            }
            // check simple Repair operator
            IRepairOperator repairOperator = new RepairOperator();
            CheckAdmissibleIndivids(repairOperator.Run(individs, data), data, 4);

            // check Efficient Repair operator
            repairOperator = new EfficientRepairOperator();
            CheckAdmissibleIndivids(repairOperator.Run(individs, data), data, 4);

            // check Betta Tournament with Penalty function and Repair operator for beta = 2
            ISelection selection = new BettaTournament(new PenaltyFunction(), new RepairOperator());
            CheckAdmissibleIndivids(selection.Run(individs, 2, data, 2), data, 2);

            // check Betta Tournament with Penalty function and Efficient Repair operator for beta = 2
            selection = new BettaTournament(new PenaltyFunction(), new EfficientRepairOperator());
            CheckAdmissibleIndivids(selection.Run(individs, 2, data, 2), data, 2);

            // check Linear Rank selection with Penalty function and Repair operator
            selection = new LinearRankSelection(new PenaltyFunction(), new RepairOperator());
            CheckAdmissibleIndivids(selection.Run(individs, 2, data), data, 2);

            // check Linear Rank selection fwith Penalty function and Efficient Repair operator
            selection = new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
            CheckAdmissibleIndivids(selection.Run(individs, 2, data), data, 2);
        }
        private void CheckIndivids(List<Individ> individs, IData data, int goldCount)
        {
            Assert.AreEqual(individs.Count, goldCount);
            foreach (var individ in individs)
            {
                // Individ contains not only zeros
                Assert.IsTrue(Helpers.GetWeight(individ, data) > 0);
            }
        }
        private void CheckAdmissibleIndivid(Individ individ, IData data)
        {
            Assert.AreEqual(data.COST.Length, individ.SIZE);
            var weight = Helpers.GetWeight(individ, data);
            Assert.IsTrue(weight <= data.CAPACITY && weight > 0);
        }

        private void CheckAdmissibleIndivids(List<Individ> individs, IData data, int goldCount)
        {
            Assert.AreEqual(goldCount, individs.Count);
            foreach (var individ in individs)
            {
                CheckAdmissibleIndivid(individ, data);
            }
        }
    }
}