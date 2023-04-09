using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.ML.Data;

namespace QuickRecipes.WebSite.Models
{
    public class RecipePrediction
    {

            [ColumnName("Score")]
            public float[] PredictedLabels { get; set; }
    }
}
