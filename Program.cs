using TaskManager.Algorithms;

namespace TaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var taskContainer = new TaskContainer(10000);

                /*foreach(Task task in taskContainer)
                {
                    Console.WriteLine(task.ToString());
                }*/

                taskContainer.Run(new FCFSAlgorithm());
                taskContainer.Run(new SJFAlgorithm());
                taskContainer.Run(new RRAlgorithm());

                Console.WriteLine(taskContainer);
                //taskContainer.CompareAlgorithms();
            }

            Console.ReadKey();
        }
    }
}
