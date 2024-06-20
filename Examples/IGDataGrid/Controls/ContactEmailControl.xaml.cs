using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using IGDataGrid.Controls.Resources;
using Infragistics.Samples.Shared.Controls;

namespace IGDataGrid.Controls
{
    /// <summary>
    /// Interaction logic for ContactEmailControl.xaml
    /// </summary>
    public partial class ContactEmailControl : ContactAdornerControlBase
    {
        public ContactEmailControl()
        {
            InitializeComponent();
        }

        void OnWriteEmailButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = e.OriginalSource as Button;
                if (btn != null)
                    Process.Start("mailto:" + btn.Tag as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class ContactEmailControl_Root : LocalizationRoot
    {
        public string Header_AllEmailAddresses
        {
            get { return Strings.ContactEmailControl_Header_AllEmailAddresses; }
        }

        public string ToolTip_PersonalEmail
        {
            get { return Strings.ContactEmailControl_ToolTip_PersonalEmail; }
        }

        public string ToolTip_WorkEmail
        {
            get { return Strings.ContactEmailControl_ToolTip_WorkEmail; }
        }

        public string ToolTip_WriteEmail
        {
            get { return Strings.ContactEmailControl_ToolTip_WriteEmail; }
        }
    }
}
