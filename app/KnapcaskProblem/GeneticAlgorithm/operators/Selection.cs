using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblemData;
using CustomLogger;

namespace Algorithm
{
    public interface ISelection: IOperator
    {
        List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args);
    }

    public abstract class ASelection : ISelection
    {
        protected Random m_random = new Random(System.DateTime.Now.Millisecond);
        protected List<Individ> GenerationModificaton(List<Individ> individs, AData data)
        {
            Logger.Get().Debug("Called generation modification");
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            List<Individ> permissibleIndivids = new List<Individ>();

            for (int i = 0; i < individs.Count; i++)
                if (Helpers.GetWeight(individs[i], data) > data.MAX_WEIGHT)
                {
                    permissibleIndivids.Add(individs[i]);
                    individs.RemoveAt(i);
                    i--;
                }
            foreach (Individ individ in permissibleIndivids)
            {
                dictionary.Clear();
                for (int i = 0; i < individ.SIZE; i++)
                    if (individ.GENOTYPE[i] == 1)
                        dictionary.Add(i, (double)data.COST[i] / data.WEIGHT[i]);

                dictionary = dictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                while (Helpers.GetWeight(individ, data) > data.MAX_WEIGHT)
                {
                    foreach (var item in dictionary)
                    {
                        if (Helpers.GetWeight(individ, data) > data.MAX_WEIGHT)
                        {
                                individ.GENOTYPE[item.Key] = 0;
                        }
                        else break;
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
            int beta = (int)args[0];

            int[] costs = PenaltyFunction.Run(individs, data);
            Logger.Get().Debug("Called " + Convert.ToString(this));
            List<Individ> population = new List<Individ>();
            for (int j = 0; j < populationCount; j++)
            {
                int c = 0;
                int count = 0;
                List<int> costList = new List<int>();
                List<int> number = new List<int>();
                for (int i = 0; i < individs.Count; i++)
                {
                    c = m_random.Next(2);
                    if (c == 1 && count < beta && !number.Contains(i))
                    {
                        costList.Add(costs[i]);
                        number.Add(i);
                        count++;
                    }
                    if (i == individs.Count - 1 && count < beta)
                        i = -1;
                }
                int maxCost = 0;
                Individ individ = null;
                for (int i = 0; i < beta; i++)
                {
                    if (costList[i] > maxCost)
                    {
                        maxCost = costList[i];
                        individ = individs[number[i]];
                    }
                }
                population.Add(individ);
            }
            population = GenerationModificaton(population, data);
            return population;
        }
    }

    public class LinearRankSelection : ASelection
    {
        public override List<Individ> Run(List<Individ> individs, int populationCount, AData data, params object[] args)
        {
            int[] costs = PenaltyFunction.Run(individs, data);
            Logger.Get().Debug("Called " + Convert.ToString(this));
            List<Individ> sortedPopulation = new List<Individ>();
            List<Individ> generation = new List<Individ>();
            List<int> costList = new List<int>();
            int[] rang = new int[individs.Count];
            double[] n = new double[individs.Count]; // expected numbers
            n[individs.Count - 1] = m_random.NextDouble() + 1.1;
            n[individs.Count - 1] = (n[individs.Count - 1] > 2) ? 2 : n[individs.Count - 1];
            n[0] = 2 - n[individs.Count - 1];
            for (int i = 0; i < individs.Count; i++)
                costList.Add(costs[i]);
            costList.Sort();
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (costs[j] != -1)
                    {
                        if (costs[j] == costList[i])
                        {
                            costs[j] = -1;
                            sortedPopulation.Add(individs[j]);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < individs.Count; i++)
            {
                rang[i] = i + 1;
                n[i] = n[0] + (n[individs.Count - 1] - n[0]) * (rang[i] - 1) / (individs.Count - 1);
                if (n[i] >= 1)
                    generation.Add(sortedPopulation[i]);
            }

            if (generation.Count < populationCount)
            {
                for (int j = 0; j < individs.Count; j++)
                    if (n[j] > 1 && m_random.Next((int)(n[j] - 1) * 100) == 0 || n[j] < 1 && m_random.Next((int)(n[j] * 100)) == 0)
                        generation.Add(sortedPopulation[j]);

            }

            if (generation.Count > populationCount)
            {
                sortedPopulation.Clear();
                int k = 0;
                for (int i = 0; i < populationCount; i++)
                {
                    k = m_random.Next(generation.Count);
                    sortedPopulation.Add(generation[k]);
                }
            }

            individs.Clear();
            individs = (generation.Count > populationCount) ? sortedPopulation : generation;
            individs = GenerationModificaton(individs, data);
            return individs;
        }
    }

    class PenaltyFunction
    {
        static public int[] Run(List<Individ> individs, AData data)
        {
            Logger.Get().Debug("Called Algorithm.PenaltyFunction");
            int[] scalledFitnessFunctions = new int[individs.Count];
            int averageCost = 0;
            for (int i = 0; i < individs.Count; i++)
                averageCost += Helpers.GetCost(individs[i], data);
            averageCost /= individs.Count;
            double coeffA = 0;
            double coeffB = 0;
            int weight = 0;
            int cost = 0;
            int minCost = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                cost = Helpers.GetCost(individs[i], data);
                weight = Helpers.GetWeight(individs[i], data);
                scalledFitnessFunctions[i] = (weight <= data.MAX_WEIGHT) ? cost : cost - (int)Math.Pow(weight - data.MAX_WEIGHT, 2);
                if (scalledFitnessFunctions[i] < minCost)
                    minCost = scalledFitnessFunctions[i];
            }
            for (int i = 0; i < individs.Count; i++)
            {
                if (scalledFitnessFunctions[i] == minCost)
                    scalledFitnessFunctions[i] = 0;
                else if (scalledFitnessFunctions[i] <= 0)
                {
                    cost = scalledFitnessFunctions[i];
                    coeffA = averageCost / (cost - averageCost);
                    coeffB = averageCost * cost / (cost - averageCost);
                    scalledFitnessFunctions[i] = Convert.ToInt32(coeffA * scalledFitnessFunctions[i] + coeffB);
                }
            }
            return scalledFitnessFunctions;
        }
    }
}
