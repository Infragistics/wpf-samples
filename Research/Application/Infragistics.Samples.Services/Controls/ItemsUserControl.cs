using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.Samples.Controls
{
    public class ItemsUserControl : UserControl
    {

        #region SelectedItem 

        public const string SelectedItemPropertyName = "SelectedItem";

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(SelectedItemPropertyName,
                typeof(object), typeof(ItemsUserControl),
                new PropertyMetadata(null, (sender, e) =>
                {
                    (sender as ItemsUserControl).OnPropertyChanged(new PropertyChangedEventArgs(SelectedItemPropertyName));
                }));

        /// <summary>
        /// Gets or sets the SelectedItem
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        #endregion

        #region SelectedIndex 

        public const string SelectedIndexPropertyName = "SelectedIndex";

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(SelectedIndexPropertyName,
                typeof(int), typeof(ItemsUserControl),
                new PropertyMetadata(0, (sender, e) =>
                {
                    (sender as ItemsUserControl).OnPropertyChanged(new PropertyChangedEventArgs(SelectedIndexPropertyName));
                }));

        /// <summary>
        /// Gets or sets the SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        #endregion
    
        #region ItemsSource 

        public const string ItemsSourcePropertyName = "ItemsSource";

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(ItemsSourcePropertyName,
                typeof(IEnumerable), typeof(ItemsUserControl),
                new PropertyMetadata(null, (sender, e) =>
                {
                    (sender as ItemsUserControl).OnPropertyChanged(new PropertyChangedEventArgs(ItemsSourcePropertyName));
                }));

        /// <summary>
        /// Gets or sets the ItemsSource
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion

        public virtual void OnPropertyChanged(PropertyChangedEventArgs ea)
        {

            System.Diagnostics.Debug.WriteLine(ea.PropertyName);


            //switch (ea.PropertyName)
            //{
            //    case PaletteColorsPropertyName:
            //        {
            //            UpdatePaletteFill();
            //            break;
            //        }
            //    case IsPaletteReversedPropertyName:
            //        {
            //            UpdatePaletteFill();
            //            break;
            //        }
            //}
        }
    }

}
