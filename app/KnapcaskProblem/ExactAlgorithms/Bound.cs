using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public interface IBound
    {
        long Calculate(Node n, List<Item> items, long capacity);
    }

    public class GreedyUpperBound : IBound
    {
        public long Calculate(Node n, List<Item> items, long capacity)
        {
            if (n.weight > capacity) return 0;

            var totalWeight = n.weight;
            var costBound = n.cost;
            int i = 0;
            for (i = n.level + 1; i < items.Count && (totalWeight + items[i].weight) <= capacity; ++i)
            {
                totalWeight += items[i].weight;
                costBound += items[i].cost;
            }

            if (i < items.Count)
            {
                costBound += (capacity - totalWeight) * items[i].cost / items[i].weight;
            }
            return costBound;
        }
    }
}