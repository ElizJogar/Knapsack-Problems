using System;
using System.Collections.Generic;

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
        long[] Cost { get; }
        long[] Weight { get; }
        long Capacity { get; }
        Range Range { get; }
        int[] ItemMaxCounts { get; set; }
    }

    public abstract class AData : IData
    {
        protected Random m_random;

        public long[] Cost { get; protected set; }
        public long[] Weight { get; protected set; }
        public int[] ItemMaxCounts { get; set; }
        public long Capacity { get; protected set; }
        public Range Range { get; protected set; }

        public AData(int size)
        {
            ItemMaxCounts = new int[size];
        }

        public AData(int size, Range range)
        {
            Range = range;
            Cost = new long[size];
            Weight = new long[size];
            ItemMaxCounts = new int[size];
            m_random = new Random(DateTime.Now.Millisecond);
        }

        public AData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
        {
            Range = range;
            Cost = cost;
            Weight = weight;
            Capacity = maxWeight;
            ItemMaxCounts = itemMaxCounts ?? (new int[cost.Length]);
            m_random = new Random(DateTime.Now.Millisecond);
        }
        protected void FillCapacity(long maxWeight)
        {
            while (Capacity < maxWeight) Capacity = m_random.Next(10000, 100001);
        }
        public abstract void Fill();

        public abstract string Str();
    }

    public class UncorrData : AData
    {
        public UncorrData(int size, Range range) : base(size, range) { }
        public UncorrData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
            : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long maxWeight = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_random.Next(Range.First, Range.Second + 1);
                Cost[i] = m_random.Next(Range.First / 10, Range.Second + 1);
                if (maxWeight < Weight[i]) maxWeight = Weight[i];
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
        public WeaklyCorrData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
            : base(range, cost, weight, maxWeight, itemMaxCounts) { }

        private long GetCost(long weight)
        {
            long cost = m_random.Next((int)weight - 100, (int)weight + 101);
            if (cost < 1) cost = GetCost(weight);
            return cost;
        }

        public override void Fill()
        {
            long maxWeight = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_random.Next(Range.First, Range.Second + 1);
                Cost[i] = GetCost(Weight[i]);
                if (maxWeight < Weight[i]) maxWeight = Weight[i];
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
        public StronglyCorrData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
            : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long maxWeight = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_random.Next(Range.First, Range.Second + 1);
                Cost[i] = Weight[i] + 100;
                if (maxWeight < Weight[i]) maxWeight = Weight[i];
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
        public SubsetSumData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
            : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long maxWeight = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_random.Next(Range.First, Range.Second + 1);
                Cost[i] = Weight[i];
                if (maxWeight < Weight[i]) maxWeight = Weight[i];
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "subset_summ";
        }
    }

    public class VeryVeryStronglyCorrData : AData
    {
        public VeryVeryStronglyCorrData(int size, Range range) : base(size, range) { }
        public VeryVeryStronglyCorrData(Range range, long[] cost, long[] weight, long maxWeight, int[] itemMaxCounts = null)
            : base(range, cost, weight, maxWeight, itemMaxCounts) { }
        public override void Fill()
        {
            long minWeight = long.MaxValue;
            long maxWeight = 0;
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_random.Next(Range.First, Range.Second + 1);
                if (minWeight > Weight[i]) minWeight = Weight[i];
                if (maxWeight < Weight[i]) maxWeight = Weight[i];
            }
            for (int i = 0; i < Cost.Length; i++)
            {
                Cost[i] = Weight[i] * (Weight[i] - minWeight + 1) / (maxWeight - minWeight + 1) * 100;
            }
            FillCapacity(maxWeight);
        }

        public override string Str()
        {
            return "strongly_correled";
        }
    }
}
