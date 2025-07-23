using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes;
using System;
using System.Collections; 
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent(); 
            InitializeSampleData();
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        void InitializeSampleData()
        {
            ArrayList typesList = new ArrayList();
            typesList.Add(new Button());
            typesList.Add(new Grid());
            typesList.Add(new Page());
            typesList.Add(new StackPanel());
            typesList.Add(new TextBox());

            this.ListOfTypes.ItemsSource = typesList;
            this.ListOfTypes.SelectedIndex = 0;
        }
    }
}
