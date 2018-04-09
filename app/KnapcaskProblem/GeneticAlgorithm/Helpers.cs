using System.Collections.Generic;
using System.Linq;
using KnapsackProblemData;

namespace Algorithm
{ 
    public class Helpers
    {
        public static int GetCost(Individ individ, AData data)
        {
            int summaryCost = 0;
            for (int i = 0; i < individ.SIZE; i++)
                if (individ.GENOTYPE[i] == 1) summaryCost += data.COST[i];
            {
            }
            return summaryCost;
        }

        public static int GetWeight(Individ individ, AData data)
        {
            int summaryWeight = 0;
            for (int i = 0; i < individ.SIZE; i++)
            {
                if (individ.GENOTYPE[i] == 1) summaryWeight += data.WEIGHT[i];
            }
            return summaryWeight;
        }

        public static int GetMaxCost(List<Individ> individs, AData data)
        {
            int maxCost = 0;

            for (int i = 0; i < individs.Count(); i++)
            {
                int cost = GetCost(individs[i], data);
                if (cost > maxCost) maxCost = cost;
            }
            return maxCost;
        }
    }
}
