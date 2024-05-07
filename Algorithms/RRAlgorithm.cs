using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Algorithms
{
    internal class RRAlgorithm : Algorithm
    {
        public string AlgorithmName { get; } = "RR";
        static private long s_unitTime = 10; // quantum time

        private List<Task> currentTasks;
        private List<Task> executedTasks;
        private int ind = 0;
        private long k = s_unitTime;

        public RRAlgorithm()
        {
            currentTasks = new List<Task>();
            executedTasks = new List<Task>();
        }
        public List<Task> GetExecutedTasks()
        {
            return executedTasks;
        }

        public bool IsPossibleEnd()
        {
            return currentTasks.Count <= 0;
        }

        public void Next(List<Task> tasks, long time)
        {
            if (currentTasks.Count != 0)
            {

                if (ind >= currentTasks.Count)
                {
                    ind = 0;
                }

                currentTasks[ind].Duration--;
                k--;

                if (currentTasks[ind].Duration == 0)
                {
                    currentTasks[ind].End(time);
                    executedTasks.Add(currentTasks[ind]);
                    currentTasks.RemoveAt(ind);
                    k = s_unitTime;
                }
                else if (k == 0)
                {
                    k = s_unitTime;
                    ind++;
                }
            }
            foreach (var task in tasks) { currentTasks.Add(task); }
        }
    }
}
