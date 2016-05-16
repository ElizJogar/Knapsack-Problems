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
        }

        public override bool Equals(object obj)
        {
            int sum = 0;
            Individ newIndivid = (Individ)obj;
            for (int i = 0; i < INDIVID.Length; i++)
                sum += INDIVID[i] ^ newIndivid.INDIVID[i]; 
            return (sum == 0);
        }
    }

   public class GenAlgorithm
    {
        private DataInstances _data;
        private Random _rand = new Random(System.DateTime.Now.Millisecond);
        private int _summaryWeight = 0;
        private int _individSize;
        private int _lim = 0;
        private int[] _scalledFitnessFunctions;
       
        public int LIMIT
        {
            get { return _lim; }
            set { _lim = value; }
        }

        public int[] WEIGHT
        {
            get { return _data.WEIGHT;  }
        }

        public int[] COST
        {
            get { return _data.COST;  }
        }

        public GenAlgorithm(DataInstances data)
        {
            _data = data;
            _individSize = data.WEIGHT.Length;
        }

        public int getCost(Individ indiv)
        {
            int summaryCost = 0;
            for (int i = 0; i < _individSize; i++)
                if (indiv.INDIVID[i] == 1)
                    summaryCost += _data.COST[i];
            return summaryCost;
        }

        public int getMaxCost(List<Individ> individs)
        {
            int maxCost = 0;
            for (int i = 0; i < individs.Count(); i++)
            {
                int cost = getCost(individs[i]);
                maxCost = (cost > maxCost) ? cost : maxCost;
            }
            return maxCost;
        }

        public int getMaxScalledCost(List<Individ> individs)
        {
            int maxCost = 0;
            for (int i = 0; i < individs.Count(); i++)
            {
                int cost = _scalledFitnessFunctions[i];
                maxCost = (cost > maxCost) ? cost : maxCost;
            }
            return maxCost;
        }

        public Individ danzigAlgorithm() // Danzig algorithm: to generate an initial population of individuals
        {
            Individ ind = new Individ(_individSize);
            List<double> specificCostList = new List<double>();
            double[] specificCost = new double[_individSize];
            _summaryWeight = 0;
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
                    if (specificCostList.ElementAt(i) == specificCost[j])
                    {
                        _summaryWeight += _data.WEIGHT[j];
                         if (_summaryWeight <= _lim)
                         {
                             ind.INDIVID[j] = _rand.Next(2);
                             if (ind.INDIVID[j] == 0)
                                 _summaryWeight -= _data.WEIGHT[j];
                         }
                         else
                             ind.INDIVID[j] = 0;
                         break;
                    }
                }
            }
                return ind;
        }

        public Individ randomAlgorithm()// Random algorithm: to generate an initial population of individuals
        {
            Individ ind = new Individ(_individSize);
            _summaryWeight = 0;
            for (int i = 0; i < _individSize; i++)
            {
                _summaryWeight += _data.WEIGHT[i];
                if (_summaryWeight <= _lim)
                {
                    ind.INDIVID[i] = _rand.Next(2);
                    if (ind.INDIVID[i] == 0)
                        _summaryWeight -= _data.WEIGHT[i];
                }
                else
                    ind.INDIVID[i] = 0;
            }
            return ind;
        }

        public List<Individ> createPopulation(int n, int k) //  Сreating population: to generate an initial population of individuals with different
        {
            List<Individ> population = new List<Individ>();
            for (int i = 0; i < n; i++)
            {
                Individ ind = new Individ(_individSize);
                switch (k)
                {
                    case 1:
                        ind = danzigAlgorithm();
                        break;
                    case 2:
                        ind = randomAlgorithm();
                        break;
                }

                if (!population.Contains(ind))
                    population.Add(ind);
                else  n++;
            }
            return population;
        }

        public List<Individ> pointCrossover(List<Individ> individs, int l)// Point crossover: single-point and two-point crossovers
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;

            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (i != j)
                    {
                        k = _rand.Next(13);
                        r = _rand.Next(k + 1, 14);
                        Individ ind = new Individ(_individSize);

                        if (l == 1)
                        {
                            for (int s = 0; s < k; s++)
                                ind.INDIVID[s] = individs.ElementAt(j).INDIVID[s];
                            for (int s = k; s < _individSize; s++)
                                ind.INDIVID[s] = individs.ElementAt(i).INDIVID[s];
                        }
                        if (l == 2)
                        {
                            for (int s = 0; s < k; s++)
                                ind.INDIVID[s] = individs.ElementAt(j).INDIVID[s];
                            for (int s = k; s < r; s++)
                                ind.INDIVID[s] = individs.ElementAt(i).INDIVID[s];
                            for (int s = r; s < _individSize; s++)
                                ind.INDIVID[s] = individs.ElementAt(j).INDIVID[s];
                        }
                        population.Add(ind);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }

        public List<Individ> uniformCrossover(List<Individ> individs)//Uniform crossover
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (i != j)
                    {
                        Individ ind = new Individ(_individSize);
                        for (int s = 0; s < _individSize; s++)
                        {
                            k = _rand.Next(10);
                            if (k > 5)
                                ind.INDIVID[s] = individs.ElementAt(j).INDIVID[s];
                            else
                                ind.INDIVID[s] = individs.ElementAt(i).INDIVID[s];
                        }
                        population.Add(ind);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }

        //mutations: point mutation, inversion, translocation, saltation
        public List<Individ> pointMutation(List<Individ> individs)
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs.ElementAt(i);
                if (_rand.Next(100) == 1)
                   ind.INDIVID[k] = (individs.ElementAt(i).INDIVID[k] == 0) ? 1 : 0;
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
                population.Add(individs.ElementAt(i));
            return population;
        }

        public List<Individ> inversion(List<Individ> individs)
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs.ElementAt(i);
                if (_rand.Next(100) == 1)
                   for (int j = k; j <= r; j++)
                       ind.INDIVID[j] = (individs.ElementAt(i).INDIVID[j] == 0) ? 1 : 0;
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
                population.Add(individs.ElementAt(i));
            return population;
        }

        public List<Individ> translocation(List<Individ> individs)
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs.ElementAt(i);
                if (_rand.Next(100) == 1)
                {
                    for (int j = 0; j <= k; j++)
                        ind.INDIVID[j] = (individs.ElementAt(i).INDIVID[j] == 0) ? 1 : 0;
                    for (int j = r; j < _individSize; j++)
                        ind.INDIVID[j] = (individs.ElementAt(i).INDIVID[j] == 0) ? 1 : 0;
                }
                int nullCount = 0;
                for (int j = 0; j < _individSize; j++)
                    if (ind.INDIVID[j] == 0)
                        nullCount++;
                if (nullCount != _individSize)
                    population.Add(ind);
            }
            for (int i = s; i < individs.Count; i++)
                population.Add(individs.ElementAt(i));
            return population;
        }

        public List<Individ> saltation(List<Individ> individs)
        {
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;
            int s = (int)(individs.Count - Math.Sqrt(individs.Count));
            for (int i = 0; i < s; i++)
            {
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
                Individ ind = new Individ(_individSize);
                ind = individs.ElementAt(i);
                if (_rand.Next(100) == 1)
                {      
                    ind.INDIVID[r] = individs.ElementAt(i).INDIVID[k];
                    ind.INDIVID[k] = individs.ElementAt(i).INDIVID[r];
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
                population.Add(individs.ElementAt(i));
            return population;
        }

        private void evaluation(List<Individ> individs)// Penalty function method
        {
            //new
            _scalledFitnessFunctions = new int[individs.Count];
            int maxCost = getMaxCost(individs);
            Console.WriteLine("maxCost - " + maxCost);
            int averageCost = 0;
            for (int i = 0; i < individs.Count; i++)
                averageCost += getCost(individs[i]);
            averageCost /= individs.Count;

            Console.WriteLine("averageCost - " + averageCost);

            double C = _rand.NextDouble()*(2.0 - 1.2) + 1.2;

            Console.WriteLine("C - " + C);

            double delta = (maxCost - averageCost);
            delta = (delta == 0) ? 0.00001 : delta; //?????

            double coeffA = (C - 1) * averageCost / delta;
            double coeffB = averageCost * (maxCost - C * averageCost) / delta;
            Console.WriteLine("coeffA - " + coeffA);
            Console.WriteLine("coeffB - " + coeffB);

            for (int i = 0; i < individs.Count; i++)
            {
                _summaryWeight = 0;
                for (int g = 0; g < _individSize; g++)
                    if (individs.ElementAt(i).INDIVID[g] == 1)
                        _summaryWeight += _data.WEIGHT[g];
                if (_summaryWeight <= _lim)
                    _scalledFitnessFunctions[i] = getCost(individs[i]);
                else
                    _scalledFitnessFunctions[i] = Convert.ToInt32(coeffA * getCost(individs[i]) + coeffB);
            }
        }

        public List<Individ> bettaTournamentSelection(List<Individ> individs, int populationCount, int beta) //Betta Tournament selection
        {
            evaluation();
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
                        costList.Add(_scalledFitnessFunctions[i]/*getCost(individs[i])*/);
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
                    if (costList.ElementAt(i) > maxCost)
                    {
                        maxCost = costList.ElementAt(i);
                        individ = individs.ElementAt(number.ElementAt(i));
                    }
                    else
                        costList[i] = maxCost;
                }
                population.Add(individ);
            }
            return population;
        }

        public List<Individ> linearRankSelection(List<Individ> individs, int c)//Linear rank selection
        {
            evaluation();
            List<Individ> population1 = new List<Individ>();
            List<Individ> population2 = new List<Individ>();
            int[] sumcost = new int[individs.Count];
            List<int> sumcost1 = new List<int>();
            int[] rang = new int[individs.Count];
            double[] n = new double[individs.Count]; // expected numbers
            n[individs.Count - 1] = _rand.NextDouble() + 1.1;
            n[0] = 2 - n[individs.Count - 1];
            for (int i = 0; i < individs.Count; i++)
            {
                sumcost[i] = _scalledFitnessFunctions[i]/*getCost(individs[i])*/;
                sumcost1.Add(sumcost[i]);
            }
            sumcost1.Sort();
            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (sumcost[j] != -1)
                    {
                        if (sumcost[j] == sumcost1.ElementAt(i))
                        {
                            sumcost[j] = -1;
                            population1.Add(individs.ElementAt(j));
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
                    population2.Add(population1[i]);
            }
            for (int i = 0; i < 1; i++)
            {
                if (population2.Count < c)
                {
                    for (int j = 0; j < individs.Count; j++)
                        if (n[j] > 1 && _rand.Next((int)(n[j] - 1) * 100) == 0 || n[j] < 1 && _rand.Next((int)(n[j] * 100)) == 0)
                            population2.Add(population1[j]);
                    i++;
                }
            }
            if (population2.Count > c)
            {
                population1.Clear();
                int k = 0;
                for (int i = 0; i < c; i++)
                {
                    k = _rand.Next(population2.Count);
                    population1.Add(population2.ElementAt(k));
                }
            }
            return (population2.Count > c) ? population1 : population2;
        }
    }
}
