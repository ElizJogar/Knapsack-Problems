using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public interface IContainer<T>
    {
        void Add(T element);
        T Peek();
        int Count();
        void Remove();
        void Clear();
    }

    public class DFSContainer<T> : IContainer<T>
    {
        private Stack<T> stack;

        public DFSContainer(int count = 0)
        {
            stack = count == 0 ? new Stack<T>() : new Stack<T>(count);
        }

        public void Add(T element)
        {
            stack.Push(element);
        }

        public T Peek()
        {
            return stack.Peek();
        }

        public void Remove()
        {
            stack.Pop();
        }
        public int Count()
        {
            return stack.Count();
        }

        public void Clear()
        {
            stack.Clear();
        }
    }

    public class BFSContainer<T> : IContainer<T>
    {
        private Queue<T> queue;

        public BFSContainer(int count = 0)
        {
            queue = count == 0 ? new Queue<T>() : new Queue<T>(count);
        }

        public void Add(T element)
        {
            queue.Enqueue(element);
        }

        public T Peek()
        {
            return queue.Peek();
        }

        public void Remove()
        {
            queue.Dequeue();
        }

        public int Count()
        {
            return queue.Count();
        }

        public void Clear()
        {
            queue.Clear();
        }
    }
}
