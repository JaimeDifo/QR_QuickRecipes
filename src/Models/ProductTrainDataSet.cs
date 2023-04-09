using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.ML.Data;

namespace QuickRecipes.WebSite.Models
{
    public class ProductTrainDataset
    {
        [LoadColumn(0)]
        public string? ID { get; set; }

        [LoadColumn(1)]
        public string? Recipe_Name { get; set; }

        [LoadColumn(2)]
        public string[]? Cleaned_Ingredients { get; set; }
    }
}
