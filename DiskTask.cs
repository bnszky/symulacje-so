using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation
{
    public class DiskTask : IComparable<DiskTask>
    {
        public int Deadline { get; set; }
        public int Position { get; set; }
        public int TimeAppearance { get; set; }

        public int CompareTo(DiskTask? other)
        {
            if (other == null) return 1;
            return Deadline - other.Deadline;
        }
    }

    public class DiskTaskComparer : IComparer<DiskTask>
    {
        public int Compare(DiskTask x, DiskTask y)
        {
            if (x == null || y == null)
            {
                throw new InvalidOperationException();
            }

            // Sort by deadline
            return x.Deadline.CompareTo(y.Deadline);
        }
    }
}
