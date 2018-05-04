using System;
using System.Collections.Generic;
using System.Linq;
using Algorithm;
using KnapsackProblem;

namespace Algorithm
{
    public class GABBHybrid: IExactAlgorithm
    {
        public long Run(IData data)
        {
            var capacity = data.Capacity;

            var ga = new GeneticAlgorithm(new RandomPopulation(),
                new UniformCrossover(),
                new PointMutation(),
                new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator()));
            ga.SetData(data);
            ga.Run(20, data.Cost.Length > 10 ? 10 : data.Cost.Length);
            var winner = ga.GetWinner();

            var result = new Dictionary<Item, int>();
            for (var i  = 0; i < winner.Size(); ++i)
            {
                var gen = winner.GetGen(i);
                var item = new Item(data.Cost[i], data.Weight[i], data.Cost.Length == data.ItemMaxCounts.Length ? data.ItemMaxCounts[i] : 1);
                result.Add(item, gen.ToInt());
            }
            var items = result.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value).Keys.ToList();

            var bb = new BranchAndBound(new U3Bound());
            return bb.Run(items, capacity);
        }
    }
}
