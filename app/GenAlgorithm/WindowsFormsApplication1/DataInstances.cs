using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public abstract class ADataInstances
    {
        protected int[] _cost;
        protected int[] _weight;
        protected int _range;
        protected Random _rand;
        public int  _maxWeight;

        public int[] COST
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public int[] WEIGHT
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int MAX_WEIGHT
        {
            get { return _maxWeight; }    
        }

        public ADataInstances(int size,int range)
        {
            _range = range;
            _cost = new int[size];
            _weight = new int[size];
            _rand = new Random(System.DateTime.Now.Millisecond);
            Fill();
        }

        public abstract void Fill();
    }
    public class TestDataInstances : ADataInstances //204
    {
        public TestDataInstances(int size, int range) : base(size, range) { }
       
        public override void Fill()
        {
            int[] tmpCost = { 18, 24, 14, 22, 13, 18, 16, 30, 25, 4, 27, 12, 19, 24, 22 };
            int[] tmpWeight = { 1, 11, 16, 6, 25, 16, 25, 9, 14, 13, 13, 4, 20, 5, 9 };
            _cost = tmpCost;
            _weight = tmpWeight;
            _maxWeight = 74; 
        }
    }

    public class UncorrDataInstances : ADataInstances
    {
        public UncorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void Fill()
        {
            int summaryWeight = 0;
            for( int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range + 1);
                _cost[i] = _rand.Next(1, _range + 1);
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8); 
        }
    }

    public class WeaklyCorrDataInstances : ADataInstances
    {
        public WeaklyCorrDataInstances(int size, int range) : base(size, range) { }

        private int getCost(int weight)
        {
            int cost = _rand.Next((weight - _range / 10), (weight + _range / 10) + 1);
            if (cost < 1)
                cost = getCost(weight);
            return cost;
        }

        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range + 1);
                _cost[i] = getCost(_weight[i]);
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8); 
        }
    }

    public class StronglyCorrDataInstances : ADataInstances
    {
        public StronglyCorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range + 1);
                _cost[i] = _weight[i] + 10;
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8);
        }
    }

    public class SubsetSumDataInstances : ADataInstances
    {
        public SubsetSumDataInstances(int size, int range) : base(size, range) {}
        public override void Fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range + 1);
                _cost[i] = _weight[i];
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8);
        }
    }
}
