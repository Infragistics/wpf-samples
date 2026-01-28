using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class DisplayFloatingPanes : SampleContainer
    {
        private string path = "/IGDockManager;component/Images/Texture/";

        public DisplayFloatingPanes()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(FloatingPanes_Loaded);
        }

        void FloatingPanes_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImagesList();
        }

        private void LoadImagesList()
        {
            this.UpdateLayout();

            ListBox listBox = (ListBox)this.FindName("ImgList");

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

            // Find OpacitySlider control
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

        private void Rb_MaxType_Root_Click(object sender, RoutedEventArgs e)
        {
            xamDockManager.MaximizeType = MaximizeType.RootVisual;
        }

        private void Rb_MaxType_DockManager_Click(object sender, RoutedEventArgs e)
        {
            xamDockManager.MaximizeType = MaximizeType.DockManager;
        }
    }
}
