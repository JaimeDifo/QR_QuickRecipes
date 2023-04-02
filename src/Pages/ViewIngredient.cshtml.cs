﻿using System;
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


namespace QuickRecipes.WebSite.Pages
{
    public class ViewIngredientsModel : PageModel
    {
        private readonly ILogger<ViewIngredientsModel> _logger;

        public ViewIngredientsModel(ILogger<ViewIngredientsModel> logger) => _logger = logger;

        public string base64svg { get; set; }
        public Ingredients ingredient { get; set; }

        public void OnGet(int id, [FromServices] IWebHostEnvironment env)
        {
            var qr = QrCode.EncodeText("http://localhost:52042/ViewIngredient?id="+id+"&controller=Home", QrCode.Ecc.Medium);
            string svg = qr.ToSvgString(4);
            this.base64svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
            var ingredient = new JsonFileIngredientService(env).GetIngredients().First(n => n.id == id);
            this.ingredient = ingredient;
        }
    }
}

