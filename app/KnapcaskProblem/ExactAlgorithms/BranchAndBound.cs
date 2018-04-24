using System.Collections.Generic;
using KnapsackProblem;

namespace Algorithm
{
    public struct Node
    {
        public int level;
        public long cost;
        public long weight;
        public long bound;
    }

    public interface IFS
    {
        IContainer<T> CreateContainer<T>();
    }
    public class DFS : IFS
    {
        public IContainer<T> CreateContainer<T>()
        {
            return new DFSContainer<T>();
        }
    }

    public class BFS : IFS
    {
        public IContainer<T> CreateContainer<T>()
        {
            return new BFSContainer<T>();
        }
    }


    public class BranchAndBound : IExactAlgorithm
    {
        private List<Item> m_items;
        private long m_capacity;
        private ITotalBound m_upperBound = new U3Bound();
        private IBound m_lowerBound = new GreedyLowerBound();
        private IFS m_fs = new DFS();

        public BranchAndBound(IData data)
        {
            Init(data);
        }
        public BranchAndBound(IData data, ITotalBound upBound, IBound lowBound)
        {
            Init(data);
            m_upperBound = upBound;
            m_lowerBound = lowBound;
        }
        public BranchAndBound(IData data, IFS fs)
        {
            Init(data);
            m_fs = fs;
        }
        public BranchAndBound(IData data, ITotalBound upBound, IBound lowBound, IFS fs)
        {
            Init(data);
            m_upperBound = upBound;
            m_lowerBound = lowBound;
            m_fs = fs;
        }
        public long Run()
        {
            var container = m_fs.CreateContainer<Node>();
            var first = new Node()
            {
                level = -1,
                cost = 0,
                weight = 0
            };

            container.Add(first);

            long lowerBound = 0;
            long upperBound = m_upperBound.Calculate(m_items, m_capacity);

            while (container.Count() != 0)
            {
                var current = container.Peek();
                container.Remove();

                var next = new Node()
                {
                    level = current.level + 1
                };

                if (current.level == m_items.Count - 1) continue;

                for (var count = m_items[next.level].maxCount; count >= 0; --count)
                {
                    next.weight = current.weight + m_items[next.level].weight * count;
                    next.cost = current.cost + m_items[next.level].cost * count;

                    if (next.weight <= m_capacity && next.cost > lowerBound)
                    {
                        lowerBound = next.cost;
                    }

                    next.bound = m_lowerBound.Calculate(next, m_items, m_capacity);
                    if (next.cost == upperBound)
                    {
                        return next.cost;
                    }
                    else if (next.bound > lowerBound)
                    {
                        container.Add(next);
                    }
                }
            }
            return lowerBound;
        }
        private void Init(IData data)
        {
            m_items = Helpers.GetItems(data);
            m_capacity = data.Capacity;
        }
    }
}
