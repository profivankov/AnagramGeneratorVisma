using System.Collections.Generic;
using System.IO;


namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        Dictionary<string, List<string>> GetDictionary(StreamReader file);
    }
}
