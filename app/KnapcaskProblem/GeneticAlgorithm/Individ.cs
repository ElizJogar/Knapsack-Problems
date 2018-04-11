using System;

namespace Algorithm
{
    public class Individ
    {
        private int m_size;
        public int[] GENOTYPE
        {
            get; set;
        }
        public int SIZE
        {
            get { return m_size; }
        }
        public Individ(int size)
        {
            m_size = size;
            GENOTYPE = new int[m_size];
        }

        public override bool Equals(object obj)
        {
            int sum = 0;
            Individ newIndivid = (Individ)obj;
            for (int i = 0; i < GENOTYPE.Length; i++)
                sum += GENOTYPE[i] ^ newIndivid.GENOTYPE[i];
            return sum == 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string Str()
        {
            return string.Join("", GENOTYPE);
        }
    }

    public class IndividEx : Individ, IComparable
    {
        public IndividEx(int size) : base(size) { }
        public int COST { set; get; }
        public int WEIGHT { set; get; }
        public int CompareTo(Object obj)
        {
            var other = (IndividEx)obj;
            if (COST < other.COST)
            {
                return -1;
            }
            else if (COST == other.COST)
            {
                return 0;
            }
            else if (COST > other.COST)
            {
                return 1;
            }
            return 0;
        }
    }
}
