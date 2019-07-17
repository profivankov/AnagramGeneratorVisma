using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;

namespace AnagramSolver.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
            var wordRepository = new FileWordRepository();
            var wordList = wordRepository.GetDictionary(null);
            var list = wordList.Values.SelectMany(x => x).ToList();

            var connect = new SqlConnection();
            connect.ConnectionString = "Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=AnagramSolverDB; Integrated Security=true;";
            connect.Open();

            var cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NVarChar));
            foreach (string word in list)
            {
                cmd.Parameters["@word"].Value = word;
                cmd.CommandText = "INSERT INTO Words (Word)" + "VALUES (@word)";
                cmd.ExecuteNonQuery();
            }

            connect.Close();
            Console.WriteLine("Done.");    
    }
}
}
