using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Strategies
{
    public class Equal : IStrategy
    {
        public string Name => "Equal";

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
            int numProcesses = differentPages.Length;
            int[] memoryAllocation = new int[numProcesses];

            int baseMemory = memorySize / numProcesses;
            int remainingMemory = memorySize % numProcesses;

            for (int i = 0; i < numProcesses; i++)
            {
                memoryAllocation[i] = baseMemory;
            }

            for (int i = 0; i < remainingMemory; i++)
            {
                memoryAllocation[i] += 1;
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
