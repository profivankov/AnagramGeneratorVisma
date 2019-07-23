using AnagramSolver.EF.CodeFirst.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IWordRepository
    {
        List<string> GetAllWords();
        List<string> GetSkipWords(int? pageNum);
        List<string> GetSearchResults(string input);
        void AddWord(Words words);
        void RemoveWord(Words words);
        int GetWordID(string input);
        int GetTotalItems();


        //bool EditWord(string wordToEdit, string newWord);
    }
}
