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
            var result = alg.Run(30, 15);
            Assert.IsTrue(GOLD - OFFSET <= result);
        }
    }
}