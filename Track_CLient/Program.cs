using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Track_CLient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:5044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/issue");

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var issues = await response.Content.ReadFromJsonAsync<IEnumerable<IssueDto>>();

                foreach (var issue in issues)
                {
                    Console.WriteLine(issue.Title);
                }
            }
            else
            {
                Console.WriteLine("No results");
            }

            Console.ReadLine();
        }
    }
}
