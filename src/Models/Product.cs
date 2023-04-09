using System;
using System.Collections.Generic;

using System.Text.Json;
using CsvHelper.Configuration.Attributes;

namespace QuickRecipes.WebSite.Models
{
    public class Product
    {
        public string? Id { get; set; }
        public string? Recipe_URLName { get; set; }
        public string? Recipe_Name { get; set; }
        public string[]? Ingredients { get; set; }
        [TypeConverter(typeof(StringListConverter))]
        public List<string>? Instruction { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public int[]? Ratings { get; set; }

    }


}

