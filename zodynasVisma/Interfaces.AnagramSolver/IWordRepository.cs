using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        Dictionary<string, List<string>> GetDictionary(int? pageNum);
        List<string> SearchRepository(string input);
        int GetTotalItems();
        bool AddWord(string input);
        bool RemoveWord(string input);
        bool EditWord(string wordToEdit, string newWord);
    }
}
