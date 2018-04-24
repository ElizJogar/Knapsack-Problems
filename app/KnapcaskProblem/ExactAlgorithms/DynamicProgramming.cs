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
            long[,] Z = new long[itemsCount + 1, capacity + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= capacity; ++w)
                {
                    if (i == 0 || w == 0)
                    {
                        Z[i, w] = 0;
                    }
                    else if (items[i - 1].weight <= w)
                    {
                        Z[i, w] = Math.Max(items[i - 1].cost + Z[i - 1, w - items[i - 1].weight], Z[i - 1, w]);
                    }
                    else
                    {
                        Z[i, w] = Z[i - 1, w];
                    }
                }
            }

            return Z[itemsCount, capacity];
        }
    }
    public class RecurrentApproach : IDPApproach
    {
        private long[,] m_z;
        private List<Item> m_items;
        public long Run(List<Item> items, long capacity)
        {
            m_items = items;

            m_z = new long[items.Count + 1, capacity + 1];
            for (var i = 0; i <= items.Count; ++i)
            {
                for (var j = 0; j <= capacity; ++j)
                {
                    m_z[i, j] = -1;
                }
            }
            return CalculateZ(items.Count, capacity);
        }
        private long CalculateZ(int index, long weight)
        {
            if (m_z[index, weight] != -1) return m_z[index, weight];
            if (index == 0 || weight == 0)
            {
                m_z[index, weight] = 0;
            }
            else if (weight >= m_items[index - 1].weight)
            {
                m_z[index, weight] = Math.Max(CalculateZ(index - 1, weight), CalculateZ(index - 1, weight - m_items[index - 1].weight) + m_items[index - 1].cost);
            }
            else
            {
                m_z[index, weight] = CalculateZ(index - 1, weight);
            }
            return m_z[index, weight];
        }
    }
    public class DynamicProgramming : IExactAlgorithm
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