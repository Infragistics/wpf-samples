using System;
using System.IO;
using System.Windows;
using Infragistics.Controls.Interactions;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class Events : SampleContainer
    {
        private MemoryStream memoryStream = new MemoryStream();
        private PersistenceSettings ps = new PersistenceSettings();
        private bool IsSaveCompleted = false;
        private bool IsLoadCompleted = false;

        public Events()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Gets defined persistence settings for source component
            ps = PersistenceManager.GetSettings(sourceXWDWindow);

            // Register events 
            ps.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);
            ps.Events.PersistenceLoaded += new EventHandler<PersistenceLoadedEventArgs>(_PersistenceLoaded);
            ps.Events.SavePropertyPersistence += new EventHandler<SavePropertyPersistenceEventArgs>(_SavePropertyPersistence);
            ps.Events.LoadPropertyPersistence += new EventHandler<LoadPropertyPersistenceEventArgs>(_LoadPropertyPersistence);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            // Saves persistence settings
            memoryStream = PersistenceManager.Save(sourceXWDWindow, ps);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            memoryStream.Position = 0;

            if (IsSaveCompleted)
            {
                // Loads persistence settings
                PersistenceManager.Load(targetXWDWindow, memoryStream, ps);
                if (chbCancelSave.IsChecked == false && IsLoadCompleted)
                {
                    targetXWDWindow.UpdateLayout();
                }
            }
        }

        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Indicator that save process is completed 
            IsSaveCompleted = true;
            txtBox.Text += PrintLog("PersistenceSaved");
            IsLoadCompleted = false;
        }

        private void _PersistenceLoaded(object sender, PersistenceLoadedEventArgs e)
        {
            txtBox.Text += PrintLog("PersistenceLoaded");
            IsLoadCompleted = true;
        }

        private void _SavePropertyPersistence(object sender, SavePropertyPersistenceEventArgs e)
        {

            txtBox.Text += PrintLog("SavePropertyPersistence");
            txtBox.Text += PrintLog("Owner: " + ((XamDialogWindow)e.RootOwner).Name);
            txtBox.Text += PrintLog(e.PropertyPath + " : " + e.Value);
            if (chbCancelSave.IsChecked == true)
            {
                // the property would not be saved 
                e.Cancel = true;
            }

            txtBox.Text += PrintLog("Cancel Save: " + e.Cancel);
        }

        private void _LoadPropertyPersistence(object sender, LoadPropertyPersistenceEventArgs e)
        {
            txtBox.Text += PrintLog("LoadPropertyPersistence");
            txtBox.Text += PrintLog("Owner " + ((XamDialogWindow)e.Owner).Name);
            txtBox.Text += PrintLog(e.PropertyPath + " : " + e.Value);
        }

        private static string PrintLog(string msg)
        {
            string logMsg = "[" + DateTime.Now.ToString("hh:mm") + "] " + msg + "\n";

            return logMsg;
        }
    }
}
