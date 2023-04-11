using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using QuickRecipes.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using CsvHelper;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace QuickRecipes.WebSite.Services
{
    public class CSVIngredientService
    {
        public CSVIngredientService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string CSVFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "Ingredients.csv");

        public IEnumerable<Ingredients> GetIngredients()
        {
            using (var reader = new StreamReader(Path.Combine(WebHostEnvironment.WebRootPath, "data", "Ingredients.csv")))


            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var records = csv.GetRecords<Ingredients>().ToList();
                return records;
            }
        }

        public async Task<string> GetRecommmendedProductsAsync(string ingredient)
        {
            HttpClient client = new HttpClient();
            string url = "localhost:8000/cosine/" + ingredient;
            var builder = new UriBuilder(url);
            //HttpResponseMessage response = await client.GetAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            var response = await client.SendAsync(request);
            int[] productIds = new int[5];
            if (response.IsSuccessStatusCode)
            {
                var result  = await response.Content.ReadAsStringAsync();
                productIds = JsonSerializer.Deserialize<int[]>(result);
            }
            return "";
        }
    }
}
