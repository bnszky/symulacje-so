using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    internal class FCFS : IManagebleAlgorithm
    {
        public string Name => "FCFS";
        Queue<DiskTask> queue = new Queue<DiskTask>();
        int currentPosition = 0;
        int? destination = null;

        public void GetTasks(List<DiskTask> tasks)
        {
            foreach (DiskTask task in tasks)
            {
                queue.Enqueue(task);
            }  
        }

        public void Init(int diskSize, int startPosition)
        {
            currentPosition = startPosition;
            destination = null;
            queue = new Queue<DiskTask>();
        }

        public bool IsExecutedTasks()
        {
            return (currentPosition == destination) ;
        }

        public int Move()
        {
            if(destination == currentPosition)
            {
                queue.Dequeue();
                destination = null;
            }
            if (destination == null && queue.Count > 0)
            {
                destination = queue.Peek().Position;
            }

            if(destination == null || destination == currentPosition) { return 0; }
            if (destination > currentPosition)
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
