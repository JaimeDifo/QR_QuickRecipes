//using System.Collections.Generic;
//using QuickRecipes.WebSite.Models;
//using QuickRecipes.WebSite.Services;
//using Microsoft.AspNetCore.Mvc;
//using static QuickRecipes.WebSite.Controllers.IngredientsController;

//namespace QuickRecipes.WebSite.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class IngredientsController : ControllerBase
//    {
//        public IngredientsController(JsonFileIngredientService ingredientsService) =>
//            IngredientsService = ingredientsService;

//        public JsonFileIngredientService IngredientsService { get; }

//        [HttpGet]
//        public IEnumerable<Ingredients> Get() => IngredientsService.GetIngredients();

//    }
//}
