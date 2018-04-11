using System.Collections.Generic;
using KnapsackProblemData;
using CustomLogger;

namespace Algorithm
{
    public class GeneticAlgorithm
{
        private AData m_data = null;
        private IInitialPopulation m_initialPopulation = null;
        private ICrossover m_crossover = null;
        private IMutation m_mutation = null;
        private ISelection m_selection = null;
        private Individ m_winner = null;

       public GeneticAlgorithm(AData data, IInitialPopulation population, ICrossover crossover, IMutation mutation, ISelection selection)
        {
            m_data = data;
            m_initialPopulation = population;
            m_crossover = crossover;
            m_mutation = mutation;
            m_selection = selection;

            // Adding log info
            Logger.Get().Debug("Genetic algorithm created.");
            Logger.Get().Debug("COST: " + string.Join(", ", data.COST));
            Logger.Get().Debug("WEIGHT: " + string.Join(", ", data.WEIGHT));            
        }
   
        public List<Individ> Init(int n)
        {
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
       
        public int Run(int populationCount, ref List<Individ> individs, params object[] args)
        {
            individs = m_crossover.Run(individs);
            individs = m_mutation.Run(individs);
            individs = m_selection.Run(individs, populationCount, m_data, args);

            return Helpers.GetMaxCost(ref m_winner, individs, m_data);
        }

        public int Run(int iterationCount, int populationCount, params object[] args)
        {
            var individs = Init(populationCount);
            for(var i = 0; i < iterationCount - 1; ++i)
            {
                Run(populationCount, ref individs, args);
            }
            return Run(populationCount, ref individs, args);
        }
        public Individ getWinner()
        {
            return m_winner;
        }
    }
}

