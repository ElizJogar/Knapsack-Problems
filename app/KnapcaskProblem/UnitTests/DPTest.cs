﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class DPTest
    {
        [TestMethod]
        public void CheckDP_Direct()
        {
            var data = new TestData();
            var alg = new DynamicProgramming(new KPTask().Create(data), new DirectApproach());
            Assert.AreEqual(data.Gold(), alg.Run());

            var data1 = new TestData1();
            alg = new DynamicProgramming(new UKPTask().Create(data1), new DirectApproach());
            Assert.AreEqual(data1.UKPGold(), alg.Run());
        }

        [TestMethod]
        public void CheckDP_Recurrent()
        {
            var data = new TestData();
            var alg = new DynamicProgramming(new KPTask().Create(data));
            Assert.AreEqual(data.Gold(), alg.Run());

            var data1 = new TestData1();
            alg = new DynamicProgramming(new UKPTask().Create(data1));
            Assert.AreEqual(data1.UKPGold(), alg.Run());
        }
    }
}