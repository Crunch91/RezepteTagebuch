using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RezepteTagebuch.Views
{
    public partial class CookDatesPopupView : ContentPage
    {
        private List<DateTime> _cookDates;
        public List<DateTime> CookDates
        {
            get { return _cookDates; }
            set { _cookDates = value; }
        }

        public CookDatesPopupView(List<DateTime> cookDates)
        {
            InitializeComponent();
            _cookDates = cookDates;
            this.BindingContext = this;
        }
    }
}
