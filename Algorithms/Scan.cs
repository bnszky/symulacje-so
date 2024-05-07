using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    internal class Scan : IManagebleAlgorithm
    {
        public string Name => "SCAN";
        int diskSize = 0;
        int currentPosition = 0;
        int direction = -1;

        public void GetTasks(List<DiskTask> tasks)
        {
            return;
        }

        public void Init(int diskSize, int startPosition)
        {
            this.diskSize = diskSize;
            this.currentPosition = startPosition;
            direction = -1;
        }

        public bool IsExecutedTasks()
        {
            return true;
        }

        public int Move()
        {
            if(currentPosition == 0)
            {
                direction = 1;
            }
            if(diskSize == currentPosition + 1)
            {
                direction = -1;
            }

            currentPosition += direction;
            return direction;
        }
    }
}
