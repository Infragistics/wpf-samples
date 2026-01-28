using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class UnpinnedPanes : SampleContainer
    {
        private string path = "/IGDockManager;component/Images/Texture/";

        public UnpinnedPanes()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(UnpinnedPanes_Loaded);
        }

        void UnpinnedPanes_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImagesList();
        }

        private void LoadImagesList()
        {
            this.UpdateLayout();

            // Find a specific content pane 
            ContentPane imgContentPane = (ContentPane)xamDockManager.FindPane("CP_ImgList");

            // Get the content of the pane
            ListBox listBox = (ListBox)imgContentPane.Content;

            for (int i = 1; i <= 14; i++)
            {
                string imgName = "Texture0" + i + ".jpg";
                listBox.Items.Add(imgName);
            }

            listBox.SelectedIndex = 0;
        }

        private void PreviewSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ContentPane imageContentPane = (ContentPane)xamDockManager.FindPane("CP_ImageContainer");

            string imgName = "Texture0" + (Math.Round(e.NewValue) + 1).ToString() + ".jpg";

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


        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ContentPane imagePane = (ContentPane)xamDockManager.FindPane("CP_ImageContainer");
            Border border = (Border)imagePane.Content;
            Image img = (Image)border.Child;
            img.Opacity = e.NewValue;
        }

        private void Rb_Flyout_Click(object sender, RoutedEventArgs e)
        {
            // If you hover over unpinned tab item, a flyout animation will appear.
            xamDockManager.UnpinnedTabHoverAction = UnpinnedTabHoverAction.Flyout;
        }

        private void Rb_None_Click(object sender, RoutedEventArgs e)
        {
            // If you hover over unpinned tab item, nothing will happen, a tab will open after a mouse click. 
            xamDockManager.UnpinnedTabHoverAction = UnpinnedTabHoverAction.None;
        }
    }
}
