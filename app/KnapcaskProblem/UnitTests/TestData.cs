using KnapsackProblem;

namespace UnitTests
{
    public class TestData : AData //204
    {
        public override void Fill()
        {
            int[] tmpCost = { 21, 19, 27, 3, 24, 30, 6, 13, 2, 21, 26, 26, 24, 1, 10 };
            int[] tmpWeight = { 2, 26, 23, 6, 19, 9, 8, 20, 11, 1, 17, 21, 7, 20, 11 };
            COST = tmpCost;
            WEIGHT = tmpWeight;
            CAPACITY = 80;
        }

        public override string Str()
        {
            return "test";
        }
    }
}
