using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager.Algorithms;

namespace TaskManager
{
    public class TaskContainer : IEnumerable<Task>
    {
        private static long IDS = 0;
        private static int s_maxTaskDuration = 2000;
        private static int s_maxTaskDelay = 1000000;

        public List<Task> tasks;
        public List<TaskSummary> algorithmsSummary;
        public String? Name { get; set; }

        public double Density { get; set; } // average number of task units until last task 
        public double AverageDuration { get; set; } // average duration of task

        public long NumberOfTasks { get; set; }
        public long LongestTask { get; set; }
        public long LatestTask { get; set; }
        public long TotalDuration { get; set; }

        public TaskContainer(long numberOfTasks, string? name = null)
        {
            Task.IDCounter = 0;
            algorithmsSummary = new List<TaskSummary>();
            Name = (name == null) ? "#" + IDS++ : name;
            NumberOfTasks = numberOfTasks;
            tasks = new List<Task>();

            Random random = new Random();
            for(long i = 0; i < NumberOfTasks; i++)
            {
                long delay = random.Next(s_maxTaskDelay+1);
                int offset = random.Next(1, s_maxTaskDuration);
                long duration = random.Next(1 + offset, s_maxTaskDuration+1);
                tasks.Add(new Task(delay, duration));
            }

            AnalyzeTaskContainer();
        }

        public TaskContainer(List<Task> tasks, string? name = null)
        {
            Name = (name == null) ? "#" + IDS++ : name;
            NumberOfTasks = tasks.Count;
            algorithmsSummary = new List<TaskSummary>();
            this.tasks = tasks;
            AnalyzeTaskContainer();
        }

        public TaskContainer()
        {
            Console.WriteLine("Podaj nazwe przypadkow testowych: ");
            String? name = Console.ReadLine();
            Name = (name == null || name.Count() == 0) ? "#" + IDS++ : name;
            algorithmsSummary = new List<TaskSummary>();
            tasks = new List<Task>();
            Console.WriteLine("Podaj liczbe taskow: ");
            NumberOfTasks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Podaj opis {NumberOfTasks} taskow w nowych liniach (delay, duration): ");

            for(long i = 0;i < NumberOfTasks; i++)
            {
                String line = Console.ReadLine();

                List<String> lineList = line.Split(' ').ToList();
                long delay = Convert.ToInt32(lineList[0]);
                long duration = Convert.ToInt32(lineList[1]);
                tasks.Add(new Task(delay, duration));
            }

            AnalyzeTaskContainer();
        }

        private void AnalyzeTaskContainer()
        {
            LatestTask = 0;
            LongestTask = 0;
            TotalDuration = 0;

            foreach (var task in tasks)
            {
                long delay = task.Delay;
                long duration = task.Duration;
                if (delay > LatestTask) LatestTask = delay;
                if (duration > LongestTask) LongestTask = duration;

                TotalDuration += duration;
            }

            AverageDuration = (NumberOfTasks == 0) ? 0 : (double)(TotalDuration) / NumberOfTasks;
            Density = (double)TotalDuration / (LatestTask+1);
            tasks = tasks.OrderBy(task => task.Delay).ToList();
        }

        public void Run(Algorithm algorithm)
        {
            int idx = 0;
            long time = 0;

            while (idx < NumberOfTasks || !algorithm.IsPossibleEnd())
            {
                List<Task> tasksToHandle = new List<Task>();
                while(idx < NumberOfTasks && tasks[idx].Delay == time)
                {
                    tasksToHandle.Add(tasks[idx++].clone());
                }
                algorithm.Next(tasksToHandle, time++);
            }
            var executedTasks = algorithm.GetExecutedTasks();

            foreach(Task task in executedTasks)
            {
                task.Duration = tasks[task.Id].Duration;
            }

            algorithmsSummary.Add(new TaskSummary(algorithm.AlgorithmName, executedTasks.OrderBy(task => task.Delay).ToList()));   
        }

        public void CompareAlgorithms()
        {
            Console.WriteLine("Fastest Task Executed By: ");
            List<Tuple<Task, String>> fastestTasks = new List<Tuple<Task, String>>();

            for(int i = 0; i < tasks.Count; i++)
            {
                fastestTasks.Add(new Tuple<Task, String>(tasks[i].clone(), ""));
            }

            foreach(TaskSummary summary in algorithmsSummary)
            {
                for(int i = 0; i < tasks.Count; i++)
                {
                    if (fastestTasks[i].Item1.ManageTime == null || summary.tasks[i].ManageTime < fastestTasks[i].Item1.ManageTime)
                    {
                        fastestTasks[i] = new Tuple<Task, String>(summary.tasks[i], summary.AlgorithmName);
                    }
                }
            }

            foreach(var item in fastestTasks)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2);
            }
            
        }

        public override string ToString()
        {
            String retStr = $"""
                ------- Package: {Name}
                Density: {Density}
                Average Duration: {AverageDuration}
                Total Duration: {TotalDuration}
                Longest Task: {LongestTask}
                Tasks: {NumberOfTasks}
                -------
                """;

            foreach(TaskSummary taskSummary in algorithmsSummary)
            {
                retStr += taskSummary.ToString() + "\n";
            }

            return retStr;
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
