using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Algorithms
{
    internal class CScan : IManagebleAlgorithm
    {
        public string Name => "CSCAN";

        public void GetTasks(List<DiskTask> tasks)
        {
            return;
        }

        public void Init(int diskSize, int startPosition)
        {
            return;
        }

        public bool IsExecutedTasks()
        {
            return true;
        }

        public int Move()
        {
            return 1;
        }
    }
}
