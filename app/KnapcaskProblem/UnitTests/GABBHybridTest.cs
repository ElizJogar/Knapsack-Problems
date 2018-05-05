using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using KnapsackProblem;
namespace UnitTests
{
    [TestClass]
    public class GABBHybridTest
    {
        [TestMethod]
        public void CheckGABBHybrid()
        {
            var data = new TestData1();
            var alg = new GABBHybrid();
            Assert.AreEqual(data.UKPGold(), alg.Run(new UKPTask().Create(data)));
        }
    }
}