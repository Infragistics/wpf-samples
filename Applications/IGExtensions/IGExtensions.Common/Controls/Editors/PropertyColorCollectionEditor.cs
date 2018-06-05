using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace IGExtensions.Common.Controls
{
    [TemplatePart(Name = "ColorPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "ColorPicker", Type = typeof(ColorPicker))]
    [TemplatePart(Name = "ColorCollectionPreviewBorder", Type = typeof(Border))]
    [TemplatePart(Name = "ColorPickerContainer", Type = typeof(Border))]
    public class PropertyColorCollectionEditor : PropertyBaseEditor  
    {
        protected ColorPicker ColorPicker;
        protected Border ColorPickerContainer;
        protected Popup ColorPopup;
        protected Border ColorCollectionPreviewBorder;
        protected bool IsTemplateApplied;
      
        public PropertyColorCollectionEditor()
        {
            DefaultStyleKey = typeof(PropertyColorCollectionEditor);

            this.Loaded += OnColorCollectionEditorLoaded;
        }

        void OnColorCollectionEditorLoaded(object sender, RoutedEventArgs e)
        {
            IsLoaded = true;
            UpdateBrushPreview();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            ColorCollectionPreviewBorder = base.GetTemplateChild("ColorCollectionPreviewBorder") as Border;
            if (ColorCollectionPreviewBorder != null)
            {
                ColorCollectionPreviewBorder.MouseLeftButtonUp += OnColorCollectionPreviewMouseClick;
            }
            ColorPickerContainer = base.GetTemplateChild("ColorPickerContainer") as Border;
            if (ColorPickerContainer != null)
            {
                ColorPickerContainer.MouseLeave += OnColorPickerContainerMouseLeave;
            } 
            ColorPicker = base.GetTemplateChild("ColorPicker") as ColorPicker;
            if (ColorPicker != null)
            {
                ColorPicker.PropertyChanged += OnColorPickerChanged;
               // ColorPicker.MouseLeave += OnColorCollectionEditorMouseLeave;
            }
            ColorPopup = base.GetTemplateChild("ColorPopup") as Popup;
            if (ColorPopup != null)
            {
               // ColorPopup.MouseLeave += OnColorPopupMouseLeave;
            }

            IsTemplateApplied = true;

            UpdateBrushPreview();
        }
      
        #region Properties

        //public static ColorCollection DefaultColors = new ColorCollection { Colors.Red, Colors.Black };
        public static ObservableCollection<Color> DefaultColors = new ObservableCollection<Color> { Colors.Red, Colors.Black };

        //public const string ColorCollectionPropertyName = "ColorCollection";
        //public static readonly DependencyProperty ColorCollectionProperty = DependencyProperty.Register(
        //    ColorCollectionPropertyName, typeof(ColorCollection), typeof(PropertyColorCollectionEditor),
        //    new PropertyMetadata(DefaultColors, (sender, e) =>
        //    {
        //        (sender as PropertyColorCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(ColorCollectionPropertyName));
        //    }));

        ///// <summary>
        ///// Gets or sets the ColorCollection property 
        ///// </summary>
        //public ColorCollection ColorCollection
        //{
        //    get { return (ColorCollection)GetValue(ColorCollectionProperty); }
        //    set { SetValue(ColorCollectionProperty, value); }
        //}
        public const string ColorCollectionPropertyName = "ColorCollection";
        public static readonly DependencyProperty ColorCollectionProperty = DependencyProperty.Register(
            ColorCollectionPropertyName, typeof(ObservableCollection<Color>), typeof(PropertyColorCollectionEditor),
            new PropertyMetadata(DefaultColors, (sender, e) =>
            {
                (sender as PropertyColorCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(ColorCollectionPropertyName));
            }));

        /// <summary>
        /// Gets or sets the ColorCollection property 
        /// </summary>
        public ObservableCollection<Color> ColorCollection
        {
            get { return (ObservableCollection<Color>)GetValue(ColorCollectionProperty); }
            set { SetValue(ColorCollectionProperty, value); }
        }
        //public const string ColorObservableCollectionPropertyName = "ColorObservableCollection";
        //public static readonly DependencyProperty ColorObservableCollectionProperty = DependencyProperty.Register(
        //    ColorObservableCollectionPropertyName, typeof(ObservableCollection<Color>), typeof(PropertyColorCollectionEditor), 
        //    new PropertyMetadata(null, (sender, e) =>
        //{
        //    (sender as PropertyColorCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(ColorObservableCollectionPropertyName));
        //})); 
        ///// <summary>
        ///// Gets or sets the ColorObservableCollection property 
        ///// </summary>
        //public ObservableCollection<Color> ColorObservableCollection
        //{
        //    get { return (ObservableCollection<Color>)GetValue(ColorObservableCollectionProperty); }
        //    set { SetValue(ColorObservableCollectionProperty, value); }
        //}
        public const string ColorCollectionPreviewPropertyName = "ColorCollectionPreview";
        /// <summary>
        /// Identifies the ColorCollectionPreview dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorCollectionPreviewProperty = DependencyProperty.Register(
            ColorCollectionPreviewPropertyName, typeof(LinearGradientBrush), typeof(PropertyColorCollectionEditor),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as PropertyColorCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(ColorCollectionPreviewPropertyName));
            }));

        /// <summary>
        /// Gets the ColorCollectionPreview.
        /// </summary>
        public LinearGradientBrush ColorCollectionPreview
        {
            get { return (LinearGradientBrush)GetValue(ColorCollectionPreviewProperty); }
            private set { SetValue(ColorCollectionPreviewProperty, value); }
        } 
        #endregion

        protected int ColorIndex = 0;
        private void OnColorCollectionPreviewMouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.ColorCollection.Count == 0 ||
                this.ColorPicker == null || this.ColorPopup == null ||
               !this.IsLoaded || !this.IsTemplateApplied) return;
            
            var element = sender as FrameworkElement;
            if (element != null)
            {
                var x = e.GetPosition(ColorCollectionPreviewBorder).X;
                var p = x / element.ActualWidth;
                this.ColorIndex = (int)(p * this.ColorCollection.Count);
                
                if (this.ColorIndex >= 0 && this.ColorIndex < this.ColorCollection.Count)
                {
                    var color = this.ColorCollection[this.ColorIndex];
                    //if (color == null) return;

                    //Debug.WriteLine("ColorCollection.[" + ColorIndex + "] => " + brush.Color);
                    this.ColorPicker.SelectedBrush = new SolidColorBrush(color);
                }
                //this.ColorPicker.Width = this.ActualWidth - 5;
                //this.ColorPicker.Height = this.ActualWidth - 5;
                this.ColorPopup.IsOpen = !ColorPopup.IsOpen;
                //this.ColorPopup.Width = this.ActualWidth;
                //this.ColorPopup.Height = this.ActualWidth;
                //this.ColorPopup.IsOpen = !this.ColorPopup.IsOpen;
                ////this.ColorPopup.IsOpen = true;
            }
        }
        private void OnColorPickerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedBrush")
            {
                if (this.ColorIndex >= 0 && this.ColorIndex < this.ColorCollection.Count)
                {
                    //Debug.WriteLine("ColorCollection.[" + ColorIndex + "] <= " + this.ColorPicker.SelectedBrush.Color);
                    this.ColorCollection[ColorIndex] = this.ColorPicker.SelectedBrush.Color;
                }
                UpdateBrushPreview();
            }
        }
        private void OnColorPickerContainerMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ColorPopup != null)
                ColorPopup.IsOpen = false;
        }

        private void OnColorCollectionEditorMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ColorPopup != null)
                ColorPopup.IsOpen = false;
        }
        private void UpdateBrushPreview()
        {
            var brushNames = string.Empty;
            //foreach (var color in this.ColorCollection)
            //{
            //    //if(brush is SolidColorBrush)
            //       brushNames += ((SolidColorBrush)brush).Color + ", ";
            //}
            //Debug.WriteLine("ColorCollection = " + brushNames);
             
            var brushPreview = this.ColorCollection.ToGradientBrush();
            this.ColorCollectionPreview = brushPreview;
        }
        private void UpdateBrushEditors()
        {
            //if (this.ColorCollectionItems == null || this.ColorCollection.Count == 0 ||
            //   !this.IsLoaded || !this.IsTemplateApplied) return;

            //ColorCollectionItems.Children.Clear();

            //var index = 0;
            //var w = (ActualWidth / this.ColorCollection.Count) - 5;
            //foreach (var brush in this.ColorCollection)
            //{
            //    if (brush is SolidColorBrush)
            //    {
            //        var margin = (ColorCollectionItems.Children.Count < this.ColorCollection.Count - 1) ? 2.5 : 0;
            //        var itemEditor = new PropertyBrushColorEditor();
            //        itemEditor.Width = w;
            //        itemEditor.BrushColor = brush as SolidColorBrush;
            //        itemEditor.Margin = new Thickness(0, 0, margin, 0);
            //        itemEditor.TabIndex = index;
            //        itemEditor.PropertyChanged += OnItemEditorPropertyChanged;
            //        ColorCollectionItems.Children.Add(itemEditor);
            //        index++;
            //    }
            //}

        }
        private void OnItemEditorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //var brushEditor = sender as PropertyBrushColorEditor;
            //if (brushEditor != null)
            //{
            //    var index = (brushEditor).TabIndex;
            //    if (index < this.ColorCollection.Count)
            //    {
            //        this.ColorCollection[index] = brushEditor.BrushColor;
            //    }
            //}
        }
        public new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case ColorCollectionPropertyName:
                    {
                        UpdateBrushPreview();
                        //ColorObservableCollection = ColorCollection;
                        break;
                    }
                //case ColorObservableCollectionPropertyName:
                //    {
                //        //ColorCollection = ColorObservableCollection;
                //        UpdateBrushPreview();
                //        break;
                //    }
                //case IsReversedColorCollectionPropertyName:
                //    {
                //        UpdatePreviewBrush(this.ColorCollection);
                //        //UpdatePreviewBrush();
                //        break;
                //    }

            }
            base.OnPropertyChanged(ea);
        }
    }
}