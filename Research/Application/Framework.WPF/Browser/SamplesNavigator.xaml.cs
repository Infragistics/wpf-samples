using Infragistics.Framework.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var asmb = SamplesManager.AppType.Assembly.FullName;
            asmb = asmb.Replace(".", " ");
            var parts = asmb.Split(',').ToList();
            var title = parts[0] + " " + this.Title;
            if (!title.StartsWith(""))
            {
                title = "Infragistics " + title;
            }
            this.Title = title;  

            if (SamplesManager.SelectedSample == -1)
            {
                SamplesManager.SelectedSample = 0;
            }
        }

        public SamplesManager SamplesManager { get; set; }

        private void OnSamplesSelectorChanged(object sender, SelectionChangedEventArgs e)
        {
            var sample = this.SamplesSelector.SelectedItem as SamplesViewModel;
            if (sample == null) return;

            if (SamplesManager == null) return;

            var sampleView = sample.CreateView<UserControl>();
            sampleView.HorizontalAlignment = HorizontalAlignment.Stretch;
            sampleView.VerticalAlignment = VerticalAlignment.Stretch;

            if (this.SampleContainer != null &&
                this.SampleContainer.Children != null)
            {
                this.SampleContainer.Children.Clear();
                this.SampleContainer.Children.Add(sampleView);
            }
        }
    }
}
