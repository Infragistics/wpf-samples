using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IGExtensions.Framework.Effects;
 
namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents the TiledImage
    /// </summary>
    [TemplatePart(Name = "TiledImageElement", Type = typeof(Image))]
    public class TiledImage : Control, INotifyPropertyChanged
    {
        private readonly TileEffect _tileEffect;
        private Image _tiledImageElement;
        private int _imageWidth;
        private int _imageHeight;
        /// <summary>
        /// Constructs an instance of the TiledImage
        /// </summary>
        public TiledImage()
        {
            // add the generic style to the control's resources
            const string stylePath = "/IGExtensions.Framework;component/Controls/TiledImage.xaml";
            this.ApplyStyle(stylePath);
      
            _tileEffect = new TileEffect();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _tiledImageElement = base.GetTemplateChild("TiledImageElement") as Image; // ImageBrush; // Image;
            _tiledImageElement.ImageOpened += TiledImageElement_ImageOpened;
            if (_tiledImageElement != null)
            {
                _tiledImageElement.Effect = _tileEffect;
            }

            this.SizeChanged += TiledImage_SizeChanged;
            this.UpdateTiledImageLayout();
        }

        #region Event Handlers

        void TiledImageElement_ImageOpened(object sender, RoutedEventArgs e)
        {
            BitmapImage bi = _tiledImageElement.Source as BitmapImage;
            if (bi != null)
            {
                _imageWidth = bi.PixelWidth;
                _imageHeight = bi.PixelHeight;
            }

            this.UpdateTiledImageLayout();
        }

        void TiledImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateTiledImageLayout();
        }
        #endregion Event Handlers

        #region Private Methods
        private void UpdateTiledImageLayout()
        {
            _tileEffect.HorizontalTileCount = this.ActualWidth / _imageWidth;
            _tileEffect.VerticalTileCount = this.ActualHeight / _imageHeight;

            DebugManager.LogData("H: " + this.ActualWidth + " / " + _imageWidth + "  > " + _tileEffect.HorizontalTileCount);
            DebugManager.LogData("V: " + this.ActualHeight + " / " + _imageHeight + "  > " + _tileEffect.VerticalTileCount);
        }
        #endregion Private Methods

        #region Properties
        #region Source (Dependency Property)

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(TiledImage), new PropertyMetadata(new PropertyChangedCallback(Source_Changed)));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void Source_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TiledImage ownerObj = (TiledImage)obj;
            ownerObj.OnPropertyChanged("Source");

            ownerObj.UpdateTiledImageLayout();
        }

        #endregion

        #endregion Properties

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
