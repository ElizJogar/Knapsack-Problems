using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface IRepairOperator : IOperator
    {
        List<Individ> Run(List<Individ> individs, IData data);
    }

    public abstract class ARepairOperator: IRepairOperator
    {
        public abstract List<Individ> Run(List<Individ> individs, IData data);
        public void  LogGeneration(List<Individ> individs, IData data)
        {
            string text = Environment.NewLine + "Generation:";
            for (int i = 0; i < individs.Count; i++)
            {
                text += individs[i].Str() + " COST: " + Helpers.GetCost(individs[i], data)
                    + " WEIGHT: " + Helpers.GetWeight(individs[i], data) + Environment.NewLine;
            }
            Logger.Get().Debug(text);
        }
    }

    public class RepairOperator : ARepairOperator
    {
        public override List<Individ> Run(List<Individ> individs, IData data)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
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

            var specificCosts = new Dictionary<int, double>();
            for (int i = 0; i < data.COST.Length; ++i)
            {
                specificCosts.Add(i, (double)data.COST[i] / data.WEIGHT[i]);
            }
            specificCosts = specificCosts.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in permissibleIndivids)
            {
                var weight = Helpers.GetWeight(individ, data);
                while (weight > data.MAX_WEIGHT)
                {
                    foreach (var item in specificCosts)
                    {
                        if (individ.GENOTYPE[item.Key] != 0)
                        {
                            individ.GENOTYPE[item.Key] = 0;
                            weight -= data.WEIGHT[item.Key];

                            if (weight <= data.MAX_WEIGHT) break;
                        }
                    }
                }
            }
            individs.AddRange(permissibleIndivids);
            LogGeneration(individs, data);
            return individs;
        }
    }

    public class EfficientRepairOperator : ARepairOperator
    {
        public override List<Individ> Run(List<Individ> individs, IData data)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var size = data.COST.Length;
            var specificCosts = new Dictionary<int, double>();

            for (int i = 0; i < size; ++i)
            {
                specificCosts.Add(i, (double)data.COST[i] / data.WEIGHT[i]);
            }
            specificCosts = specificCosts.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in individs)
            {
                var weight = Helpers.GetWeight(individ, data);
                while (weight > data.MAX_WEIGHT)
                {
                    foreach (var item in specificCosts)
                    {
                        if (individ.GENOTYPE[item.Key] != 0)
                        {
                            individ.GENOTYPE[item.Key] = 0;
                            weight -= data.WEIGHT[item.Key];

                            if (weight <= data.MAX_WEIGHT) break;
                        }
                    }
                }
            }
            specificCosts = specificCosts.Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in individs)
            {
                var weight = Helpers.GetWeight(individ, data);
                foreach (var item in specificCosts)
                {
                    if (individ.GENOTYPE[item.Key] == 0 &&
                        (weight + data.WEIGHT[item.Key]) <= data.MAX_WEIGHT)
                    {
                        individ.GENOTYPE[item.Key] = 1;
                        weight += data.WEIGHT[item.Key];
                    }
                }
            }
            LogGeneration(individs, data);
            return individs;
        }
    }
}
