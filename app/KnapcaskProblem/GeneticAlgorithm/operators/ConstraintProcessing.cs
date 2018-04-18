using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface IConstraintProcessing : IOperator
    {
        List<IndividEx> Run(List<Individ> individs, IData data);
    }

    public class PenaltyFunction : IConstraintProcessing
    {
        public List<IndividEx> Run(List<Individ> individs, IData data)
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

            for (var i = 0; i < individs.Count; ++i)
            {
                var individEx = new IndividEx(individs[i].SIZE)
                {
                    GENOTYPE = individs[i].GENOTYPE
                };

                cost = Helpers.GetCost(individs[i], data);
                weight = Helpers.GetWeight(individs[i], data);
                individEx.WEIGHT = weight;
                individEx.COST = weight <= data.CAPACITY ? cost : cost - (int)Math.Pow(weight - data.CAPACITY, 2);
                individsEx.Add(individEx);
            }

            for (var i = 0; i < individs.Count; ++i)
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
