using IGDataGrid.Controls.Resources;
using Infragistics.Samples.Shared.Controls;

namespace IGDataGrid.Controls
{
    /// <summary>
    /// Interaction logic for ContactPhotoAndNotesControl.xaml
    /// </summary>
    public partial class ContactPhotoAndNotesControl : ContactAdornerControlBase
    {
        public ContactPhotoAndNotesControl()
        {
            InitializeComponent();
        }

        protected override void OnGotFocus(System.Windows.RoutedEventArgs e)
        {
            this.ThrowIfUninitialized();

            base.OnGotFocus(e);

            // This control's IsTabStop property is set to true,
            // so when we are given focus, redirect it to the TextBox.
            this.textBox.Focus();
            this.textBox.SelectAll();
        }
    }

    public class ContactPhotoAndNotesControl_Root : LocalizationRoot
    {
        public string Header_Notes
        {
            get { return Strings.ContactPhotoAndNotesControl_Header_Notes; }
        }
    }
}
