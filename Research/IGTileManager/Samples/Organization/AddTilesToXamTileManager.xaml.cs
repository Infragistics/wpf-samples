using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IGTileManager.Resources;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGTileManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for AddTilesToXamTileManager.xaml
    /// </summary>
    public partial class AddTilesToXamTileManager : SampleContainer
    {
        private int imageIndex = 0;

        public AddTilesToXamTileManager()
        {
            InitializeComponent();
            this.xamTileManager1.AnimationEnded += new EventHandler(xamTileManager1_AnimationEnded);
        }

        void xamTileManager1_AnimationEnded(object sender, EventArgs e)
        {
            this.xamTileManager1.AnimationEnded -= new EventHandler(xamTileManager1_AnimationEnded);
            OnAddTile(null, null);
        }

        private void OnAddTile(object sender, RoutedEventArgs e)
        {
            BitmapImage personImage = new BitmapImage(GetImageLocation());

            XamTile addTile = new XamTile
            {
                Header = TileManagerStrings.LayoutAndBehavior_AddTilesToXamTilesControl_NewlyAddedTile_Header + " " + (this.xamTileManager1.Items.Count + 1),
                Content = new Image { Source = personImage }
            };

            this.xamTileManager1.Items.Add(addTile);
        }

        private void OnRemoveTiles(object sender, RoutedEventArgs e)
        {
            this.xamTileManager1.Items.Clear();
        }

        private Uri GetImageLocation()
        {
            Uri imageLocation = ImageFilePathProvider.GetImageLocalUri(
                String.Format("People/{0}{1:00}.jpg", (imageIndex % 2 == 0) ? "Guy" : "Girl", (imageIndex % 32) + 1));

            imageIndex++;

            return imageLocation;
        }
    }
}
