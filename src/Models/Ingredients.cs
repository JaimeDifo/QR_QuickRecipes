using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.ML.Data;

namespace QuickRecipes.WebSite.Models
{
    public class Ingredients
    {
        public int? id { get; set; }
        public string? ingredientsURLName { get; set; }
        public string? ingredients { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public static Ingredients FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Ingredients ingredients = new Ingredients();
            ingredients.id = Convert.ToInt32(values[0]);
            ingredients.ingredientsURLName = values[1];
            ingredients.ingredients = (values[2]);
            ingredients.Description = (values[3]);
            ingredients.Image = (values[4]);

            return ingredients;
        }
    }
}
