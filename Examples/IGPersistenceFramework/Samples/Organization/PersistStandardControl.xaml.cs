using System;
using System.IO;
using System.Windows;
using Infragistics.Persistence;
using Infragistics.Persistence.Primitives;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class PersistStandardControl : SampleContainer
    {
        private PersistenceSettings persistenceSettings = new PersistenceSettings();
        private MemoryStream memoryStream = new MemoryStream();
        private bool IsSaveCompleted = false;

        public PersistStandardControl()
        {
            InitializeComponent();

            persistenceSettings.SavePersistenceOptions = PersistenceOption.AllButIgnored;
            persistenceSettings.IgnorePropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "RenderSize" });

            // Register Event handler for PersistenceSaved
            persistenceSettings.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            // save component settings
            memoryStream = PersistenceManager.Save(testImage, persistenceSettings);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            // If control's properties are saved, then load them
            if (IsSaveCompleted)
            {
                // restore components settings
                memoryStream.Position = 0;
                PersistenceManager.Load(testImage, memoryStream, persistenceSettings);
            }
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Flag for Save process completion
            this.IsSaveCompleted = true;
        }
    }
}
