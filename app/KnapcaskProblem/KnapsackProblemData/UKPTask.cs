using System;
using System.Collections.Generic;

namespace KnapsackProblem
{
    public class UKPTask: ITask
    {
        private IData m_data;
        public IData Create(IData data)
        {
            m_data = data;
            m_data.Fill();

            var itemMaxCounts = new int [data.Cost.Length];
            for (var i = 0; i < m_data.Weight.Length; ++i)
            {
                itemMaxCounts[i] = (int)(m_data.Capacity / m_data.Weight[i]);
            }
            return (IData)Activator.CreateInstance(m_data.GetType(),
                m_data.Range, m_data.Cost, m_data.Weight, m_data.Capacity, itemMaxCounts);
        }
        public string Str()
        {
            return "UKP";
        }
    }
}
