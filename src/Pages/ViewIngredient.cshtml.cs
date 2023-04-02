using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuickRecipes.WebSite.Services;
using QuickRecipes.WebSite.Models;

namespace QuickRecipes.WebSite.Pages
{
    public class ViewIngredientsModel : PageModel
    {
        private readonly ILogger<ViewIngredientsModel> _logger;

        public ViewIngredientsModel(ILogger<ViewIngredientsModel> logger) => _logger = logger;

        public Ingredients ingredient { get; set; } 
        public void OnGet(int id, [FromServices] IWebHostEnvironment env)
        {
            var ingredient = new JsonFileIngredientService(env).GetIngredients().First(n => n.id == id);
            this.ingredient = ingredient;
        }
    }
}

