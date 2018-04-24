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
            var alg = new BranchAndBound(new KPTask().Create(data));
            Assert.AreEqual(data.Gold(), alg.Run());

            alg = new BranchAndBound(new UKPTask().Create(data));
            Assert.AreEqual(data.UKPGold(), alg.Run());

            var data1 = new TestData1();
            alg = new BranchAndBound(new UKPTask().Create(data1));
            Assert.AreEqual(data1.UKPGold(), alg.Run());
        }

        [TestMethod]
        public void CheckBB_BFS()
        {
            var data = new TestData();
            var alg = new BranchAndBound(new KPTask().Create(data), new BFS());
            Assert.AreEqual(data.Gold(), alg.Run());

            alg = new BranchAndBound(new UKPTask().Create(data), new BFS());
            Assert.AreEqual(data.UKPGold(), alg.Run());

            var data1 = new TestData1();
            alg = new BranchAndBound(new UKPTask().Create(data1), new BFS());
            Assert.AreEqual(data1.UKPGold(), alg.Run());
        }
    }
}