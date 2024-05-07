using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    public interface IManagebleAlgorithm
    {
        string Name { get; }
        void Init(int diskSize, int startPosition);
        void GetTasks(List<DiskTask> tasks);
        bool IsExecutedTasks();
        int Move(); // -1 -> left, 0 -> no move, 1 -> right

    }
}
