using Infragistics.Samples.Framework;
using System.Windows;

namespace IGEditors.Samples.Display
{
    /// <summary>
    /// Interaction logic for MaskedEditor_ChangeMask.xaml
    /// </summary>

    public partial class MaskedEditor_ChangeMask : SampleContainer
    {
        public MaskedEditor_ChangeMask()
        {
            InitializeComponent();
        }
    }

    public class CustomComboBoxItem : DependencyObject
    {
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(CustomComboBoxItem), new PropertyMetadata(null));
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register("Mask", typeof(string), typeof(CustomComboBoxItem), new PropertyMetadata(null));
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(CustomComboBoxItem), new PropertyMetadata(null));

        public string DisplayName
        {
            get { return (string)this.GetValue(CustomComboBoxItem.DisplayNameProperty); }
            set { this.SetValue(CustomComboBoxItem.DisplayNameProperty, value); }
        }

        public string Mask
        {
            get { return (string)this.GetValue(CustomComboBoxItem.MaskProperty); }
            set { this.SetValue(CustomComboBoxItem.MaskProperty, value); }
        }

        public string Value
        {
            get { return (string)this.GetValue(CustomComboBoxItem.ValueProperty); }
            set { this.SetValue(CustomComboBoxItem.ValueProperty, value); }
        }
    }
}