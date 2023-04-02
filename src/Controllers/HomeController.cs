using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuickRecipes.WebSite.Models;
using QuickRecipes.WebSite.Services;

namespace QuickRecipes.WebSite.Controllers
{
    public class HomeController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ingredients()
        {
            return View();
        }*/
        /*public IActionResult ViewIngredient(int? id, [FromServices] IWebHostEnvironment env)
        {
            ViewData["ingredient"] = new JsonFileIngredientService(env).GetIngredients().First(n => n.id == id);
            
            return View();
        }*/
    }
}
