using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Statistics
{
    public class StatisticsRealTime : Statistics
    {
        List<string> algorithmNames;
        List<int> headMoves;
        List<int> jumps;
        List<int> executedTasks;
        List<int> notExecutedTasks;
        public StatisticsRealTime(string idName, int tasks, int length, int startPos) : base(idName + "_rtime", tasks, length, startPos)
        {
            algorithmNames = new List<string>();
            headMoves = new List<int>();
            jumps = new List<int>();
            executedTasks = new List<int>();
            notExecutedTasks = new List<int>();
        }

        public void Add(string algorithmName, int headMove, int jump, int executedTask, int notExecutedTask)
        {
            algorithmNames.Add(algorithmName);
            headMoves.Add(headMove);
            jumps.Add(jump);
            executedTasks.Add(executedTask);
            notExecutedTasks.Add(notExecutedTask);
        }
        public override void GenerateGraph()
        {
            List<Tick> ticks = new List<Tick>();
            ScottPlot.Palettes.Category10 palette = new();
            List<Bar> bars = new List<Bar>();

            int pos = 0;
            for (int i = 0; i < algorithmNames.Count; i++)
            {
                bars.Add(new Bar() { Position = pos++, Value = executedTasks[i], FillColor = palette.GetColor(0) });
                ticks.Add(new Tick(pos, algorithmNames[i]));
                bars.Add(new Bar() { Position = pos++, Value = notExecutedTasks[i], FillColor = palette.GetColor(1) });
                pos++;
            }

            plot.Legend.IsVisible = true;
            plot.Legend.Location = Alignment.UpperLeft;
            plot.Legend.ManualItems.Add(new() { Label = "Executed Tasks", FillColor = palette.GetColor(0) });
            plot.Legend.ManualItems.Add(new() { Label = "Not Executed Tasks", FillColor = palette.GetColor(1) });

            plot.Add.Bars(bars);
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            plot.Axes.Bottom.MajorTickStyle.Length = 0;
            plot.HideGrid();

            AddAnnotation();
            Save(plot);
        }

        public override string ToString()
        {
            String ret = "Tests: " + idName + "\n";
            for (int i = 0; i < algorithmNames.Count(); i++)
            {
                ret += algorithmNames[i] + ": (Moves) " + headMoves[i] + ", (Jumps) " + jumps[i] + ", (executed) " + executedTasks[i] + "\n";
            }
            return ret;
        }
    }
}
