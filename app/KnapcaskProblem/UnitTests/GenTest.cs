using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;

namespace UnitTests
{
    [TestClass]
    public class GenTest
    {
        [TestMethod]
        public void CheckGen()
        {
            long cost = 70;
            long weight = 80;

            var gen = new Gen(cost, weight, 4);
            Assert.AreEqual(3, gen.Size());

            for (var i = 0; i < 2; ++i)
            {
                gen.SetBit(i, true);
            }
            Assert.AreEqual(210, gen.GetCost());
            Assert.AreEqual(240, gen.GetWeight());

            int gold = 3;
            while(gen.Decrement())
            {
                Assert.AreEqual(gen.ToInt(), --gold);
            }

            while(gen.Increment())
            {
                Assert.AreEqual(gen.ToInt(), ++gold);
            }
        }
    }
}
