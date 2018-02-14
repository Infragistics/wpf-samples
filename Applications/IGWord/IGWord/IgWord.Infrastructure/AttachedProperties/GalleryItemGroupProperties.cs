using Infragistics.Windows.Ribbon;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IgWord.Infrastructure.AttachedProperties
{
    public class GalleryItemGroupProperties
    {
        #region DataSourceProperty

        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.RegisterAttached("DataSource", typeof(IEnumerable), typeof(GalleryItemGroupProperties), new PropertyMetadata(null, OnDataSourceChanged));

        public static void SetDataSource(GalleryItemGroup source, object value)
        {
            source.SetValue(DataSourceProperty, value);
        }

        public static object GetDataSource(GalleryItemGroup source)
        {
            return (object)source.GetValue(DataSourceProperty);
        }

        private static void OnDataSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GalleryItemGroup galleryItemGroup = d as GalleryItemGroup;

            if (e.NewValue == null)
                return;

            //Clear any existing items that were owned by this 
            if (galleryItemGroup.ItemKeys.Count > 0)
            {
                foreach (var key in galleryItemGroup.ItemKeys)
                {
                    if (galleryItemGroup.GalleryTool.Items.ContainsKey(key))
                    {
                        galleryItemGroup.GalleryTool.Items.Remove(galleryItemGroup.GalleryTool.Items[key]);
                    }
                }
            }

            galleryItemGroup.ItemKeys.Clear();

            //Get the image converter that will provide image paths
            var imageConverter = galleryItemGroup.GetValue(GalleryItemGroupProperties.ImageConverterProperty) as IValueConverter;

            foreach (var item in (IEnumerable)e.NewValue)
            {
                var galleryItem = new GalleryItem
                {
                    Key = item.ToString(),
                    Text = item.ToString(),
                };

                if (imageConverter != null)
                {
                    galleryItem.Image = new BitmapImage(new Uri((string)imageConverter.Convert(item, typeof(string), null, null), UriKind.RelativeOrAbsolute));
                }

                galleryItemGroup.GalleryTool.Items.Add(galleryItem);
                galleryItemGroup.ItemKeys.Add(item.ToString());
            }

            //Check if GalleryToolProperties.SelectedItemKey was set
            var selectedItem = galleryItemGroup.GalleryTool.GetValue(GalleryToolProperties.SelectedItemKeyProperty);
            {
                if (selectedItem != null)
                {
                    galleryItemGroup.GalleryTool.SetValue(GalleryToolProperties.SelectedItemKeyProperty, null);
                    galleryItemGroup.GalleryTool.SetValue(GalleryToolProperties.SelectedItemKeyProperty, selectedItem);
                }
            }

        }


        #endregion //DataSourceProperty

        #region ImageConverterProperty

        public static readonly DependencyProperty ImageConverterProperty = DependencyProperty.RegisterAttached("ImageConverter", typeof(IValueConverter), typeof(GalleryItemGroupProperties), new PropertyMetadata(null));

        public static void SetImageConverter(GalleryItemGroup source, object value)
        {
            source.SetValue(ImageConverterProperty, value);
        }

        public static object GetImageConverter(GalleryItemGroup source)
        {
            return (object)source.GetValue(ImageConverterProperty);
        }


        #endregion //ImageConverterProperty
    }
}
