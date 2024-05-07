using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Algorithms
{
    public class FCFSAlgorithm : Algorithm
    {
        Queue<Task> queue;
        Task? currentTask;
        List<Task> executedTasks;
        public string AlgorithmName { get; } = "FCFS";

        public FCFSAlgorithm()
        {
            queue = new Queue<Task>();
            executedTasks = new List<Task>();
        }
        public bool IsPossibleEnd()
        {
            return (currentTask == null || currentTask.Duration == 0) && queue.Count == 0;
        }

        public void Next(List<Task> tasks, long time)
        {
            foreach (var task in tasks) { queue.Enqueue(task); }

            if (currentTask != null)
            {
                currentTask.Duration--;

                if (currentTask.Duration == 0)
                {
                    currentTask.End(time);
                    executedTasks.Add(currentTask);
                }

                if (currentTask.Duration <= 0 && queue.Count > 0) currentTask = queue.Dequeue();
            }
            else if (queue.Count > 0)
            {
                currentTask = queue.Dequeue();
            }
        }

        public List<Task> GetExecutedTasks()
        {
            return executedTasks;
        }
    }
}
