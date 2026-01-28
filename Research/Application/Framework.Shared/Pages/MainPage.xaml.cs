using Infragistics.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class NavigationApp : Application
    {
        public SamplesManager SamplesManager { get; set; }

        //public NavigationPage NavigationPage { get; set; }

        private NavigationPage _NavigationPage;
        /// <summary> Gets or sets NavigationPage </summary>
        public NavigationPage NavigationPage
        {
            get { return _NavigationPage; }
            set
            {
                if (_NavigationPage != null)
                {
                    _NavigationPage.Popped -= OnNavigationPopped;
                    _NavigationPage.Pushed -= OnNavigationPushed;
                }

                if (_NavigationPage == value) return;

                _NavigationPage = value;

                if (_NavigationPage != null)
                {
                    _NavigationPage.Popped += OnNavigationPopped;
                    _NavigationPage.Pushed += OnNavigationPushed;
                }
            }
        }
        
        public static event EventHandler<NavigationEventArgs> Popped;
        public static event EventHandler<NavigationEventArgs> Pushed;

        private void OnNavigationPopped(object sender, NavigationEventArgs e)
        {
            var handler = Popped;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        private void OnNavigationPushed(object sender, NavigationEventArgs e)
        {
            var handler = Pushed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }


        
    }

    public partial class MainPage : ContentPage
    {
        public MainPage(NavigationApp app)
        {
            App = app;

            InitializeComponent();
             
            this.ListView.ItemsSource = App.SamplesManager.Groups; 
            this.ListView.ItemSelected += ListView_ItemSelected;

            this.Title = typeof(MainPage).GetTypeInfo().Assembly.GetName().Name.Replace("."," ");
        }
        protected NavigationApp App;

        protected override void OnAppearing()
        { 
            if (App.NavigationPage != null  )
            {
                App.NavigationPage.Popped += (s, e) =>
                {
                    this.ListView.SelectedItem = null;
                };
            }
        } 
         
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        { 
            var group = e.SelectedItem as SamplesGroup;
            if (group == null) return;

            Debug.WriteLine("selecting sample group: " + e.SelectedItem.ToString());
            
            var page = new ListPage(App);
            page.Title = group.Name;
            page.BindingContext = group;
             
            Debug.WriteLine("navigating to samples group: " + e.SelectedItem.ToString());
            await this.Navigation.PushAsync(page);
        }
         
 
    }
}
