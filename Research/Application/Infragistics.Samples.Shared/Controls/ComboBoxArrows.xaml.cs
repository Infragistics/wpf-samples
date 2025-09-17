using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources; 
using System.Windows;
using System.Windows.Controls; 

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Represent UI control for selecting items 
    /// </summary>
    public partial class ComboBoxArrows : UserControl
    {
        public ComboBoxArrows()
        {
            InitializeComponent();
             
            this.PrevButton.Click += OnThemePrevButtonClick;
            this.NextButton.Click += OnThemeNextItemButtonClick;
        }
        
        //public event SelectionChangedEventHandler SelectionChanged;
         
        //public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (SelectionChanged == null) return;

        //    SelectedItem = this.ComboBox.SelectedItem;
        //    SelectionChanged(this, e);
        //}

        private void OnThemePrevButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ComboBox == null) return;

            if (this.SelectedIndex <= 0)            
                this.SelectedIndex = this.ComboBox.Items.Count - 1;            
            else            
                this.SelectedIndex -= 1;
        }
        
        private void OnThemeNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ComboBox == null) return;

            this.SelectedIndex = (this.SelectedIndex + 1) % this.ComboBox.Items.Count;
        }
                        
        private int SelectedIndex
        {
            get {  return ComboBox.SelectedIndex; }
            set {  ComboBox.SelectedIndex = value; }
        }

        private object SelectedItem
        {
            get {  return ComboBox.SelectedItem; }
            set { ComboBox.SelectedItem = value; }
        }
        
        public ComboBox ComboBox
        {
            get { return (ComboBox)GetValue(ComboBoxProperty); }
            set { SetValue(ComboBoxProperty, value); }
        }
                
        public static readonly DependencyProperty ComboBoxProperty =
            DependencyProperty.Register("ComboBox", typeof(ComboBox), 
                typeof(ComboBoxArrows), 
                new PropertyMetadata(null, new PropertyChangedCallback(OnComboBoxChanged)));


        private static void OnComboBoxChanged(
            DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs e)
        {
            var owner = (ComboBoxArrows)dependencyObject;
            //if (owner == null) return;
            //if (owner.ComboBox != null)
            //{
            //    owner.ComboBox.SelectionChanged -= owner.OnSelectionChanged;             
            //}

            //owner.ComboBox.SelectionChanged += owner.OnSelectionChanged;

            //Type enumType = (Type)e.NewValue;           
        }
    }
}
