using System;
using System.Collections.Generic;
using KnapsackProblem;
using System.Linq;

namespace Algorithm
{
    public interface IInitialPopulation : IOperator
    {
        Individ Run(IData data);
    }

    public class DantzigAlgorithm : IInitialPopulation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public Individ Run(IData data)
        {
            var size = data.Cost.Length;
            var individ = new Individ(data);
            var specificCosts = new Dictionary<int, double>();

            for (int i = 0; i < size; ++i)
            {
                specificCosts.Add(i, (double)data.Cost[i] / data.Weight[i]);
            }
            specificCosts = specificCosts.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            long weight = 0;
            foreach (var pair in specificCosts)
            {
                var gen = individ.GetGen(pair.Key);
                for(var i = 0; i < gen.Size(); ++i)
                {
                    gen.SetBit(i, m_random.Next(2) == 1);
                    if (gen.GetBit(i) && weight + gen.GetWeight() > data.Capacity)
                    {
                        gen.SetBit(i, false);
                    }
                    else
                    {
                        weight += gen.GetWeight();
                    }
                }
            }
            return individ;
        }
    }
    public class RandomPopulation : IInitialPopulation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public Individ Run(IData data)
        {
            var individ = new Individ(data);
            var size = individ.FlatSize();

            for (var i = 0; i < size; ++i)
            {
                individ.SetBit(i, m_random.Next(2) == 1);
                if (individ.GetBit(i) && individ.GetWeight() > data.Capacity)
                {
                    individ.SetBit(i, false);
                }
            }
            return individ;
        }
    }
}
