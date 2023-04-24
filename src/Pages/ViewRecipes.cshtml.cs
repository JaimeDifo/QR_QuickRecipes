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
using System.IO;
using System.Text;
using Net.Codecrete.QrCodeGenerator;
using static System.Net.WebRequestMethods;


namespace QuickRecipes.WebSite.Pages
{
    public class ViewRecipeModel : PageModel
    {
        private readonly ILogger<ViewRecipeModel> _logger;

        public ViewRecipeModel(ILogger<ViewRecipeModel> logger) => _logger = logger;

        public string base64svg { get; set; }
        public Product product { get; set; }

        public void OnGet(string id, [FromServices] IWebHostEnvironment env)
        {
            var qr = QrCode.EncodeText("http://192.168.1.186:52042/ViewRecipes?id=" + id + "&controller=Home", QrCode.Ecc.Medium);
            string svg = qr.ToSvgString(4);
            this.base64svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
            var product = new CSVFileProductService(env).GetProducts().First(n => n.Id == id);
            this.product = product;
        }
    }
}