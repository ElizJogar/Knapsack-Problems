using System.Collections.Generic;
using System.Linq;
using KnapsackProblem;
using CustomLogger;

namespace Algorithm
{
    public interface IHeuristicAlgorithm
    {
        void SetData(IData data);
        long Run(int iterationCount, int populationCount, params object[] args);
    }
    public class GeneticAlgorithm : IHeuristicAlgorithm
    {
        private const int INVALID_RESULT = -1;
        private IData m_data = null;
        private IInitialPopulation m_initialPopulation = null;
        private ICrossover m_crossover = null;
        private IMutation m_mutation = null;
        private ISelection m_selection = null;
        private Individ m_winner = null;
        public GeneticAlgorithm(IData data = null)
        {
            SetData(data);
            m_initialPopulation = new DantzigAlgorithm();
            m_crossover = new SinglePointCrossover();
            m_mutation = new PointMutation();
            m_selection = new LinearRankSelection(new PenaltyFunction(), new EfficientRepairOperator());
        }
        public GeneticAlgorithm(IInitialPopulation population, ICrossover crossover, IMutation mutation, ISelection selection, IData data = null)
        {
            SetData(data);
            m_initialPopulation = population;
            m_crossover = crossover;
            m_mutation = mutation;
            m_selection = selection;
        }
        public void SetData(IData data)
        {
            m_data = data;
            if (m_data != null)
            {
                Logger.Get().Debug("Cost: " + string.Join(", ", data.Cost));
                Logger.Get().Debug("Weight: " + string.Join(", ", data.Weight));
            }
        }
        public List<Individ> Init(int n)
        {
            if (m_data == null) return null;

            m_winner = null;

            Logger.Get().Debug("Called CreatePopulation function.");
            List<Individ> population = new List<Individ>();
            for (int i = 0; i < n; ++i)
            {
                Individ individ = m_initialPopulation.Run(m_data);
                if (!population.Contains(individ))
                {
                    population.Add(individ);
                }
                else
                {
                    ++n;
                }
            }

            string text = "Initial population: ";
            for (int i = 0; i < population.Count; i++)
            {
                text += population[i].Str() + ", ";
            }
            Logger.Get().Debug(text);

            return population;
        }
        public long Run(int populationCount, ref List<Individ> individs, params object[] args)
        {
            if (individs == null || m_data == null) return INVALID_RESULT;

            individs = m_crossover.Run(individs);
            individs = m_mutation.Run(individs);
            individs = m_selection.Run(individs, populationCount, m_data, args);

            m_winner = individs.Max();
            return m_winner.GetCost();
        }
        public long Run(int iterationCount, int populationCount, params object[] args)
        {
            long maxValue = 0;
            Individ winner = null;
            if (m_data == null) return INVALID_RESULT;

            var individs = Init(populationCount);
            for (var i = 0; i < iterationCount; ++i)
            {
                var result = Run(populationCount, ref individs, args);
                if (result >= maxValue)
                {
                    maxValue = result;
                    winner = m_winner;
                }
            }
            m_winner = winner;
            return maxValue;
        }
        public Individ GetWinner()
        {
            return m_winner;
        }
    }
}

