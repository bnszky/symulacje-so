using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Strategies
{
    internal class WSS : IStrategy
    {
        public string Name => "WSS";

        private const int maxTime = 10;
        private int[] times;
        private int[] wss;
        private int[] isInMemory;
        private int[] memoryAllocation;
        private bool[] isActive;
        private Queue<int>[] queue;
        private int memorySize;

        private int wssSum = 0;
        public (int, int) CheckIfExpand()
        {
            return (0, 0);
        }

        public (int, int) CheckIfReduce()
        {
            return (0, 0);
        }

        public int[] Init(int[] differentPages, int memorySize)
        {
            times = new int[differentPages.Length];
            wss = new int[differentPages.Length];
            isInMemory = new int[differentPages.Sum()];
            queue = new Queue<int>[differentPages.Length];
            for (int i = 0; i < differentPages.Length; i++) queue[i] = new Queue<int>();
            isActive = new bool[differentPages.Length];
            for (int i = 0; i < differentPages.Length; i++) isActive[i] = true;
            this.memorySize = memorySize;

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
            return memoryAllocation;
        }

        private int FindLowestWSS()
        {
            int mini = int.MaxValue;
            int index = -1;
            for(int i = 0; i < wss.Length; i++)
            {
                if (wss[i] < mini && isActive[i])
                {
                    mini = wss[i];
                    index = i;
                }
            }

            return index;
        }

        public void ProcessTask(int process, int type, bool isError)
        {
            wssSum -= wss[process];

            if (times[process] >= maxTime)
            {
                int elem = queue[process].Dequeue();
                isInMemory[elem]--;
                times[process]--;
                if (isInMemory[elem] == 0)
                {
                    wss[process]--;
                }
            }

            queue[process].Enqueue(type);
            isInMemory[type]++;
            times[process]++;
            if (isInMemory[type] == 1)
            {
                wss[process]++;
            }

            wssSum += wss[process];
        }

        public int? DeleteProcess() {
            if(wssSum > memorySize)
            {
                int index = FindLowestWSS();
                wssSum -= wss[index];
                return index;
            }

            return null;
        }

        public void SetCurrentTrashingRate(int process, double trashingRate)
        {
            return;
        }
    }
}
