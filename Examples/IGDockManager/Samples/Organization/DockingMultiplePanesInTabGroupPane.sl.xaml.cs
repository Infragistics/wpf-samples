using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class DockingMultiplePanesInTabGroupPane : SampleContainer
    {
        private string path = "/IGDockManager;component/Images/Texture/";

        public DockingMultiplePanesInTabGroupPane()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DockingPanesInTabGroupPane_Loaded);
        }

        void DockingPanesInTabGroupPane_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImagesList();
        }

        private void LoadImagesList()
        {
            this.UpdateLayout();

            // Find element with specified identifier name
            ListBox listBox = (ListBox)this.FindName("ImgList");

            for (int i = 1; i <= 14; i++)
            {
                string imgName = "Texture0" + i + ".jpg";
                listBox.Items.Add(imgName);
            }

            listBox.SelectedIndex = 0;
        }

        private void ImgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentPane imageContentPane = (ContentPane)xamDockManager.FindPane("CP_ImageContainer");
            string imgName = e.AddedItems[0].ToString();

            Uri imageFileUri = new Uri(path + imgName, UriKind.Relative);

            Border container = imageContentPane.Content as Border;
            container.Child = CreateImageFromUri(imageFileUri);

            Slider opSlider = (Slider)this.FindName("OpacitySlider");
            if (opSlider != null)
            {
                opSlider.Value = 1.0;
            }
        }

        private Image CreateImageFromUri(Uri uri)
        {
            Image image = new Image();
            BitmapImage bmpImage = new BitmapImage(uri);
            image.Source = bmpImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            return image;
        }

        private void Rb_ActivePane_Click(object sender, RoutedEventArgs e)
        {
            // XamDockManager PinBehavior property defines how the panes will react to pin/unpin behavior
            // Here, in the tab Group only the active pane will react to pin/unpin/Auto Hide command. 
            xamDockManager.PinBehavior = PaneActionBehavior.ActivePane;
        }

        private void Rb_AllPanes_Click(object sender, RoutedEventArgs e)
        {
            // All panes in the tab group pane will react to pin/unpin/Auto Hide command.
            xamDockManager.PinBehavior = PaneActionBehavior.AllPanes;
        }
    }
}
