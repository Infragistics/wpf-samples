using Infragistics.Controls.Editors;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace IgWord.Infrastructure
{
    public class XamColorPickerExtended : XamColorPicker
    {
        #region Constructor

        public XamColorPickerExtended()
        {
            this.DefaultStyleKey = typeof(XamColorPickerExtended);

            this.SelectedColorChanged += (s, a) =>
            {
                if (a.NewColor.HasValue && a.NewColor == Colors.Transparent)
                {
                    IsSelectedColorTransparent = Visibility.Visible;

                }
                else
                {
                    IsSelectedColorTransparent = Visibility.Hidden;
                }
            };

            if (this.SelectedColor == Colors.Transparent)
            {
                IsSelectedColorTransparent = Visibility.Visible;
            }
            else
            {
                IsSelectedColorTransparent = Visibility.Hidden;
            }

            SelectTransparentColor = new DelegateCommand(ExecuteSelectTransparentColor);
        }

        #endregion //Constructor

        #region SelectTransparentColor

        public ICommand SelectTransparentColor { get; private set; }

        public void ExecuteSelectTransparentColor()
        {
            this.SelectedColor = Colors.Transparent;
            this.IsDropDownOpen = false;
        }

        #endregion //SelectTransparentColor

        #region TransparentColorItemText
        public static readonly DependencyProperty TransparentColorItemTextProperty = DependencyProperty.Register("TransparentColorItemText",
                  typeof(string), typeof(XamColorPickerExtended), new PropertyMetadata(null));

        public string TransparentColorItemText
        {
            get
            {
                return (string)this.GetValue(XamColorPickerExtended.TransparentColorItemTextProperty);
            }
            set
            {
                this.SetValue(XamColorPickerExtended.TransparentColorItemTextProperty, value);
            }
        }

        #endregion //TransparentColorItemText

        #region IsSelectedColorTransparent
        public static readonly DependencyProperty IsSelectedColorTransparentProperty = DependencyProperty.Register("IsSelectedColorTransparent",
                  typeof(Visibility), typeof(XamColorPickerExtended), new PropertyMetadata(Visibility.Hidden));

        public Visibility IsSelectedColorTransparent
        {
            get
            {
                return (Visibility)this.GetValue(XamColorPickerExtended.IsSelectedColorTransparentProperty);
            }
            set
            {
                this.SetValue(XamColorPickerExtended.IsSelectedColorTransparentProperty, value);
            }
        }

        #endregion //IsSelectedColorTransparent
    }
}
