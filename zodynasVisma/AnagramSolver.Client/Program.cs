using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AnagramSolver.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GetWordList();
        }
        static async Task GetWordList()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/anagrams/");
                //HTTP GET
                var responseTask = await client.GetAsync("labas");

                if (responseTask.IsSuccessStatusCode)
                {
                    //HttpResponseMessage response = await client.GetAsync("resultList");

                    var readTask = await responseTask.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<string>>(readTask);

                    foreach (var word in result)
                    {
                        Console.WriteLine(word);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}

//https://www.tutorialsteacher.com/webapi/consuming-web-api-in-dotnet-using-httpclient
//kazkodel neveikia nrml jei kitoj klasej