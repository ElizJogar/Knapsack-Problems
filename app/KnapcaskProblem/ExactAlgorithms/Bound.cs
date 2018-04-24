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
        public long Calculate(List<Item> items, long capacity)
        {
            long c0 = capacity % items[0].weight;
            long c1 = c0 % items[1].weight;
            long z = capacity / items[0].weight * items[0].cost + c0 / items[1].weight * items[1].cost;
            long U0 = z + c1 / items[2].weight * items[2].cost;
            long U1 = z + (c1 + ((items[1].weight - c1) / items[0].weight) * items[0].weight)
                * items[1].cost / items[1].weight - ((items[1].weight - c1) / items[0].weight) * items[1].cost;

            return Math.Max(U0, U1);
        }
    }

    public class GreedyLowerBound : IBound
    {
        public long Calculate(Node n, List<Item> items, long capacity)
        {
            if (n.weight > capacity) return 0;

            var totalWeight = n.weight;
            var costBound = n.cost;
            int i = 0;
            for (i = n.level + 1; i < items.Count && (totalWeight + items[i].weight) <= capacity; ++i)
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