using System;

namespace KnapsackProblemData
{
    public interface IData
    {
        void Fill();
        string GetStringType();
    }

    public abstract class AData: IData
    {
        protected int[] m_cost;
        protected int[] m_weight;
        protected int m_range;
        protected Random m_rand;
        public int  m_maxWeight;

        public int[] COST
        {
            get { return m_cost; }
        }

        public int[] WEIGHT
        {
            get { return m_weight; }
        }

        public int MAX_WEIGHT
        {
            get { return m_maxWeight; }    
        }

        public AData(int size,int range)
        {
            m_range = range;
            m_cost = new int[size];
            m_weight = new int[size];
            m_rand = new Random(System.DateTime.Now.Millisecond);
        }
        public abstract void Fill();

        public abstract string GetStringType();
    }
    public class TestData : AData //204
    {
        public TestData(int size, int range) : base(size, range) { }
        public override void Fill()
        {
            int[] tmpCost = {21, 19, 27, 3, 24, 30, 6, 13, 2, 21, 26, 26, 24, 1, 10 };
            int[] tmpWeight = {2, 26, 23, 6, 19,  9,  8,  20, 11,  1, 17, 21, 7, 20, 11};
            m_cost = tmpCost;
            m_weight = tmpWeight;
            m_maxWeight = 80; 
        }

        public override string GetStringType()
        {
               return "test";
        }
    }

    public class UncorrData : AData
    {
        public UncorrData(int size, int range) : base(size, range) {}
        public override void Fill()
        {
            int summaryWeight = 0;
            for( int i = 0; i < m_weight.Length; i++)
            {
                m_weight[i] = m_rand.Next(1, m_range + 1);
                m_cost[i] = m_rand.Next(1, m_range + 1);
                summaryWeight += m_weight[i];
            }
            m_maxWeight = (int)(summaryWeight * 0.8); 
        }

        public override string GetStringType()
        {
            return "uncorreled";
        }
    }

    public class WeaklyCorrData : AData
    {
        public WeaklyCorrData(int size, int range) : base(size, range) { }

        private int getCost(int weight)
        {
            int cost = m_rand.Next((weight - m_range / 10), (weight + m_range / 10) + 1);
            if (cost < 1)
                cost = getCost(weight);
            return cost;
        }

        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < m_weight.Length; i++)
            {
                m_weight[i] = m_rand.Next(1, m_range + 1);
                m_cost[i] = getCost(m_weight[i]);
                summaryWeight += m_weight[i];
            }
            m_maxWeight = (int)(summaryWeight * 0.8); 
        }

        public override string GetStringType()
        {
            return "weakly_correled";
        }
    }

    public class StronglyCorrData : AData
    {
        public StronglyCorrData(int size, int range) : base(size, range) {}

        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < m_weight.Length; i++)
            {
                m_weight[i] = m_rand.Next(1, m_range + 1);
                m_cost[i] = m_weight[i] + 10;
                summaryWeight += m_weight[i];
            }
            m_maxWeight = (int)(summaryWeight * 0.8);
        }

        public override string GetStringType()
        {
            return "strongly_correled";
        }
    }

    public class SubsetSumData : AData
    {
        public SubsetSumData(int size, int range) : base(size, range) {}

        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < m_weight.Length; i++)
            {
                m_weight[i] = m_rand.Next(1, m_range + 1);
                m_cost[i] = m_weight[i];
                summaryWeight += m_weight[i];
            }
            m_maxWeight = (int)(summaryWeight * 0.8);
        }

        public override string GetStringType()
        {
            return "subset_summ";
        }
    }
}
