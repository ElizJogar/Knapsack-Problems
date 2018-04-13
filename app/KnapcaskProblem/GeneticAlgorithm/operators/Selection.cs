using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblemData;
using CustomLogger;

namespace Algorithm
{
    public interface ISelection : IOperator
    {
        List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args);
    }

    public abstract class ASelection : ISelection
    {
        protected Random m_random = new Random(System.DateTime.Now.Millisecond);
        protected List<Individ> ModifyGeneration(List<Individ> individs, AData data)
        {
            Logger.Get().Debug("Called generation modification");
            var permissibleIndivids = new List<Individ>();

            individs.RemoveAll(individ =>
            {
                if (Helpers.GetWeight(individ, data) > data.MAX_WEIGHT)
                {
                    permissibleIndivids.Add(individ);
                    return true;
                }
                return false;
            });
 
            foreach (var individ in permissibleIndivids)
            {
                var dictionary = new Dictionary<int, double>();
                for (int i = 0; i < individ.SIZE; ++i)
                {
                    if (individ.GENOTYPE[i] == 1)
                    {
                        dictionary.Add(i, (double)data.COST[i] / data.WEIGHT[i]);
                    }
                }
                dictionary = dictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                var weight = Helpers.GetWeight(individ, data);
                while (weight > data.MAX_WEIGHT)
                {
                    foreach (var item in dictionary)
                    {
                        if (weight <= data.MAX_WEIGHT) break;

                        individ.GENOTYPE[item.Key] = 0;
                        weight -= data.WEIGHT[item.Key];
                    }
                }
            }
            individs.AddRange(permissibleIndivids);
            string text = Environment.NewLine + "Generation:";
            for (int i = 0; i < individs.Count; i++)
            {
                text += individs[i].Str() + " COST: " + Helpers.GetCost(individs[i], data) + " WEIGHT: " + Helpers.GetWeight(individs[i], data) + Environment.NewLine;
            }
            Logger.Get().Debug(text);
            return individs;
        }
        public abstract List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args);
    }

    public class BettaTournament : ASelection
    {
        public override List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var beta = (int)args[0];
            var individsEx = PenaltyFunction.Run(individs, data);
            List<Individ> population = new List<Individ>();
            for (int j = 0; j < populationCount; j++)
            {
                var individsEx4Beta = new List<IndividEx>();
                var index = new List<int>();
                while (individsEx4Beta.Count != beta)
                {
                    for (int i = 0; i < individsEx.Count; ++i)
                    {
                        if (individsEx4Beta.Count >= beta) break;

                        int randomNumber = m_random.Next(100);
                        if (randomNumber < 50 && !index.Contains(i))
                        {  
                            individsEx4Beta.Add(individsEx[i]);
                            index.Add(i);
                        }
                    }
                }
                population.Add(individsEx4Beta.Max());
            }
            return ModifyGeneration(population, data);
        }
    }

    public class LinearRankSelection : ASelection
    {
        public override List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args)
        {
            var size = individs.Count;

            Logger.Get().Debug("Called " + Convert.ToString(this));

            var individsEx = PenaltyFunction.Run(individs, data);
            List<Individ> generation = new List<Individ>();

            int[] rang = new int[size];
            double[] nCopy = new double[size];
            double a = m_random.NextDouble() + 1.1;
            nCopy[size - 1] = a > 2 ? 2 : a;
            nCopy[0] = 2 - nCopy[size - 1];

            individsEx.Sort();

            for (int i = 0; i < size; ++i)
            {
                if (generation.Count >= populationCount) break;

                rang[i] = i + 1;
                nCopy[i] = nCopy[0] + (nCopy[size - 1] - nCopy[0]) * (rang[i] - 1) / (size - 1);
                if (nCopy[i] >= 1 && !generation.Contains(individsEx[i]))
                {
                    var copies = 0;
                    while (copies++ < (int)nCopy[i] && generation.Count < populationCount)
                    {
                        generation.Add(individsEx[i]);
                    }
                }
            }

            while (generation.Count < populationCount)
            {
                var index = m_random.Next(0, size);
                generation.Add(individsEx[index]);
            }
            return ModifyGeneration(generation, data);
        }
    }

    class PenaltyFunction
    {
        static public List<IndividEx> Run(List<Individ> individs, AData data)
        {
            Logger.Get().Debug("Called Algorithm.PenaltyFunction");
            var individsEx = new List<IndividEx>();

            int averageCost = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                averageCost += Helpers.GetCost(individs[i], data);
            }
            averageCost /= individs.Count;
            double coeffA = 0;
            double coeffB = 0;
            int weight = 0;
            int cost = 0;
            int minCost = 0;

            for (int i = 0; i < individs.Count; i++)
            {
                var individEx = new IndividEx(individs[i].SIZE);
                individEx.GENOTYPE = individs[i].GENOTYPE;

                cost = Helpers.GetCost(individs[i], data);
                weight = Helpers.GetWeight(individs[i], data);
                individEx.WEIGHT = weight;
                individEx.COST = weight <= data.MAX_WEIGHT ? cost : cost - (int)Math.Pow(weight - data.MAX_WEIGHT, 2);
                individsEx.Add(individEx);
            }

            for (int i = 0; i < individs.Count; i++)
            {
                if (individsEx[i].COST == minCost)
                {
                    individsEx[i].COST = 0;
                }
                else if (individsEx[i].COST <= 0)
                {
                    cost = individsEx[i].COST;
                    coeffA = averageCost / (cost - averageCost);
                    coeffB = averageCost * cost / (cost - averageCost);
                    individsEx[i].COST = Convert.ToInt32(coeffA * cost + coeffB);
                }
            }
            return individsEx;
        }
    }
}