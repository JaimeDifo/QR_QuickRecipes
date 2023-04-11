using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.ML.Data;

namespace QuickRecipes.WebSite.Models
{
    public class RecipeData
    {
        [LoadColumn(0)]
        public float[] ID { get; set; }

        [LoadColumn(1)]
        public float[] Recipe_Name { get; set; }

        [LoadColumn(2)]
        public float[] Cleaned_Ingredients { get; set; }

        [LoadColumn(3)]
        public float[] Instructions { get; set; }
    }
}
