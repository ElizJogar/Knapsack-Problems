using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface ISelection : IOperator
    {
        List<Individ> Run(List<Individ> individs, int populationCount, IData data, params object[] args);
    }

    public abstract class ASelection : ISelection
    {
        protected Random m_random = new Random(DateTime.Now.Millisecond);
        protected IConstraintProcessing m_cp;
        protected IRepairOperator m_op;

        public ASelection(IConstraintProcessing cp, IRepairOperator op)
        {
            m_cp = cp;
            m_op = op;
        }
        public abstract List<Individ> Run(List<Individ> individs, int populationCount, IData data, params object[] args);
    }

    public class BettaTournament : ASelection
    {
        public BettaTournament(IConstraintProcessing cp, IRepairOperator op) : base(cp, op) { }

        public override List<Individ> Run(List<Individ> individs, int populationCount, IData data, params object[] args)
        {
            Logger.Get().Debug("Called " + Convert.ToString(this));
            var beta = (int)args[0];
            var customIndivids = m_cp.Run(individs, data);
            List<Individ> population = new List<Individ>();
            for (int j = 0; j < populationCount; j++)
            {
                var individs4Beta = new List<CustomIndivid>();
                var index = new List<int>();
                while (individs4Beta.Count != beta)
                {
                    for (int i = 0; i < customIndivids.Count; ++i)
                    {
                        if (individs4Beta.Count >= beta) break;

                        int randomNumber = m_random.Next(100);
                        if (randomNumber < 50 && !index.Contains(i))
                        {  
                            individs4Beta.Add(customIndivids[i]);
                            index.Add(i);
                        }
                    }
                }
                population.Add(individs4Beta.Max().Original());
            }
            return m_op.Run(population, data);
        }
    }

    public class LinearRankSelection : ASelection
    {
        public LinearRankSelection(IConstraintProcessing cp, IRepairOperator op) : base(cp, op) { }
        public override List<Individ> Run(List<Individ> individs, int populationCount, IData data, params object[] args)
        {
            var size = individs.Count;

            Logger.Get().Debug("Called " + Convert.ToString(this));

            var customIndivids = m_cp.Run(individs, data);
            List<Individ> generation = new List<Individ>();

            int[] rang = new int[size];
            double[] nCopy = new double[size];
            double a = m_random.NextDouble() + 1.1;
            nCopy[size - 1] = a > 2 ? 2 : a;
            nCopy[0] = 2 - nCopy[size - 1];

            customIndivids.Sort();

            for (int i = 0; i < size; ++i)
            {
                if (generation.Count >= populationCount) break;

                rang[i] = i + 1;
                nCopy[i] = nCopy[0] + (nCopy[size - 1] - nCopy[0]) * (rang[i] - 1) / (size - 1);
                if (nCopy[i] >= 1 && !generation.Contains(customIndivids[i].Original()))
                {
                    var copies = 0;
                    while (copies++ < (int)nCopy[i] && generation.Count < populationCount)
                    {
                        generation.Add(customIndivids[i].Original());
                    }
                }
            }

            while (generation.Count < populationCount)
            {
                var index = m_random.Next(0, size);
                generation.Add(customIndivids[index].Original());
            }
            return m_op.Run(generation, data);
        }
    }
}