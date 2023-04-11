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
using Microsoft.AspNetCore.Mvc;
using Net.Codecrete.QrCodeGenerator;
using System.Text;

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

        public async Task<List<Product>> GetRecommmendedProductsAsync(string ingredient)
        {
            HttpClient client = new HttpClient();
            string url = "127.0.0.1:8080/cosine/" + ingredient + "/";
            var builder = new UriBuilder(url);
            //HttpResponseMessage response = await client.GetAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            var response = client.Send(request);
            List<int> productIds = new List<int>();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                productIds = JsonSerializer.Deserialize<List<int>>(result);
            }

            var Service = new CSVFileProductService(WebHostEnvironment);
            var RecommendedProducts = new List<Product>();
            foreach(int id in productIds)
            {
                RecommendedProducts.Add(Service.GetProducts().Where(n => n.Id == id.ToString()).First<Product>());
            }
            
            return RecommendedProducts;
           
        }

    }
}
