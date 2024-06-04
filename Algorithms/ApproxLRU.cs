using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public class ApproxLRU : IAlgorithm
    {
        public string Name => "ApproxLRU";

        int[] memory;
        bool[] secondChance;
        List<int> pages;
        Queue<int> queue;

        public int Add(int index)
        {
            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == -1)
                {
                    memory[i] = pages[index];
                    queue.Enqueue(pages[index]);
                    return i;
                }
            }

            while(secondChance[queue.Peek()])
            {
                int elem = queue.Dequeue();
                secondChance[elem] = false;
                queue.Enqueue(elem);
            }

            int pageToRemove = queue.Dequeue();

            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == pageToRemove)
                {
                    memory[i] = pages[index];
                    queue.Enqueue(pages[index]);
                    return i;
                }
            }

            throw new Exception("Isn't possible to add new page");
        }

        public void Init(int differentPages, int memorySize, List<int> pages)
        {
            queue = new Queue<int>();
            memory = new int[memorySize];
            secondChance = new bool[differentPages];
            for (int i = 0; i < memorySize; i++) memory[i] = -1;
            this.pages = pages;
        }

        public void Update(int index)
        {
            secondChance[pages[index]] = true;
        }
    }
}
