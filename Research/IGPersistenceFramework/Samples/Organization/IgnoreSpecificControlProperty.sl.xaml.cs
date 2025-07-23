using System;
using System.IO;
using System.Windows;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class IgnoreSpecificControlProperty : SampleContainer
    {
        private PersistenceSettings persistenceSettings = new PersistenceSettings();
        private MemoryStream memoryStream = new MemoryStream();
        private bool IsSaveCompleted = false;

        public IgnoreSpecificControlProperty()
        {
            InitializeComponent();

            //Gets defined in XAML PersistenceSettings
            persistenceSettings = PersistenceManager.GetSettings(XamDialogWindow);

            // Registers Event handler for PersistenceSaved
            persistenceSettings.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            // Saves control settings
            memoryStream = PersistenceManager.Save(XamDialogWindow, persistenceSettings);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            memoryStream.Position = 0;

            //Checks if save is executed
            if (IsSaveCompleted)
            {
                // Restores control settings
                PersistenceManager.Load(XamDialogWindow, memoryStream, persistenceSettings);
            }
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Flag for Save process completion
            this.IsSaveCompleted = true;
        }
    }
}
