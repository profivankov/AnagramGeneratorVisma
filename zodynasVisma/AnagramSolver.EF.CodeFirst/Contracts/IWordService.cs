using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IWordService
    {
        Dictionary<string, List<string>> GetDictionary(int? pageNum);
        List<string> SearchRepository(string input);
        int TotalItems();
        bool HasWordBeenAdded(string input);
        bool HasWordBeenRemoved(string input);
    }
}
