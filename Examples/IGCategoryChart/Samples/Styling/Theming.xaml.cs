using IGCategoryChart.Samples.ViewModels;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes; 
using System.Windows.Controls;

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for Theming.xaml
    /// </summary>
    public partial class Theming : Infragistics.Samples.Framework.SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();

            //this.DataContext = new VariedCollectionViewModel();

            //var VM = (VariedCollectionViewModel)this.DataContext;
            //chart1.ItemsSource = VM.SmallSixDataPtCollection;
        }
                 
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme to the CategoryChart controls
            ThemeManager.SetTheme(this.ChartContainer, theme.Resources);
        }
    }
}
