using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class Task : IComparable<Task>
    {
        public static int IDCounter;
        public int Id { get; }
        public long Delay { get; }
        public long Duration { get; set; }

        public long? EndTime { get; set; }
        public long? ManageTime { get; set; }

        public Task(long delay, long duration)
        {
            Id = IDCounter++;
            Delay = delay;
            Duration = duration;
        }

        private Task(int id, long delay, long duration, long? endTime, long? manageTime) {
            Id = id;
            Delay = delay;
            Duration = duration;
            EndTime = endTime;
            ManageTime = manageTime;
        }

        public void End(long time)
        {
            EndTime = time;
            ManageTime = EndTime - Delay;
        }

        public Task clone()
        {
            return new Task(Id, Delay, Duration, EndTime, ManageTime);
        }

        public override string ToString()
        {
            string retStr = Delay + " " + Duration;
            if(ManageTime != null) retStr += " time: " + ManageTime.ToString();
            return retStr;
        }

        public int CompareTo(Task other)
        {
            if(other == null) return 1;
            if(Duration.CompareTo(other.Duration) == 0) return Id.CompareTo(other.Id);
            return Duration.CompareTo(other.Duration);
        }
    }
}
