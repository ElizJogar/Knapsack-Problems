using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var data = new TestData();
            var alg = new BranchAndBound();
            Assert.AreEqual(data.Gold(), alg.Run(new KPTask().Create(data)));
            Assert.AreEqual(data.UKPGold(), alg.Run(new UKPTask().Create(data)));

            var data1 = new TestData1();
            alg = new BranchAndBound(new U3Bound());
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }

        [TestMethod]
        public void CheckBB_BFS()
        {
            var data = new TestData();
            var alg = new BranchAndBound(new BFS());
            Assert.AreEqual(data.Gold(), alg.Run(new KPTask().Create(data)));
            Assert.AreEqual(data.UKPGold(), alg.Run(new UKPTask().Create(data)));

            var data1 = new TestData1();
            alg = new BranchAndBound(new BFS(), new U3Bound());
            Assert.AreEqual(data1.UKPGold(), alg.Run(new UKPTask().Create(data1)));
        }
    }
}