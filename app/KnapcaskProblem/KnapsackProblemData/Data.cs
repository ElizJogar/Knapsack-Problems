using System;

namespace KnapsackProblem
{
    public class Range
    {
        public int First = 1;
        public int Second = 1;

        public Range(int second)
        {
            Second = second;
        }

        public Range(int first, int second)
        {
            First = first;
            Second = second;
        }
    }
    public interface IData
    {
        void Fill();
        string Str();
        int[] COST { get; }
        int[] WEIGHT { get; }
        int CAPACITY { get; }
        Range RANGE { get; }
    }

    public abstract class AData : IData
    {
        protected Random m_random;

        public int[] COST { get; protected set; }
        public int[] WEIGHT { get; protected set; }
        public int CAPACITY { get; protected set; }
        public Range RANGE { get; protected set; }

        public AData() { }

        public AData(int size, Range range)
        {
            RANGE = range;
            COST = new int[size];
            WEIGHT = new int[size];
            m_random = new Random(System.DateTime.Now.Millisecond);
        }

        public AData(Range range, int[] cost, int[] weight, int maxWeight)
        {
            RANGE = range;
            COST = cost;
            WEIGHT = weight;
            CAPACITY = maxWeight;
            m_random = new Random(System.DateTime.Now.Millisecond);
        }
        public void FillCapacity(int maxWeight)
        {
            while (CAPACITY < maxWeight) CAPACITY = m_random.Next(10000, 100001);
        }

        public abstract void Fill();

        public abstract string Str();
    }

    public class UncorrData : AData
    {
        public UncorrData(int size, Range range) : base(size, range) { }
        public UncorrData(Range range, int[] cost, int[] weight, int maxWeight)
            : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            var maxWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(RANGE.First, RANGE.Second + 1);
                COST[i] = m_random.Next(RANGE.First / 10, RANGE.Second + 1);
                if (maxWeight < WEIGHT[i]) maxWeight = WEIGHT[i];
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "uncorreled";
        }
    }

    public class WeaklyCorrData : AData
    {
        public WeaklyCorrData(int size, Range range) : base(size, range) { }
        public WeaklyCorrData(Range range, int[] cost, int[] weight, int maxWeight)
            : base(range, cost, weight, maxWeight) { }

        private int GetCost(int weight)
        {
            int cost = m_random.Next(weight - 100, weight + 101);
            if (cost < 1) cost = GetCost(weight);
            return cost;
        }

        public override void Fill()
        {
            var maxWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(RANGE.First, RANGE.Second + 1);
                COST[i] = GetCost(WEIGHT[i]);
                if (maxWeight < WEIGHT[i]) maxWeight = WEIGHT[i];
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "weakly_correled";
        }
    }

    public class StronglyCorrData : AData
    {
        public StronglyCorrData(int size, Range range) : base(size, range) { }
        public StronglyCorrData(Range range, int[] cost, int[] weight, int maxWeight)
            : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            var maxWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(RANGE.First, RANGE.Second + 1);
                COST[i] = WEIGHT[i] + 100;
                if (maxWeight < WEIGHT[i]) maxWeight = WEIGHT[i];
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "strongly_correled";
        }
    }

    public class SubsetSumData : AData
    {
        public SubsetSumData(int size, Range range) : base(size, range) { }
        public SubsetSumData(Range range, int[] cost, int[] weight, int maxWeight)
            : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            var maxWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(RANGE.First, RANGE.Second + 1);
                COST[i] = WEIGHT[i];
                if (maxWeight < WEIGHT[i]) maxWeight = WEIGHT[i];
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "subset_summ";
        }
    }

    public class VeryVeryStronglyCorrData: AData
    {
        public VeryVeryStronglyCorrData(int size, Range range) : base(size, range) { }
        public VeryVeryStronglyCorrData(Range range, int[] cost, int[] weight, int maxWeight)
            : base(range, cost, weight, maxWeight) { }
        public override void Fill()
        {
            var minWeight = int.MaxValue;
            var maxWeight = 0;
            for (int i = 0; i < WEIGHT.Length; i++)
            {
                WEIGHT[i] = m_random.Next(RANGE.First, RANGE.Second + 1);
                if (minWeight > WEIGHT[i]) minWeight = WEIGHT[i];
                if (maxWeight < WEIGHT[i]) maxWeight = WEIGHT[i];
            }
            for (int i = 0; i < COST.Length; i++)
            {
                COST[i] = WEIGHT[i] * (WEIGHT[i] - minWeight + 1)/(maxWeight - minWeight + 1) * 100;
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "strongly_correled";
        }
    }
}
