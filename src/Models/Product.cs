using System;
using System.Collections.Generic;

using System.Text.Json;
using CsvHelper.Configuration.Attributes;
using Microsoft.ML.Data;

namespace QuickRecipes.WebSite.Models
{
    public class Product
    {
        [LoadColumn(0)]
        public string? Id { get; set; }
        [LoadColumn(1)]
        public string? Recipe_URLName { get; set; }
        [LoadColumn(2)]
        public string? Recipe_Name { get; set; }
        [TypeConverter(typeof(StringListConverter))]
        [LoadColumn(3)]
        public List<string>? Ingredients { get; set; }
        [LoadColumn(4)]
        [TypeConverter(typeof(StringListConverter))]
        public List<string>? Instruction { get; set; }
        [LoadColumn(5)]
        public string? Image { get; set; }
        [LoadColumn(6)]
        public string Description { get; set; }
        [NoColumn]
        public int[]? Ratings { get; set; }

    }


}

