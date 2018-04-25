using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var alg = new DynamicProgramming(new DirectApproach());
            Assert.AreEqual(data.Gold(), alg.Run(new KPTask().Create(data)));

            var data1 = new TestData1();
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }

        [TestMethod]
        public void CheckDP_Recurrent()
        {
            var data = new TestData();
            var alg = new DynamicProgramming();
            Assert.AreEqual(data.Gold(), alg.Run(new KPTask().Create(data)));

            var data1 = new TestData1();
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }

        [TestMethod]
        public void CheckDP_ClassicalUKP()
        {
            var data = new TestData();
            var alg = new DynamicProgramming(new ClassicalUKPApproach());
            Assert.AreEqual(data.UKPGold(), alg.Run(new UKPTask().Create(data)));

            var data1 = new TestData1();
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }

        [TestMethod]
        public void CheckDP_EDUK_EX()
        {
            var data = new TestData();
            var alg = new DynamicProgramming(new EDUK_EX(5, 5));
            Assert.AreEqual(data.UKPGold(), alg.Run(new UKPTask().Create(data)));

            var data1 = new TestData1();
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }
    }
}