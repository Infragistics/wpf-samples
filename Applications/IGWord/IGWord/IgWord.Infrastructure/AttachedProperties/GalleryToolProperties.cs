using Infragistics.Windows.Ribbon;
using System.Linq;
using System.Windows;

namespace IgWord.Infrastructure.AttachedProperties
{
    public class GalleryToolProperties
    {
        #region SelectedItemKeyProperty

        public static readonly DependencyProperty SelectedItemKeyProperty = DependencyProperty.RegisterAttached("SelectedItemKey", typeof(object), typeof(GalleryToolProperties), new PropertyMetadata(null, OnSelectedItemKeyChanged));

        public static void SetSelectedItemKey(GalleryTool source, object value)
        {
            source.SetValue(SelectedItemKeyProperty, value);
        }

        public static object GetSelectedItemKey(GalleryTool source)
        {
            return (object)source.GetValue(SelectedItemKeyProperty);
        }

        private static void OnSelectedItemKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GalleryTool galleryTool = d as GalleryTool;

            if (e.NewValue == null)
                return;

            var target = galleryTool.Items.FirstOrDefault(i => i.Key == e.NewValue.ToString());

            if (target != null)
            {
                galleryTool.ItemSelected -= galleryTool_ItemSelected;
                galleryTool.SelectedItem = target;
                galleryTool.ItemSelected += galleryTool_ItemSelected;
            }
        }

        static void galleryTool_ItemSelected(object sender, Infragistics.Windows.Ribbon.Events.GalleryItemEventArgs e)
        {
            GalleryTool galleryTool = sender as GalleryTool;

            galleryTool.ItemSelected -= galleryTool_ItemSelected;
            SetSelectedItemKey((GalleryTool)sender, e.Item.Key);
            galleryTool.ItemSelected += galleryTool_ItemSelected;
        }


        #endregion //SelectedItemKeyProperty

        #region EnableKeySelectionProperty

        public static readonly DependencyProperty EnableKeySelectionProperty = DependencyProperty.RegisterAttached("EnableKeySelection", typeof(bool), typeof(GalleryToolProperties), new PropertyMetadata(false, OnEnableKeySelectionChanged));

        public static void SetEnableKeySelection(GalleryTool source, bool value)
        {
            source.SetValue(EnableKeySelectionProperty, value);
        }

        public static bool GetEnableKeySelection(GalleryTool source)
        {
            return (bool)source.GetValue(EnableKeySelectionProperty);
        }

        private static void OnEnableKeySelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            GalleryTool galleryTool = d as GalleryTool;


            if ((bool)e.NewValue)
            {
                galleryTool.ItemSelected += galleryTool_ItemSelected;
            }
            else
            {
                galleryTool.ItemSelected -= galleryTool_ItemSelected;
            }
        }

        #endregion //EnableKeySelectionProperty
    }
}
