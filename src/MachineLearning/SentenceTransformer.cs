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

namespace QuickRecipes.WebSite.MachineLearning
{
    public class SentenceTransformer
    {
        public SentenceTransformer(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string CSVFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "Producttraindataset.csv");

        public IEnumerable<ProductTrainDataset> GetProductTrainDatasets()
        {
            using (var reader = new StreamReader(Path.Combine(WebHostEnvironment.WebRootPath, "data", "Producttraindataset.csv")))


            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var records = csv.GetRecords<ProductTrainDataset>().ToList();
                return records;
            }
        }

        public void TrainModel()
        {

            // Create ML context
            var context = new MLContext();

            // Load data
            var data = context.Data.LoadFromTextFile<ProductTrainDataset>((Path.Combine(WebHostEnvironment.WebRootPath, "data", "Producttraindataset.csv")));

            // Split data into training and testing sets
            var trainTestSplit = context.Data.TrainTestSplit(data);

            // Define data preparation pipeline
            var pipeline = context.Transforms.Conversion.MapValueToKey("Label", "SomeLabelColumn")
                .Append(context.Transforms.Text.FeaturizeText("SomeTextColumn", "Features"))
                .Append(context.Transforms.NormalizeMinMax("SomeNumericColumn", "NormalizedNumericColumn"))
                .Append(context.Transforms.Concatenate("Features", "NormalizedNumericColumn"));

            // Define model and evaluate
            var trainer = context.MulticlassClassification.Trainers.LbfgsMaximumEntropy();
            var trainerPipeline = pipeline.Append(trainer);
            var trainedModel = trainerPipeline.Fit(trainTestSplit.TrainSet);
            var predictions = trainedModel.Transform(trainTestSplit.TestSet);
            var metrics = context.MulticlassClassification.Evaluate(predictions);

            // Use trained model to predict new data
           // var predictionEngine = context.Model.CreatePredictionEngine<SomeData, SomePrediction>(trainedModel);
           // var prediction = predictionEngine.Predict(new SomeData { SomeTextColumn = "some text", SomeNumericColumn = 0.5f });

        }
    }

}
