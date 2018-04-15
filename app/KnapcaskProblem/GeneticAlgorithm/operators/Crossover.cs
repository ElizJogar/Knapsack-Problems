using System;
using System.Collections.Generic;
using CustomLogger;

namespace Algorithm
{
    public interface ICrossover: IOperator
    {
        List<Individ> Run(List<Individ> individs);
    }

    public class SinglePointCrossover : ICrossover
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            for (var i = 0; i < individs.Count - 1; ++i)
            {
                for (var j = i + 1; j < individs.Count; ++j)
                {
                    var k = m_random.Next(individs[i].SIZE - 1);
                    var descendant1 = new Individ(individs[i].SIZE);
                    var descendant2 = new Individ(individs[i].SIZE);
                    for (var s = 0; s < k; ++s)
                    {
                        descendant1.GENOTYPE[s] = individs[j].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[i].GENOTYPE[s];
                    }
                    for (var s = k; s < individs[i].SIZE; ++s)
                    {
                        descendant1.GENOTYPE[s] = individs[i].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[j].GENOTYPE[s];
                    }
                    population.Add(descendant1);
                    population.Add(descendant2);
                }
            }
            population.AddRange(individs);
            return population;
        }
    }

    public class TwoPointCrossover: ICrossover
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();

            for (var i = 0; i < individs.Count; ++i)
            {
                for (var j = i + 1; j < individs.Count; ++j)
                {
                    var k = m_random.Next(individs[i].SIZE / 2);
                    var r = m_random.Next(k + 1, individs[i].SIZE - 1);
                    var descendant1 = new Individ(individs[i].SIZE);
                    var descendant2 = new Individ(individs[i].SIZE);
                    for (var s = 0; s < k; ++s)
                    {
                        descendant1.GENOTYPE[s] = individs[j].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[i].GENOTYPE[s];
                    }
                    for (var s = k; s < r; ++s)
                    {
                        descendant1.GENOTYPE[s] = individs[i].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[j].GENOTYPE[s];
                    }
                    for (var s = r; s < individs[i].SIZE; ++s)
                    {
                        descendant1.GENOTYPE[s] = individs[j].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[i].GENOTYPE[s];
                    }
                    population.Add(descendant1);
                    population.Add(descendant2);
                }
            }
            population.AddRange(individs);
            return population;
        }
    }

    public class UniformCrossover: ICrossover
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public List<Individ> Run(List<Individ> individs)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var population = new List<Individ>();
            for (var i = 0; i < individs.Count; ++i)
            {
                for (var j = 0; j < individs.Count; ++j)
                {
                    if (i != j)
                    {
                        var individ = new Individ(individs[i].SIZE);
                        for (var s = 0; s < individs[i].SIZE; ++s)
                        {
                            individ.GENOTYPE[s] = m_random.Next(2) == 1 ? individs[j].GENOTYPE[s] : individs[i].GENOTYPE[s];
                        }
                        population.Add(individ);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }
    }    
}
