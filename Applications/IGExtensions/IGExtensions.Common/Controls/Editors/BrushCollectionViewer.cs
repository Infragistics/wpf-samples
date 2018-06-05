using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExtensions.Framework;
using Infragistics;

namespace IGExtensions.Common.Controls
{
    public class BrushCollectionViewer : ItemsControl // Control // ItemsControl
    {
        public BrushCollectionViewer()
        {
            //var palette = new PaletteColors();
            //palette.Add(Colors.White);
            //palette.Add(Colors.Red);

            //this.PaletteColors = palette;

            // TODO: load default styles in themes\generic.xaml
            //string stylePath = "/Infragistics.Samples.Shared;component/Controls/BrushPaletteViewer.xaml";

            //this.BrushCollection = new BrushCollection();
            
            this.DefaultStyleKey = typeof(BrushCollectionViewer);

            //// Add the default style to the control's resources
            //ResourceDictionary style = new ResourceDictionary();
            //style.Source = new System.Uri(stylePath, System.UriKind.Relative);
            //this.Resources.MergedDictionaries.Add(style);
        }
        #region Properties

        //public const string IsReversedBrushCollectionPropertyName = "IsReversedBrushCollection";

        ///// <summary>
        ///// Identifies the IsReversedBrushCollection dependency property.
        ///// </summary>
        //public static readonly DependencyProperty IsReversedBrushCollectionProperty =
        //    DependencyProperty.Register(IsReversedBrushCollectionPropertyName, typeof(bool), typeof(BrushCollectionViewer),
        //    new PropertyMetadata(false, (sender, e) =>
        //    {
        //        (sender as BrushCollectionViewer).OnPropertyChanged(new PropertyChangedEventArgs(IsReversedBrushCollectionPropertyName));
        //    }));

        ///// <summary>
        ///// Gets or sets BrushCollection is reversed.
        ///// </summary>
        //public bool IsReversedBrushCollection
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsReversedBrushCollectionProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsReversedBrushCollectionProperty, value);
        //    }
        //}
         
        public const string BrushCollectionPropertyName = "BrushCollection";
        public static readonly DependencyProperty BrushCollectionProperty = DependencyProperty.Register(
            BrushCollectionPropertyName, typeof(BrushCollection), typeof(BrushCollectionViewer), new PropertyMetadata(null, (sender, e) =>
        {
            (sender as BrushCollectionViewer).OnPropertyChanged(new PropertyChangedEventArgs(BrushCollectionPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the BrushCollection property 
        /// </summary>
        public BrushCollection BrushCollection
        {
            get { return (BrushCollection)GetValue(BrushCollectionProperty); }
            set { SetValue(BrushCollectionProperty, value); }
        }

        public const string PaletteColorsPropertyName = "PaletteColors";
        /// <summary>
        /// Identifies the PaletteColors dependency property.
        /// </summary>
        public static readonly DependencyProperty PaletteColorsProperty =
            DependencyProperty.Register(PaletteColorsPropertyName, typeof(PaletteColors), typeof(BrushCollectionViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as BrushCollectionViewer).OnPropertyChanged(new PropertyChangedEventArgs(PaletteColorsPropertyName));
            }));
        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public PaletteColors PaletteColors
        {
            get { return (PaletteColors)GetValue(PaletteColorsProperty); }
            set { SetValue(PaletteColorsProperty, value); }
        }
        

        public const string PreviewBrushPropertyName = "PreviewBrush";
        /// <summary>
        /// Identifies the PreviewBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty PreviewBrushProperty =
            DependencyProperty.Register(PreviewBrushPropertyName, typeof(LinearGradientBrush), typeof(BrushCollectionViewer),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as BrushCollectionViewer).OnPropertyChanged(new PropertyChangedEventArgs(PreviewBrushPropertyName));
            }));

        /// <summary>
        /// Gets the PreviewBrush.
        /// </summary>
        public LinearGradientBrush PreviewBrush
        {
            get { return (LinearGradientBrush)GetValue(PreviewBrushProperty); }
            private set { SetValue(PreviewBrushProperty, value); }
        }
        #endregion

        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case PaletteColorsPropertyName:
                    {
                        //UpdatePreviewBrush();
                        break;
                    }
                case BrushCollectionPropertyName:
                    {
                        UpdatePreviewBrush(this.BrushCollection);
                        break;
                    }
                //case IsReversedBrushCollectionPropertyName:
                //    {
                //        UpdatePreviewBrush(this.BrushCollection);
                //        //UpdatePreviewBrush();
                //        break;
                //    }

            }
        }
        private void UpdatePreviewBrush(BrushCollection brushCollection)
        {
            var brush = brushCollection.ToGradientBrush();
            //if (this.IsReversedBrushCollection)
            //    brush = brush.Reverse();
            this.PreviewBrush = brush;
        }

        //private void UpdatePreviewBrush()
        //{
        //    var brush = new LinearGradientBrush();
        //    if (this.PaletteColors == null || this.PaletteColors.Count == 0)
        //    {
        //        brush.GradientStops.Add(new GradientStop { Color = Colors.White, Offset = 0 });
        //        brush.GradientStops.Add(new GradientStop { Color = Colors.DarkGray, Offset = 1 });
        //    }
        //    else
        //    {
        //        double offsetChange = 1.0 / this.PaletteColors.Count;
        //        double offset = 0;
        //        foreach (Color color in this.PaletteColors)
        //        {
        //            brush.GradientStops.Add(new GradientStop { Color = color, Offset = offset });
        //            offset += offsetChange;
        //        }

        //        if (this.IsReversedBrushCollection)
        //        {
        //            brush.SpreadMethod = GradientSpreadMethod.Reflect;
        //        }
        //    }

        //    //this.PreviewBrush = brush;

        //}
    }

    [TemplatePart(Name = "BrushesComboBox", Type = typeof(ComboBox))]
    [TemplatePart(Name = "BrushesReversedCheckBox", Type = typeof(CheckBox))]
    public class BrushCollectionSelector : Control
    {
        public BrushCollectionSelector()
        {
            this.DefaultStyleKey = typeof(BrushCollectionSelector);

        }

        protected ComboBox BrushesComboBox;
        protected CheckBox BrushesReversedCheckBox;

        public event EventHandler SelectionChanged;
        public BrushCollection SelectedBrushCollection { get; private set; }
      
        /// <summary>
        /// Gets or sets SelectedBrushCollectionIndex property 
        /// </summary>
        public int SelectedBrushCollectionIndex
        {
            get
            {
                if (BrushesComboBox != null)
                    return BrushesComboBox.SelectedIndex;
                return -1;
            }
            set
            {
                if (BrushesComboBox != null && BrushCollectionList.Count > value)
                    BrushesComboBox.SelectedIndex = value;
            }
        }
        public void OnSelectionChanged()
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BrushesComboBox = base.GetTemplateChild("BrushesComboBox") as ComboBox;
            BrushesReversedCheckBox = base.GetTemplateChild("BrushesReversedCheckBox") as CheckBox;

            if (BrushesComboBox != null)
            {
                BrushesComboBox.SelectionChanged += OnBrushComboBoxSelectionChanged;
            }

            if (BrushesReversedCheckBox != null)
            {
                BrushesReversedCheckBox.Click += OnBrushesReversedCheckBoxClick;
            }
        }

        private void OnBrushesReversedCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var isChecked = BrushesReversedCheckBox.IsChecked.Value;
            this.IsReversedBrushItems = isChecked;
        }

        private void OnBrushComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var brushCollection = e.AddedItems[0] as BrushCollection;
                DebugManager.LogTrace("SelectedBrushCollection: " + brushCollection.ToText());
                
                this.SelectedBrushCollection = brushCollection;
                //this.SelectedBrushCollectionIndex = _brushComboBox.SelectedIndex;
                OnSelectionChanged();    
            }
            
        }

        #region Properties

        public const string BrushCollectionListPropertyName = "BrushCollectionList";
        public static readonly DependencyProperty BrushCollectionListProperty = DependencyProperty.Register(
            BrushCollectionListPropertyName, typeof(BrushCollectionList), typeof(BrushCollectionSelector), new PropertyMetadata(null, (sender, e) =>
            {
                (sender as BrushCollectionSelector).OnPropertyChanged(new PropertyChangedEventArgs(BrushCollectionListPropertyName));
            }));
        /// <summary>
        /// Gets or sets the BrushCollectionList property 
        /// </summary>
        public BrushCollectionList BrushCollectionList
        {
            get { return (BrushCollectionList)GetValue(BrushCollectionListProperty); }
            set { SetValue(BrushCollectionListProperty, value); }
        }
        public const string BrushesReversedToggleVisibilityPropertyName = "BrushesReversedToggleVisibility";
        public static readonly DependencyProperty BrushesReversedToggleVisibilityProperty = DependencyProperty.Register(
            BrushesReversedToggleVisibilityPropertyName, typeof(Visibility), typeof(BrushCollectionSelector),
            new PropertyMetadata(Visibility.Collapsed, (sender, e) =>
        {
            (sender as BrushCollectionSelector).OnPropertyChanged(new PropertyChangedEventArgs(BrushesReversedToggleVisibilityPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the BrushesReversedToggleVisibility property 
        /// </summary>
        public Visibility BrushesReversedToggleVisibility
        {
            get { return (Visibility)GetValue(BrushesReversedToggleVisibilityProperty); }
            set { SetValue(BrushesReversedToggleVisibilityProperty, value); }
        }

        public const string IsReversedBrushItemsPropertyName = "IsReversedBrushItems";
        /// <summary>
        /// Identifies the IsReversedBrushItems dependency property.
        /// </summary>
        public static readonly DependencyProperty IsReversedBrushItemsProperty =
            DependencyProperty.Register(IsReversedBrushItemsPropertyName, typeof(bool), typeof(BrushCollectionSelector),
            new PropertyMetadata(false, (sender, e) =>
            {
                (sender as BrushCollectionSelector).OnPropertyChanged(new PropertyChangedEventArgs(IsReversedBrushItemsPropertyName));
            }));

        /// <summary>
        /// Gets or sets IsReversedBrushItems is reversed.
        /// </summary>
        public bool IsReversedBrushItems
        {
            get { return (bool)GetValue(IsReversedBrushItemsProperty); }
            set { SetValue(IsReversedBrushItemsProperty, value); }
        } 
        #endregion
        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                //case PaletteColorsPropertyName:
                //    {
                //        //UpdatePreviewBrush();
                //        break;
                //    }
                //case BrushCollectionPropertyName:
                //    {
                //        UpdatePreviewBrush(this.BrushCollection);
                //        break;
                //    }
                case IsReversedBrushItemsPropertyName:
                    {
                        //if (this.IsReversedBrushItems)
                        //{
                        //    this.BrushCollectionList = this.BrushCollectionList.ReverseItems();
                        //}
                        //System.Diagnostics.Debug.WriteLine("-----------------------");
                        //this.SelectedBrushCollection = null;


                        var index = this.SelectedBrushCollectionIndex;
                        if (index >= 0) DebugManager.LogData(this.BrushCollectionList[index].ToText());
                            
                        this.BrushCollectionList = this.BrushCollectionList.ReverseItems();

                        if (index >= 0) DebugManager.LogData(this.BrushCollectionList[index].ToText());
                        if (index >= 0) this.SelectedBrushCollectionIndex = index;
                        //UpdatePreviewBrush(this.BrushCollection);
                        //UpdatePreviewBrush();
                        break;
                    }

            }
        }
    }
    public class PaletteColors : ObservableCollection<Color>
    {

    }

    public class PaletteColorsCollection : ObservableCollection<PaletteColors>
    {

    }
    //public class BrushCollections : List<BrushCollection>
    //{

    //}
}