using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Algorithms
{
    public class FIFO
    {
        public string Name = "FIFO";

        public int Size = 0;
        private Queue<int> queue = new Queue<int>();
        private bool[] isInMemory;

        public FIFO(int pagesNumber, int initialSize) {
            Size = initialSize;
            isInMemory = new bool[pagesNumber];
        }

        private void RemoveAndAdd(int val)
        {
            Remove();
            Add(val);
        }

        private void Add(int val)
        {
            if (queue.Count < Size)
            {
                queue.Enqueue(val);
                isInMemory[val] = true;
            }
        }

        private void Remove()
        {
            if (queue.Count > 0)
            {
                int f = queue.Dequeue();
                isInMemory[f] = false;
            }
        }

        // if return true = page error occured
        public bool TryAdd(int val)
        {
            if (Size == 0) return true;

            if (isInMemory[val])
            {
                return false;
            }

            if (queue.Count == Size)
            {
                RemoveAndAdd(val);
                return true;
            }

            Add(val);
            return false;
        }

        public void Expand(int size)
        {
            Size += size;
        }

        public void Reduce(int size)
        {
            Size -= size;
            Size = Math.Max(Size, 0);

            while(queue.Count > Size) {
                Remove();
            }
        }

    }
}
