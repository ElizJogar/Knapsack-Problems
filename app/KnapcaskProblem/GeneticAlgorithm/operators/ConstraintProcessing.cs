using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface IConstraintProcessing : IOperator
    {
        List<CustomIndivid> Run(List<Individ> individs, IData data);
    }

    public class PenaltyFunction : IConstraintProcessing
    {
        public List<CustomIndivid> Run(List<Individ> individs, IData data)
        {
            Logger.Get().Debug("Called Algorithm.PenaltyFunction");
            var customIndivids = new List<CustomIndivid>();

            long averageCost = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                averageCost += individs[i].GetCost();
            }
            averageCost /= individs.Count;
            double coeffA = 0;
            double coeffB = 0;
            long weight = 0;
            long cost = 0;
            long minCost = 0;

            for (var i = 0; i < individs.Count; ++i)
            {
                var customIndivid = new CustomIndivid(individs[i]);

                cost = individs[i].GetCost();
                weight = individs[i].GetWeight();

                customIndivid.SetWeight(weight);
                cost = weight <= data.Capacity ? cost : cost - (long)Math.Pow(weight - data.Capacity, 2);
                customIndivid.SetCost(cost);
                customIndivids.Add(customIndivid);
            }

            minCost = customIndivids.Min().GetCost();

            for (var i = 0; i < individs.Count; ++i)
            {
                if (customIndivids[i].GetCost() == minCost)
                {
                    customIndivids[i].SetCost(0);
                }
                else if (customIndivids[i].GetCost() <= 0)
                {
                    cost = customIndivids[i].GetCost();
                    coeffA = averageCost / (0 - averageCost + cost);
                    coeffB = averageCost * cost / (0 - averageCost + cost);
                    customIndivids[i].SetCost(Convert.ToInt64(coeffA * cost + coeffB));
                }
            }
            return customIndivids;
        }
    }
}
