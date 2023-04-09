using System.Collections.Generic;
using QuickRecipes.WebSite.Models;
using QuickRecipes.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace QuickRecipes.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
            CSVFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public CSVFileProductService ProductService { get; }
        public IEnumerable<Product>? Products { get; private set; }

        public void OnGet() => Products = ProductService.GetProducts();
    }
}
