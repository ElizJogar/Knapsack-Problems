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

    public class UncorrDataInstances : DataInstances
    {
        public UncorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void fill()
        {
            for( int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _rand.Next(1, _range);
            } 
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
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = getCost(_weight[i]);
            }
        }
    }

    public class StronglyCorrDataInstances : DataInstances
    {
        public StronglyCorrDataInstances(int size, int range) : base(size, range) {}
       
        public override void fill()
        {
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _weight[i] + 10;
            }
        }
    }

    public class SubsetSumDataInstances : DataInstances
    {
        public SubsetSumDataInstances(int size, int range) : base(size, range) {}
        public override void fill()
        {
            for (int i = 0; i < _weight.Length; i++)
            {
                _weight[i] = _rand.Next(1, _range);
                _cost[i] = _weight[i];
            }
        }
    }
}
