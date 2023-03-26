using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickRecipes.WebSite.Models
{
    public class Ingredients
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        public string? cuisine { get; set; }
        public string? Image { get; set; }
        public string? ingredients { get; set; }
    }
}
