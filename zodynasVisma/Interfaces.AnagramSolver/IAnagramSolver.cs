using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.Contracts
{
    public interface IAnagramSolver
    {
        IList<string> GetAnagrams(string[] myWords);
    }
}
