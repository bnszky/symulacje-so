using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Strategies
{
    public class Proportional : IStrategy
    {
        public string Name => "Proportional";

        public (int, int) CheckIfExpand()
        {
            return (0, 0);
        }

        public (int, int) CheckIfReduce()
        {
            return (0, 0);
        }

        public int? DeleteProcess()
        {
            return null;
        }

        public int[] Init(int[] differentPages, int memorySize)
        {
            int totalPages = 0;
            foreach (int pages in differentPages)
            {
                totalPages += pages;
            }

            int[] memoryAllocation = new int[differentPages.Length];

            for (int i = 0; i < differentPages.Length; i++)
            {
                double proportion = (double)differentPages[i] / totalPages;
                memoryAllocation[i] = (int)Math.Round(proportion * memorySize);
            }

            int allocatedMemory = 0;
            foreach (int mem in memoryAllocation)
            {
                allocatedMemory += mem;
            }


            int diff = memorySize - allocatedMemory;
            if (diff != 0)
            {
                memoryAllocation[0] += diff;
            }

            return memoryAllocation;
        }

        public void ProcessTask(int process, int type, bool isError)
        {
            return;
        }

        public void SetCurrentTrashingRate(int process, double trashingRate)
        {
            return;
        }
    }
}
