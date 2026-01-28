using System.Windows;
using System.Windows.Controls; 

namespace Infragistics.Framework.Browser
{ 
    public partial class SamplesNavigator : Window
    {
        public SamplesNavigator()
        {            
            InitializeComponent();                       
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SamplesManager = DataContext as SamplesManager;
            //var asmb = SamplesManager.AppAssembly.FullName;
            //asmb = asmb.Replace(".", " ");
            //var parts = asmb.Split(',').ToList();
           
            this.Title = SamplesManager.AppTitle + " " + this.Title;

            if (SamplesManager.SelectedSample == -1)
            {
                SamplesManager.SelectedSample = 0;
            }
        }

        public SamplesManager SamplesManager { get; set; }

        private void OnSamplesSelectorChanged(object sender, SelectionChangedEventArgs e)
        {
            var sample = this.SamplesSelector.SelectedItem as SamplesModel;
            if (sample == null) return;

            if (SamplesManager == null) return;

            var result = sample.CreateView<UserControl>();
            if (result.IsValid)
            {
                var sampleView = result.Value;
                sampleView.HorizontalAlignment = HorizontalAlignment.Stretch;
                sampleView.VerticalAlignment = VerticalAlignment.Stretch;
                sampleView.Margin = new Thickness(5);

                if (this.SampleContainer != null &&
                    this.SampleContainer.Children != null)
                {
                    this.SampleContainer.Children.Clear();
                    this.SampleContainer.Children.Add(sampleView);
                }
            }
          
        }
    }
}
