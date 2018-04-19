using System;
using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public class ExhaustiveSearchAlgorithm
    {
        private IData m_data;
        public ExhaustiveSearchAlgorithm (IData data)
        {
            m_data = data;
        }
        public long Run()
        {
            var itemsCount = 0;
            var indices = new List<int> { 0 };
            foreach (var count in m_data.ItemMaxCounts)
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
                    cost[j] = m_data.Cost[i];
                    weight[j] = m_data.Weight[i];
                }
            }
            long limit = m_data.Capacity;
            long[,] K = new long[itemsCount + 1, limit + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= limit; ++w)
                {
                    if (i == 0 || w == 0)
                        K[i, w] = 0;
                    else if (weight[i - 1] <= w)
                        K[i, w] = Math.Max(cost[i - 1] + K[i - 1, w - weight[i - 1]], K[i - 1, w]);
                    else
                        K[i, w] = K[i - 1, w];
                }
            }

            return K[itemsCount, m_data.Capacity];
        }
    }
}
