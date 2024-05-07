using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiskSimulation.Algorithms;

namespace DiskSimulation
{
    internal class SimulationHandler
    {
        public int DiskSize { get; set; }
        public int StartPosition { get; set; }
        private List<DiskTask> _tasks;
        int taskNumber;

        public void SetTests(List<DiskTask> list)
        {
            _tasks = list.OrderBy(task => task.TimeAppearance).ToList();
            taskNumber = _tasks.Count;
        }
        public void Generate(int number, int maxTime, int maxDeadline)
        {
            _tasks = new List<DiskTask>();
            taskNumber = number;

            maxDeadline = Math.Max(maxDeadline, maxTime);

            Random random = new Random();

            for (int i = 0; i < number; i++)
            {
                int time = random.Next(maxTime);
                _tasks.Add(new DiskTask { Position = random.Next(DiskSize), Deadline = random.Next(time, maxDeadline), TimeAppearance = time});
            }

            _tasks = _tasks.OrderBy(task => task.TimeAppearance).ToList();
        }

        private List<DiskTask> AddTasksInCurrentTime(Queue<DiskTask> tasks, List<DiskTask>[] proceededTasks, int time)
        {
            List<DiskTask> currentTasks = new List<DiskTask>();
            while (tasks.Count > 0 && tasks.Peek().TimeAppearance <= time)
            {
                DiskTask task = tasks.Dequeue();

                currentTasks.Add(task);
                if (proceededTasks[task.Position] == null)
                {
                    proceededTasks[task.Position] = new List<DiskTask>();
                }
                proceededTasks[task.Position].Add(task);
            }

            return currentTasks;
        }

        public (int, int) RunBasic(IManagebleAlgorithm algorithm)
        {
            int headMove = 0;
            int jumpMove = 0;

            //

            algorithm.Init(DiskSize, StartPosition);

            List<DiskTask>[] proceededTasks = new List<DiskTask>[DiskSize];
            for(int i = 0; i < DiskSize; i++) proceededTasks[i] = new List<DiskTask>();
            int proceededTasksNumber = 0;

            int currentPosition = StartPosition;
            int time = 0;

            Queue<DiskTask> tasks = new Queue<DiskTask>();

            foreach (DiskTask task in _tasks)
            {
                tasks.Enqueue(task);
            }

            while (proceededTasksNumber < taskNumber)
            {
                List<DiskTask> currentTasks = AddTasksInCurrentTime(tasks, proceededTasks, time);

                algorithm.GetTasks(currentTasks);

                if (algorithm.IsExecutedTasks())
                {
                    proceededTasksNumber += proceededTasks[currentPosition].Count();
                    proceededTasks[currentPosition] = new List<DiskTask>();
                }

                int move = algorithm.Move();
                if (move != 0) {
                    headMove++;
                }

                currentPosition += move;
                if(currentPosition >= DiskSize)
                {
                    jumpMove++;
                    currentPosition = 0;
                }
                if(currentPosition < 0)
                {
                    jumpMove++;
                    currentPosition = DiskSize - 1;
                }

                time++;
            }
            return (headMove, jumpMove);
        }

        public (int, int, int, int) RunRealTime(IManagebleAlgorithm algorithm)
        {
            int headMove = 0;
            int jumpMove = 0;
            int executedTasks = 0;

            //

            algorithm.Init(DiskSize, StartPosition);

            int currentPosition = StartPosition;
            int time = 0;

            List<DiskTask>[] proceededTasks = new List<DiskTask>[DiskSize];
            for (int i = 0; i < DiskSize; i++) proceededTasks[i] = new List<DiskTask>();
            Queue<DiskTask> tasks = new Queue<DiskTask>();

            int maxDeadline = 0;

            foreach(DiskTask task in _tasks) {
                tasks.Enqueue(task);
                if(task.Deadline > maxDeadline) { maxDeadline = task.Deadline; }
            }

            while (time < maxDeadline)
            {
                List<DiskTask> currentTasks = AddTasksInCurrentTime(tasks, proceededTasks, time);

                algorithm.GetTasks(currentTasks);

                if (algorithm.IsExecutedTasks())
                {
                    executedTasks += proceededTasks[currentPosition].Where(task => (task.Deadline >= time)).Count();
                    proceededTasks[currentPosition] = new List<DiskTask>();
                }

                int move = algorithm.Move();
                if (move != 0)
                {
                    headMove++;
                }

                currentPosition += move;
                if (currentPosition >= DiskSize)
                {
                    jumpMove++;
                    currentPosition = 0;
                }

                time++;
            }

            return (headMove, jumpMove, executedTasks, taskNumber-executedTasks);
        }
    }
}
