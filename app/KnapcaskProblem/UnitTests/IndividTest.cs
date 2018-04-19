using System.Collections.Generic;
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
            Assert.AreEqual(53, individ.FlatSize());

            individ.SetBit(3, true);
            individ.SetBit(5, true);

            Assert.AreEqual(840, individ.GetCost());
            Assert.AreEqual(80, individ.GetWeight());

            var individs = new List<Individ> { individ };
            Assert.IsTrue(individs.Contains(individ));

            var g = individ.GetGen(0);
            g.SetBit(0, true);

            Assert.AreEqual(861, individ.GetCost());
            Assert.AreEqual(82, individ.GetWeight());
        }
    }
}