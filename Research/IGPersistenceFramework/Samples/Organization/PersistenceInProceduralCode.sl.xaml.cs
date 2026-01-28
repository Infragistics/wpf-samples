using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class PersistenceInProceduralCode : SampleContainer
    {
        private List<string> ignoredPropertiesList = new List<string>();
        private MemoryStream memoryStream = new MemoryStream();
        private PersistenceSettings settings = new PersistenceSettings();
        private bool IsSaveCompleted = false;

        public PersistenceInProceduralCode()
        {
            InitializeComponent();

            // Registers Event handler for PersistenceSaved
            settings.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            if (settings.IgnorePropertySettings.Count > 0)
            {
                settings.IgnorePropertySettings.Clear();
            }

            // Checks which properties will not be persisted
            if (chbTop.IsChecked == true)
            {
                ignoredPropertiesList.Add("Top");
            }
            if (chbLeft.IsChecked == true)
            {
                ignoredPropertiesList.Add("Left");
            }
            if (chbHeight.IsChecked == true)
            {
                ignoredPropertiesList.Add("Height");
            }
            if (chbWidth.IsChecked == true)
            {
                ignoredPropertiesList.Add("Width");
            }

            // Let's set SavePersistenceOptions to "AllButIgnored"
            // All properties will be saved except the ignored
            settings.SavePersistenceOptions = Infragistics.Persistence.Primitives.PersistenceOption.AllButIgnored;
            // Add excluded properties to IgnorePropertySettings
            foreach (string property in ignoredPropertiesList)
            {
                PropertyNamePersistenceInfo pnpi = CreatePropertyNamePersistenceInfo(property);
                settings.IgnorePropertySettings.Add(pnpi);
            }

            // Saves persisted component properties
            memoryStream = PersistenceManager.Save(XamDialogWindow, settings);

            ignoredPropertiesList.Clear();
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            memoryStream.Position = 0;

            //Checks if save is executed
            if (IsSaveCompleted)
            {
                // Loads component's properties from MemoryStream
                PersistenceManager.Load(XamDialogWindow, memoryStream, settings);
            }
        }

        private PropertyNamePersistenceInfo CreatePropertyNamePersistenceInfo(String propertyName)
        {
            PropertyNamePersistenceInfo pnpi = new PropertyNamePersistenceInfo();
            pnpi.PropertyName = propertyName;

            return pnpi;
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Flag for Save process completion
            this.IsSaveCompleted = true;
        }
    }
}
