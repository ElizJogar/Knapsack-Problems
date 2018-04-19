using System;
using System.Collections.Generic;

namespace KnapsackProblem
{
    public class UKPTask : ITask
    {
        public IData Create(IData data)
        {
            data.Fill();

            var itemMaxCounts = new int[data.Cost.Length];
            for (var i = 0; i < data.Weight.Length; ++i)
            {
                itemMaxCounts[i] = (int)(data.Capacity / data.Weight[i]);
            }
            return (IData)Activator.CreateInstance(data.GetType(),
                data.Range, data.Cost, data.Weight, data.Capacity, itemMaxCounts);
        }
        public string Str()
        {
            return "UKP";
        }
    }
}
