using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace QuickRecipes.WebSite.Pages
{
    public class IngredientsModel : PageModel
    {
        private readonly ILogger<IngredientsModel> _logger;

        public IngredientsModel(ILogger<IngredientsModel> logger) => _logger = logger;

        public void OnGet()
        {
        }
    }
}

