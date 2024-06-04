using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public class LRU : IAlgorithm
    {
        public string Name => "LRU";

        int[] memory;
        List<int> pages;
        int[] lastQuery;

        public int Add(int index)
        {
            int oldestIndex = index;
            int oldestNumber = -1;
            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == -1)
                {
                    memory[i] = pages[index];
                    return i;
                }
                else
                {
                    if (oldestIndex > lastQuery[memory[i]])
                    {
                        oldestIndex = lastQuery[memory[i]];
                        oldestNumber = memory[i];
                    }
                }
            }

            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == oldestNumber)
                {
                    memory[i] = pages[index];
                    return i;
                }
            }

            throw new Exception("Isn't possible to add new page");
        }

        public void Init(int differentPages, int memorySize, List<int> pages)
        {
            memory = new int[memorySize];
            for (int i = 0; i < memorySize; i++) memory[i] = -1;
            this.pages = pages;
            lastQuery = new int[differentPages];
        }

        public void Update(int index)
        {
            lastQuery[pages[index]] = index;
        }
    }
}
