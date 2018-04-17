using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class GATest
    {
        private int GOLD = 175;
        private int OFFSET = 10;

        [TestMethod]
        public void CheckGAWithParams()
        {
                var data = new KPTask().Create(new TestData(15, 100));
                var alg = new GeneticAlgorithm(data,
                new RandomPopulation(),
                new SinglePointCrossover(),
                new PointMutation(),
                new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator()));
                Assert.IsTrue(GOLD - OFFSET <= alg.Run(30, 15));
        }
    }

    [TestClass]
    public class InitialPopulationTest
    {
        [TestMethod]
        public void CheckDanzigAlgorithm()
        {
            var alg = new DanzigAlgorithm();
            var data  = new KPTask().Create(new TestData(15, 100));
            var individ = alg.Run(data);

            Assert.AreEqual(individ.SIZE, data.COST.Length);
        }
    }
}