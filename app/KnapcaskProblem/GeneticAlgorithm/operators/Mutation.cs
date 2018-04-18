using System;
using System.Collections.Generic;
using CustomLogger;

namespace Algorithm
{
    public interface IMutation: IOperator
    {
        List<Individ> Run(List<Individ> individs);
    }

    public class PointMutation: IMutation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];
                var k = m_random.Next(individ.FlatSize());
                if (m_random.Next(100) <= 50) individ.SetBit(k, !individs[i].GetBit(k));
                if (individ.GetWeight() > 0) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
    public class Inversion : IMutation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];

                var k = m_random.Next(individ.FlatSize() - 1);
                var r = m_random.Next(k + 1, individ.FlatSize() - 1);

                if (m_random.Next(100) <= 50)
                {
                    for (var j = k; j <= r; ++j)
                    {
                        individ.SetBit(j, !individs[i].GetBit(j));
                    }
                }
                if (individ.GetWeight() > 0) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
    public class Translocation : IMutation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];
                var k = m_random.Next(individ.FlatSize() - 1);
                var r = m_random.Next(k + 1, individ.FlatSize());

                if (m_random.Next(100) <= 50)
                {
                    for (var j = 0; j <= k; ++j)
                    {
                        individ.SetBit(j, !individs[i].GetBit(j));
                    }
                    for (var j = r; j < individs[i].FlatSize(); ++j)
                    {
                        individ.SetBit(j, !individs[i].GetBit(j));
                    }
                }
                if (individ.GetWeight() > 0) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }

    public class Saltation : IMutation
    {
        private Random m_random = new Random(DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];

                var k = m_random.Next(individs[i].FlatSize() / 2);
                var r = m_random.Next(k + 1, individs[i].FlatSize());

                if (m_random.Next(100) <= 50)
                {
                    individ.SetBit(r, individs[i].GetBit(k));
                    individ.SetBit(k, individs[i].GetBit(r));
                }
                if (individ.GetWeight() > 0) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
}
