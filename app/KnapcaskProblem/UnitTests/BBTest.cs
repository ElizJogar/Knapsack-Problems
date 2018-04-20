﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class BBTest
    {
        [TestMethod]
        public void CheckBB_DFS()
        {
            const int GOLD = 175;
            var data = new KPTask().Create(new TestData());
            var alg = new BranchAndBound(data);
            Assert.AreEqual(GOLD, alg.Run());

            const int UKP_GOLD = 243004;
            data = new UKPTask().Create(new TestData1());
            alg = new BranchAndBound(data);
            Assert.AreEqual(UKP_GOLD, alg.Run());
        }

        [TestMethod]
        public void CheckBB_BFS()
        {
            const int GOLD = 175;
            var data = new KPTask().Create(new TestData());
            var alg = new BranchAndBound(data, new BFS());
            Assert.AreEqual(GOLD, alg.Run());
        }
    }
}