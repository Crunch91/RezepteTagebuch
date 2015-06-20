using RezepteTagebuch.Shared.Models;
using RezepteTagebuch.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs.Services.Media;

namespace RezepteTagebuch.Views
{
    public partial class RecipeView : ContentPage // BaseView<RecipeViewModel>
    {
        readonly RecipeViewModel _viewModel;
        public RecipeViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        public RecipeView(Recipe recipe)
            : this()
        {
            _viewModel = new RecipeViewModel(Navigation, recipe);
            init();
        }

        public RecipeView()
        {
            _viewModel = new RecipeViewModel(Navigation);
            init();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // Es ist nicht mögich die Collection vom XAML aus zu binden. Daher ist folgendes im Code-Behind zu erledigen:      
            this.DishCategoriesPicker.Items.Clear();
            foreach (var item in ViewModel.DishCategories)
            {
                this.DishCategoriesPicker.Items.Add(item);
            }
        }

        private void init()
        {
            InitializeComponent();
            Title = "+";
            _viewModel.CookDatesClicked += (sender, args) =>
            {
                var vm = (RecipeViewModel)sender;
                Navigation.PushModalAsync(new CookDatesPopupView(vm.CookDates.ToList()));
            };

            this.BindingContext = _viewModel;
        }

        //protected async void OnSelectFoodPicture(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        var mediaPicker = DependencyService.Get<IMediaPicker>();
        //        await mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
        //        {
        //            DefaultCamera = CameraDevice.Front,
        //        }).ContinueWith(t =>
        //        {
        //            if (t.IsCanceled || t.IsFaulted) // user canceled or error
        //                return;

        //            _viewModel.FoodPicture = ImageSource.FromStream(() => t.Result.Source);
        //            _viewModel.FoodPicturePath = t.Result.Path;
        //        });
        //    }
        //    catch (System.Exception)
        //    {
        //        //this.status = ex.Message;
        //        throw;
        //    }

        //    //DisplayActionSheet("Mein titel", "schliessen", "Destruct Button");
        //    //DisplayAlert("Alarm Titel", "Alarm Message", "Alarm cancel");
        //}
    }
}
