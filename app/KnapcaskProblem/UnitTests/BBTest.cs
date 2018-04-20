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
            //TODO: find test data to check
        }

        [TestMethod]
        public void CheckBB_BFS()
        {
            var data = new TestData();
            var alg = new BranchAndBound(new KPTask().Create(data), new BFS());
            Assert.AreEqual(data.Gold(), alg.Run());
        }
    }
}