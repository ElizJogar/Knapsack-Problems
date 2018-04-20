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
        IContainer<T> Create<T>();
    }
    public class DFS: IFS
    {
        public IContainer<T> Create<T>()
        {
            return new DFSContainer<T>();
        }
    }

    public class BFS : IFS
    {
        public IContainer<T> Create<T>()
        {
            return new BFSContainer<T>();
        }
    }


    public class BranchAndBound
    {
        private List<Item> m_items;
        private long m_capacity;
        private IBound m_bound = new GreedyUpperBound();
        private IFS m_fs = new DFS();

        public BranchAndBound(IData data)
        {
            Init(data);
        }
        public BranchAndBound(IData data, IBound bound)
        {
            Init(data);
            m_bound = bound;
        }
        public BranchAndBound(IData data, IFS fs)
        {
            Init(data);
            m_fs = fs;
        }
        public BranchAndBound(IData data, IBound bound, IFS fs)
        {
            Init(data);
            m_bound = bound;
            m_fs = fs;
        }
        public long Run()
        {
            var container = new BFSContainer<Node>();
            var first = new Node()
            {
                level = -1,
                cost = 0,
                weight = 0
            };

            container.Add(first);

            long maxCost = 0;
            while (container.Count() != 0)
            {
                var current = container.Peek();
                container.Remove();

                var next = new Node()
                {
                    level = current.level + 1
                };

                if (current.level == m_items.Count - 1) continue;

                next.weight = current.weight + m_items[next.level].weight;
                next.cost = current.cost + m_items[next.level].cost;

                if (next.weight <= m_capacity && next.cost > maxCost)
                {
                    maxCost = next.cost;
                }

                next.bound = m_bound.Calculate(next, m_items, m_capacity);

                // branch with taking next item
                if (next.bound > maxCost) container.Add(next);

                next.weight = current.weight;
                next.cost = current.cost;
                next.bound = m_bound.Calculate(next, m_items, m_capacity);

                // branch with not taking next item
                if (next.bound > maxCost) container.Add(next);
            }
            return maxCost;
        }

        private void Init(IData data)
        {
            m_items = Helpers.GetItems(data);
            m_capacity = data.Capacity;
        }
    }
}
