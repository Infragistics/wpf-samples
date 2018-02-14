using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IGExtensions.Common.Maps.Imagery;
using Infragistics.Controls.Maps;

namespace IGShowcase.GeographicMapBrowser.Views
{
    public partial class MapImagerySelector : UserControl
    {
        public MapImagerySelector()
        {
            InitializeComponent();
             
            this.Loaded += OnControlLoaded;
        }

        void MapImagerySelector_SizeChanged(object sender, SizeChangedEventArgs e)
        { 
        }

        void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            this.MapImageryItemsPresenter.ItemsSource = GeoMapImagryProvider.GetGeographicMapImageryGroups();
            this.SizeChanged += MapImagerySelector_SizeChanged;
       
        }

        protected const Visibility DefaultImageryLabelVisibility = Visibility.Visible;
        public const string ImageryLabelVisibilityPropertyName = "ImageryLabelVisibility";
        public static readonly DependencyProperty ImageryLabelVisibilityProperty = 
            DependencyProperty.Register(ImageryLabelVisibilityPropertyName, typeof(Visibility), typeof(MapImagerySelector), 
            new PropertyMetadata(DefaultImageryLabelVisibility, (s, e) =>
        {
            (s as MapImagerySelector).OnPropertyChanged(new PropertyChangedEventArgs(ImageryLabelVisibilityPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the ImageryLabelVisibility property 
        /// </summary>
        public Visibility ImageryLabelVisibility
        {
            get { return (Visibility)GetValue(ImageryLabelVisibilityProperty); }
            set { SetValue(ImageryLabelVisibilityProperty, value); }
        }

        protected const Visibility DefaultImageryPreviewVisibility = Visibility.Visible;
        public const string ImageryPreviewVisibilityPropertyName = "ImageryPreviewVisibility";
        public static readonly DependencyProperty ImageryPreviewVisibilityProperty = 
            DependencyProperty.Register(ImageryPreviewVisibilityPropertyName, typeof(Visibility), typeof(MapImagerySelector),
            new PropertyMetadata(DefaultImageryPreviewVisibility, (s, e) =>
        {
            (s as MapImagerySelector).OnPropertyChanged(new PropertyChangedEventArgs(ImageryPreviewVisibilityPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the ImageryPreviewVisibility property 
        /// </summary>
        public Visibility ImageryPreviewVisibility
        {
            get { return (Visibility)GetValue(ImageryPreviewVisibilityProperty); }
            set { SetValue(ImageryPreviewVisibilityProperty, value); }
        }


        public const string ImageryViewSourcePropertyName = "ImageryViewSource";
        public static readonly DependencyProperty ImageryViewSourceProperty = DependencyProperty.Register(
            ImageryViewSourcePropertyName, typeof(GeoImageryViewModel), typeof(MapImagerySelector),
            new PropertyMetadata(null, (s, e) =>
            {
                (s as MapImagerySelector).OnPropertyChanged(new PropertyChangedEventArgs(ImageryViewSourcePropertyName));
            }));
        /// <summary>
        /// Gets or sets the ImageryViewSource property 
        /// </summary>
        public GeoImageryViewModel ImageryViewSource
        {
            get { return (GeoImageryViewModel)GetValue(ImageryViewSourceProperty); }
            set { SetValue(ImageryViewSourceProperty, value); }
        }
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
        }

        private void ImageryButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (sender as Button).DataContext;
            var imageryView = (context as GeoImageryViewModel);
            this.ImageryViewSource = imageryView; 
        }
    }
}
