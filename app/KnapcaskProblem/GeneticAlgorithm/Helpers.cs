﻿using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;

namespace Algorithm
{ 
    public class Helpers
    {
        public static int GetCost(Individ individ, IData data)
        {
            int summaryCost = 0;
            for (int i = 0; i < individ.SIZE; ++i)
                if (individ.GENOTYPE[i] == 1) summaryCost += data.COST[i];
            {
            }
            return summaryCost;
        }

        public static int GetWeight(Individ individ, IData data)
        {
            int summaryWeight = 0;
            for (int i = 0; i < individ.SIZE; ++i)
            {
                if (individ.GENOTYPE[i] == 1) summaryWeight += data.WEIGHT[i];
            }
            return summaryWeight;
        }

        public static int GetMaxCost(List<Individ> individs, IData data)
        {
            int maxCost = GetCost(individs.First(), data);

            for (int i = 0; i < individs.Count(); ++i)
            {
                int cost = GetCost(individs[i], data);
                if (cost > maxCost) maxCost = cost;
            }
            return maxCost;
        }

        public static int GetMaxCost(ref Individ individ, List<Individ> individs, IData data)
        {
            individ = individs.First();
            int maxCost = GetCost(individ, data);

            for (int i = 0; i < individs.Count(); ++i)
            {
                int cost = GetCost(individs[i], data);
                if (cost > maxCost)
                {
                    maxCost = cost;
                    individ = individs[i];
                }
            }
            return maxCost;

        }
        public static int GetMinCost(List<Individ> individs, IData data)
        {
            int minCost = GetCost(individs.First(), data);

            for (int i = 0; i < individs.Count(); ++i)
            {
                int cost = GetCost(individs[i], data);
                if (cost < minCost) minCost = cost;
            }
            return minCost;
        }
    }
}
