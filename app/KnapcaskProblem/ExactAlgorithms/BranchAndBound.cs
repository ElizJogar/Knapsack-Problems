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
        private ITotalBound m_upperBound;
        private IBound m_lowerBound;
        private IFS m_fs = new DFS();

        public BranchAndBound(ITotalBound upBound = null, IBound lowBound = null)
        {
            m_upperBound = upBound ?? new FakeTotalBound();
            m_lowerBound = lowBound ?? new GreedyLowerBound();
        }
        public BranchAndBound(IFS fs, ITotalBound upBound = null, IBound lowBound = null)
        {
            m_fs = fs;
            m_upperBound = upBound ?? new FakeTotalBound();
            m_lowerBound = lowBound ?? new GreedyLowerBound();
        }
        public long Run(IData data)
        {
            var items = Helpers.GetItems(data);
            var capacity = data.Capacity;
            return Run(items, capacity);
        }
        public long Run(List<Item> items, long capacity)
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

                if (items[next.level].cost == 0) continue;

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
