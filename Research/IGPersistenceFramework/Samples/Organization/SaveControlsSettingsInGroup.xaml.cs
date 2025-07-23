using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGPersistenceFramework.Resources;
using Infragistics.Controls.Layouts;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class SaveControlsSettingsInGroup : SampleContainer
    {
        private MemoryStream memoryStream = new MemoryStream();
        private PersistenceGroup persistenceGroup = new PersistenceGroup();
        private bool IsSaveCompleted = false;

        public SaveControlsSettingsInGroup()
        {
            InitializeComponent();

            // Gets the persistence group which is defined in UserControl Resources Collection in XAML
            persistenceGroup = this.Resources["igPG"] as PersistenceGroup;

            // Registers event handler for the group
            persistenceGroup.Events.PersistenceSaved += new EventHandler<PersistenceSavedEventArgs>(_PersistenceSaved);

            // adding items in the list
            PreferenceListItem[] items = new PreferenceListItem[6];

            string prefTitle = PersistenceStrings.CPF_Group_Pref;

            for (int i = 0; i < 6; i++)
            {
                items[i] = new PreferenceListItem(prefTitle + " " + (i + 1));
            }

            ChannelsList.ItemsSource = items;
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            // Saves persisted group
            memoryStream = PersistenceManager.Save(persistenceGroup);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            memoryStream.Position = 0;

            if (IsSaveCompleted)
            {
                // Loads persistence group from MemoryStream
                PersistenceManager.Load(persistenceGroup, memoryStream);
            }
        }

        private void ChannelsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            int selectedIndex = lb.SelectedIndex;
            XamTile activeTilePane = (XamTile)xamTileManager.Items[selectedIndex];
            activeTilePane.IsMaximized = true;
        }

        private void _PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            // Flag for Save process completion
            this.IsSaveCompleted = true;
        }
    }

    public class PreferenceListItem
    {
        private string theTitle;

        public PreferenceListItem(string newTitle)
        {
            theTitle = newTitle;
        }

        public string Title
        {
            get { return theTitle; }
        }
    }
}
