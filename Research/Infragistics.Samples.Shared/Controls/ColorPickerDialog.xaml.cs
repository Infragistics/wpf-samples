using System.Windows;
using System.Windows.Media;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerDialog.xaml
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        private Color modifiedColor = new Color();
        private Color startingColor = new Color();

        public ColorPickerDialog()
        {
            InitializeComponent();
            this.Title = CommonStrings.SelectAColor;
        }

        private void OnOkClicked(object o, RoutedEventArgs rea)
        {
            btnOK.IsEnabled = false;

            modifiedColor = customColorPickerModified.SelectedColor;

            DialogResult = true;
            Hide();
        }

        private void OnCancelClicked(object o, RoutedEventArgs rea)
        {
            btnOK.IsEnabled = false;
            DialogResult = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            btnOK.IsEnabled = false;
            base.OnClosing(e);
        }

        public Color DialogSelectedColor
        {
            get
            {
                return modifiedColor;
            }
        }

        public Color StartingColor
        {
            get
            {
                return startingColor;
            }
            set
            {
                customColorPickerModified.SelectedColor = value;
                btnOK.IsEnabled = false;
            }
        }

        private void customColorPickerModifired_SelectedColorChanged(object sender, DependencyPropertyChangedEventArgs dpcea)
        {
            Color dump = (Color)dpcea.NewValue;

            if (dump != modifiedColor)
            {
                btnOK.IsEnabled = true;
            }
        }
    }
}