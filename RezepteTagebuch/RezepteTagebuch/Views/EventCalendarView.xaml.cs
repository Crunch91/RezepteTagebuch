using RezepteTagebuch.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;

namespace RezepteTagebuch.Views
{
    public partial class EventCalendarView : ContentPage
    {
        private EventCalendarViewModel _viewModel;
        public EventCalendarViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public EventCalendarView()
        {
            InitializeComponent();
            Title = "Kalender";
            _viewModel = new EventCalendarViewModel(Navigation);
            this.BindingContext = _viewModel;

            var stackLayout = this.FindByName<StackLayout>("stack");
            CalendarView calendarControl;
            calendarControl = new CalendarView()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand                
            };
            stackLayout.Children.Insert(0, calendarControl);


            //CalendarView calendarControl;
            //StackLayout stackLayout;

            //stackLayout = new StackLayout();
            //Content = stackLayout;

            //calendarControl = new CalendarView()
            //{
            //    VerticalOptions = LayoutOptions.Start,
            //    HorizontalOptions = LayoutOptions.CenterAndExpand
            //};

            //stackLayout.Children.Add(calendarControl);

            //var listView = new ListView();
            //var dataTemplate = new DataTemplate(typeof(TextCell));
            ////var viewCell = new ViewCell();
            ////var childStackLayout = new StackLayout();
            ////var label = new Label();

            //listView.SetBinding(ListView.ItemsSourceProperty, new Binding("Recipes"));
            //dataTemplate.SetBinding(TextCell.TextProperty, "Title");
            //listView.ItemTemplate = dataTemplate;
            //stackLayout.Children.Add(listView);

            calendarControl.DateSelected += (object sender, DateTime e) =>
            {
                _viewModel.DateSelected(e);

                //stackLayout.Children.Add(new Label()
                //    {
                //        Text = "Date Was Selected" + e.ToString("d"),
                //        VerticalOptions = LayoutOptions.Start,
                //        HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    });
            };
        }
    }
}
