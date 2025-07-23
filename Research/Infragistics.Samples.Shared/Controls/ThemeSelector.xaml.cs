using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources; 
using System.Windows;
using System.Windows.Controls; 
using System.Collections;

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Represent UI control for selecting and chaning a theme
    /// </summary>
    public partial class ThemeSelector : UserControl
    {
        public ThemeSelector()
        {
            InitializeComponent();

            this.ThemeInfo.Text = CommonStrings.XW_ThemeSupport_Themes;

            this.ThemeComboBox.ItemsSource = new ThemeList();
            this.ThemeComboBox.DisplayMemberPath = "Name";
            this.ThemeComboBox.SelectionChanged += OnSelectionChanged;
             
            this.ThemePrevButton.Click += OnThemePrevButtonClick;
            this.ThemeNextButton.Click += OnThemeNextItemButtonClick;

            this.Loaded += ThemeSelector_Loaded;
        }

        private void ThemeSelector_Loaded(object sender, RoutedEventArgs e)
        {
            this.SelectedIndex = 0;
        }

        public event SelectionChangedEventHandler SelectionChanged;
         
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged == null) return;

            SelectedItem = this.ThemeComboBox.SelectedItem;
            SelectionChanged(this, e);
        }

        private void OnThemePrevButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.SelectedIndex == 0)            
                this.SelectedIndex = this.ThemeComboBox.Items.Count - 1;            
            else            
                this.SelectedIndex -= 1;
        }
        
        private void OnThemeNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.SelectedIndex = (this.SelectedIndex + 1) % this.ThemeComboBox.Items.Count;
        }
                 
        public int SelectedIndex
        {
            get {  return ThemeComboBox.SelectedIndex; }
            set {  ThemeComboBox.SelectedIndex = value; }
        }

        public object SelectedItem
        {
            get {  return ThemeComboBox.SelectedItem; }
            set { ThemeComboBox.SelectedItem = value; }
        }

        public IEnumerable ItemsSource
        {
            get { return ThemeComboBox.ItemsSource; }
            set { ThemeComboBox.ItemsSource = value; }
        }
    }
}
