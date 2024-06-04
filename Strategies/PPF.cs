using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Strategies
{
    public class PPF : IStrategy
    {
        public string Name => "PPF";
        private int[] memoryAllocation;
        private double[] trashingRates;

        const double maxTrashingRate = 0.25;
        int? toExpand = null;
        int? toReduce = null;

        public (int, int) CheckIfExpand()
        {
            return ( (toExpand != null) ? ((int)toExpand, 1) : (0, 0) );
        }

        public (int, int) CheckIfReduce()
        {
            return ((toReduce != null) ? ((int)toReduce, -1) : (0, 0));
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

            this.memoryAllocation = memoryAllocation;
            trashingRates = new double[memoryAllocation.Length];
            return memoryAllocation;
        }

        public void ProcessTask(int process, int type, bool isError)
        {
            return;
        }

        private int FindMinTrashingProcess()
        {
            double mini = (double)int.MaxValue;
            int index = -1;
            for(int i = 0; i <  trashingRates.Length; i++)
            {
                if (memoryAllocation[i] > 0 && trashingRates[i] < mini)
                {
                    index = i;
                    mini = trashingRates[i];
                }
            }

            memoryAllocation[index]--;
            return index;
        }

        public void SetCurrentTrashingRate(int process, double trashingRate)
        {
            trashingRates[process] = trashingRate;
            if(trashingRate > maxTrashingRate)
            {
                memoryAllocation[process]++;
                toExpand = process;
                toReduce = FindMinTrashingProcess();
            }
            else
            {
                toExpand = null;
                toReduce = null;
            }
        }

        public int? DeleteProcess()
        {
            return null;
        }
    }
}
