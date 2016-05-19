﻿using System;
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
        private DataInstances _data;
        private Random _rand = new Random(System.DateTime.Now.Millisecond);
        private int _individSize;
       
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

        public GenAlgorithm(DataInstances data)
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

        public Individ DanzigAlgorithm() // Danzig algorithm: to generate an initial population of individuals
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
                         break;
                    }
                }
            }
            return ind;
        }

        public Individ RandomAlgorithm()// Random algorithm: to generate an initial population of individuals
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

        public List<Individ> CreatePopulation(int n, int k) //  Сreating population: to generate an initial population of individuals with different
        {
            Logger.Get().Debug("called CreatePopulation function.");
            List<Individ> population = new List<Individ>();
            for (int i = 0; i < n; i++)
            {
                Individ ind = new Individ(_individSize);
                switch (k)
                {
                    case 0:
                        ind = DanzigAlgorithm();
                        break;
                    case 1:
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
                text = "";
                for (int j = 0; j < population[i].SIZE; j++)
                    text += population[i].INDIVID[j];
                text += ", ";
            
            }
            Logger.Get().Debug(text);
            return population;
        }

        public List<Individ> PointCrossover(List<Individ> individs, int l)// Point crossover: single-point and two-point crossovers
        {
            Logger.Get().Debug("called point crossover.");
            List<Individ> population = new List<Individ>();
            int k = 0;
            int r = 0;

            for (int i = 0; i < individs.Count; i++)
            {
                for (int j = 0; j < individs.Count; j++)
                {
                    if (i != j)
                    {
                        k = _rand.Next(_individSize - 2);
                        r = _rand.Next(k + 1, _individSize - 1);
                        Individ ind = new Individ(_individSize);

                        if (l == 1)
                        {
                            for (int s = 0; s < k; s++)
                                ind.INDIVID[s] = individs[j].INDIVID[s];
                            for (int s = k; s < _individSize; s++)
                                ind.INDIVID[s] = individs[i].INDIVID[s];
                        }
                        if (l == 2)
                        {
                            for (int s = 0; s < k; s++)
                                ind.INDIVID[s] = individs[j].INDIVID[s];
                            for (int s = k; s < r; s++)
                                ind.INDIVID[s] = individs[i].INDIVID[s];
                            for (int s = r; s < _individSize; s++)
                                ind.INDIVID[s] = individs[j].INDIVID[s];
                        }
                        population.Add(ind);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }

        public List<Individ> UniformCrossover(List<Individ> individs)//Uniform crossover
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
                        Individ ind = new Individ(_individSize);
                        for (int s = 0; s < _individSize; s++)
                        {
                            k = _rand.Next(10);
                            if (k > 5)
                                ind.INDIVID[s] = individs[j].INDIVID[s];
                            else
                                ind.INDIVID[s] = individs[i].INDIVID[s];
                        }
                        population.Add(ind);
                    }
                }
            }
            population.AddRange(individs);
            return population;
        }

        //mutations: point mutation, Inversion, Translocation, Saltation
        public List<Individ> PointMutation(List<Individ> individs)
        {
            Logger.Get().Debug("called point mutation.");
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
                   ind.INDIVID[k] = (individs[i].INDIVID[k] == 0) ? 1 : 0;
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
                k = _rand.Next(_individSize - 2);
                r = _rand.Next(k + 1, _individSize - 1);
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

        private int[] PenaltyFunctionMethod(List<Individ> individs)// Penalty function method
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
            string text = "";
            for(int i = 0; i < individs.Count; i++)
            {
                if (scalledFitnessFunctions[i] == minCost)
                    scalledFitnessFunctions[i] = 0;
                else if (scalledFitnessFunctions[i] > 0)
                 {
                   if(GetWeight(individs[i]) <= LIMIT)
                       text += "|normal " + scalledFitnessFunctions[i];
                   else
                       text +="|p (positive) " + scalledFitnessFunctions[i];
                 }
                 else
                 {
                     text +="|p (negative) " + scalledFitnessFunctions[i];
                     cost = scalledFitnessFunctions[i];
                     coeffA = averageCost / (cost - averageCost);
                     coeffB = averageCost * cost / (cost - averageCost);
                     scalledFitnessFunctions[i] = Convert.ToInt32(coeffA * scalledFitnessFunctions[i] + coeffB);
                     text += " average - " + averageCost + " cost -  " + cost + " coefficient A - " + coeffA + ", coefficient B - " + coeffB + "|scalled - " + scalledFitnessFunctions[i];
                 }
            }
            Logger.Get().Debug(text);
            return scalledFitnessFunctions;
        }

        public List<Individ> BettaTournamentSelection(List<Individ> individs, int populationCount, int beta) //Betta Tournament selection
        {
            int[] costs = PenaltyFunctionMethod(individs);
            Logger.Get().Debug("called betta - tournament selection.");
            List<Individ> population = new List<Individ>();
            string text = "";
            for (int j = 0; j < populationCount; j++)
            {
                int c = 0;
                int count = 0;
                List<int> costList = new List<int>();
                List<int> number = new List<int>();
                for (int i = 0; i < individs.Count; i++)
                {
                    c = _rand.Next(2);
                    text += " c - " + c;
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
                        text += "maxCost - " + maxCost + "individ - " + individ.ConvertToString();
                    }
                }
                population.Add(individ);
            }
            Logger.Get().Debug(text);
            text = "";
            for (int i = 0; i < population.Count; i++)
            {
                text = "";
                for (int j = 0; j < population[i].SIZE; j++)
                    text += population[i].INDIVID[j];
                text += " COST: " + GetCost(population[i]);
                int weight = GetWeight(population[i]);
                Logger.Get().Debug(text);
                if (weight > LIMIT)
                    Logger.Get().Warning("Weight is invalid. - " + weight);
            }
            return population;
        }

        public List<Individ> LinearRankSelection(List<Individ> individs, int c)//Linear rank selection
        {
            int[] costs = PenaltyFunctionMethod(individs);
            Logger.Get().Debug("called linear rank selection.");
            List<Individ> population1 = new List<Individ>();
            List<Individ> population2 = new List<Individ>();
            List<int> costList = new List<int>();
            int[] rang = new int[individs.Count];
            double[] n = new double[individs.Count]; // expected numbers
            n[individs.Count - 1] = _rand.NextDouble() + 1.1;
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
                            population1.Add(individs[j]);
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
                    population1.Add(population2[k]);
                }
            }
            List<Individ> population = (population2.Count > c) ? population1 : population2;
            string text = "";
            for (int i = 0; i < population.Count; i++)
            {
                text = "";
                for (int j = 0; j < population[i].SIZE; j++)
                    text += population[i].INDIVID[j];
                text += " COST: " + GetCost(population[i]);
                Logger.Get().Debug(text);
                int weight = GetWeight(population[i]);
                if (weight > LIMIT)
                    Logger.Get().Warning("Weight is invalid. - " + weight);
            }
            return population;
        }
    }
}
