using DiskSimulation.Algorithms;
using System.Runtime.ExceptionServices;

namespace DiskSimulation
{
    internal class Program
    {
        static List<DiskTask> test1 = new List<DiskTask>()
        {
            new DiskTask{Position = 98, TimeAppearance = 1, Deadline = 45},
            new DiskTask{Position = 183, TimeAppearance = 2, Deadline = 9000},
            new DiskTask{Position = 37, TimeAppearance = 3, Deadline = 34},
            new DiskTask{Position = 122, TimeAppearance = 4, Deadline = 94},
            new DiskTask{Position = 14, TimeAppearance = 5, Deadline = 31},
            new DiskTask{Position = 124, TimeAppearance = 6, Deadline = 180},
            new DiskTask{Position = 65, TimeAppearance = 7, Deadline = 40000},
            new DiskTask{Position = 67, TimeAppearance = 8, Deadline = 25000},
        };

        static void RunFirstStrategy(SimulationHandler simulationHandler, List<IManagebleAlgorithm> algorithms, String packageName)
        {
            Random random = new Random();
            int tasks = random.Next(5000);
            int diskSize = 100;
            int startPosition = random.Next(diskSize);
            simulationHandler.DiskSize = diskSize;
            simulationHandler.StartPosition = startPosition;
            simulationHandler.Generate(tasks, random.Next(10000), 10000);
            //simulationHandler.SetTests(test1);

            var stats = new Statistics.StatisticsBasic(packageName, tasks, diskSize, startPosition);
            algorithms.ForEach(algorithm =>
            {
                int headMove;
                int jumps;

                (headMove, jumps) = simulationHandler.RunBasic(algorithm);
                stats.Add(algorithm.Name, headMove, jumps);
            });

            Console.WriteLine(stats.ToString());
            stats.GenerateGraph();
        }

        static void RunSecondStrategy(SimulationHandler simulationHandler, List<IManagebleAlgorithm> algorithms, String packageName)
        {
            Random random = new Random();
            int tasks = random.Next(10000);
            int diskSize = 1000;
            int startPosition = random.Next(diskSize);
            simulationHandler.DiskSize = diskSize;
            simulationHandler.StartPosition = startPosition;
            simulationHandler.Generate(tasks, 1, 100000);
            //simulationHandler.SetTests(test1);

            var stats = new Statistics.StatisticsRealTime(packageName + "_a", tasks, diskSize, startPosition);
            var stats2 = new Statistics.StatisticsBasic(packageName + "_b", tasks, diskSize, startPosition);
            algorithms.ForEach(algorithm =>
            {
                int headMove;
                int jumps;
                int executedTasks;
                int notExecutedTasks;

                (headMove, jumps, executedTasks, notExecutedTasks) = simulationHandler.RunRealTime(algorithm);
                stats.Add(algorithm.Name, headMove, jumps, executedTasks, notExecutedTasks);
                stats2.Add(algorithm.Name, headMove, jumps);
            });

            Console.WriteLine(stats.ToString());
            stats.GenerateGraph();
            stats2.GenerateGraph();
        }
        static void Main(string[] args)
        {
            SimulationHandler simulationHandler = new SimulationHandler();
            List<IManagebleAlgorithm> algorithms = new List<IManagebleAlgorithm>();
            algorithms.Add(new FCFS());
            algorithms.Add(new SSTF());
            algorithms.Add(new Scan());
            algorithms.Add(new CScan());

            List<IManagebleAlgorithm> algorithmsRT = new List<IManagebleAlgorithm>();
            algorithmsRT.Add(new EDF());
            algorithmsRT.Add(new FDSCAN());

            for(int i = 0; i < 100; i++)
            {
                RunFirstStrategy(simulationHandler, algorithms, "#" + i.ToString());
                RunSecondStrategy(simulationHandler, algorithmsRT, "#" + i.ToString());
            }
            Console.ReadKey();
        }
    }
}
