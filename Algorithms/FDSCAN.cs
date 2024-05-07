using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    internal class FDSCAN : IManagebleAlgorithm
    {
        public string Name => "FDSCAN";
        public SortedSet<DiskTask> currentTasks = new SortedSet<DiskTask>();
        int currentPosition = 0;
        int time = 0;

        public void GetTasks(List<DiskTask> tasks)
        {
            foreach (DiskTask task in tasks)
            {
                currentTasks.Add(task);
            }
        }

        public void Init(int diskSize, int startPosition)
        {
            currentPosition = startPosition;
            currentTasks = new SortedSet<DiskTask>();
            time = 0;
        }

        public bool IsExecutedTasks()
        {
            return (currentTasks.Count > 0 && currentTasks.Min.Position == currentPosition);
        }

        public int Move()
        {
            while (currentTasks.Count > 0 && (currentTasks.Min.Deadline < time || currentTasks.Min.Position == currentPosition ||
                Math.Abs(currentTasks.Min.Position - currentPosition) + time > currentTasks.Min.Deadline))
            {
                currentTasks.Remove(currentTasks.Min);
            }

            time++;
            if (currentTasks.Count == 0) return 0;

            if (currentTasks.Min.Position > currentPosition)
            {
                currentPosition++;
                return 1;
            }
            else
            {
                currentPosition--;
                return -1;
            }
        }
    }
}
