using RezepteTagebuch.Shared.Models;
using RezepteTagebuch.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Services.Media;

namespace RezepteTagebuch.Shared.ViewModels
{
    public class RecipeViewModel : BaseViewModel
    {
        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/dependency-service/
        private ICameraService _cameraService;
        private IRecipeService _recipeService;
        private IMediaPicker _mediaPicker;
        private IFileService _fileService;

        private float _thumbnailWidth = 100;
        private float _thumbnailHeight = 100;

        /****************************/
        /****** CONSTRUCTORS ********/
        /****************************/

        public RecipeViewModel(INavigation navigation)
            : base(navigation)
        {
            // Sollten im Konstruktor die private Variabeln angesprochen werden?

            // Gerichtekategorie
            this.dishCategories = new ObservableCollection<string>();
            this.dishCategories.Add("Vorspeise");
            this.dishCategories.Add("Hauptgericht");
            this.dishCategories.Add("Nachspeise");

            // Kochdatum - default
            this.cookDate = DateTime.Now.Date;

            // Kochdaten - default
            this.cookDates = new ObservableCollection<DateTime>();
            this.cookDates.CollectionChanged += OnCollectionChanged;

            // Commands
            this.AddCookDateCommand = new RelayCommand(AddCookDate, AddCookDateCanExecute);
            this.SaveRecipeCommand = new RelayCommand(SaveRecipe);
            this.CancelRecipeCommand = new RelayCommand(CancelRecipe);
            this.SelectFoodPictureCommand = new RelayCommand(SelectFoodPicture);
            this.SelectDescriptionPictureCommand = new RelayCommand(SelectDescriptionPicture);
            this.ShowCookDatesCommand = new RelayCommand(ShowCookDates);

            // Services
            _cameraService = DependencyService.Get<ICameraService>();
            _recipeService = DependencyService.Get<IRecipeService>();
            _mediaPicker = DependencyService.Get<IMediaPicker>();
            _fileService = DependencyService.Get<IFileService>();
        }

        public RecipeViewModel(INavigation navigation, Recipe recipe)
            : this(navigation)
        {
            this.recipeId = recipe.Id;
            this.recipeTitle = recipe.Title;
            this.description = recipe.Description;
            this.hint = recipe.Hint;
            this.tags = recipe.Tags;
            this.cookDates = new ObservableCollection<DateTime>(recipe.CookDates);
            this.dishCategorySelectedIndex = (int)recipe.DishCategory;
            this.FoodPicturePath = recipe.FoodPicturePath;
            this.DescriptionPicturePath = recipe.DescriptionPicturePath;
        }

        /****************************/
        /********* EVENTS ***********/
        /****************************/

        public event EventHandler CookDatesClicked;


        /****************************/
        /******* PROPERTIES *********/
        /****************************/

        private Guid recipeId { get; set; }

        private string recipeTitle;
        public string RecipeTitle
        {
            get { return recipeTitle; }
            set
            {
                if (recipeTitle == value) return;
                recipeTitle = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> cookDates;
        public ObservableCollection<DateTime> CookDates
        {
            get { return cookDates; }
            set
            {
                if (cookDates == value) return;
                cookDates = value;
                OnPropertyChanged();
            }
        }

        private DateTime cookDate;
        public DateTime CookDate
        {
            get { return cookDate; }
            set
            {
                if (cookDate == value) return;
                cookDate = value;
                AddCookDateCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private int dishCategorySelectedIndex;
        public int DishCategoryIndex
        {
            get { return dishCategorySelectedIndex; }
            set
            {
                if (dishCategorySelectedIndex == value) return;
                dishCategorySelectedIndex = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> dishCategories;
        public ObservableCollection<string> DishCategories
        {
            get { return dishCategories; }
            set
            {
                //if (cookDates == value) return; 
                dishCategories = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string hint;
        public string Hint
        {
            get { return hint; }
            set { hint = value; }
        }

        private string tags;
        public string Tags
        {
            get { return tags; }
            set
            {
                if (tags == value) return;
                tags = value;
                OnPropertyChanged();
            }
        }

        private ImageSource foodPicture;
        public ImageSource FoodPicture
        {
            get { return foodPicture; }
            set
            {
                if (foodPicture == value) return;
                foodPicture = value;
                OnPropertyChanged();
            }
        }

        private string foodPicturePath;
        public string FoodPicturePath
        {
            get { return foodPicturePath; }
            set
            {
                if (foodPicturePath == value) return;
                foodPicturePath = value;
                OnPropertyChanged();
            }
        }

        private ImageSource descriptionPicture;
        public ImageSource DescriptionPicture
        {
            get { return descriptionPicture; }
            set
            {
                if (descriptionPicture == value) return;
                descriptionPicture = value;
                OnPropertyChanged();
            }
        }

        private string descriptionPicturePath;
        public string DescriptionPicturePath
        {
            get { return descriptionPicturePath; }
            set
            {
                if (descriptionPicturePath == value) return;
                descriptionPicturePath = value;
                OnPropertyChanged();
            }
        }

        /****************************/
        /******** COMMANDS **********/
        /****************************/

        // https://github.com/jamesmontemagno/MasterDetail-Xamarin
        public RelayCommand AddCookDateCommand { protected set; get; }
        public RelayCommand SaveRecipeCommand { protected set; get; }
        public RelayCommand CancelRecipeCommand { protected set; get; }
        public RelayCommand SelectFoodPictureCommand { protected set; get; }
        public RelayCommand SelectDescriptionPictureCommand { protected set; get; }
        public RelayCommand ShowCookDatesCommand { protected set; get; }

        private void AddCookDate()
        {
            if (!CookDates.Contains(CookDate))
            {
                CookDates.Add(CookDate);
            }
        }

        private async void SelectFoodPicture()
        {
            await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
            {
                DefaultCamera = CameraDevice.Front,
            }).ContinueWith(t =>
            {
                if (t.IsCanceled || t.IsFaulted) // user canceled or error
                    return;

                this.FoodPicturePath = t.Result.Path;
                this.FoodPicture = CreateThumbnail(t.Result.Source);
            });
        }

        private void ShowCookDates()
        {
            CookDatesClicked(this, null);
        }

        private async void SelectDescriptionPicture()
        {
            await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
            {
                DefaultCamera = CameraDevice.Front,
            }).ContinueWith(t =>
            {
                if (t.IsCanceled || t.IsFaulted) // user canceled or error
                    return;

                this.DescriptionPicture = CreateThumbnail(t.Result.Source);
                this.DescriptionPicturePath = t.Result.Path;
            });
        }

        private bool AddCookDateCanExecute()
        {
            return !CookDates.Contains(CookDate);
        }

        private void SaveRecipe()
        {
            AddCookDate();
            this.FoodPicturePath = _fileService.CopyFile(FoodPicturePath, "Essensbilder");
            this.DescriptionPicturePath = _fileService.CopyFile(DescriptionPicturePath, "Rezeptbilder");

            var recipe = new Recipe
            {
                Title = this.RecipeTitle,
                Description = this.Description,
                Hint = this.Hint,
                Tags = this.Tags,
                CookDates = this.CookDates.ToList(),
                //DishCategory = this.DishCategories[dishCategorySelectedIndex]
                DishCategory = (DishCategory)this.dishCategorySelectedIndex,
                FoodPicturePath = this.FoodPicturePath,
                DescriptionPicturePath = this.DescriptionPicturePath
            };

            _recipeService.AddOrUpdate(recipe);
            _navigator.PopModalAsync(true);
        }

        private void CancelRecipe()
        {
            _navigator.PopModalAsync();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddCookDateCommand.RaiseCanExecuteChanged();
        }

        private ImageSource CreateThumbnail(Stream source)
        {
            using (var memoryStream = new MemoryStream())
            {
                source.CopyTo(memoryStream);
                byte[] resizedImage = _fileService.ResizeImage(memoryStream.ToArray(), _thumbnailWidth, _thumbnailHeight);
                return ImageSource.FromStream(() => new MemoryStream(resizedImage));
            }
        }
    }
}



