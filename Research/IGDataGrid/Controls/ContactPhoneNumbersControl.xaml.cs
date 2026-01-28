using IGDataGrid.Controls.Resources;
using Infragistics.Samples.Shared.Controls;

namespace IGDataGrid.Controls
{
    /// <summary>
    /// Interaction logic for ContactPhoneNumbersControl.xaml
    /// </summary>
    public partial class ContactPhoneNumbersControl : ContactAdornerControlBase
    {
        public ContactPhoneNumbersControl()
        {
            InitializeComponent();
        }
    }

    public class ContactPhoneNumbersControl_Root : LocalizationRoot
    {
        public string Header_AllNumbers
        {
            get { return Strings.ContactPhoneNumbersControl_Header_AllNumbers; }
        }

        public string ToolTip_Home
        {
            get { return Strings.ContactPhoneNumbersControl_ToolTip_Home; }
        }

        public string ToolTip_Mobile
        {
            get { return Strings.ContactPhoneNumbersControl_ToolTip_Mobile; }
        }

        public string ToolTip_Work
        {
            get { return Strings.ContactPhoneNumbersControl_ToolTip_Work; }
        }
    }
}
