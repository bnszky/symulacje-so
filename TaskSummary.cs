using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskSummary : IEnumerable<Task>
    {
        public double AverageWaitingTime { get; }
        public long LongestWaitingTime { get; }
        public long TotalWaitingTime { get; }
        public long EndOfExecution { get; }
        public String AlgorithmName { get; set; }

        public List<Task> tasks;

        public TaskSummary(String name, List<Task> tasks)
        {
            AlgorithmName = name;
            this.tasks = tasks;

            foreach(Task task in tasks)
            {
                long waitingTime = (long)task.ManageTime - task.Duration;

                if (waitingTime > LongestWaitingTime) LongestWaitingTime = waitingTime;
                if (task.EndTime > EndOfExecution) EndOfExecution = (long)task.EndTime;
                TotalWaitingTime += waitingTime;
            }

            AverageWaitingTime = (tasks.Count() == 0) ? 0 : (double)TotalWaitingTime/tasks.Count();
        }

        public override string ToString()
        {
            return $"""
                ------- Algorithm {AlgorithmName} Summary:
                Average Waiting Time: {AverageWaitingTime}
                Total Waiting Time: {TotalWaitingTime}
                Longest Waiting Time: {LongestWaitingTime}
                End of Execution: {EndOfExecution}
                -------
                """;
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
