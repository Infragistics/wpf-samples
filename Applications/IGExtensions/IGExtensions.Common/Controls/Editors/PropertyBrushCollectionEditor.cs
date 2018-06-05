using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using IGExtensions.Framework;
using Infragistics;

namespace IGExtensions.Common.Controls
{
    [TemplatePart(Name = "BrushColorPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "BrushColorPicker", Type = typeof(ColorPicker))]
    [TemplatePart(Name = "BrushCollectionPreviewBorder", Type = typeof(Border))]
    [TemplatePart(Name = "BrushColorPickerContainer", Type = typeof(Border))]
    public class PropertyBrushCollectionEditor : PropertyBaseEditor //ObservableControl
    {
        protected ColorPicker BrushColorPicker;
        protected Border BrushColorPickerContainer;
        protected Popup BrushColorPopup;
        protected Border BrushCollectionPreviewBorder;
        protected bool IsTemplateApplied;
        
        public PropertyBrushCollectionEditor()
        {
            DefaultStyleKey = typeof(PropertyBrushCollectionEditor);

            this.Loaded += OnBrushCollectionEditorLoaded;
        }

        void OnBrushCollectionEditorLoaded(object sender, RoutedEventArgs e)
        {
            IsLoaded = true;
            UpdateBrushPreview();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            BrushCollectionPreviewBorder = base.GetTemplateChild("BrushCollectionPreviewBorder") as Border;
            if (BrushCollectionPreviewBorder != null)
            {
                BrushCollectionPreviewBorder.MouseLeftButtonUp += OnBrushCollectionPreviewMouseClick;
            }
            BrushColorPickerContainer = base.GetTemplateChild("BrushColorPickerContainer") as Border;
            if (BrushColorPickerContainer != null)
            {
                BrushColorPickerContainer.MouseLeave += OnBrushColorPickerContainerMouseLeave;
            } 
            BrushColorPicker = base.GetTemplateChild("BrushColorPicker") as ColorPicker;
            if (BrushColorPicker != null)
            {
                BrushColorPicker.PropertyChanged += OnBrushColorPickerChanged;
               // BrushColorPicker.MouseLeave += OnBrushCollectionEditorMouseLeave;
            }
            BrushColorPopup = base.GetTemplateChild("BrushColorPopup") as Popup;
            if (BrushColorPopup != null)
            {
               // BrushColorPopup.MouseLeave += OnBrushColorPopupMouseLeave;
            }

            IsTemplateApplied = true;

            UpdateBrushPreview();
        }
      
        #region Properties

        public static BrushCollection Brushes =
          new BrushCollection { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black) };

        public const string BrushCollectionPropertyName = "BrushCollection";

        public static readonly DependencyProperty BrushCollectionProperty = DependencyProperty.Register(
            BrushCollectionPropertyName, typeof(BrushCollection), typeof(PropertyBrushCollectionEditor),
            new PropertyMetadata(Brushes, (sender, e) =>
            {
                (sender as PropertyBrushCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(BrushCollectionPropertyName));
            }));

        /// <summary>
        /// Gets or sets the BrushCollection property 
        /// </summary>
        public BrushCollection BrushCollection
        {
            get { return (BrushCollection)GetValue(BrushCollectionProperty); }
            set { SetValue(BrushCollectionProperty, value); }
        }

        public const string BrushCollectionPreviewPropertyName = "BrushCollectionPreview";
        /// <summary>
        /// Identifies the BrushCollectionPreview dependency property.
        /// </summary>
        public static readonly DependencyProperty BrushCollectionPreviewProperty = DependencyProperty.Register(
            BrushCollectionPreviewPropertyName, typeof(LinearGradientBrush), typeof(PropertyBrushCollectionEditor),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as PropertyBrushCollectionEditor).OnPropertyChanged(new PropertyChangedEventArgs(BrushCollectionPreviewPropertyName));
            }));

        /// <summary>
        /// Gets the BrushCollectionPreview.
        /// </summary>
        public LinearGradientBrush BrushCollectionPreview
        {
            get { return (LinearGradientBrush)GetValue(BrushCollectionPreviewProperty); }
            private set { SetValue(BrushCollectionPreviewProperty, value); }
        } 
        #endregion

        protected int BrushIndex = 0;
        private void OnBrushCollectionPreviewMouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.BrushCollection.Count == 0 ||
                this.BrushColorPicker == null || this.BrushColorPopup == null ||
               !this.IsLoaded || !this.IsTemplateApplied) return;
            
            var element = sender as FrameworkElement;
            if (element != null)
            {
                var x = e.GetPosition(BrushCollectionPreviewBorder).X;
                var p = x / element.ActualWidth;
                this.BrushIndex = (int)(p * this.BrushCollection.Count);
                
                if (this.BrushIndex >= 0 && this.BrushIndex < this.BrushCollection.Count)
                {
                    var brush = this.BrushCollection[this.BrushIndex] as SolidColorBrush;
                    if (brush == null) return;

                    //Debug.WriteLine("BrushCollection.[" + BrushIndex + "] => " + brush.Color);
                    this.BrushColorPicker.SelectedBrush = brush;
                }
                //this.BrushColorPicker.Width = this.ActualWidth - 5;
                //this.BrushColorPicker.Height = this.ActualWidth - 5;
                this.BrushColorPopup.IsOpen = !BrushColorPopup.IsOpen;
                //this.BrushColorPopup.Width = this.ActualWidth;
                //this.BrushColorPopup.Height = this.ActualWidth;
                //this.BrushColorPopup.IsOpen = !this.BrushColorPopup.IsOpen;
                ////this.BrushColorPopup.IsOpen = true;
            }
        }
        private void OnBrushColorPickerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedBrush")
            {
                if (this.BrushIndex >= 0 && this.BrushIndex < this.BrushCollection.Count)
                {
                    //Debug.WriteLine("BrushCollection.[" + BrushIndex + "] <= " + this.BrushColorPicker.SelectedBrush.Color);
                    this.BrushCollection[BrushIndex] = this.BrushColorPicker.SelectedBrush;
                }
                UpdateBrushPreview();
            }
        }
        private void OnBrushColorPickerContainerMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (BrushColorPopup != null)
                BrushColorPopup.IsOpen = false;
        }

        private void OnBrushCollectionEditorMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (BrushColorPopup != null)
                BrushColorPopup.IsOpen = false;
        }
        private void UpdateBrushPreview()
        {
            var brushNames = string.Empty;
            foreach (var brush in this.BrushCollection)
            {
                if(brush is SolidColorBrush)
                   brushNames += ((SolidColorBrush)brush).Color + ", ";
            }
            //Debug.WriteLine("BrushCollection = " + brushNames);
             
            var brushPreview = this.BrushCollection.ToGradientBrush();
            this.BrushCollectionPreview = brushPreview;
        }
        private void UpdateBrushEditors()
        {
            //if (this.BrushCollectionItems == null || this.BrushCollection.Count == 0 ||
            //   !this.IsLoaded || !this.IsTemplateApplied) return;

            //BrushCollectionItems.Children.Clear();

            //var index = 0;
            //var w = (ActualWidth / this.BrushCollection.Count) - 5;
            //foreach (var brush in this.BrushCollection)
            //{
            //    if (brush is SolidColorBrush)
            //    {
            //        var margin = (BrushCollectionItems.Children.Count < this.BrushCollection.Count - 1) ? 2.5 : 0;
            //        var itemEditor = new PropertyBrushColorEditor();
            //        itemEditor.Width = w;
            //        itemEditor.BrushColor = brush as SolidColorBrush;
            //        itemEditor.Margin = new Thickness(0, 0, margin, 0);
            //        itemEditor.TabIndex = index;
            //        itemEditor.PropertyChanged += OnItemEditorPropertyChanged;
            //        BrushCollectionItems.Children.Add(itemEditor);
            //        index++;
            //    }
            //}

        }
        private void OnItemEditorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var brushEditor = sender as PropertyBrushColorEditor;
            if (brushEditor != null)
            {
                var brushIndex = (brushEditor).TabIndex;
                if (brushIndex < this.BrushCollection.Count)
                {
                    this.BrushCollection[brushIndex] = brushEditor.BrushColor;
                }
            }
        }
        public new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case BrushCollectionPropertyName:
                    {
                        UpdateBrushPreview();
                        break;
                    }
                //case IsReversedBrushCollectionPropertyName:
                //    {
                //        UpdatePreviewBrush(this.BrushCollection);
                //        //UpdatePreviewBrush();
                //        break;
                //    }

            }
            base.OnPropertyChanged(ea);
        }
    }
}