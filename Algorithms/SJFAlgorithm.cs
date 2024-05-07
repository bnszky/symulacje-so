using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Algorithms
{
    public class SJFAlgorithm : Algorithm
    {
        public string AlgorithmName { get; } = "SJF";

        private SortedSet<Task> tasksInQueue;
        Task? currentTask;
        List<Task> executedTasks;

        public SJFAlgorithm()
        {
            tasksInQueue = new SortedSet<Task>();
            executedTasks = new List<Task>();
        }
        private Task? GetFirstTask()
        {
            if (tasksInQueue.Count <= 0) return null;
            Task retTask = tasksInQueue.Min;
            tasksInQueue.Remove(retTask);
            return retTask;
        }

        public List<Task> GetExecutedTasks()
        {
            return executedTasks;
        }

        public bool IsPossibleEnd()
        {
            return (currentTask == null || currentTask.Duration <= 0) && tasksInQueue.Count <= 0;
        }

        public void Next(List<Task> tasks, long time)
        {
            foreach (var task in tasks) { tasksInQueue.Add(task); }

            if (currentTask != null)
            {
                currentTask.Duration--;
                if (currentTask.Duration == 0)
                {
                    currentTask.End(time);
                    executedTasks.Add(currentTask);
                }

                if (currentTask.Duration <= 0 && tasksInQueue.Count > 0)
                {
                    currentTask = GetFirstTask();
                }
            }
            else if (tasksInQueue.Count > 0)
            {
                currentTask = GetFirstTask();
            }
        }
    }
}
