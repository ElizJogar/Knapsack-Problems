using System;
using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public interface IDPApproach
    {
        long Run(List<Item> items, long capacity);
    }
    public class DirectApproach : IDPApproach
    {
        public long Run(List<Item> items, long capacity)
        {
            var itemsCount = items.Count;
            long[,] K = new long[itemsCount + 1, capacity + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= capacity; ++w)
                {
                    if (i == 0 || w == 0)
                    {
                        K[i, w] = 0;
                    }
                    else if (items[i - 1].weight <= w)
                    {
                        K[i, w] = Math.Max(items[i - 1].cost + K[i - 1, w - items[i - 1].weight], K[i - 1, w]);
                    }
                    else
                    {
                        K[i, w] = K[i - 1, w];
                    }
                }
            }

            return K[itemsCount, capacity];
        }
    }
    public class RecurrentApproach : IDPApproach
    {
        private long[,] m_z;
        private List<Item> m_items;
        public long Run(List<Item> items, long capacity)
        {
            m_items = items;

            m_z = new long[items.Count, capacity];
            for (var i = 0; i < items.Count; ++i)
            {
                for (var j = 0; j < capacity; ++j)
                {
                    m_z[i, j] = -1;
                }
            }
            return CalculateZ(items.Count - 1, capacity);
        }
        private long CalculateZ(int index, long weight)
        {
            if (m_z[index, weight - 1] != -1)
            {
                return m_z[index, weight - 1];
            }

            if (index == 0)
            {
                m_z[index, weight - 1] = weight >= m_items[0].weight ? m_items[0].cost : 0;
                return m_z[index, weight - 1];
            }

            var first = CalculateZ(index - 1, weight);
            var newWeight = weight - m_items[index].weight;
            if (newWeight < 0)
            {
                m_z[index, weight - 1] = first;
                return first;
            }

            var second = newWeight != 0 ? CalculateZ(index - 1, newWeight) + m_items[index].cost : m_items[index].cost;

            m_z[index, weight - 1] = Math.Max(first, second);
            return m_z[index, weight - 1];
        }
    }
    public class DynamicProgramming
    {
        private IDPApproach m_approach = new RecurrentApproach();
        private List<Item> m_items;
        long m_capacity;

        public DynamicProgramming(IData data)
        {
            m_items = Helpers.GetItems(data);
            m_capacity = data.Capacity;
        }
        public DynamicProgramming(IData data, IDPApproach approach)
        {
            m_approach = approach;
            m_items = Helpers.GetItems(data);
            m_capacity = data.Capacity;
        }

        public long Run()
        {
            return m_approach.Run(m_items, m_capacity);
        }
    }
}