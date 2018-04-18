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
        public void  LogGeneration(List<Individ> individs)
        {
            string text = Environment.NewLine + "Generation:";
            for (int i = 0; i < individs.Count; i++)
            {
                text += individs[i].Str() + ", Cost: " + individs[i].GetCost()
                    + ", Weight: " + individs[i].GetWeight() + Environment.NewLine;
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
                if (individ.GetWeight() > data.Capacity)
                {
                    permissibleIndivids.Add(individ);
                    return true;
                }
                return false;
            });

            if (permissibleIndivids.Count == 0) return individs;

            var specificCosts = new Dictionary<int, double>();
            for (int i = 0; i < data.Cost.Length; ++i)
            {
                specificCosts.Add(i, (double)data.Cost[i] / data.Weight[i]);
            }
            specificCosts = specificCosts.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in permissibleIndivids)
            {
                var weight = individ.GetWeight();
                while (weight > data.Capacity)
                {
                    foreach (var item in specificCosts)
                    {
                        var gen = individ.GetGen(item.Key);
                        while (weight > data.Capacity)
                        {
                            if (gen.Decrement())
                            {
                                weight -= data.Weight[item.Key];
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (weight <= data.Capacity) break;
                    }
                }
            }
            individs.AddRange(permissibleIndivids);
            LogGeneration(individs);
            return individs;
        }
    }

    public class EfficientRepairOperator : ARepairOperator
    {
        public override List<Individ> Run(List<Individ> individs, IData data)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var size = data.Cost.Length;
            var specificCosts = new Dictionary<int, double>();

            for (int i = 0; i < size; ++i)
            {
                specificCosts.Add(i, (double)data.Cost[i] / data.Weight[i]);
            }
            specificCosts = specificCosts.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in individs)
            {
                var weight = individ.GetWeight();
                while (weight > data.Capacity)
                {
                    foreach (var item in specificCosts)
                    {
                        var gen = individ.GetGen(item.Key);
                        while (weight > data.Capacity)
                        {
                            if (gen.Decrement())
                            {
                                weight -= data.Weight[item.Key];
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (weight <= data.Capacity) break;
                    }
                }
            }
            specificCosts = specificCosts.Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var individ in individs)
            {
                var weight = individ.GetWeight();
                foreach (var item in specificCosts)
                {
                    var gen = individ.GetGen(item.Key);
                    while ((weight + data.Weight[item.Key]) <= data.Capacity)
                    {
                        if (gen.Increment())
                        {
                            weight += data.Weight[item.Key];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            LogGeneration(individs);
            return individs;
        }
    }
}
