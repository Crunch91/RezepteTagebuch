using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RezepteTagebuch.Shared.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigation _navigator;

        public BaseViewModel(INavigation navigation)
        {
            _navigator = navigation;
        }

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            var ev = PropertyChanged;
            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
