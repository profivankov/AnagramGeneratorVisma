using System;
using System.Collections.Generic;
using System.Text;


namespace Interfaces.AnagramSolver
{
    public interface IWordRepository
    {
        Dictionary<string, List<string>> GetDictionary(); // parodomas return type???
    }
}
