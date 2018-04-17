using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class ESATest
    {
        [TestMethod]
        public void CheckESA()
        {
            const int GOLD = 175;
            var data = new KPTask().Create(new TestData());
            var alg = new ExhaustiveSearchAlgorithm(data);
            Assert.AreEqual(GOLD, alg.Run());
        }
    }
}