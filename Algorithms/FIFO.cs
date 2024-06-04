using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public class FIFO : IAlgorithm
    {
        public string Name => "FIFO";

        int[] memory;
        Queue<int> queue;
        List<int> pages;
        public int Add(int index)
        {
            for(int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == -1)
                {
                    memory[i] = pages[index];
                    queue.Enqueue(pages[index]);
                    return i;
                }
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
            for(int i = 0;i < memorySize; i++) memory[i] = -1;
            this.pages = pages;
        }

        public void Update(int index)
        {
            return;
        }
    }
}
