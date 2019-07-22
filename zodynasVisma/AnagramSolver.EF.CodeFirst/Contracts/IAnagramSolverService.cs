using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IAnagramSolverService
    {
        IList<string> GetAnagrams(string input);
        int MaxResultAmount();
    }
}
