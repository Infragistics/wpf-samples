using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class DataColumnList : List<DataColumnBase>
    {

    }
   
    public abstract class DataColumnBase : BindableObject
    {
        protected DataColumnBase()
        {
            IsVisible = true;
            Cells = new List<DataCell>();

            HorizontalOptions = LayoutOptions.Fill;// CenterAndExpand;
            VerticalOptions = LayoutOptions.Fill;
            Width = new GridLength(1, GridUnitType.Star);

            HeaderUseCaps = true;
            HeaderFontSize = double.NaN;
            HeaderFontAttributes = FontAttributes.Bold;
            HeaderForeground = Colors.Black;
            HeaderBackground = Colors.White;
            HeaderPadding = new Thickness(5, 2, 5, 2);

            CellFontSize = double.NaN;
            CellFontAttributes = FontAttributes.None;
            CellForeground = Colors.Black;
            CellPadding = new Thickness(0, 0, 0, 0);

            SelectionFontSize = double.NaN;
            SelectionFontAttributes = FontAttributes.Bold;
            SelectionForeground = Colors.Black;
            //SelectedUseCaps = true;
        }
        public List<DataCell> Cells { get; private set; }
    
        //public string MemberPath { get; set; }

        public static readonly BindableProperty MemberPathProperty =
            BindableProperty.Create("MemberPath", typeof(string), typeof(DataColumnBase), "-");

        public string MemberPath
        {
            get { return (string)GetValue(MemberPathProperty); }
            set { SetValue(MemberPathProperty, value); }
        }

        #region Properties - Layout
        public GridLength Width { get; set; }
        public ColumnDefinition Definition { get { return new ColumnDefinition { Width = Width }; } }

        public LayoutOptions HorizontalOptions { get; set; }
        public LayoutOptions VerticalOptions { get; set; }
        public bool IsVisible { get; set; }
        #endregion

        #region Properties - Header
        [TypeConverter(typeof(FontSizeConverter))]
        public double HeaderFontSize { get; set; }
        public string HeaderFontFamily { get; set; }
        //public string HeaderText { get; set; }

        public static readonly BindableProperty HeaderTextProperty =
           BindableProperty.Create("HeaderText", typeof(string), typeof(DataColumnBase), null);

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public FontAttributes HeaderFontAttributes { get; set; }
        public Color HeaderForeground { get; set; }
        public Color HeaderBackground { get; set; }
        public Thickness HeaderPadding { get; set; }
        /// <summary> Use capitalized letters in headers </summary>
        public bool HeaderUseCaps { get; set; }
        #endregion

        #region Properties - Cell
        [TypeConverter(typeof(FontSizeConverter))]
        public double CellFontSize { get; set; }
        public string CellFontFamily { get; set; }
        public FontAttributes CellFontAttributes { get; set; }
        public Color CellForeground { get; set; }
        public Thickness CellPadding { get; set; }
        #endregion

        #region Properties - Selected
        [TypeConverter(typeof(FontSizeConverter))]
        public double SelectionFontSize { get; set; }
        public string SelectionFontFamily { get; set; }
        public FontAttributes SelectionFontAttributes { get; set; }
        public Color SelectionForeground { get; set; } 
        #endregion
         
        /// <summary>
        /// Creates column header with binding to MemberPath or HeaderText (if not null)
        /// </summary> 
        public virtual DataCell CreateHeader()
        {
            //var text = string.IsNullOrEmpty(HeaderText) ? MemberPath : HeaderText;
            var binding = new Binding();
            binding.Path = string.IsNullOrEmpty(HeaderText) ? "MemberPath" : "HeaderText";
            binding.Converter = new StringCaseConverter(HeaderUseCaps);

            var content = new Label();
            content.BindingContext = this;

            content.SetBinding(Label.TextProperty, binding);
            // TODO change to property bindings           
            content.LineBreakMode = LineBreakMode.TailTruncation;
            content.HorizontalTextAlignment = TextAlignment.Center;
            content.VerticalTextAlignment = TextAlignment.Center;
            content.TextColor = HeaderForeground;
            content.HorizontalOptions = HorizontalOptions;
            content.VerticalOptions = VerticalOptions;
            content.FontAttributes = HeaderFontAttributes;
            if (!double.IsNaN(HeaderFontSize)) content.FontSize = HeaderFontSize;
            if (!string.IsNullOrEmpty(HeaderFontFamily)) content.FontFamily = HeaderFontFamily;

            var view = new DataCell();
            view.Padding = HeaderPadding;
            view.BackgroundColor = HeaderBackground;
            view.HorizontalOptions = LayoutOptions.Fill;
            //view.Children.Add(content);
            view.Content = content;
            //view.Text = view.BindingContext.ToString();
            //view.SetBinding(Label.TextProperty, new Binding("BindingContext"));
            return view;
        }
        /// <summary>
        /// Creates a view for representing an item
        /// <remarks>The view must have transparent background</remarks>
        /// </summary> 
        public abstract View CreateItemView(object item, bool isSelected);

        public abstract void UpdateSelection(DataCell cell, bool isSelected);

        //public abstract void UpdateContext(DataCell cell, bool isSelected);

        /// <summary>
        /// Creates cell view that wraps an item view and provides row highlighting 
        /// </summary> 
        public virtual DataCell CreateCellView(object item, bool isSelected)
        {
            var content = CreateItemView(item, isSelected);

            var cell = new DataCell();
            cell.Padding = CellPadding; // new Thickness(0);
            //cell.BackgroundColor = Colors.Green;
            cell.BorderThickness = new Thickness(0);
            cell.Content = content;
            cell.HorizontalOptions = HorizontalOptions;
            cell.VerticalOptions = VerticalOptions;
            cell.BindingContext = item;
            
            content.SetBinding(BindingContextProperty, new Binding { Path = "BindingContext", Source = cell });

            return cell;
        }

        public abstract void UpdateBindings(IList items);
    }
    
}