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
            const int GOLD = 175;
            var data = new KPTask().Create(new TestData());
            var alg = new DynamicProgramming(data, new DirectApproach());
            Assert.AreEqual(GOLD, alg.Run());

            const int UKP_GOLD = 243004;
            data = new UKPTask().Create(new TestData1());
            alg = new DynamicProgramming(data, new DirectApproach());
            Assert.AreEqual(UKP_GOLD, alg.Run());
        }

        [TestMethod]
        public void CheckDP_Recurrent()
        {
            const int GOLD = 175;
            var data = new KPTask().Create(new TestData());
            var alg = new DynamicProgramming(data);
            Assert.AreEqual(GOLD, alg.Run());

            const int UKP_GOLD = 243004;
            data = new UKPTask().Create(new TestData1());
            alg = new DynamicProgramming(data);
            Assert.AreEqual(UKP_GOLD, alg.Run());
        }
    }
}