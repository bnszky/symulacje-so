using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2.Strategies
{
    public interface IStrategy
    {
        string Name { get; }
        int[] Init(int[] differentPages, int memorySize);
        void ProcessTask(int process, int type, bool isError);
        void SetCurrentTrashingRate(int process, double trashingRate);
        (int, int) CheckIfExpand();
        (int, int) CheckIfReduce();
        int? DeleteProcess();
    }
}
