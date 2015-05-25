using RezepteTagebuch.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RezepteTagebuch.Views
{
    public partial class MainView : TabbedPage
    {
        private readonly AllRecipeView _allRecipeView;
        private readonly EventCalendarView _eventCalendarView;
        public MainView()
        {
            base.ToolbarItems.Add(new ToolbarItem("Neu", "", async () =>
            {
                await Navigation.PushModalAsync(new RecipeView(), true);
                //var page = new ContentPage();
                //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
            }));

            _allRecipeView = new AllRecipeView();
            _eventCalendarView = new EventCalendarView();

            this.Children.Add(_allRecipeView);
            this.Children.Add(_eventCalendarView);

            this.CurrentPageChanged += (obj, args) =>
            {
                if (this.CurrentPage == _allRecipeView)
                {
                    _allRecipeView.ViewModel.RefreshRecipes();
                }
            };

            InitializeComponent();
        }

        //protected async void OnNewRecipe(object sender, EventArgs args)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new RecipeView()));
        //    //await Navigation.PushAsync(new RecipeView());
        //    await Navigation.PushModalAsync(new RecipeView());
        //    var poppedPage = await Navigation.PopModalAsync();

        //    // http://stackoverflow.com/questions/24174241/how-can-i-await-modal-form-dismissal-using-xamarin-forms
        //}

        protected async void OnEventCalendar(object sender, EventArgs args)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new RecipeView()));
            await Navigation.PushAsync(new EventCalendarView());
            //var poppedPage = await Navigation.PopModalAsync();
        }



    }
}
