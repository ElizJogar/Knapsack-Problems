﻿using System;
using System.Collections.Generic;
using KnapsackProblem;
using System.Linq;
using CustomLogger;

namespace Algorithm
{
    public interface IInitialPopulation: IOperator
    {
        Individ Run(IData data);
    }

    public class DanzigAlgorithm: IInitialPopulation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public Individ Run(IData data)
        {
            var size = data.COST.Length;
            var individ = new Individ(size);
            var specificCosts = new Dictionary<int, double>();

            for (int i = 0; i < size; ++i)
            {
                specificCosts.Add(i, (double)data.COST[i] / data.WEIGHT[i]);
            }
            specificCosts = specificCosts.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            var summaryWeight = 0;
            foreach (var pair in specificCosts)
            {
                summaryWeight += data.WEIGHT[pair.Key];
                individ.GENOTYPE[pair.Key] = summaryWeight <= data.MAX_WEIGHT ? m_random.Next(2) : 0;
                if (individ.GENOTYPE[pair.Key] == 0) summaryWeight -= data.WEIGHT[pair.Key];
            }
            return individ;
        }
    }
    public class RandomPopulation: IInitialPopulation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public Individ Run(IData data)
        {
            var size = data.COST.Length;
            var individ = new Individ(size);
            var summaryWeight = 0;
            for (var i = 0; i < size; ++i)
            {
                summaryWeight += data.WEIGHT[i];
                individ.GENOTYPE[i] = summaryWeight <= data.MAX_WEIGHT ? m_random.Next(2) : 0;
                if (individ.GENOTYPE[i] == 0) summaryWeight -= data.WEIGHT[i];
            }
            return individ;
        }
    }
}