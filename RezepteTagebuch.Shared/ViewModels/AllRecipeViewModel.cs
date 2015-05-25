using RezepteTagebuch.Shared.Models;
using RezepteTagebuch.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RezepteTagebuch.Shared.ViewModels
{
    public class AllRecipeViewModel : BaseViewModel
    {
        private IRecipeService _recipeService;

        public AllRecipeViewModel(INavigation navigation)
            : base(navigation)
        {
            _recipeService = DependencyService.Get<IRecipeService>();

            // Rezepte
            RefreshRecipes();
        }

        private ObservableCollection<Recipe> recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { return recipes; }
            set
            {
                if (value == null) return;
                recipes = value;
                OnPropertyChanged();
            }
        }

        public void RefreshRecipes()
        {
            Recipes = new ObservableCollection<Recipe>(_recipeService.GetAll());
        }
    }
}
