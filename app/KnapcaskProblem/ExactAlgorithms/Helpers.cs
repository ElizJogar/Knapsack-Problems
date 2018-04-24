using System;
using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public struct Item
    {
        public long cost;
        public long weight;
        public int maxCount;
        public Item(long cost, long weight, int maxCount = 1)
        {
            this.cost = cost;
            this.weight = weight;
            this.maxCount = maxCount;
        }
    }
    class Helpers
    {
        public static List<Item> GetItems(IData data)
        {
            var items = new List<Item>();
            for (int i = 0; i < data.Cost.Length; ++i)
            {
                items.Add(new Item(data.Cost[i], data.Weight[i], data.Cost.Length == data.ItemMaxCounts.Length ? data.ItemMaxCounts[i] : 1));
            }
            items.Sort((a, b) =>
            {
                double first = (double)a.cost / a.weight;
                double second = (double)b.cost / b.weight;
                return second.CompareTo(first);
            });
            return items;
        }
        public static IData ExtendData(IData data)
        {
            var itemsCount = 0;
            var indices = new List<int> { 0 };
            foreach (var count in data.ItemMaxCounts)
            {
                itemsCount += count;
                indices.Add(itemsCount);
            }
            var cost = new long[itemsCount];
            var weight = new long[itemsCount];

            for (var i = 0; i < indices.Count - 1; ++i)
            {
                for (var j = indices[i]; j < indices[i + 1]; ++j)
                {
                    cost[j] = data.Cost[i];
                    weight[j] = data.Weight[i];
                }
            }

            return (IData)Activator.CreateInstance(data.GetType(),
               data.Range, cost, weight, data.Capacity, data.ItemMaxCounts);
        }
    }
}
