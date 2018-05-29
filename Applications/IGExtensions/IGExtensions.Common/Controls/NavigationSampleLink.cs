using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IGExtensions.Framework.Controls;

namespace IGExtensions.Common.Controls
{
    [TemplatePart(Name = NavigationButtonImageName, Type = typeof(Image))]
    [TemplatePart(Name = NavigationButtonTextBlockName, Type = typeof(TextBlock))]
    public class NavigationSampleLink : NavigationButton
    {
        public NavigationSampleLink()
        {
            this.DefaultStyleKey = typeof(NavigationSampleLink);
            this.Loaded += new RoutedEventHandler(NavigationSampleLink_Loaded);
        }
        void NavigationSampleLink_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.NavigationButtonImage != null)
                this.NavigationButtonImage.Source = new BitmapImage(this.GetComponentImageUri());
            if (this.NavigationButtonTextBlock != null)
                this.NavigationButtonTextBlock.Text = this.GetComponentName();
            this.NavigationUri = this.GetComponentSampleUri();
        }

        #region Properties
        public const string ComponentPropertyName = "Component";
        public static readonly DependencyProperty ComponentProperty = DependencyProperty.Register(
            ComponentPropertyName, typeof(IgComponents), typeof(NavigationSampleLink),
         new PropertyMetadata(IgComponents.DataChart, (sender, e) =>
         {
             (sender as NavigationSampleLink).OnPropertyChanged(new PropertyChangedEventArgs(ComponentPropertyName));
         }));
        /// <summary>
        /// Gets or sets the Component property 
        /// </summary>
        public IgComponents Component
        {
            get { return (IgComponents)GetValue(ComponentProperty); }
            set { SetValue(ComponentProperty, value); }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (ea.PropertyName == ComponentPropertyName)
            {
                if (!IsTemplateApplied) return;

                if (this.NavigationButtonImage != null)
                    this.NavigationButtonImage.Source = new BitmapImage(this.GetComponentImageUri());
                if (this.NavigationButtonTextBlock != null)
                    this.NavigationButtonTextBlock.Text = this.GetComponentName();
                this.NavigationUri = this.GetComponentSampleUri();
            }
        } 
        #endregion
        protected const string NavigationButtonImageName = "NavigationButtonImage";
        protected const string NavigationButtonTextBlockName = "NavigationButtonTextBlock";

        protected Image NavigationButtonImage;
        protected TextBlock NavigationButtonTextBlock;

        protected bool IsTemplateApplied;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            NavigationButtonImage = base.GetTemplateChild(NavigationButtonImageName) as Image;
            NavigationButtonTextBlock = base.GetTemplateChild(NavigationButtonTextBlockName) as TextBlock;

            IsTemplateApplied = true;
        }

        #region Component Image

        private string GetComponentImagePath()
        {
            var path = "/IGExtensions.Common;component/Assets/Images/Controls/IG";

            if (this.Component == IgComponents.ExcelEngine)
                path += "Excel";
            else if (this.Component == IgComponents.MathLibrary)
                path += "MathLibrary";
            else if (this.Component == IgComponents.CompressionFramework)
                path += "Compression";
            else if (this.Component == IgComponents.WordLibrary)
                path += "Word";
            else if (this.Component == IgComponents.SyntaxParsingEngine)
                path += "SyntaxEditor";
            else if (this.Component == IgComponents.Gauge ||
                     this.Component == IgComponents.LinearGauge ||
                     this.Component == IgComponents.RadialGauge)
                path += "Gauge";
            else
                path += this.Component;

            path += ".png";
            return path;
        }
        private Uri GetComponentImageUri()
        {
            var path = new Uri(GetComponentImagePath(), UriKind.RelativeOrAbsolute);
            return path;
        }
        #endregion

        #region Component Name
        private string GetComponentName()
        {
            var name = "";
            if (this.Component == IgComponents.BarcodeReader)
                name += "Barcode Reader"; 
            else if (this.Component == IgComponents.CompressionFramework)
                name += "Compression Framework"; 
            else if (this.Component == IgComponents.DragDropFramework)
                name += "Drag & Drop Framework";
            else if (this.Component == IgComponents.ExcelEngine)
                name += "Excel Engine"; 
            else if (this.Component == IgComponents.MathLibrary)
                name += "Math Library"; 
            else if (this.Component == IgComponents.PersistenceFramework)
                name += "Persistence Framework"; 
            else if (this.Component == IgComponents.Reporting)
                name += "Reporting Framework";
            else if (this.Component == IgComponents.ResourceWasher)
                name += "Resource Washer";
            else if (this.Component == IgComponents.SyntaxParsingEngine)
                name += "Syntax Parsing Engine"; 
            else if (this.Component == IgComponents.UndoRedo)
                name += "Undo / Redo Framework"; 
            else if (this.Component == IgComponents.WordLibrary)
                name += "Word Library";
            else
                name += "Xam" + this.Component;
              
            return name;
        }

        #endregion

        #region Component Sample

        private Uri GetComponentSampleUri()
        {
            var path = new Uri(GetComponentSamplePath(), UriKind.Absolute);
            return path;
        }

        private string GetSamplesComponentName(IgComponents component)
        {
            switch (component)
            {
                //frameworks with odd names of help pages     
                case IgComponents.BarcodeReader:          return "ig-barcode-reader";
                case IgComponents.PersistenceFramework:   return "control-persistence-framework";
                case IgComponents.DragDropFramework:      return "drag-and-drop-framework";
                case IgComponents.ExcelEngine:            return "wpf-infragistics-excel-engine";
                case IgComponents.MathCalculators:        return "ig-math-calculators";
                case IgComponents.MathLibrary:            return "ig-math-infragistics";
                case IgComponents.Reporting:              return "wpf-reporting";
                case IgComponents.ResourceWasher:         return "reswash";
                case IgComponents.SyntaxParsingEngine:    return "ig-spe";
                case IgComponents.ThemeManager:           return "thememanager";
                case IgComponents.UndoRedo:               return "undo-redo-framework";
                case IgComponents.WordLibrary:            return "word-library";
                //controls with odd names of help pages     
                case IgComponents.CategoryChart: return "categorychart-overview";
                case IgComponents.BulletGraph: return "BulletGraph";
                case IgComponents.ComboEditor: return "xaml-xamComboEditor";
                case IgComponents.DataChart: return "DataChart-DataChart";
                case IgComponents.DataGridX: return "DataGrid";
                case IgComponents.FunnelChart: return "FunnelChart";
                case IgComponents.DoughnutChart: return "DoughnutChart";
                case IgComponents.Gantt: return "xamGantt-xamGantt";
                case IgComponents.PieChart: return "PieChart";
                case IgComponents.Spreadsheet: return "Spreadsheet";
                case IgComponents.RadialGauge: return "RadialGauge";
                case IgComponents.LinearGauge: return "LinearGauge";
                case IgComponents.GeographicMap: return "GeographicMap";
                //controls with standard names of help pages     
                default:
                    return "xam" + component.ToString();
            } 
        }

        private string GetProductsComponentName(IgComponents comp)
        {
            switch (comp)
            {
                case IgComponents.DragDropFramework: return "drag-and-drop-framework";
                case IgComponents.ExcelEngine: return "infragistics-excel-framework";
                case IgComponents.MathLibrary: return "math-library";
                case IgComponents.SpellChecker: return "spell-check-editor";
                case IgComponents.UndoRedo: return "undo-redo-framework";
                case IgComponents.WordLibrary: return "infragistics-word-framework";
                case IgComponents.Map: return "geographic-map";
            }
            return this.GetSamplesComponentName(comp);
        }

        private string GetGroupFromComponent(IgComponents comp)
        {
            switch (comp)
            {
                case IgComponents.BarcodeReader:
                    return "barcodes";
                case IgComponents.BulletGraph:
                case IgComponents.RadialGauge:
                case IgComponents.LinearGauge:
                case IgComponents.SegmentedDisplay:
                    return "gauges";
                case IgComponents.CalculationManager:
                case IgComponents.ColorPicker:
                case IgComponents.ComboEditor:
                case IgComponents.FormulaEditor:
                case IgComponents.MultiColumnCombo:
                case IgComponents.SpellChecker:
                case IgComponents.SyntaxEditor:
                    return "editors";
                case IgComponents.CarouselListbox:
                case IgComponents.CarouselPanel:
                case IgComponents.DataCards:
                case IgComponents.DataPresenter:
                case IgComponents.DockManager:
                case IgComponents.TabControl:
                case IgComponents.TileManager:
                    return "layouts";
                case IgComponents.ContextMenu:
                case IgComponents.DataTree:
                case IgComponents.OutlookBar:
                case IgComponents.TagCloud:
                    return "menus";
                case IgComponents.DataCarousel:
                case IgComponents.DialogWindow:
                    return "interactions";
                case IgComponents.DataChart:
                case IgComponents.PieChart:
                    return "charts";
                case IgComponents.DataGrid:
                case IgComponents.PivotGrid:
                    return "grids";
                case IgComponents.DragDropFramework:
                case IgComponents.ExcelEngine:
                case IgComponents.MathLibrary:
                case IgComponents.PersistenceFramework:
                case IgComponents.Reporting:
                case IgComponents.ResourceWasher:
                case IgComponents.SyntaxParsingEngine:
                case IgComponents.UndoRedo:
                case IgComponents.WordLibrary:
                    return "frameworks";
                case IgComponents.MonthCalendar:
                    return "schedules";
                case IgComponents.NetworkNode:
                case IgComponents.OrgChart:
                case IgComponents.GeographicMap:
                case IgComponents.Map:
                    return "maps";
            }
            return string.Empty;
        }

        private string GetComponentSamplePath()
        {
            var path = "http://";
            var culture = NavigationApp.CurrentCultureName().ToLower();
            if (culture.Contains("jp"))
            {
                path += "jp.";
            }
            else
            {
                path += "www.";
            }
            path += "infragistics.com/help/wpf/";
            path += GetSamplesComponentName(this.Component);
            return path.ToLower();
        }

        #endregion
    }

    public enum IgComponents
    {
        Barcode,
        BarcodeReader,
        BulletGraph,
        CalculationManager,
        Calendar,
        CarouselListbox,
        CarouselPanel,
        CategoryChart,
        ColorPicker,
        ComboEditor, 
        ContextMenu,
        CompressionFramework,
        DataCards,
        DataCarousel,
        DataChart,
        DataGrid,
        DataGridX,
        DataPresenter,
        DataTree,
        DatePicker,
        DialogWindow,
        DockManager,
        DoughnutChart,
        DragDropFramework,
        Editors,
        ExcelEngine,
        FormulaEditor,
        FunnelChart,
        Gantt,
        Gauge,
        LinearGauge,
        RadialGauge,
        SegmentedDisplay,
        GeographicMap,
        Grid,
        HtmlViewer,
        Inputs,
        Map,
        MathCalculators,
        MathLibrary,
        Menu,
        MonthCalendar,
        MultiColumnCombo,
        NetworkNode,
        OrgChart,
        OutlookBar,
        PersistenceFramework,
        PieChart,
        PivotGrid,
        Reporting,
        ResourceWasher,
        Ribbon,
        Schedule,
        Slider,
        Sparkline,
        SpellChecker,
        Spreadsheet,
        SyntaxEditor,
        SyntaxParsingEngine,
        TabControl,
        TagCloud,
        ThemeManager,
        TileManager,
        TileView,
        Timeline,
        Tree,
        Treemap,
        UndoRedo, 
        WordLibrary,
        Zoombar,
    }

}