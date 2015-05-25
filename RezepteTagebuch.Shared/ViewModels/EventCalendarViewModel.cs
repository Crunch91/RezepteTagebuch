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
    public class EventCalendarViewModel : BaseViewModel
    {
        private IRecipeService _recipeService;

        public EventCalendarViewModel(INavigation navigation)
            : base(navigation)
        {
            _recipeService = DependencyService.Get<IRecipeService>();
            Recipes = new ObservableCollection<Recipe>();
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

        public void DateSelected(DateTime date)
        {
            Recipes = new ObservableCollection<Recipe>(_recipeService.GetByDate(date));
        }

        //private class MiniRecipe : Recipe
        //{
        //    public string Title { get; set; }
        //    public DishCategory DishCategory { get; set; }
        //    public string DescriptionPicturePath { get; set; }
        //    public string FoodPicturePath { get; set; }

        //    // TODO: Überlegen, ob man nicht ImageSource anstatt des String-Pfades verwendet.
        //    //       Evtl. funktioniert dann auch das Binding an Oberfläche ohne OutOfMemoryException?
        //    //public ImageSource DescriptionPicture { get; set; }
        //    //public ImageSource FoodPicture { get; set; }

        //    private MiniRecipe()
        //    {
        //    }
        //    private MiniRecipe(Recipe recipe)
        //    {
        //        this.Title = recipe.Title;
        //        this.DishCategory = recipe.DishCategory;
        //        this.DescriptionPicturePath = recipe.DescriptionPicturePath;
        //        this.FoodPicturePath = recipe.FoodPicturePath;
        //    }
        //}
    }
}
