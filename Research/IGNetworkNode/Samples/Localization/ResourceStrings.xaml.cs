using System;
using System.Windows;
using System.Windows.Navigation;
using Infragistics.Controls.Maps;

namespace IGNetworkNode.Samples.Localization
{
    public partial class ResourceStrings : Infragistics.Samples.Framework.SampleContainer
    {
        public ResourceStrings()
        {
            //Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamNetworkNode.RegisterResources(
                "IGNetworkNode.Resources.SampleNetworkNodeStrings", typeof(IGNetworkNode.Resources.SampleNetworkNodeStrings).Assembly);

            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamNetworkNode.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGNetworkNode.Resources.SampleNetworkNodeStrings");

            base.OnNavigatingFrom(e);
        }
        
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //Set the Target Type Name of the Node Layout to null.
                //This will cause the NetworkNode to throw an exception.
                networkNode.GlobalNodeLayouts[0].TargetTypeName = null;

                //Note that the exception message is stored
                //in the .resx file, which was set using
                //the XamNetworkNode.RegisterResources method
                //in the constructor of the page.
            }
            catch (Exception exc)
            {
                //Display an error window with the custom message.
                //ChildWindow errorWin = new ErrorWindow(exc.Message);
                //errorWin.Show();
                MessageBox.Show(exc.Message);
            }
            finally
            {
                //Restore the Target Type Name of the Node Layout.
                networkNode.GlobalNodeLayouts[0].TargetTypeName = "NodeModel";
            }
        }
    }
}
