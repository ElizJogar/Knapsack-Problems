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
        private ITotalBound m_upperBound = new U3Bound();
        private IBound m_lowerBound = new GreedyLowerBound();
        private IFS m_fs = new DFS();

        public BranchAndBound()
        {
        }
        public BranchAndBound(ITotalBound upBound, IBound lowBound)
        {
            m_upperBound = upBound;
            m_lowerBound = lowBound;
        }
        public BranchAndBound(IFS fs)
        {
            m_fs = fs;
        }
        public BranchAndBound(IData data, ITotalBound upBound, IBound lowBound, IFS fs)
        {
            m_upperBound = upBound;
            m_lowerBound = lowBound;
            m_fs = fs;
        }
        public long Run(IData data)
        {
            var items = Helpers.GetItems(data);
            var capacity = data.Capacity;

            var container = m_fs.CreateContainer<Node>();
            var first = new Node()
            {
                level = -1,
                cost = 0,
                weight = 0
            };

            container.Add(first);

            long lowerBound = 0;
            long upperBound = m_upperBound.Calculate(items, capacity);

            while (container.Count() != 0)
            {
                var current = container.Peek();
                container.Remove();

                var next = new Node()
                {
                    level = current.level + 1
                };

                if (current.level == items.Count - 1) continue;

                for (var count = items[next.level].maxCount; count >= 0; --count)
                {
                    next.weight = current.weight + items[next.level].weight * count;
                    next.cost = current.cost + items[next.level].cost * count;

                    if (next.weight > capacity) continue;

                    if (next.cost > lowerBound)
                    {
                        lowerBound = next.cost;
                    }

                    next.bound = m_lowerBound.Calculate(next, items, capacity);
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
    }
}
