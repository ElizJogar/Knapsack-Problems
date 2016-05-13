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
       
        public int LIMIT
        {
            get { return _lim; }
            set { _lim = value; }
        }

        public int INDIVID_SIZE
        {
            get { return _individSize; }
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
        //--------------------------------------------------------------------------------------
        public Individ danzigAlgorithm() // алгоритм Данцига для  особи из начальной популяции
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
                int spCostIndex = specificCostList.IndexOf(specificCost[i]);
                _summaryWeight += _data.WEIGHT[i];
                if (_summaryWeight <= _lim)
                {
                    ind.INDIVID[spCostIndex] = _rand.Next(2);
                    if(ind.INDIVID[spCostIndex] == 0)
                        _summaryWeight -= _data.WEIGHT[i];
                }
                else
                    ind.INDIVID[spCostIndex] = 0;
            }
            return ind;
        }
        //---------------------------------------------------------------------------------
        public Individ randomAlgorithm()// случайный алгоритм для особи из начальной популяции
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

        //-------------------------------------------------------------------------------------------------
        public List<Individ> createPopulation(int n, int k) // для разных особей в популяции
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
        //-------------------------------------------------------------------------------------------
        public List<Individ> pointCrossover(List<Individ> individs, int l)// для кроссоверов одноточечного и двуточечного
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
        //------------------------------------------------------------
        public List<Individ> uniformCrossover(List<Individ> individs)//для однородного кроссовера
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

        //----------------------------------------------------------------------------
       //мутация точечная, инверсия, транслокация, сальтация
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
        //--------------------------------------------------------
        public List<Individ> evaluation(List<Individ> individs)// для отбора особей, подходящих под ограничение
        {
            List<Individ> population = new List<Individ>();
            for (int i = 0; i < individs.Count; i++)
            {
                _summaryWeight = 0;
                for (int g = 0; g < _individSize; g++)
                    if (individs.ElementAt(i).INDIVID[g] == 1)
                        _summaryWeight += _data.WEIGHT[g];
                if (_summaryWeight <= _lim)
                    population.Add(individs.ElementAt(i));
            }
            return population;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public Individ selection(List<Individ> population, int beta) //селекция бета-турнир
        {
            int c = 0;
            int count = 0;
            int summaryCost;
            List<int> costList = new List<int>();
            List<int> number = new List<int>();
            for (int i = 0; i < population.Count; i++)
            {
                c = _rand.Next(2);
                summaryCost = 0;
                if (c == 1 && count < beta && !number.Contains(i))
                {
                    for (int g = 0; g < _individSize; g++)
                        if (population.ElementAt(i).INDIVID[g] == 1)
                            summaryCost += _data.COST[g];
                    costList.Add(summaryCost);
                    count++;
                    number.Add(i);
                }
                if (i == population.Count - 1 && count < beta)
                    i = -1;
            }
            int maxCost = 0;
            Individ individ = new Individ(_individSize);
            for (int i = 0; i < beta; i++)
            {
                if (costList.ElementAt(i) > maxCost)
                {
                    maxCost = costList.ElementAt(i);
                    individ = population.ElementAt(number.ElementAt(i));
                }
                else
                    costList[i] = maxCost;
            }
            return individ;
        }
        //-------------------------------------------------------------

        public List<Individ> linearRankSelection(List<Individ> population, int c)// линейная ранговая схема селекции
        {
            List<Individ> population1 = new List<Individ>();
            List<Individ> population2 = new List<Individ>();
            int[] sumcost = new int[population.Count];
            List<int> sumcost1 = new List<int>();
            int[] rang = new int[population.Count]; //для рангов
            double[] n = new double[population.Count]; // для всех ожид чисел 
            n[population.Count - 1] = _rand.NextDouble() + 1.1;
            n[0] = 2 - n[population.Count - 1];
            for (int i = 0; i < population.Count; i++)
            {
                for (int g = 0; g < _individSize; g++)
                    if (population.ElementAt(i).INDIVID[g] == 1)
                        sumcost[i] += _data.COST[g];
                sumcost1.Add(sumcost[i]);
            }
            sumcost1.Sort();
            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < population.Count; j++)
                {
                    if (sumcost[j] != -1)
                    {
                        if (sumcost[j] == sumcost1.ElementAt(i))
                        {
                            sumcost[j] = -1;
                            population1.Add(population.ElementAt(j));
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < population.Count; i++)
            {
                rang[i] = i + 1;
                n[i] = n[0] + (n[population.Count - 1] - n[0]) * (rang[i] - 1) / (population.Count - 1);
                if (n[i] >= 1)
                    population2.Add(population1[i]);
            }
            for (int i = 0; i < 1; i++)
            {
                if (population2.Count < c)
                {
                    for (int j = 0; j < population.Count; j++)
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
            if (population2.Count > c)
                return (population1);
            else
                return population2;
        }
        //-----------------------------------------------------------
        public int returnMaxCost(Individ indiv)
        {
            int summaryCost = 0;
            for (int g = 0; g < _individSize; g++)
                if (indiv.INDIVID[g] == 1)
                    summaryCost += _data.COST[g];
            return summaryCost;

        }  // для вывода функции приспособленности
        //----------------------------------------------------------
        public int returnMaxCostAtGeneration(List<Individ> indiv)
        {
            int summaryCost = 0;
            for (int i = 0; i < indiv.Count(); i++)
            {
                if (returnMaxCost(indiv[i]) > summaryCost)
                    summaryCost = returnMaxCost(indiv[i]);
            }
            return summaryCost;
        }
    }
}
