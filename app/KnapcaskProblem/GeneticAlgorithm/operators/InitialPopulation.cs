using System;
using System.Collections.Generic;
using KnapsackProblemData;
using CustomLogger;

namespace Algorithm
{
    public interface IInitialPopulation: IOperator
    {
        Individ Run(AData data);
    }

    public class DanzigAlgorithm: IInitialPopulation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public Individ Run(AData data)
        {
            var size = data.COST.Length;
            var individ = new Individ(size);
            var specificCostList = new List<double>();
            double[] specificCost = new double[size];
            var summaryWeight = 0;
            for (int i = 0; i < size; ++i)
            {
                specificCost[i] = (double) data.COST[i] / data.WEIGHT[i];
                specificCostList.Add(specificCost[i]);
            }
            specificCostList.Sort();
            specificCostList.Reverse();

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (specificCostList[i] == specificCost[j])
                    {
                        summaryWeight +=  data.WEIGHT[j];
                        if (summaryWeight <= data.MAX_WEIGHT)
                        {
                            individ.GENOTYPE[j] = m_random.Next(2);
                            if (individ.GENOTYPE[j] == 0) summaryWeight -=  data.WEIGHT[j];
                        }
                        else
                        {
                            individ.GENOTYPE[j] = 0;
                        }
                        specificCost[j] = -1;
                        break;
                    }
                }
            }
            return individ;
        }
    }
    public class RandomPopulation: IInitialPopulation
    {
        private Random m_random = new Random(System.DateTime.Now.Millisecond);
        public Individ Run(AData data)
        {
            var size = data.COST.Length;
            Individ individ = new Individ(size);
            int summaryWeight = 0;
            for (int i = 0; i < size; i++)
            {
                summaryWeight +=  data.WEIGHT[i];
                if (summaryWeight <= data.MAX_WEIGHT)
                {
                    individ.GENOTYPE[i] = m_random.Next(2);
                    if (individ.GENOTYPE[i] == 0) summaryWeight -=  data.WEIGHT[i];
                }
                else
                    individ.GENOTYPE[i] = 0;
            }
            return individ;
        }
    }
}
