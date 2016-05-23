using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public class Individ
    {
        private int[] _individ;
        private int _size;
        public int[] INDIVID
        {
            get { return _individ; }
        }
        public int SIZE
        {
            get { return _size; }
        }

        public Individ(int size)
        {
            _size = size;
            _individ = new int[_size];
            for (int i = 0; i < _size; i++)
                _individ[i] = 0;
        }

        public override bool Equals(object obj)
        {
            int sum = 0;
            Individ newIndivid = (Individ)obj;
            for (int i = 0; i < INDIVID.Length; i++)
                sum += INDIVID[i] ^ newIndivid.INDIVID[i]; 
            return (sum == 0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string ConvertToString()
        {
            string str = "";
            for (int i = 0; i < _size; i++)
                str += _individ[i];
            return str;
        }
    }

   public class GenAlgorithm
    {
        private ADataInstances _data;
        private Random _rand = new Random(System.DateTime.Now.Millisecond);
        private int _individSize;
        private const int _DANZIG_ALGORITHM = 0;
        private const int _RANDOM_ALGORITHM = 1;
        private const int _SINGLE_POINT_CROSSOVER = 2;
        private const int _TWO_POINT_CROSSOVER = 3;
        private const int _UNIFORM_CROSSOVER = 4;
        private const int _POINT_MUTATION = 5;
        private const int _INVERSION = 6;
        private const int _SALTATION = 7;
        private const int _TRANSLOCATION = 8;
        private const int _BETTA_TOURNAMENT = 9;
        private const int _LINEAR_RANK_SELECTION = 10;

        public int DANZIG_ALGORITHM      
        {
            get { return _DANZIG_ALGORITHM; }
        }

        public int RANDOM_ALGORITHM 
        {
            get { return _RANDOM_ALGORITHM; }
        }

        public int SINGLE_POINT_CROSSOVER
        {
            get { return _SINGLE_POINT_CROSSOVER; }
        }

        public int TWO_POINT_CROSSOVER
        {
            get { return _TWO_POINT_CROSSOVER; }
        }

        public int UNIFORM_CROSSOVER
        {
            get { return _UNIFORM_CROSSOVER; }
        }

        public int POINT_MUTATION
        {
            get { return _POINT_MUTATION; }
        }

        public int INVERSION
        {
            get { return _INVERSION; }
        }

        public int SALTATION
        {
            get { return _SALTATION; }
        }

        public int TRANSLOCATION
        {
            get { return _TRANSLOCATION; }
        }

        public int BETTA_TOURNAMENT
        {
            get { return _BETTA_TOURNAMENT; }
        }

        public int LINEAR_RANK_SELECTION 
        {
            get { return 10; }
        } 

        public int LIMIT
        {
            get { return _data.MAX_WEIGHT; }
        }

        public int[] WEIGHT
        {
            get { return _data.WEIGHT;  }
        }

        public int[] COST
        {
            get { return _data.COST;  }
        }
        public int INDIVID_SIZE
        {
            get { return _individSize; }
        }
       public GenAlgorithm(ADataInstances data)
        {
            _data = data;
            _individSize = data.WEIGHT.Length;
            Logger.Get().Debug("Genetic algorithm created.");
             String text = "COST: ";
            for (int i = 0; i < data.COST.Length; i++)
                text += data.COST[i] + " ";
            Logger.Get().Debug(text);
            text = "WEIGHT: ";
            for (int i = 0; i < data.WEIGHT.Length; i++)
                text += data.WEIGHT[i] + " ";
            Logger.Get().Debug(text);
        }

        public int GetCost(Individ indiv)
        {
            int summaryCost = 0;
            for (int i = 0; i < indiv.SIZE; i++)
                if (indiv.INDIVID[i] == 1)
                    summaryCost += _data.COST[i];
            return summaryCost;
        }

        public int GetWeight(Individ indiv)
        {
            int summaryWeight = 0;
            for (int i = 0; i < indiv.SIZE; i++)
                if (indiv.INDIVID[i] == 1)
                    summaryWeight += _data.WEIGHT[i];
            return summaryWeight;
        }

        public int GetMaxCost(List<Individ> individs)
        {
            int maxCost = 0;
            for (int i = 0; i < individs.Count(); i++)
            {
                int cost = GetCost(individs[i]);
                maxCost = (cost > maxCost) ? cost : maxCost;
            }
            return maxCost;
        }

        private Individ DanzigAlgorithm()
        {
            Individ ind = new Individ(_individSize);
            List<double> specificCostList = new List<double>();
            double[] specificCost = new double[_individSize];
            int summaryWeight = 0;
            for (int i = 0; i < _individSize; i++)
            {
                specificCost[i] = (double)_data.COST[i] / _data.WEIGHT[i];
                specificCostList.Add(specificCost[i]);
            }
            specificCostList.Sort();
            specificCostList.Reverse();

            for (int i = 0; i < _individSize; i++)
            {
                for( int j = 0; j < _individSize; j++)
                {
                    if (specificCostList[i] == specificCost[j])
                    {
                        summaryWeight += _data.WEIGHT[j];
                        if (summaryWeight <= LIMIT)
                         {
                             ind.INDIVID[j] = _rand.Next(2);
                             if (ind.INDIVID[j] == 0)
                                 summaryWeight -= _data.WEIGHT[j];
                         }
                        else
                            ind.INDIVID[j] = 0;
                        specificCost[j] = -1;
                        break;
                    }
                }
            }
            return ind;
        }

        private Individ RandomAlgorithm()
        {
            Individ ind = new Individ(_individSize);
            int summaryWeight = 0;
            for (int i = 0; i < _individSize; i++)
            {
                summaryWeight += _data.WEIGHT[i];
                if (summaryWeight <= LIMIT)
                {
                    ind.INDIVID[i] = _rand.Next(2);
                    if (ind.INDIVID[i] == 0)
                        summaryWeight -= _data.WEIGHT[i];
                }
                else
                    ind.INDIVID[i] = 0;
            }
            return ind;
        }

        public List<Individ> CreatePopulation(int n, int type)
        {
            Logger.Get().Debug("called CreatePopulation function.");
            List<Individ> population = new List<Individ>();
            for (int i = 0; i < n; i++)
            {
                Individ ind = new Individ(_individSize);
                switch (type)
                {
                    case _DANZIG_ALGORITHM:
                        ind = DanzigAlgorithm();
                        break;
                    case _RANDOM_ALGORITHM:
                        ind = RandomAlgorithm();
                        break;
                }

                if (!population.Contains(ind))
                    population.Add(ind);
                else  n++;
            }
            string text = "Initial population";
            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < population[i].SIZE; j++)
                    text += population[i].INDIVID[j];
                text += ", ";
            }
            Logger.Get().Debug(text);
            return population;
        }
        public List<Individ> SinglePointCrossover(List<Individ> individs)
        {
            Logger.Get().Debug("called single point crossover.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = i + 1; j < individs.Count; j++)
                {
                     k = _rand.Next(_individSize - 1);
                     Individ descendant1 = new Individ(_individSize);
                     Individ descendant2 = new Individ(_individSize);
                     for (int s = 0; s < k; s++)
                     {
                         descendant1.INDIVID[s] = individs[j].INDIVID[s];
                         descendant2.INDIVID[s] = individs[i].INDIVID[s];
                     }
                     for (int s = k; s < _individSize; s++)
                     {
                         descendant1.INDIVID[s] = individs[i].INDIVID[s];
                         descendant2.INDIVID[s] = individs[j].INDIVID[s];
                     }
                     population.Add(descendant1);
                     population.Add(descendant2);
                }
            }
            population.AddRange(individs);
            return population;
        }

        public List<Individ> TwoPointCrossover(List<Individ> individs) 
        {
            Logger.Get().Debug("called two-point crossover.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;

            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = i + 1; j < individs.Count; j++)
                {
                    k = _rand.Next(_individSize/2);
                    r = _rand.Next(k + 1, _individSize - 1);
                    Individ descendant1 = new Individ(_individSize);
                    Individ descendant2 = new Individ(_individSize);
                    for (int s = 0; s < k; s++)
                    {
                        descendant1.INDIVID[s] = individs[j].INDIVID[s];
                        descendant2.INDIVID[s] = individs[i].INDIVID[s];
                    }
                    for (int s = k; s < r; s++)
                    {
                        descendant1.INDIVID[s] = individs[i].INDIVID[s];
                        descendant2.INDIVID[s] = individs[j].INDIVID[s];
                    }
                    for (int s = r; s < _individSize; s++)
                    {
                        descendant1.INDIVID[s] = individs[j].INDIVID[s];
                        descendant2.INDIVID[s] = individs[i].INDIVID[s];
                    }
                    population.Add(descendant1);
                    population.Add(descendant2);
                }
            }
            population.AddRange(individs);
            return population;
        }

        public List<Individ> UniformCrossover(List<Individ> individs)
        {
            Logger.Get().Debug("called uniform crossover.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (i != j)
                    {
                        Individ individ = new Individ(_individSize);
                        for (int s = 0; s < _individSize; s++)
                        {
                            k = _rand.Next(10);
                            if (k > 5)
                                individ.INDIVID[s] = individs[j].INDIVID[s];
                            else
                                individ.INDIVID[s] = individs[i].INDIVID[s];
                        }
                        population.Add(individ);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }

        public List<Individ> PointMutation(List<Individ> individs)
        {
            Logger.Get().Debug("called point mutation.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize);
                Individ individ = new Individ(_individSize);
                individ = individs[i];
                if (_rand.Next(100) == 1)
                    individ.INDIVID[k] = (individs[i].INDIVID[k] == 0) ? 1 : 0;
                int nullCount = 0;
                for (int j = 0; j < _individSize; j++)
                {
                    if (individ.INDIVID[j] == 0)
                        nullCount++;
                }
                if (nullCount != _individSize)
                    population.Add(individ);
            }
            for (int i = s; i < individs.Count; i++)
                population.Add(individs[i]);
            return population;
        }

        public List<Individ> Inversion(List<Individ> individs)
        {
            Logger.Get().Debug("called Inversion.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs[i];
                if (_rand.Next(100) == 1)
                   for (int j = k; j <= r; j++)
                       ind.INDIVID[j] = (individs[i].INDIVID[j] == 0) ? 1 : 0;
                int nullCount = 0;
                for (int j = 0; j < _individSize; j++)
                {
                    if (ind.INDIVID[j] == 0)
                        nullCount++;
                }
                if (nullCount != _individSize)
                    population.Add(ind);
            }
            for (int i = s; i < individs.Count; i++)
                population.Add(individs[i]);
            return population;
        }

        public List<Individ> Translocation(List<Individ> individs)
        {
            Logger.Get().Debug("called Translocation.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs[i];
                if (_rand.Next(100) == 1)
                {
                    for (int j = 0; j <= k; j++)
                        ind.INDIVID[j] = (individs[i].INDIVID[j] == 0) ? 1 : 0;
                    for (int j = r; j < _individSize; j++)
                        ind.INDIVID[j] = (individs[i].INDIVID[j] == 0) ? 1 : 0;
                }
                int nullCount = 0;
                for (int j = 0; j < _individSize; j++)
                    if (ind.INDIVID[j] == 0)
                        nullCount++;
                if (nullCount != _individSize)
                    population.Add(ind);
            }
            for (int i = s; i < individs.Count; i++)
                population.Add(individs[i]);
            return population;
        }

        public List<Individ> Saltation(List<Individ> individs)
        {
            Logger.Get().Debug("called Saltation.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize/2);
                r = _rand.Next(k + 1, _individSize);
                Individ ind = new Individ(_individSize);
                ind = individs[i];
                if (_rand.Next(100) == 1)
                {      
                    ind.INDIVID[r] = individs[i].INDIVID[k];
                    ind.INDIVID[k] = individs[i].INDIVID[r];
                }
                int nullCount = 0;
                for (int j = 0; j < _individSize; j++)
                {
                    if (ind.INDIVID[j] == 0)
                        nullCount++;
                }
                if (nullCount != _individSize)
                    population.Add(ind);
            }
            for (int i = s; i < individs.Count; i++)
                population.Add(individs[i]);
            return population;
        }

        private int[] PenaltyFunctionMethod(List<Individ> individs)
        {
            Logger.Get().Debug("called penalty function method.");
            int [] scalledFitnessFunctions = new int[individs.Count];
            int averageCost = 0;
            for (int i = 0; i < individs.Count; i++)
                averageCost += GetCost(individs[i]);
            averageCost /= individs.Count;
            double coeffA = 0;
            double coeffB = 0;
            int weight = 0;
            int cost = 0;
            int minCost = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                cost = GetCost(individs[i]);
                weight = GetWeight(individs[i]);
                scalledFitnessFunctions[i] = (weight <= LIMIT) ? cost : cost - (int)Math.Pow(weight - LIMIT, 2);
                if (scalledFitnessFunctions[i] < minCost)
                    minCost = scalledFitnessFunctions[i];
            }
            for(int i = 0; i < individs.Count; i++)
            {
                if (scalledFitnessFunctions[i] == minCost)
                    scalledFitnessFunctions[i] = 0;
                else if (scalledFitnessFunctions[i] <= 0)
                 {
                     cost = scalledFitnessFunctions[i];
                     coeffA = averageCost / (cost - averageCost);
                     coeffB = averageCost * cost / (cost - averageCost);
                     scalledFitnessFunctions[i] = Convert.ToInt32(coeffA * scalledFitnessFunctions[i] + coeffB);    
                 }
            }
            return scalledFitnessFunctions;
        }

        public List<Individ> BettaTournamentSelection(List<Individ> individs, int populationCount, int beta)
        {
            int[] costs = PenaltyFunctionMethod(individs);
            Logger.Get().Debug("called betta - tournament selection.");
            List<Individ> population = new List<Individ>();
            for (int j = 0; j < populationCount; j++)
            {
                int c = 0;
                int count = 0;
                List<int> costList = new List<int>();
                List<int> number = new List<int>();
                for (int i = 0; i < individs.Count; i++)
                {
                    c = _rand.Next(2);
                    if (c == 1 && count < beta && !number.Contains(i))
                    {
                        costList.Add(costs[i]);
                        number.Add(i);
                        count++;
                    }
                    if (i == individs.Count - 1 && count < beta)
                        i = -1;
                }
                int maxCost = 0;
                Individ individ = new Individ(_individSize);
                for (int i = 0; i < beta; i++)
                {
                    if (costList[i] > maxCost)
                    {
                        maxCost = costList[i];
                        individ = individs[number[i]];
                    }
                }
                population.Add(individ);
            }
            population = GenerationModificaton(population);
            return population;
        }

        public List<Individ> LinearRankSelection(List<Individ> individs, int populationCount)
        {
            int[] costs = PenaltyFunctionMethod(individs);
            Logger.Get().Debug("called linear rank selection.");
            List<Individ> sortedPopulation = new List<Individ>();
            List<Individ> generation = new List<Individ>();
            List<int> costList = new List<int>();
            int[] rang = new int[individs.Count];
            double[] n = new double[individs.Count]; // expected numbers
            n[individs.Count - 1] = _rand.NextDouble() + 1.1;
            n[individs.Count - 1] = (n[individs.Count - 1] > 2) ? 2 : n[individs.Count - 1];
            n[0] = 2 - n[individs.Count - 1];
            for (int i = 0; i < individs.Count; i++)
                costList.Add(costs[i]);
            costList.Sort();
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (costs[j] != -1)
                    {
                        if (costs[j] == costList[i])
                        {
                            costs[j] = -1;
                            sortedPopulation.Add(individs[j]);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < individs.Count; i++)
            {
                rang[i] = i + 1;
                n[i] = n[0] + (n[individs.Count - 1] - n[0]) * (rang[i] - 1) / (individs.Count - 1);
                if (n[i] >= 1)
                    generation.Add(sortedPopulation[i]);
            }

            if (generation.Count < populationCount)
            {
                for (int j = 0; j < individs.Count; j++)
                    if (n[j] > 1 && _rand.Next((int)(n[j] - 1) * 100) == 0 || n[j] < 1 && _rand.Next((int)(n[j] * 100)) == 0)
                         generation.Add(sortedPopulation[j]);
                   
            }

            if (generation.Count > populationCount)
            {
                sortedPopulation.Clear();
                int k = 0;
                for (int i = 0; i < populationCount; i++)
                {
                    k = _rand.Next(generation.Count);
                    sortedPopulation.Add(generation[k]);
                }
            }

            individs.Clear();
            individs = (generation.Count > populationCount) ? sortedPopulation : generation;
            individs = GenerationModificaton(individs);
            return individs;
        }

        private List<Individ> GenerationModificaton(List<Individ> individs)
        {
            Logger.Get().Debug("called generation modification.");
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            int permissibleIndividCount = 0;
            List<Individ> permissibleIndivids = new List<Individ>();
            for (int i = 0; i < individs.Count; i++)
                if (GetWeight(individs[i]) > LIMIT)
                {
                    permissibleIndividCount++;
                    permissibleIndivids.Add(individs[i]);
                    individs.RemoveAt(i);
                }
            if ( permissibleIndividCount >= individs.Count / 3)
            {
                foreach (Individ individ in permissibleIndivids)
                {
                    dictionary.Clear();
                    for (int i = 0; i < _individSize; i++)
                        if (individ.INDIVID[i] == 1)
                            dictionary.Add(i, (double)_data.COST[i] / _data.WEIGHT[i]);

                    dictionary = dictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                    while(GetWeight(individ) > LIMIT)
                        foreach (var item in dictionary)
                        {
                            if (GetWeight(individ) > LIMIT)
                            {
                                if(_rand.Next(10) <= 5)
                                individ.INDIVID[item.Key] = 0;
                            }
                            else break;
                        }
                }
            }
            individs.AddRange(permissibleIndivids);
            string text = "Generation \r\n";
            for (int i = 0; i < individs.Count; i++)
            {
                text = "";
                for (int j = 0; j < individs[i].SIZE; j++)
                    text += individs[i].INDIVID[j];
                text += " COST: " + GetCost(individs[i]) +" WEIGHT: " + GetWeight(individs[i]);
                Logger.Get().Debug(text);
            }
            return individs;
        }

        public List<Individ> Run(int iterationCount, int populationCount, List<Individ> individs, int crossoverType,
            int mutationType, int selectionType, int betta)
        {
                switch (crossoverType)
                {
                    case _SINGLE_POINT_CROSSOVER:
                        individs = SinglePointCrossover(individs);
                        break;
                    case _TWO_POINT_CROSSOVER:
                        individs = TwoPointCrossover(individs);
                        break;
                    case _UNIFORM_CROSSOVER:
                        individs = UniformCrossover(individs);
                        break;
                }
                switch (mutationType)
                {
                    case _POINT_MUTATION:
                        individs = PointMutation(individs);
                        break;
                    case _INVERSION:
                        individs = Inversion(individs);
                        break;
                    case _TRANSLOCATION:
                        individs = Translocation(individs);
                        break;
                    case _SALTATION:
                        individs = Saltation(individs);
                        break;
                }
                switch (selectionType)
                {
                    case _BETTA_TOURNAMENT:
                        individs = BettaTournamentSelection(individs, populationCount, betta);
                        break;
                    case _LINEAR_RANK_SELECTION:
                        individs = LinearRankSelection(individs, populationCount);
                        break;
                }
                return individs;
        }
    }
}
