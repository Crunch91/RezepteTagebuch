﻿using RezepteTagebuch.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RezepteTagebuch.Views
{
    public partial class AllRecipeView : ContentPage
    {
        private AllRecipeViewModel _viewModel;
        public AllRecipeViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public AllRecipeView()
        {
            InitializeComponent();
            Title = "Alle Rezepte";
            _viewModel = new AllRecipeViewModel(Navigation);
            this.BindingContext = _viewModel;
        }
    }
}
