using Microsoft.CodeAnalysis.CSharp.Syntax;
using ScottPlot.AxisRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    internal class SSTF : IManagebleAlgorithm
    {
        public string Name => "SSTF";
        public SortedSet<int> currentTasks = new SortedSet<int>();
        int currentPosition = 0;
        public void GetTasks(List<DiskTask> tasks)
        {
            foreach(var task in tasks) { 
                currentTasks.Add(task.Position);
            }
        }

        public void Init(int diskSize, int startPosition)
        {
            currentPosition = startPosition;
            currentTasks = new SortedSet<int>();
        }

        public bool IsExecutedTasks()
        {
            return true;
        }

        public int Move()
        {
            while(currentTasks.Count > 0 && currentTasks.Contains(currentPosition)) {
                currentTasks.Remove(currentPosition);
            }

            if (currentTasks.Count == 0) return 0;

            int mini = int.MaxValue;
            int num = -1;

            foreach(var elem in currentTasks)
            {
                if(Math.Abs(elem - currentPosition) < mini)
                {
                    num = elem;
                    mini = Math.Abs(elem - currentPosition);
                }
            }

            if(num > currentPosition)
            {
                currentPosition++;
                return 1;
            } else
            {
                currentPosition--;
                return -1;
            }
        }
    }
}
