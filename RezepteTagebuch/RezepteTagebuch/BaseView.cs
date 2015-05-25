using RezepteTagebuch.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RezepteTagebuch
{
    // http://arteksoftware.com/clean-viewmodels-with-xamarin-forms/
    public class BaseView<T> : ContentPage where T : IViewModel, new()
    {
        readonly T _viewModel;

        public T ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        public BaseView()
        {
            _viewModel = new T();
            BindingContext = _viewModel;
        }
    }
}
