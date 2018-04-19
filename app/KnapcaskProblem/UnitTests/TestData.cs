using KnapsackProblem;

namespace UnitTests
{
    public class TestData : AData
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

    public class UKPTestData : AData
    {
        public UKPTestData() : base(15) { }
        public UKPTestData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
           : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long[] tmpCost = { 9160, 1098, 4725, 3383, 4493, 8074, 2453, 2755, 8350, 3076, 3672, 8846, 6996, 3191, 5324 };
            long[] tmpWeight = { 6568, 6300, 3970, 2729, 7950, 2333, 3572, 3678, 4939, 795, 5045, 9026, 8391, 3296, 4926 };
            Cost = tmpCost;
            Weight = tmpWeight;
            Capacity = 63072;
        }
        public override string Str()
        {
            return "test";
        }
    }
}
