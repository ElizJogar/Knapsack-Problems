using System;
using System.Collections;


namespace Algorithm
{
    public interface IGen
    {
        long GetCost();
        long GetWeight();
        void Set(BitArray value);
        BitArray Get();
        void SetBit(int index, bool value);
        bool GetBit(int index);
        bool Increment();
        bool Decrement();
        int Size();
    }

    public class Gen : IGen
    {
        private BitArray m_value;
        private long m_cost;
        private long m_weight;

        public Gen(long cost, long weight, int size = 1)
        {
            m_cost = cost;
            m_weight = weight;
            m_value = new BitArray(size);
        }

        public void Set(BitArray value)
        {
            m_value = value;
        }
        public void SetBit(int index, bool value)
        {
            m_value[index] = value;
        }

        public BitArray Get()
        {
            return m_value;
        }
        public bool GetBit(int index)
        {
            return m_value[index];
        }
        public bool Increment()
        {
            var decVal = ToInt();
            if (decVal == Math.Pow(2, m_value.Length) - 1)
            {
                return false;
            }
            ++decVal;
            var newValue = new BitArray(new int[] { decVal });
            for (int i = 0; i < m_value.Length; ++i)
            {
                m_value[i] = newValue[i];
            }
            // TODO: use it optimal soultion 
            // for (int i = 0; i < m_value.Length && !(m_value[i] = !m_value[i++]););
            return true;
        }
        public bool Decrement()
        {
            var decVal = ToInt();
            if (decVal == 0)
            {
                return false;
            }
            --decVal;
            var newValue = new BitArray(new int[] { decVal });
            for (int i = 0; i < m_value.Length; ++i)
            {
                m_value[i] = newValue[i];
            }
            return true;
        }

        public long GetCost()
        {
            return ToInt() * m_cost;
        }

        public long GetWeight()
        {
            return ToInt() * m_weight;
        }

        public int Size()
        {
            return m_value.Count;
        }
        protected int ToInt()
        {
            int[] array = new int[1];
            m_value.CopyTo(array, 0);
            return array[0];
        }
    }
}
