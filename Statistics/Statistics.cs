using Microsoft.CodeAnalysis.CSharp.Syntax;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DiskSimulation.Statistics
{
    public abstract class Statistics
    {
        private string pathToDirectory = Directory.GetCurrentDirectory();

        protected string idName;
        protected int width = 400;
        protected int height = 300;
        protected Plot plot;

        int tasksAmount;
        int diskLength;
        int startPosition;

        protected Statistics(string _idName, int tasksAmount, int diskLength, int startPosition)
        {
            idName = _idName;
            plot = new Plot();

            this.tasksAmount = tasksAmount;
            this.diskLength = diskLength;
            this.startPosition = startPosition;

            pathToDirectory = pathToDirectory.Replace("\\bin\\Debug\\net8.0", "");
            pathToDirectory = Path.Combine(pathToDirectory, "Graphs\\");

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }
        }

        protected void AddAnnotation()
        {
            plot.Add.Annotation("Tasks: " + tasksAmount + "\n" +
                                "Disk Length: " + diskLength + "\n" +
                                "Start Position: " + startPosition, Alignment.UpperRight);
        }

        protected void Save(Plot plot)
        {
            plot.SavePng(pathToDirectory + idName + ".png", width, height);
        }
        public abstract void GenerateGraph();
    }
}
