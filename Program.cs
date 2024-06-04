using PageReplacement.Algorithms;

namespace PageReplacement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> exampleTests = [1, 2, 3, 4, 1, 2, 5, 1, 2, 3, 4, 5];
            int exampleDifferentSites = 6;
            int exampleMemorySize = 4;

            List<IAlgorithm> algorithms = new List<IAlgorithm>();
            algorithms.Add(new FIFO());
            algorithms.Add(new OPT());
            algorithms.Add(new LRU());
            algorithms.Add(new ApproxLRU());
            algorithms.Add(new RAND());
            SimulationHandler simulation = new SimulationHandler(0, exampleDifferentSites, exampleMemorySize, algorithms);

            simulation.SetTests(exampleTests);
            simulation.Run(true);

            Random ran = new Random();

            for(int i = 1; i <= 10; i++)
            {
                simulation = new SimulationHandler(i, 100, ran.Next(1, 100), algorithms);
                simulation.GenerateTests(ran.Next(1, 100000));
                simulation.Run(true);
            }

            for (int i = 11; i <= 20; i++)
            {
                simulation = new SimulationHandler(i, 100, ran.Next(1, 100), algorithms);
                simulation.GenerateTests2(ran.Next(1, 100), ran.Next(1, 1000));
                simulation.Run(true);
            }

            Console.ReadKey();
        }
    }
}
