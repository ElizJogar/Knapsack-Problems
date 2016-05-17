using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public abstract class DataInstances
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

        public DataInstances(int size,int range)
        {
            _range = range;
            _cost = new int[size];
            _weight = new int[size];
            _rand = new Random(System.DateTime.Now.Millisecond);
            fill();
        }

        public abstract void fill();
    }
    public class TestDataInstances : DataInstances
    {
        public TestDataInstances(int size, int range) : base(size, range) { }
       
        public override void fill()
        {
            int[] tmpCost =  { 21, 19, 27, 3, 24, 30, 6, 13, 2, 21, 26, 26, 24, 1, 10 };
            int[] tmpWeight = { 2, 26, 23, 6, 19, 9, 8, 20, 11, 1, 17, 21, 7, 20, 11 };
            _cost = tmpCost;
            _weight = tmpWeight;
            _maxWeight = 80; 
        }
    }

    public class UncorrDataInstances : DataInstances
    {
        public UncorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void fill()
        {
            int summaryWeight = 0;
            for( int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _rand.Next(1, _range);
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8); 
        }
    }

    public class WeaklyCorrDataInstances : DataInstances
    {
        public WeaklyCorrDataInstances(int size, int range) : base(size, range) { }

        private int getCost(int weight)
        {
            int cost = _rand.Next((weight - _range / 10), (weight + _range / 10));
            if (cost < 1)
                cost = getCost(weight);
            return cost;
        }

        public override void fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = getCost(_weight[i]);
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8); 
        }
    }

    public class StronglyCorrDataInstances : DataInstances
    {
        public StronglyCorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _weight[i] + 10;
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8);
        }
    }

    public class SubsetSumDataInstances : DataInstances
    {
        public SubsetSumDataInstances(int size, int range) : base(size, range) {}
        public override void fill()
        {
            int summaryWeight = 0;
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _weight[i];
                summaryWeight += _weight[i];
            }
            _maxWeight = (int)(summaryWeight * 0.8);
        }
    }
}
