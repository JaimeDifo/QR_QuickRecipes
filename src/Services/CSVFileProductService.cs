using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using QuickRecipes.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;
using System;
using CsvHelper;
using System.Globalization;

namespace QuickRecipes.WebSite.Services
{
    public class CSVFileProductService
    {
        public CSVFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string CSVFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

        public IEnumerable<Product> GetProducts()
        {
            using (var reader = new StreamReader(Path.Combine(WebHostEnvironment.WebRootPath, "data", "Products.csv")))


            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var records = csv.GetRecords<Product>().ToList();
                return records;
            }
        }






            

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(CSVFileName);

            JsonSerializer.Serialize<IEnumerable<Product>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                products
            );
        }
        public void doStuff()
        {
            var jsonString = File.ReadAllText(Path.Combine(WebHostEnvironment.WebRootPath, "data", "Producttraindataset.csv"));
          //  var recipes = JsonSerializer.Deserialize<List<ProductTrainDataset>>(jsonString);


            var mlContext = new MLContext();
           // var data =  mlContext.Data.LoadFromEnumerable(recipes);
            var data = mlContext.Data.LoadFromTextFile<ProductTrainDataset>(Path.Combine(WebHostEnvironment.WebRootPath, "data","Producttraindataset.csv"), hasHeader: true, separatorChar: ',');

            /* var dataPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ProductTrainDataset.Recipe_Name))
                  .Append(mlContext.Transforms.Text.FeaturizeText("IngredientsFeatures", nameof(ProductTrainDataset.Cleaned_Ingredients)))
                  .Append(mlContext.Transforms.Text.FeaturizeText("ContextFeatures", nameof(ProductTrainDataset.Instructions)))
                  .Append(mlContext.Transforms.Concatenate("InputFeatures", "IngredientsFeatures", "ContextFeatures"))
                  .Append(mlContext.Transforms.NormalizeMinMax("InputFeatures"))
                  .Append(mlContext.Transforms.Conversion.MapValueToKey("Features", "InputFeatures"))
                  .Append(mlContext.Transforms.NormalizeMinMax("Features"));*/

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", "Cleaned_Ingredients")
            .Append(mlContext.Transforms.Concatenate("Features", "Features"))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Recipe_Name"))
            .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression());
            


            var trainer = mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated();
            var trainingPipeline = pipeline.Append(trainer).Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

            var model = pipeline.Fit(data);

        
        }
    }

    
}
