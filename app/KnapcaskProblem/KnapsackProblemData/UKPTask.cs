using System;
using System.Collections.Generic;

namespace KnapsackProblem
{
    public class UKPTask: ITask
    {
        private List<int> m_indices;
        private IData m_data;
        public IData Create(IData data)
        {
            m_data = data;
            m_data.Fill();

            var totalCount = 0;

            m_indices = new List<int> { 0 };
  
            Array.ForEach(m_data.WEIGHT, w =>
            { 
                totalCount += m_data.MAX_WEIGHT / w;
                m_indices.Add(totalCount);
            });

            var cost = new int [totalCount];
            var weight = new int [totalCount];

            for (var i = 0; i < m_indices.Count - 1; ++i)
            {
                for (var j = m_indices[i]; j < m_indices[i + 1]; ++j)
                {
                    cost[j] = m_data.COST[i];
                    weight[j] = m_data.WEIGHT[i];
                }
            }
            return (IData)Activator.CreateInstance(m_data.GetType(), m_data.RANGE, cost, weight, m_data.MAX_WEIGHT);
        }
        public string Str()
        {
            return "UKP";
        }
        public int[] GetX()
        {
            var x = new int[m_data.COST.Length];
            for (var i = 0; i < x.Length; ++i)
            {
                x[i] = m_indices[i + 1] - m_indices[i];
            }
            return x;
        }
    }
}
