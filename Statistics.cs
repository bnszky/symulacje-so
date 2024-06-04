using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PageReplacement2.Strategies;
using ScottPlot;

namespace PageReplacement2
{
    public class Statistics
    {
        private string pathToDirectory = Directory.GetCurrentDirectory();

        private int id;
        private int width = 400;
        private int height = 300;
        private Plot plot;

        int[] differentPages;
        int memorySize;
        int pages;

        List<string> algorithmNames = [];
        List<List<int>> pageErrors = [];
        List<int> trashings = [];

        public Statistics(int id, int[] differentPages, int memorySize, int pages)
        {
            this.id = id;
            this.differentPages = differentPages;
            this.memorySize = memorySize;
            this.pages = pages;

            pathToDirectory = pathToDirectory.Replace("\\bin\\Debug\\net8.0", "");
            pathToDirectory = Path.Combine(pathToDirectory, "Graphs\\");

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }
        }

        public void Add(string algorithmName, List<int> pageError, int trashing)
        {
            algorithmNames.Add(algorithmName);
            pageErrors.Add(pageError);
            trashings.Add(trashing);
        }

        private void AddAnnotation()
        {
            plot.Add.Annotation("Processes: " + differentPages.Length + "\n" +
                                "Memory Size: " + memorySize + "\n" +
                                "Pages: " + pages, Alignment.UpperRight);
        }

        private void Save(Plot plot)
        {
            plot.SavePng(pathToDirectory + id + ".png", width, height);
        }
        public void GenerateGraph()
        {
            plot = new Plot();
            List<Tick> ticks = new List<Tick>();
            ScottPlot.Palettes.Category10 palette = new();
            List<Bar> bars = new List<Bar>();

            int pos = 0;
            for (int i = 0; i < algorithmNames.Count; i++)
            {
                ticks.Add(new Tick(pos + ((double)pageErrors[i].Count)/2, algorithmNames[i]));
                bars.Add(new Bar() { Position = pos++, Value = trashings[i], FillColor = palette.GetColor(0) });
                for(int j = 0; j < pageErrors[i].Count; j++)
                {
                    bars.Add(new Bar() { Position = pos++, Value = pageErrors[i][j], FillColor = palette.GetColor(j+1) });
                }
                pos++;
            }

            plot.Legend.IsVisible = true;
            plot.Legend.Location = Alignment.UpperLeft;
            plot.Legend.ManualItems.Add(new() { Label = "Trashing", FillColor = palette.GetColor(0) });
            for (int j = 0; j < differentPages.Length; j++)
            {
                plot.Legend.ManualItems.Add(new() { Label = $"P[{differentPages[j]}]", FillColor = palette.GetColor(j+1) });
            }

            plot.Add.Bars(bars);
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            plot.Axes.Bottom.MajorTickStyle.Length = 0;
            plot.HideGrid();

            AddAnnotation();
            Save(plot);
        }
        public override string ToString()
        {
            String ret = "Page Errors for test: " + id + "\n";
            for (int i = 0; i < algorithmNames.Count(); i++)
            {
                ret += algorithmNames[i] + ": \n";
                ret += $"Trashing: {trashings[i]}\n";
                for(int j = 0; j < pageErrors[i].Count; j++)
                {
                    ret += $"P[{differentPages[j]}]: {pageErrors[i][j]}\n";
                }
            }
            return ret;
        }
    }
}
