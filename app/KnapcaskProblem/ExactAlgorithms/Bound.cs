using System;
using System.Collections.Generic;

namespace Algorithm
{
    public interface ITotalBound
    {
        long Calculate(List<Item> items, long capacity);
    }
    public interface IBound
    {
        long Calculate(Node n, List<Item> items, long capacity);
    }

    public class U3Bound : ITotalBound
    {
        private bool m_needSort;
        public U3Bound(bool needSort = false)
        {
            m_needSort = needSort;
        }
        public long Calculate(List<Item> items, long capacity)
        {
            if (m_needSort)
            {
                items = new List<Item>(items);
                items.Sort((a, b) =>
                {
                    double first = (double)a.cost / a.weight;
                    double second = (double)b.cost / b.weight;
                    return second.CompareTo(first);
                });
            }

            long c0 = capacity % items[0].weight;
            long c1 = c0 % items[1].weight;
            long z = capacity / items[0].weight * items[0].cost + c0 / items[1].weight * items[1].cost;

            var multiplier = (int)Math.Ceiling((double)(items[1].weight - c1) / items[0].weight);
            double term = (c1 + multiplier * items[0].weight) * (double)(items[1].cost) / items[1].weight - items[0].cost;
            long U1 = z + (long)term;

            if (items.Count == 2) return U1;

            long U0 = z + c1 * items[2].cost / items[2].weight;

            return Math.Max(U0, U1);
        }
    }
    public class FakeTotalBound : ITotalBound
    {
        public long Calculate(List<Item> items, long capacity)
        {
            return long.MaxValue;
        }
    }

    public class GreedyLowerBound : IBound
    {
        private bool m_needSort;
        public GreedyLowerBound(bool needSort = false)
        {
            m_needSort = needSort;
        }
        public long Calculate(Node n, List<Item> items, long capacity)
        {
            if (n.weight > capacity) return 0;

            var totalWeight = n.weight;
            var costBound = n.cost;

            var i = n.level + 1;
            if (m_needSort)
            {
                i = 0;
                items = items.GetRange(n.level + 1, items.Count - (n.level + 1));
                items.Sort((a, b) =>
                {
                    double first = (double)a.cost / a.weight;
                    double second = (double)b.cost / b.weight;
                    return second.CompareTo(first);
                });
            }

            for (; i < items.Count && (totalWeight + items[i].weight) <= capacity; ++i)
            {
                var count = items[i].maxCount;
                while (count-- > 0 && (totalWeight + items[i].weight) <= capacity)
                {
                    totalWeight += items[i].weight;
                    costBound += items[i].cost;
                }
            }

            if (i < items.Count)
            {
                costBound += (capacity - totalWeight) * items[i].cost / items[i].weight;
            }
            return costBound;
        }
    }
}