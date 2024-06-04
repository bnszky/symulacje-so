using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public class OPT : IAlgorithm
    {
        public string Name => "OPT";

        int[] memory;
        List<int> pages;
        int[] nextByIndex;
        int[] nextByNumber;
        public int Add(int index)
        {
            int lateIndex = -1;
            int lateNumber = -1;
            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == -1)
                {
                    memory[i] = pages[index];
                    return i;
                } 
                else
                {
                    if (nextByNumber[memory[i]] > lateIndex)
                    {
                        lateIndex = nextByNumber[memory[i]];
                        lateNumber = memory[i];
                    }
                }
            }

            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == lateNumber)
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
            nextByIndex = new int[pages.Count];
            nextByNumber = new int[differentPages];

            for(int i = 0; i < differentPages; i++)
            {
                nextByNumber[i] = int.MaxValue;
            }

            for(int i = pages.Count - 1; i >= 0; i--) {
                nextByIndex[i] = nextByNumber[pages[i]];
                nextByNumber[pages[i]] = i;
            }
        }

        public void Update(int index)
        {
            nextByNumber[pages[index]] = nextByIndex[index];
        }
    }
}
