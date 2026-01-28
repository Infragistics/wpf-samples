using System.Diagnostics;
using IGDataGrid.Behaviors;
using IGDataGrid.Controls;
using IGDataGrid.Datasources;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for AdorningEditors.xaml
    /// </summary>
    public partial class AdorningEditors : SampleContainer
    {
        readonly ContactEmailControl _contactEmailControl;
        readonly ContactPhoneNumbersControl _contactPhoneNumbersControl;
        readonly ContactPhotoAndNotesControl _contactPhotoAndNotesControl;

        public AdorningEditors()
        {
            InitializeComponent();

            _contactEmailControl = new ContactEmailControl();
            _contactPhoneNumbersControl = new ContactPhoneNumbersControl();
            _contactPhotoAndNotesControl = new ContactPhotoAndNotesControl();

            _contactEmailControl.Initialize(this.xamDG);
            _contactPhoneNumbersControl.Initialize(this.xamDG);
            _contactPhotoAndNotesControl.Initialize(this.xamDG);

            base.DataContext = Contact.CreateContacts();
        }

        // This handles the RequestAdorningEditor event of the XamDataGridBehavior class.
        // It determines what adorning editor to display for the active cell in the grid.
        void OnRequestAdorningEditor(object sender, RequestAdorningEditorRoutedEventArgs e)
        {
            ContactAdornerControlBase adorningEditor = null;

            switch (e.AdornedCell.Field.Name)
            {
                case "OnlineStatus":
                    adorningEditor = null;
                    break;

                case "DisplayName":
                    adorningEditor = _contactPhotoAndNotesControl;
                    break;

                case "CellPhone":
                    adorningEditor = _contactPhoneNumbersControl;
                    break;

                case "PersonalEmail":
                    adorningEditor = _contactEmailControl;
                    break;

                default:
                    Debug.Fail("Unexpected Field: " + e.AdornedCell.Field.Name);
                    break;
            }

            if (adorningEditor != null)
            {
                adorningEditor.DataContext = e.AdornedCell.Record.DataItem;

                if (!adorningEditor.IsClosed)
                    e.AdorningEditor = adorningEditor;
            }
        }

        void linkToBlogPost_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // We cannot use the standard RequestNavigate event handler in the main Window
            // because this sample is shown in a Frame, which navigates to the web page.
            string url = "http://www.infragistics.com/community/blogs/josh_smith/archive/2008/09/15/introducing-a-new-xamdatagrid-behavior-displayadorningeditors.aspx";
            try
            {
                Process.Start(url);
            }
            catch
            {
            }
        }
    }
}
