using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class ESATest
    {
        private int GOLD = 175;
        [TestMethod]
        public void CheckESA()
        {
            var data = new KPTask().Create(new TestData(15, 100));
            var alg = new ExhaustiveSearchAlgorithm(data);
            Assert.AreEqual(GOLD, alg.Run());
        }
    }
}