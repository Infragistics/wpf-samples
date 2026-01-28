using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class DataGrid : ContentView
    {
        public DataGrid()
        {
            Logs.Trace(this, "initializing...");

            Columns = new DataColumnList();
            RowsCells = new List<List<DataCell>>();
             
            this.IsClippedToBounds = true;

            RowTapBackground = Paint.FromArgb(255, 188, 232, 255).ToColor();
            RowBackground = Paints.White.ToColor();
            RowAltBackground = Paint.FromArgb(255, 236, 236, 236).ToColor();
            //UseAlternativeRows = true;

            Border = Paint.FromString("#CCCCCC").ToColor();
            BorderThickness = new Thickness(2);

            ContentLayout = new Grid();
            ContentLayout.HorizontalOptions = LayoutOptions.Fill;
            ContentLayout.VerticalOptions = LayoutOptions.Fill;
            ContentLayout.BackgroundColor = Colors.White;
            ContentLayout.RowSpacing = 0;
            ContentLayout.ColumnSpacing = 0; 
            ContentLayout.Padding = new Thickness(0, 0, 0, 0);
            //ContentLayout.LayoutChanged += ContentLayout_LayoutChanged;
            //ContentLayout.MeasureInvalidated += ContentLayout_MeasureInvalidated;

            var columns = ContentLayout.ColumnDefinitions == null ? 0 : ContentLayout.ColumnDefinitions.Count;
            Logs.Trace(this, "initializing grid columns: " + columns);
            Logs.Trace(this, "initializing data columns: " + ColumnsCount);
            
            this.Padding = new Thickness(0);
            this.BackgroundColor = Paints.White.ToColor();
            //this.Content = contentBorder;
            this.Content = ContentLayout;
            Logs.Trace(this, "initializing... done");

            this.SizeChanged += DataGrid_SizeChanged;
        }

        void DataGrid_SizeChanged(object sender, EventArgs e)
        {
            var h = this.Height;
            var w = this.Width;
            Logs.Trace(this, "SizeChanged..." + w + " " + h);
   
        }

        void DataGrid_MeasureInvalidated(object sender, EventArgs e)
        {
            var h = this.Height;
            var w = this.Width;
            Logs.Trace(this, "MeasureInvalidated..." + w + " " + h); 
        }

        void ContentLayout_MeasureInvalidated(object sender, EventArgs e)
        {
            Logs.Trace(this, "Content MeasureInvalidated...");
        }

        void ContentLayout_LayoutChanged(object sender, EventArgs e)
        {
            Logs.Trace(this, "Content LayoutChanged...");
   
        }

        /// <summary>  Occurs when an item is tapped  </summary>
        public event EventHandler<DataRowTappedEventArgs> RowTapped;
       
        #region Fields
        protected List<List<DataCell>> RowsCells;
        protected Grid ContentLayout;
        protected DataCell TappedCell;

        #endregion
        #region Properties
        public Color RowAltBackground { get; set; }
        public Color RowBackground { get; set; }
        public Color RowTapBackground { get; set; }

        ///// <summary> Header Visible </summary>
        //public bool HeaderVisible { get; set; }
        ///// <summary> Selected Visible </summary>
        //public bool RowsSelectable { get; set; }

        ///// <summary> Use Alternative Rows </summary>
        //public bool UseAlternativeRows { get; set; }

        public static readonly BindableProperty UseAlternativeRowsProperty =
           BindableProperty.Create("UseAlternativeRows", typeof(bool), typeof(DataGrid), true);
        public bool UseAlternativeRows
        {
            get { return (bool)GetValue(UseAlternativeRowsProperty); }
            set { SetValue(UseAlternativeRowsProperty, value); }
        }

        public static readonly BindableProperty RowsSelectableProperty =
           BindableProperty.Create("RowsSelectable", typeof(bool), typeof(DataGrid), false);
        public bool RowsSelectable
        {
            get { return (bool)GetValue(RowsSelectableProperty); }
            set { SetValue(RowsSelectableProperty, value); }
        }

        public static readonly BindableProperty HeaderVisibleProperty =
           BindableProperty.Create("HeaderVisible", typeof(bool), typeof(DataGrid), true);
        public bool HeaderVisible
        {
            get { return (bool)GetValue(HeaderVisibleProperty); }
            set { SetValue(HeaderVisibleProperty, value); }
        }
        
        public static readonly BindableProperty BorderThicknessProperty =
           BindableProperty.Create("BorderThickness", typeof(Thickness), typeof(DataGrid), new Thickness(0));
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        
        public static readonly BindableProperty BorderProperty =
           BindableProperty.Create("Border", typeof(Color), typeof(DataGrid), Colors.Gray);
        public Color Border
        {
            get { return (Color)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }

        public static readonly BindableProperty BorderInnerThicknessProperty =
           BindableProperty.Create("BorderInnerThickness", typeof(Thickness), typeof(DataGrid), new Thickness(1));
        public Thickness BorderInnerThickness
        {
            get { return (Thickness)GetValue(BorderInnerThicknessProperty); }
            set { SetValue(BorderInnerThicknessProperty, value); }
        }

        public static readonly BindableProperty BorderInnerProperty =
           BindableProperty.Create("BorderInner",
           typeof(Color), typeof(DataGrid), Colors.Gray);
        public Color BorderInner
        {
            get { return (Color)GetValue(BorderInnerProperty); }
            set { SetValue(BorderInnerProperty, value); }
        }

        public static readonly BindableProperty ColumnsProperty = BindableProperty.Create(
                "Columns", typeof(DataColumnList), typeof(DataGrid), new DataColumnList());

        public DataColumnList Columns
        {
            get { return (DataColumnList)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public int ColumnsCount
        {
            get { return ColumnsVisible == null ? 0 : ColumnsVisible.Count(); }
        }
        public List<DataColumnBase> ColumnsVisible
        {
            get
            {
                return Columns == null ? null :
                       Columns.Where(i => i.IsVisible).ToList();
            }
        }

        public object SelectedItem { get; set; }

        #region ItemsSource
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            "ItemsSource", typeof(IEnumerable), typeof(DataGrid), null, BindingMode.Default, null, OnItemsSourceChanged);
      //  BindableProperty.Create<DataGrid, IEnumerable>(o => o.ItemsSource,
      //     default(IEnumerable), BindingMode.Default, null, OnItemsSourceChanged);

        /// <summary>
        /// Gets or sets ItemsSource 
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable,
            object oldValue, object vewValue)
        {
            var owner = bindable as DataGrid;
            if (owner == null) return;
             
            //owner.OnItemsSourceUpdateStarted();

            owner.ItemsCount = owner.ItemsSource == null ? 0 : 
                               owner.ItemsSource.Cast<object>().Count();

            Logs.Trace(owner, "updating ItemsSource with " + owner.ItemsCount + " items");

            if (owner.IsUpdateContentRequired())
            {
                owner.UpdateContent();
            }
            else
            {
                owner.UpdateView();
            }
            //owner.OnItemsSourceUpdateCompleted();
        }


        #endregion

        #endregion
        
        public Thickness GetRowBorderThickness(int row, int column)
        { 
            var left = column == 0 ? BorderInnerThickness.Left : 0;
            var right = column == ColumnsCount - 1 ? BorderInnerThickness.Right : 0;
            var top = row == 0 ? BorderInnerThickness.Top : 0;
            var bottom = row == RowsCount - 1 ? BorderInnerThickness.Bottom : 0;

            return new Thickness(left, top, right, bottom);
        }

        protected Color GetRowBackground(int rowId)
        {
            if (UseAlternativeRows && rowId % 2 == 0)
                return this.RowAltBackground;
            
            return this.RowBackground;
        }


        public int RowsCount { get { return RowsCells == null ? 0 : RowsCells.Count; } }

        public int ItemsCount { get; private set; }

        public bool IsUpdateContentRequired()
        {
            var dataRows = RowsCells.Count;

            return dataRows == this.ItemsCount;
        }

        /// <summary>
        /// Update content of data cells 
        /// </summary>
        protected void UpdateContent()
        {
            Logs.Trace(this, "updating view content...");
             
            foreach (var column in Columns)
            {
                column.UpdateBindings(ItemsSource as IList);
            }
        }

        protected void UpdateView()
        {
            Logs.Trace(this, "updating view...");
            ContentLayout.Children.Clear();
            ContentLayout.RowDefinitions.Clear();
            ContentLayout.ColumnDefinitions.Clear();
            RowsCells.Clear();  
           
            #region Validation

            if (this.ItemsSource == null)
            {
                Logs.Warning(this, "detected null ItemsSource ");
                ContentLayout.Children.Add(new Label { HeightRequest = 30, Text = "ItemsSource is null!" });
                return;
            }
            var itemType = this.ItemsSource.GetType();
            if (ItemsCount == 0)
            {
                Logs.Warning(this, "detected empty ItemsSource ");
                ContentLayout.Children.Add(new Label { HeightRequest = 30, Text = "ItemsSource is empty!" });
                return;
            }

            if (this.Columns == null || this.ColumnsCount == 0)
            {
                Logs.Warning(this, "detected no data columns ");
                return;
            }
            Logs.Trace(this, "updating view with data columns: " + ColumnsCount + "/" + Columns.Count);
            #endregion
           
            var rowsSize = 1.0 / ItemsCount;
            var rowId = 0;
            var columnId = 0;
           
            if (HeaderVisible)
            {  
                // add one row for columns' headers
                this.ContentLayout.AddRow(rowsSize, GridUnitType.Star);
            }
            // add one rows for all data items
            for (var i = 0; i < ItemsCount; i++)
            {
                this.ContentLayout.AddRow(rowsSize, GridUnitType.Star);
                RowsCells.Add(new List<DataCell>());
            }

            foreach (var column in ColumnsVisible)
            {
                column.Cells.Clear();
               
                this.ContentLayout.AddColumn(column.Definition);
               
                var header = column.CreateHeader();
                Grid.SetRow(header, 0);
                Grid.SetColumn(header, columnId);
                this.ContentLayout.Children.Add(header);

                foreach (var item in this.ItemsSource)
                {
                    itemType = item.GetType();

                    var isSelected = RowsSelectable && rowId == RowsCount - 1;
                    var cellView = column.CreateCellView(item, isSelected);
                    cellView.Tapped += OnCellTapped;
                    //cellView.BindingContext = item; // itemContext; 
                    cellView.BackgroundColor = GetRowBackground(rowId);
                    if (column is BulletDataColumn)
                    {
                        //cellView.BackgroundColor = Colors.Green;
                        //cellView.Padding = new Thickness(0);
                    }
                    cellView.Row = rowId;
                    cellView.Column = columnId;

                    if (isSelected)
                    {
                        TappedCell = cellView;
                    }

                    column.Cells.Add(cellView);

                    RowsCells[rowId].Add(cellView);

                    var row = HeaderVisible ? rowId + 1 : rowId;
                    Grid.SetRow(cellView, row);
                    Grid.SetColumn(cellView, columnId);

                    this.ContentLayout.Children.Add(cellView);
                    rowId++;
                }

                rowId = 0;
                columnId++;
            }
          
            Logs.Trace(this, "updating view... done");
            Logs.Trace(this, "has " + ItemsCount + " items of " + itemType.Name + " type");
        }

   
        protected void OnCellTapped(object sender, EventArgs e)
        {
            if (TappedCell != null)
            {
                foreach (var cell in RowsCells[TappedCell.Row])
                {
                    ColumnsVisible[cell.Column].UpdateSelection(cell, false); 
                }
            }
            TappedCell = sender as DataCell;
            if (TappedCell == null) return;

            SelectedItem = TappedCell.BindingContext;

            foreach (var cell in RowsCells[TappedCell.Row])
            {
                ColumnsVisible[cell.Column].UpdateSelection(cell, true); 
            }

            if (RowTapped != null)
            {
                RowTapped(this, new DataRowTappedEventArgs(TappedCell, TappedCell.BindingContext));
            }
            
        }

        

    }
}