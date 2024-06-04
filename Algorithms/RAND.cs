using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public class RAND : IAlgorithm
    {
        public string Name => "RAND";

        int[] memory;
        List<int> pages;
        public int Add(int index)
        {
            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == -1)
                {
                    memory[i] = pages[index];
                    return i;
                }
            }

            Random random = new Random();

            int ind = random.Next(memory.Length);

            memory[ind] = pages[index];
            return ind;
        }

        public void Init(int differentPages, int memorySize, List<int> pages)
        {
            memory = new int[memorySize];
            for (int i = 0; i < memorySize; i++) memory[i] = -1;
            this.pages = pages;
        }

        public void Update(int index)
        {
            return;
        }
    }
}
