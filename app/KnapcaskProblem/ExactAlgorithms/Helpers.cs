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
        public int id;

        public Item(long cost, long weight, int maxCount = 1, int id = -1)
        {
            this.cost = cost;
            this.weight = weight;
            this.maxCount = maxCount;
            this.id = id;
        }
    }
    public class Helpers
    {
        public delegate int Callback(Item a, Item b);
        public static List<Item> GetItems(IData data, Callback sort = null)
        {
            var items = new List<Item>();
            for (int i = 0; i < data.Cost.Length; ++i)
            {
                items.Add(new Item(data.Cost[i], data.Weight[i], 
                    data.Cost.Length == data.ItemMaxCounts.Length ? data.ItemMaxCounts[i] : 1));
            }
            sort = sort ?? ((a, b) =>
            {
                double first = (double)a.cost / a.weight;
                double second = (double)b.cost / b.weight;
                return second.CompareTo(first);
            });

            items.Sort((a, b) =>
            {
                return sort(a, b);
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
