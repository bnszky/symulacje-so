using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageReplacement.Algorithms
{
    public interface IAlgorithm
    {
        string Name { get; }
        void Init(int differentPages, int memorySize, List<int> pages);
        int Add(int index);
        void Update(int index);
    }
}
