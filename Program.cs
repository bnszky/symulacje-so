using PageReplacement2.Algorithms;
using PageReplacement2.Strategies;

namespace PageReplacement2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> nums = [0, 0, 1, 1, 0, 2, 2, 0, 0];
            List<int> types = [0, 1, 3, 3, 2, 5, 5, 0, 1];
            List<Process> exampleTests = [];

            int[] processes = [3, 2, 1];

            for(int i = 0; i <  nums.Count; i++)
            {
                exampleTests.Add(new Process { Num = nums[i], Type = types[i] });
            }

            List<IStrategy> strategies = [new Equal(), new Proportional(), new PPF(), new WSS()];

            // many different sites - 3 processes - small memorySize
            for(int i = 0; i < 100; i++)
            {
                SimulationHandler simulation = new SimulationHandler(i, 10, strategies);

                simulation.GenerateTests(3, 1000, 10000);
                simulation.Run();
                Console.WriteLine(simulation);
            }

            // many different sites - 4 processes - big memorySize
            for (int i = 100; i < 200; i++)
            {
                SimulationHandler simulation = new SimulationHandler(i, 1000, strategies);

                simulation.GenerateTests(4, 1000, 10000);
                simulation.Run();
                Console.WriteLine(simulation);
            }

            // many different sites - 4 processes - medium memorySize
            for (int i = 200; i < 300; i++)
            {
                SimulationHandler simulation = new SimulationHandler(i, 500, strategies);

                simulation.GenerateTests(4, 1000, 10000);
                simulation.Run();
                Console.WriteLine(simulation);
            }

            Console.ReadKey();
        }
    }
}
