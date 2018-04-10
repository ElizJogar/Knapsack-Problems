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
            List<Individ> population = new List<Individ>();
            int k = 0;
            for (int i = 0; i < individs.Count - 1; i++)
            {
                for (int j = i + 1; j < individs.Count; j++)
                {
                    k = m_random.Next(individs[i].SIZE - 1);
                    Individ descendant1 = new Individ(individs[i].SIZE);
                    Individ descendant2 = new Individ(individs[i].SIZE);
                    for (int s = 0; s < k; s++)
                    {
                        descendant1.GENOTYPE[s] = individs[j].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[i].GENOTYPE[s];
                    }
                    for (int s = k; s < individs[i].SIZE; s++)
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
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;

            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = i + 1; j < individs.Count; j++)
                {
                    k = m_random.Next(individs[i].SIZE / 2);
                    r = m_random.Next(k + 1, individs[i].SIZE - 1);
                    Individ descendant1 = new Individ(individs[i].SIZE);
                    Individ descendant2 = new Individ(individs[i].SIZE);
                    for (int s = 0; s < k; s++)
                    {
                        descendant1.GENOTYPE[s] = individs[j].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[i].GENOTYPE[s];
                    }
                    for (int s = k; s < r; s++)
                    {
                        descendant1.GENOTYPE[s] = individs[i].GENOTYPE[s];
                        descendant2.GENOTYPE[s] = individs[j].GENOTYPE[s];
                    }
                    for (int s = r; s < individs[i].SIZE; s++)
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
            List<Individ> population = new List<Individ>();
            int k = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (i != j)
                    {
                        Individ individ = new Individ(individs[i].SIZE);
                        for (int s = 0; s < individs[i].SIZE; s++)
                        {
                            k = m_random.Next(10);
                            if (k > 5)
                                individ.GENOTYPE[s] = individs[j].GENOTYPE[s];
                            else
                                individ.GENOTYPE[s] = individs[i].GENOTYPE[s];
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
