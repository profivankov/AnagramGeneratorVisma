using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnagramSolver.Tests
{
    public class TestWordRepository : IWordRepository
    {
        public Dictionary<string, List<string>> GetDictionary(StreamReader file)
        {
            return new Dictionary<string, List<string>>
                {
                    { "alsu", new List<string> { "alus", "sula"} },
                    { "gghcvhv", new List<string> { } },
                    { "adeisv", new List<string> { "dievas", "veidas", "vedasi" } },
                    { "aabls", new List<string> {"labas", "balas"} }
                };
            
        }
    }
}
