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
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];
                var k = m_random.Next(individ.SIZE);
                if (m_random.Next(100) <= 50) individ.GENOTYPE[k] = individs[i].GENOTYPE[k] == 0 ? 1 : 0;
                var zeroCount = 0;
                Array.ForEach(individ.GENOTYPE, gen =>
                {
                    if (gen == 0) ++zeroCount;
                });
                if (zeroCount != individ.SIZE) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
    public class Inversion : IMutation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];

                var k = m_random.Next(individ.SIZE - 2);
                var r = m_random.Next(k + 1, individ.SIZE - 1);

                if (m_random.Next(100) <= 50)
                {
                    for (var j = k; j <= r; ++j)
                    {
                        individ.GENOTYPE[j] = individs[i].GENOTYPE[j] == 0 ? 1 : 0;
                    }
                }
                var zeroCount = 0;
                Array.ForEach(individ.GENOTYPE, gen =>
                {
                    if (gen == 0) ++zeroCount;
                });
                if (zeroCount != individ.SIZE) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
    public class Translocation : IMutation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];
                var k = m_random.Next(individ.SIZE - 2);
                var r = m_random.Next(k + 1, individ.SIZE - 1);

                if (m_random.Next(100) <= 50)
                {
                    for (var j = 0; j <= k; ++j)
                    {
                        individ.GENOTYPE[j] = individs[i].GENOTYPE[j] == 0 ? 1 : 0;
                    }
                    for (var j = r; j < individs[i].SIZE; ++j)
                    {
                        individ.GENOTYPE[j] = individs[i].GENOTYPE[j] == 0 ? 1 : 0;
                    }
                }
                var zeroCount = 0;
                Array.ForEach(individ.GENOTYPE, gen =>
                {
                    if (gen == 0) ++zeroCount;
                });
                if (zeroCount != individ.SIZE) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }

    public class Saltation : IMutation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            var s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (var i = 0; i < s; ++i)
            {
                var individ = individs[i];

                var k = m_random.Next(individs[i].SIZE / 2);
                var r = m_random.Next(k + 1, individs[i].SIZE);

                if (m_random.Next(100) <= 50)
                {
                    individ.GENOTYPE[r] = individs[i].GENOTYPE[k];
                    individ.GENOTYPE[k] = individs[i].GENOTYPE[r];
                }
                var zeroCount = 0;
                Array.ForEach(individ.GENOTYPE, gen =>
                {
                    if (gen == 0) ++zeroCount;
                });
                if (zeroCount != individ.SIZE) population.Add(individ);
            }
            for (var i = s; i < individs.Count; ++i) population.Add(individs[i]);
            return population;
        }
    }
}
