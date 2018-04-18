﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;

namespace UnitTests
{
    [TestClass]
    public class IndividTest
    {
        [TestMethod]
        public void CheckIndivid()
        {
            var individ = new Individ(new UKPTask().Create(new TestData()));

            Assert.AreEqual(15, individ.Size());
            Assert.AreEqual(38, individ.FlatSize());

            individ.SetBit(3, true);
            individ.SetBit(5, true);

            Assert.AreEqual(187, individ.GetCost());
            Assert.AreEqual(42, individ.GetWeight());

            var individs = new List<Individ> { individ };
            Assert.IsTrue(individs.Contains(individ));

            var g = individ.GetGen(0);
            g.SetBit(0, true);

            Assert.AreEqual(208, individ.GetCost());
            Assert.AreEqual(44, individ.GetWeight());
        }
    }
}