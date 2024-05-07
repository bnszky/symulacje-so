using Microsoft.FSharp.Collections;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Statistics
{
    public class StatisticsBasic : Statistics
    {
        List<string> algorithmNames;
        List<int> headMoves;
        List<int> jumps;
        public StatisticsBasic(string _idName, int tasks, int length, int startPos) : base(_idName + "_basic", tasks, length, startPos)
        {
            algorithmNames = new List<string>();
            headMoves = new List<int>();
            jumps = new List<int>();
        }

        public void Add(string algorithmName, int headMove, int jump)
        {
            algorithmNames.Add(algorithmName);
            headMoves.Add(headMove);
            jumps.Add(jump);
        }
        public override void GenerateGraph()
        {
            List<Tick> ticks = new List<Tick>();

            for (int i = 0; i < algorithmNames.Count; i++)
            {
                plot.Add.Bar(position: i, value: headMoves[i]);
                ticks.Add(new Tick(i, algorithmNames[i]));
            }

            plot.Legend.IsVisible = true;
            plot.Legend.Location = Alignment.UpperLeft;
            plot.Legend.ManualItems.Add(new() { Label = "Head Moves" });

            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            plot.Axes.Bottom.MajorTickStyle.Length = 0;
            plot.HideGrid();

            AddAnnotation();
            Save(plot);
        }

        public override string ToString()
        {
            String ret = "Tests: " + idName + "\n";
            for(int i = 0; i < algorithmNames.Count(); i++)
            {
                ret += algorithmNames[i] + ": (Moves) " + headMoves[i] + ", (Jumps) " + jumps[i] + "\n";
            }
            return ret;
        }
    }
}
