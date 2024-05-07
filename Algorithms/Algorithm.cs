using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Algorithms
{
    public interface Algorithm
    {
        string AlgorithmName { get; }

        void Next(List<Task> tasks, long time);

        bool IsPossibleEnd();
        List<Task> GetExecutedTasks();
    }
}
