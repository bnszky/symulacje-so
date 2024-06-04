using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScottPlot;

namespace PageReplacement
{
    public class Statistics
    {
        private string pathToDirectory = Directory.GetCurrentDirectory();

        private int id;
        private int width = 400;
        private int height = 300;
        private Plot plot;

        int differentPages;
        int memorySize;
        int pages;

        List<string> algorithmNames = [];
        List<int> pageErrors = [];

        public Statistics(int id, int differentPages, int memorySize, int pages)
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

        public void Add(string algorithmName, int pageError)
        {
            algorithmNames.Add(algorithmName);
            pageErrors.Add(pageError);
        }

        private void AddAnnotation()
        {
            plot.Add.Annotation("Different pages: " + differentPages + "\n" +
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

            for (int i = 0; i < algorithmNames.Count; i++)
            {
                plot.Add.Bar(position: i, value: pageErrors[i]);
                ticks.Add(new Tick(i, algorithmNames[i]));
            }

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
                ret += algorithmNames[i] + ": " + pageErrors[i] + "\n";
            }
            return ret;
        }
    }
}
