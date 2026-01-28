using System;
using System.IO;
using System.Windows;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class PersistSpecificControlProperty : SampleContainer
    {
        private MemoryStream memoryStream = new MemoryStream();
        private PersistenceSettings persistenceSettings = new PersistenceSettings();
        private bool IsSaveCompleted = false;

        public PersistSpecificControlProperty()
        {
            InitializeComponent();
            persistenceSettings = PersistenceManager.GetSettings(XamDialogWindow);
            // Register Event handler for PersistenceSaved
            persistenceSettings.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);
            persistenceSettings.Events.PersistenceLoaded += new EventHandler<PersistenceLoadedEventArgs>(_PersistenceLoaded);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            // save control settings
            memoryStream = PersistenceManager.Save(XamDialogWindow, persistenceSettings);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            memoryStream.Position = 0;

            if (IsSaveCompleted)
            {
                // restore control settings
                PersistenceManager.Load(XamDialogWindow, memoryStream, persistenceSettings);
            }
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Flag for Save process completion
            this.IsSaveCompleted = true;
        }

        private void _PersistenceLoaded(object sender, PersistenceLoadedEventArgs e)
        {
            XamDialogWindow.UpdateLayout();
        }
    }
}
