using KnapsackProblem;

namespace UnitTests
{
    public class TestData : AData //204
    {
        public TestData() : base(15) { }
        public TestData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
           : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long[] tmpCost = { 21, 19, 27, 3, 24, 30, 6, 13, 2, 21, 26, 26, 24, 1, 10 };
            long[] tmpWeight = { 2, 26, 23, 6, 19, 9, 8, 20, 11, 1, 17, 21, 7, 20, 11 };
            Cost = tmpCost;
            Weight = tmpWeight;
            Capacity = 80;
        }

        public override string Str()
        {
            return "test";
        }
    }
}
