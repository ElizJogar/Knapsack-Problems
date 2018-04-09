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
      
        public int[] WEIGHT
        {
            get { return m_data.WEIGHT;  }
        }

        public int[] COST
        {
            get { return m_data.COST;  }
        }
       public GeneticAlgorithm(AData data, IInitialPopulation population, ICrossover crossover, IMutation mutation, ISelection selection)
        {
            m_data = data;
            // TODO: refactor
            m_data.Fill();

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
       
        public List<Individ> Run(int iterationCount, int populationCount, List<Individ> individs, params object[] args)
        {
            individs = m_crossover.Run(individs);
            individs = m_mutation.Run(individs);
            return m_selection.Run(individs, populationCount, m_data, args);
        }
    }
}
