using RezepteTagebuch.Data;
using RezepteTagebuch.Services;
using RezepteTagebuch.Shared.Services;
using RezepteTagebuch.Shared.ViewModels;
using RezepteTagebuch.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Services.Media;

namespace RezepteTagebuch
{
    public class App : Application
    {
        public App()
        {
            // Register Implements for Interfaces
            DependencyService.Register<IRecipeDataService, SqlRecipeDataService>();
            DependencyService.Register<IRecipeService, RecipeService>();

            //base.BindingContext = new MainViewModel();
            base.MainPage = new NavigationPage(new MainView());                
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }
}
