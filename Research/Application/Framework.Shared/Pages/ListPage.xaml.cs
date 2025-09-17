using Infragistics.Framework;
using System; 
using System.Diagnostics; 
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public partial class ListPage : ContentPage
    {
        public ListPage(NavigationApp app)
        {
            App = app;

            InitializeComponent();
              
            this.ListView.ItemSelected += ListView_ItemSelected; 
        }
        protected NavigationApp App;

        protected override void OnAppearing()
        {
            if (App.NavigationPage != null)
            {
                App.NavigationPage.Popped += (s, e) =>
                {
                    this.ListView.SelectedItem = null;
                };
            }
        }
         
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        { 
            var sample = e.SelectedItem as SamplesModel;

            if (sample == null) return;
            Debug.WriteLine("selecting sample: " + sample.Type.ToString());
             
            var result = sample.CreateView<ContentView>();
            if (result.HasError)
            {
                var errorPage = new ErrorPage();
                errorPage.BindingContext = result;
                await this.Navigation.PushAsync(errorPage);
            }
            else
            {
                Debug.WriteLine("creating sample: " + sample.Type.ToString());
                var sampleView = result.Value;
                sampleView.Margin = new Thickness(10);

                var page = new TestPage();
                page.Content = sampleView;
                page.Title = sample.Name;

                try
                {

                    Debug.WriteLine("navigating to sample: " + sample.Type.ToString());
                    await this.Navigation.PushAsync(page);
                }
                catch (Exception ex)
                { 
                    var errorPage = new ErrorPage();
                    errorPage.BindingContext = Result.Fail(ex);
                    await this.Navigation.PushAsync(errorPage);
                }
            } 
        }
         
        
    }
}
