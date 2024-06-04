using PageReplacement.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement
{
    public class SimulationHandler
    {
        int id;
        int differentPages;
        int memorySize;
        int pages;
        List<IAlgorithm> algorithms;
        Statistics statistics;

        List<int> tests = [];
        public SimulationHandler(int id, int differentPages, int memorySize, List<IAlgorithm> algorithms) {
            this.differentPages = differentPages;
            this.memorySize = memorySize;
            this.algorithms = algorithms;
            this.id = id;
        }

        public void GenerateTests(int number)
        {
            pages = number;
            Random random = new Random();
            for(int i = 0; i < pages; i++)
            {
                tests.Add(random.Next(differentPages));
            }
        }

        public void GenerateTests2(int numberOfSections, int sizeOfSection)
        {
            pages = numberOfSections*sizeOfSection;
            Random random = new Random();
            for(int i = 0; i < numberOfSections; i++)
            {
                for(int j = 0; j < sizeOfSection; j++)
                {
                    if(i%2 == 0)
                    {
                        tests.Add(random.Next(differentPages));
                    } else
                    {
                        int a = random.Next(differentPages-2);
                        int b = a + random.Next(2);
                        tests.Add(random.Next(a, b));
                    }
                }
            }
        }

        public void SetTests(List<int> _tests)
        {
            pages = _tests.Count;
            tests = _tests;
        }

        public void Run(bool isWriteInConsole)
        {
            this.statistics = new Statistics(id, differentPages, memorySize, pages);
            foreach (IAlgorithm algorithm in algorithms)
            {
                Run(algorithm);
            }
            statistics.GenerateGraph();
            if (isWriteInConsole)
            {
                Console.WriteLine(statistics);
            }
        }
        private void Run(IAlgorithm algorithm)
        {
            int pageErrors = 0;
            List<int> testsClone = [];
            foreach(int i in tests)
            {
                testsClone.Add(i);
            }

            algorithm.Init(differentPages, memorySize, testsClone);

            bool[] isInMemory = new bool[differentPages];
            int[] memory = new int[memorySize];
            for (int i = 0; i < memorySize; i++) memory[i] = -1;

            for(int i = 0; i < tests.Count; i++)
            {
                if (!isInMemory[tests[i]])
                {
                    pageErrors++;
                    int index = algorithm.Add(i);
                    if (memory[index] != -1) isInMemory[memory[index]] = false;
                    memory[index] = tests[i];
                    isInMemory[memory[index]] = true;
                }
                algorithm.Update(i);
            }

            statistics.Add(algorithm.Name, pageErrors);
        }
    }
}
