using System;

namespace KnapsackProblem
{
    public interface IData
    {
        void Fill();
        string Str();
        int[] COST { get; }
        int[] WEIGHT { get; }
        int MAX_WEIGHT { get; }
        int RANGE { get; }
       }

    public abstract class AData : IData
    {
        protected Random m_random;

        public int[] COST { get; protected set; }
        public int[] WEIGHT { get; protected set; }
        public int MAX_WEIGHT { get; protected set; }
        public int RANGE { get; protected set; }

        public AData(int range, int[] cost, int[] weight, int maxWeight)
        {
            RANGE = range;
            COST = cost;
            WEIGHT = weight;
            MAX_WEIGHT = maxWeight;
            m_random = new Random(System.DateTime.Now.Millisecond);
        }

        public AData(int size, int range)
        {
            RANGE = range;
            COST = new int[size];
            WEIGHT = new int[size];
            m_random = new Random(System.DateTime.Now.Millisecond);
        }

        public abstract void Fill();

        public abstract string Str();
    }
    public class TestData : AData //204
    {
        public TestData(int size, int range) : base(size, range) { }
        public TestData(int range, int[] cost, int[] weight, int maxWeight) : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            int[] tmpCost = { 21, 19, 27, 3, 24, 30, 6, 13, 2, 21, 26, 26, 24, 1, 10 };
            int[] tmpWeight = { 2, 26, 23, 6, 19, 9, 8, 20, 11, 1, 17, 21, 7, 20, 11 };
            COST = tmpCost;
            WEIGHT = tmpWeight;
            MAX_WEIGHT = 80;
        }

        public override string Str()
        {
            return "test";
        }
    }

    public class UncorrData : AData
    {
        public UncorrData(int size, int range) : base(size, range) { }
        public UncorrData(int range, int[] cost, int[] weight, int maxWeight) : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(1, RANGE + 1);
                COST[i] = m_random.Next(1, RANGE + 1);
                summaryWeight += WEIGHT[i];
            }
            MAX_WEIGHT = (int)(summaryWeight * 0.8);
        }

        public override string Str()
        {
            return "uncorreled";
        }
    }

    public class WeaklyCorrData : AData
    {
        public WeaklyCorrData(int size, int range) : base(size, range) { }
        public WeaklyCorrData(int range, int[] cost, int[] weight, int maxWeight) : base(range, cost, weight, maxWeight) { }

        private int GetCost(int weight)
        {
            int cost = m_random.Next((weight - RANGE / 10), (weight + RANGE / 10) + 1);
            if (cost < 1)
                cost = GetCost(weight);
            return cost;
        }

        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(1, RANGE + 1);
                COST[i] = GetCost(WEIGHT[i]);
                summaryWeight += WEIGHT[i];
            }
            MAX_WEIGHT = (int)(summaryWeight * 0.8);
        }

        public override string Str()
        {
            return "weakly_correled";
        }
    }

    public class StronglyCorrData : AData
    {
        public StronglyCorrData(int size, int range) : base(size, range) { }
        public StronglyCorrData(int range, int[] cost, int[] weight, int maxWeight) : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(1, RANGE + 1);
                COST[i] = WEIGHT[i] + 10;
                summaryWeight += WEIGHT[i];
            }
            MAX_WEIGHT = (int)(summaryWeight * 0.8);
        }

        public override string Str()
        {
            return "strongly_correled";
        }
    }

    public class SubsetSumData : AData
    {
        public SubsetSumData(int size, int range) : base(size, range) { }
        public SubsetSumData(int range, int[] cost, int[] weight, int maxWeight) : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(1, RANGE + 1);
                COST[i] = WEIGHT[i];
                summaryWeight += WEIGHT[i];
            }
            MAX_WEIGHT = (int)(summaryWeight * 0.8);
        }

        public override string Str()
        {
            return "subset_summ";
        }
    }
}
