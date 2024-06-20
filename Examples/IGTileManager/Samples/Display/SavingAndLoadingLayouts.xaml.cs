using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Layouts;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGTileManager.Samples.Display
{
    /// <summary>
    /// Interaction logic for SavingAndLoadingLayouts.xaml
    /// </summary>
    public partial class SavingAndLoadingLayouts : SampleContainer
    {
        PersistenceSettings _settings;
        byte[] _savedState = null;
        private ObservableCollection<string> _imageNameList;

        public SavingAndLoadingLayouts()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SavingAndLoadingLayouts_Loaded);
            this.xamTileManager1.AnimationEnded += new System.EventHandler(xamTileManager1_AnimationEnded);
        }

        void xamTileManager1_AnimationEnded(object sender, System.EventArgs e)
        {
           if (this.cbIamges.SelectedIndex<0)
               this.cbIamges.SelectedIndex = 1;
        }

        void SavingAndLoadingLayouts_Loaded(object sender, RoutedEventArgs e)
        {
            // This list of file names is bound to combo box
            _imageNameList = new ObservableCollection<string>
            {
                "Guy01.jpg",
                "Guy02.jpg",
                "Guy03.jpg",
                "Guy04.jpg",
                "Guy05.jpg",
                "Girl01.jpg",
                "Girl02.jpg",
                "Girl03.jpg",
                "Girl04.jpg",
                "Girl05.jpg"
            };

            this.cbIamges.ItemsSource = _imageNameList;
            _settings = new PersistenceSettings();           
        }       

        private void OnSaveLayout(object sender, RoutedEventArgs e)
        {
            MemoryStream memoryStream = PersistenceManager.Save(xamTileManager1, _settings);
            _savedState = memoryStream.ToArray();
        }

        private void OnLoadLayout(object sender, RoutedEventArgs e)
        {
            if (_savedState == null) return;
            PersistenceManager.Load(xamTileManager1, new MemoryStream(_savedState), _settings);
        }

        private void cbIamges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string imageFileName = e.AddedItems[0].ToString();

                // Check to see if a tile already exists with a given serialization ID
                if (!DoesTileWithSerializationIDExist(imageFileName))
                {
                    XamTile tile = CreateTile(imageFileName);
                    XamTileManager.SetSerializationId(tile, imageFileName);
                    this.xamTileManager1.Items.Add(tile);
                }
            }
        }

        // Helper method to check if a Tile with a specific SerializationId already exists
        private bool DoesTileWithSerializationIDExist(string serializationId)
        {
            foreach (XamTile tile in this.xamTileManager1.Items)
            {
                if (tile != null)
                {
                    string tileSerializationId = tile.Tag as string;
                    if (tileSerializationId != null && tileSerializationId == serializationId)
                        return true;
                }
            }
            return false;
        }

        // Helper method for creating Tiles
        private XamTile CreateTile(string imageName)
        {
            XamTile tile = new XamTile
            {
                Header = imageName,
                Content = GetImage(imageName),
                Tag = imageName
            };

            return tile;
        }

        // Helper method for creating Images
        private Image GetImage(string imageName)
        {
            Image personImage = new Image
            {
                Source = new BitmapImage(ImageFilePathProvider.GetImageLocalUri("People/" + imageName))
            };

            return personImage;
        }
    }
}
