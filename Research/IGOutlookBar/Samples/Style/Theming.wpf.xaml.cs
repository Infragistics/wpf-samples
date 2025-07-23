using Infragistics.Samples.Framework;
using Infragistics.Windows.Themes;
using System.Windows;

namespace IGOutlookBar.Samples.Style
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        public void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            HydrateAllThemeResources("RoyalLight");
            this.OutlookBar.Theme = "RoyalLight";
        }

        public static void HydrateAllThemeResources(string theme)
        {
            var rd = ThemeManager.GetResourceSet(theme, ThemeManager.AllGroupingsLiteral);
            AccessAllThemeResources(rd);
        }

        private static void AccessAllThemeResources(ResourceDictionary resources)
        {
            foreach (System.Collections.DictionaryEntry o in resources)
            {
                object x = o.Value;
            }

            foreach (var rd in resources.MergedDictionaries)
            {
                AccessAllThemeResources(rd);
            }
        }
    }
}
