using System;
using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public class Individ : IComparable
    {
        protected List<IGen> m_genes;
        protected IData m_data;
        protected Dictionary<int, int[]> m_indices;

        public Individ(IData data)
        {
            m_data = data;
            m_genes = new List<IGen>();
            m_indices = new Dictionary<int, int[]>();
            var totalSize = 0;
            for (var i = 0; i < data.Cost.Length; ++i)
            {
                m_genes.Add(new Gen(data.Cost[i], data.Weight[i], data.ItemMaxCounts[i]));                
                for (var j = 0; j < m_genes[i].Size(); ++j)
                {
                    m_indices.Add(totalSize + j, new int[2] { i, j });
                }
                totalSize += m_genes[i].Size();
            }
        }
        public int Size()
        {
            return m_data.Cost.Length;
        }
        public int FlatSize()
        {
            return m_indices.Count;
        }
        public IGen GetGen(int index)
        {
            return m_genes[index];
        }
        public void SetGen(int index, IGen gen)
        {
            m_genes[index] = gen;
        }
        public bool GetBit(int index)
        {
            var indices = m_indices[index];
            return m_genes[indices[0]].GetBit(indices[1]);
        }
        public void SetBit(int index, bool value)
        {
            var indices = m_indices[index];
            m_genes[indices[0]].SetBit(indices[1], value);
        }
        public virtual long GetCost()
        {
            long cost = 0;
            m_genes.ForEach(gen => cost += gen.GetCost());
            return cost;
        }
        public virtual long GetWeight()
        {
            long weight = 0;
            m_genes.ForEach(gen => weight += gen.GetWeight());
            return weight;
        }
        public IData GetData()
        {
            return m_data;
        }
        public int CompareTo(Object obj)
        {
            var other = (Individ)obj;
            var cost = GetCost();
            var otherCost = other.GetCost();
            if (cost < otherCost)
            {
                return -1;
            }
            else if (cost == otherCost)
            {
                return 0;
            }
            else if (cost > otherCost)
            {
                return 1;
            }
            return 0;
        }
        public override bool Equals(object obj)
        {
            var newIndivid = (Individ)obj;
            for (var i = 0; i < FlatSize(); ++i)
            {
                if (GetBit(i) != newIndivid.GetBit(i)) return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string Str()
        {
            var str = "";
            foreach(var gen in m_genes)
            {
                foreach (var bit in gen.Get()) 
                {
                    str += (bool)bit ? '1' : '0';
                }
            }
            return str;
        }
    }

    public class CustomIndivid
    {
        private long m_cost;
        private long m_weight;
        private Individ m_individ;

        public CustomIndivid(Individ individ) : base(individ.GetData())
        {
            for (var i = 0; i < m_genes.Count; ++i)
            {
                m_genes[i].Set(individ.GetGen(i).Get());
            }
            m_individ = individ;
        }
        public void SetCost(long cost)
        {
            m_cost = cost;
        }
        public override long GetCost()
        {
            return m_cost;
        }
        public void SetWeight(long weight)
        {
            m_weight = weight;
        }
        public override long GetWeight()
        {
            return m_weight;
        }

        public Individ Original()
        {
            return m_individ;
        }
    }

}
