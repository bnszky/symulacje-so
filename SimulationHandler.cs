using PageReplacement2.Algorithms;
using PageReplacement2.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement2
{
    public class SimulationHandler
    {
        int id;
        int memorySize;
        Statistics statistics;
        List<IStrategy> strategies;

        List<Process> listOfTasks;
        int[] differentPages;
        int processes;
        int tasksNumber;
        int pagesNumber;

        public SimulationHandler(int id, int memorySize, List<IStrategy> strategies) {
            this.memorySize = memorySize;
            this.id = id;
            this.strategies = strategies;
        }

        private int GetNum(int type)
        {
            int j = 0;
            for(int i = 0; i < pagesNumber; i += differentPages[j-1]) {

                if (i <= type && type < i + differentPages[j]) return j;
                j++;
            }
            return j;
        }

        public void SetTests(int[] differentPages, List<Process> listOfTasks)
        {
            this.differentPages = differentPages;
            this.listOfTasks = listOfTasks;

            this.processes = differentPages.Length;
            this.tasksNumber = listOfTasks.Count;
            this.pagesNumber = 0;
            foreach (int page in differentPages) { pagesNumber+=page; }
        }

        private int[] GenerateDifferentPages(int processes, int pagesNumber)
        {
            Random random = new Random();
            int[] allocations = new int[processes];

            for (int i = 0; i < processes; i++)
            {
                allocations[i] = random.Next(pagesNumber);
            }

            int totalAllocated = allocations.Sum();

            while (totalAllocated != pagesNumber)
            {
                int randomProcess = random.Next(processes);
                while (allocations[randomProcess] <= 1) randomProcess = random.Next(processes);
                int val = totalAllocated > pagesNumber ? -1 : 1;
                allocations[randomProcess] += val;
                totalAllocated += val;
            }

            return allocations;
        }



        public void GenerateTests(int processes, int pagesNumber, int tasksNumber)
        {
            this.processes = processes;
            this.tasksNumber = tasksNumber;
            this.pagesNumber = pagesNumber;

            Random random = new Random();

            this.differentPages = GenerateDifferentPages(processes, pagesNumber);

            this.listOfTasks = new List<Process>();
            for(int i = 0; i < tasksNumber; i++)
            {
                int type = random.Next(pagesNumber);
                int num = GetNum(type);
                listOfTasks.Add(new Process { Num = num, Type = type });
            }
        }

        public void Run()
        {
            statistics = new Statistics(id, differentPages, memorySize, listOfTasks.Count);
            foreach(IStrategy strategy in strategies)
            {
                Run(strategy);
            }
            statistics.GenerateGraph();
        }

        public void Run(IStrategy strategy)
        {
            int[] initialSizes = strategy.Init(differentPages, memorySize);

            List<FIFO> processes = new List<FIFO>();
            List<Queue<bool>> queue = new List<Queue<bool>>();
            int[] time = new int[differentPages.Length];
            int[] errors = new int[differentPages.Length];

            int[] errorsAllTime = new int[differentPages.Length];
            int trashing = 0;

            const int lengthTime = 10;
            const double maxTrashingRate = .5;

            for (int i = 0;i < initialSizes.Length; i++) {
                processes.Add(new FIFO(pagesNumber, initialSizes[i]));
                queue.Add(new Queue<bool>());
            }

            foreach(var task in listOfTasks)
            {
                bool isError = processes[task.Num].TryAdd(task.Type);

                time[task.Num]++;
                queue[task.Num].Enqueue(isError);
                if(isError)
                {
                    errors[task.Num] ++;
                }

                if (time[task.Num] > lengthTime)
                {
                    time[task.Num] --;
                    bool f = queue[task.Num].Dequeue();
                    if(f)
                    {
                        errors[task.Num] --;
                    }
                }

                double currentTrashingRate = (double)errors[task.Num]/time[task.Num];
                if(currentTrashingRate > maxTrashingRate)
                {
                    trashing++;
                }

                if(isError) errorsAllTime[task.Num] ++;

                if (processes[task.Num].Size > 0)
                {
                    strategy.ProcessTask(task.Num, task.Type, isError);
                    strategy.SetCurrentTrashingRate(task.Num, currentTrashingRate);
                }

                int process1;
                int size1;
                (process1, size1) = strategy.CheckIfExpand();
                processes[process1].Expand(size1);

                (process1, size1) = strategy.CheckIfReduce();
                processes[process1].Reduce(size1);

                int? index = strategy.DeleteProcess();
                if (index != null)
                {
                    int size = processes[(int)index].Size;
                    processes[(int)index].Reduce(size);

                    int ind = 0;
                    while(size > 0)
                    {
                        if (processes[ind].Size > 0)
                        {
                            processes[ind].Expand(1);
                            size--;
                        }
                        ind++;
                        if (ind >= processes.Count) ind = 0;
                    }   
                }
            }

            statistics.Add(strategy.Name, errorsAllTime.ToList(), trashing);
        }

        public override string ToString()
        {
            string basicInfo = $"""
                Test: {id}
                Processes: {processes}
                Tasks: {tasksNumber}
                """;

            basicInfo += "\n-----\n";

            int ind = 0;
            for (int i = 0; i < pagesNumber; i += differentPages[ind-1])
            {
                basicInfo += $"Process {ind + 1}: ({i} - {i + differentPages[ind] - 1})\n";
                ind++;
            }

            basicInfo += statistics.ToString();

            return basicInfo;
        }


    }
}
