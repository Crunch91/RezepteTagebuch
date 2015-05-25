using RezepteTagebuch.Data;
using RezepteTagebuch.Shared.Models;
using RezepteTagebuch.Shared.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(RezepteTagebuch.Services.RecipeService))]
namespace RezepteTagebuch.Services
{
    public class RecipeService : IRecipeService
    {
        private IRecipeDataService _recipeDataService;

        public RecipeService()
        {
            _recipeDataService = DependencyService.Get<IRecipeDataService>();
        }

        public List<Recipe> GetAll()
        {
            return _recipeDataService.GetAll().ToList();
        }

        public List<Recipe> GetByDate(DateTime date)
        {
            return _recipeDataService.GetByDate(date).ToList();
        }

        public Recipe GetById(Guid id)
        {
            return _recipeDataService.GetById(id);
        }

        public void AddOrUpdate(Recipe recipe)
        {
            _recipeDataService.AddOrUpdate(recipe);
        }
    }
}
