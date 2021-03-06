﻿using System;
using System.Collections.Generic;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface IDPApproach
    {
        long Run(IData data);
    }
    public class DirectApproach : IDPApproach
    {
        public long Run(IData data)
        {
            var items = Helpers.GetItems(Helpers.ExtendData(data));
            var itemsCount = items.Count;
            long[,] Z = new long[itemsCount + 1, data.Capacity + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= data.Capacity; ++w)
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

            return Z[itemsCount, data.Capacity];
        }
    }
    public class RecurrentApproach : IDPApproach
    {
        private long[,] m_z;
        private List<Item> m_items;
        public long Run(IData data)
        {
            m_items = Helpers.GetItems(Helpers.ExtendData(data));
            m_z = new long[m_items.Count + 1, data.Capacity + 1];
            for (var i = 0; i <= m_items.Count; ++i)
            {
                for (var j = 0; j <= data.Capacity; ++j)
                {
                    m_z[i, j] = -1;
                }
            }
            return CalculateZ(m_items.Count, data.Capacity);
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

    public class ClassicalUKPApproach : IDPApproach
    {
        public long Run(IData data)
        {
            var items = Helpers.GetItems(data);
            long[] Z = new long[data.Capacity + 1];

            for (int c = 1; c <= data.Capacity; ++c)
            {
                Z[c] = Z[c - 1];
                for (int i = 0; i < items.Count; ++i)
                {
                    if (items[i].weight <= c)
                    {
                        Z[c] = Math.Max(items[i].cost + Z[c - items[i].weight], Z[c]);
                    }
                }
            }

            return Z[data.Capacity];
        }
    }
    public class EDUK_EX : IDPApproach
    {
        private int m_itemSlices = 1;
        private int m_capacitySlices = 1;
        public EDUK_EX(int itemSlices, int capacitySlices)
        {
            m_itemSlices = itemSlices;
            m_capacitySlices = capacitySlices;
        }
        public long Run(IData data)
        {
            var items = Helpers.GetItems(data, (a, b) =>
            {
                if (a.weight == b.weight)
                {
                    return b.cost.CompareTo(a.cost);
                }
                return a.weight.CompareTo(b.weight);
            });
            var capacity = data.Capacity;
            // cost table for current problem
            long[] Z = new long[capacity + 1];
            // undominated items
            var F = new List<int>();
            // last capacity in which item was the most efficient item in an optimal solution
            long[] L = new long[items.Count];
            int j = 0;
;            for (var i = 0; i < m_itemSlices; ++i)
            {
                int count = i < m_itemSlices - 1 ? (items.Count / m_itemSlices) + j : items.Count;
                for (; j < count; ++j)
                {
                    var c = j > 0 ? items[j - 1].weight + 1 : 1;
                    for (; c <= items[j].weight; ++c)
                    {
                        Z[c] = 0;
                        foreach (var f in F)
                        {
                            if (Z[c - items[f].weight] + items[f].cost >= Z[c])
                            {
                                L[f] = c;
                                Z[c] = Z[c - items[f].weight] + items[f].cost;
                            }
                        }
                    }
                    // j is not dominated by F
                    if (items[j].cost > 0 && Z[items[j].weight] <= items[j].cost)
                    {
                        Z[items[j].weight] = items[j].cost;

                        F.Add(j);

                        F.Sort((a, b) =>
                        {
                            double eff1 = (double)items[a].cost / items[a].weight;
                            double eff2 = (double)items[b].cost / items[b].weight;
                            if (eff1 == eff2)
                            {
                                return items[a].weight.CompareTo(items[b].weight);
                            }
                            return eff2.CompareTo(eff1);
                        });
                    }
                }
                CheckThresholdDominance(items[j - 1].weight, F, L, items);
            }
            long w = 1;
            for (var i = 0; i < m_capacitySlices; ++i)
            {
                if (F.Count == 1)
                {
                    Logger.Get().Info("Periodicity achieved. ");
                    // periodicity achivied
                    var index = F[0];
                    var l = L[index];
                    var z = Z[l];
                    while (l - 1 >= 0 && Z[l - 1] == z) --l;
                    var multiplier = (capacity - l) / items[index].weight;
                    Z[capacity] = Z[l] + multiplier * items[index].cost;
                    return Z[capacity];
                }
                long count = i < m_capacitySlices - 1 ? (capacity / m_capacitySlices) + w : capacity;
                for (; w <= count; ++w)
                {
                    foreach (var f in F)
                    {
                        if (w - items[f].weight >= 0 && Z[w - items[f].weight] + items[f].cost >= Z[w])
                        {
                            L[f] = w;
                            Z[w] = Z[w - items[f].weight] + items[f].cost;
                        }
                    }
                }
                CheckThresholdDominance(w - 1, F, L, items);
            }
            return Z[capacity];
        }

        private void CheckThresholdDominance(long capacity, List<int> F, long[] L, List<Item> items)
        {
            F.RemoveAll(f =>
           {
               return L[f] < capacity - items[f].weight;
           });
        }
    }

    public class DynamicProgramming : IExactAlgorithm
    {
        private IDPApproach m_approach = new RecurrentApproach();

        public DynamicProgramming()
        {
        }
        public DynamicProgramming(IDPApproach approach)
        {
            m_approach = approach;
        }

        public long Run(IData data)
        {
            return m_approach.Run(data);
        }
    }
}