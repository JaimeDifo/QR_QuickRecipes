using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using QuickRecipes.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using CsvHelper;
using System.Globalization;

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
    }
}
