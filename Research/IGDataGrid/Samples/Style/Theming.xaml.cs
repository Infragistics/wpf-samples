using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Themes;
using XmlData = Infragistics.Samples.Shared.DataProviders.XmlDataProvider;
using Infragistics.Samples.Shared.Models;

namespace IGDataGrid.Samples.Style
{ 
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            var source = this.LayoutRoot.Resources["QuarterbackData"] as XmlDataProvider;
            source.Source = XmlData.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }
    }
}
