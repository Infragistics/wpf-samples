using System;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace Infragistics.Framework
{
    public class GridSplitter : ContentView
    { 
        public GridSplitter()
        {
            this.MinimumHeightRequest = 10;
            this.BackgroundColor = Color.FromHex("#FFDEDCDC");
            this.VerticalOptions = LayoutOptions.Center; 

            var box1 = new BoxView { IsEnabled = false };
            var box2 = new BoxView { IsEnabled = false };

            box1.SetBinding(BackgroundColorProperty, new Binding { Source = this, Path = "LineColor" });
            box2.SetBinding(BackgroundColorProperty, new Binding { Source = this, Path = "LineColor" });

            box1.SetBinding(HeightRequestProperty, new Binding { Source = this, Path = "LineHeightRequest" });
            box2.SetBinding(HeightRequestProperty, new Binding { Source = this, Path = "LineHeightRequest" });

            box1.SetBinding(WidthRequestProperty, new Binding { Source = this, Path = "LineWidthRequest" });
            box2.SetBinding(WidthRequestProperty, new Binding { Source = this, Path = "LineWidthRequest" });

            var layout = new StackLayout();
            layout.Margin = new Thickness(5);
            layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
            layout.SetBinding(PaddingProperty, new Binding { Source = this, Path = "LinePadding" });

            layout.Children.Add(box1);
            layout.Children.Add(box2);

            this.Content = layout;
        }

        public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
                  "LineColor", typeof(Color), typeof(GridSplitter), Color.FromHex("#FF525354"));
        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public static readonly BindableProperty LinePaddingProperty = BindableProperty.Create(
                 "LinePadding", typeof(Thickness), typeof(GridSplitter), new Thickness(2));
        public Thickness LinePadding
        {
            get { return (Thickness)GetValue(LinePaddingProperty); }
            set { SetValue(LinePaddingProperty, value); }
        }

        public static readonly BindableProperty LineHeightRequestProperty = BindableProperty.Create(
                 "LineHeightRequest", typeof(double), typeof(GridSplitter), 2.0);
        public double LineHeightRequest
        {
            get { return (double)GetValue(LineHeightRequestProperty); }
            set { SetValue(LineHeightRequestProperty, value); }
        }

        public static readonly BindableProperty LineWidthRequestProperty = BindableProperty.Create(
                 "LineWidthRequest", typeof(double), typeof(GridSplitter), 30.0);
        public double LineWidthRequest
        {
            get { return (double)GetValue(LineWidthRequestProperty); }
            set { SetValue(LineWidthRequestProperty, value); }
        }


        public void UpdateGrid(double dragOffsetX, double dragOffsetY)
        {
            if (Parent as Grid == null)
            {
                throw new Exception("GridSplitter does not have a Grid as its parent");
            }

            if (IsRowSplitter())            
                UpdateRow(dragOffsetY);            
            else
                UpdateColumn(dragOffsetX);
        }

        private bool IsRowSplitter()
        {
            return HorizontalOptions.Alignment == LayoutAlignment.Fill;
        }

        private void UpdateRow(double offsetY)
        {
            if (offsetY == 0)
            {
                return;
            }

            var grid = Parent as Grid;
            var row = Grid.GetRow(this);
            int rowCount = grid.RowDefinitions.Count();
            if (rowCount <= 1 ||
                row == 0 ||
                row == rowCount - 1 ||
                row + Grid.GetRowSpan(this) >= rowCount)
            {
                return;
            }

            var rowAbove = grid.RowDefinitions[row - 1];
            var actualHeight = GetRowDefinitionActualHeight(rowAbove) + offsetY;
            if (actualHeight < 0)
            {
                actualHeight = 0;
            }
            else if (actualHeight >= grid.Height - this.Height)
            {
                actualHeight = grid.Height - this.Height;
            }
            //Debug.WriteLine("Parent " + grid.Height + ", Height " + Height + ", actualHeight" + actualHeight);

            rowAbove.Height = new GridLength(actualHeight);
        }

        private void UpdateColumn(double offsetX)
        {
            if (offsetX == 0)
            {
                return;
            }

            var grid = Parent as Grid;
            var column = Grid.GetColumn(this);
            int columnCount = grid.ColumnDefinitions.Count();
            if (columnCount <= 1 ||
                column == 0 ||
                column == columnCount - 1 ||
                column + Grid.GetColumnSpan(this) >= columnCount)
            {
                return;
            }

            var columnLeft = grid.ColumnDefinitions[column - 1];
            var actualWidth = GetColumnDefinitionActualWidth(columnLeft) + offsetX;
            if (actualWidth < 0)
            {
                actualWidth = 0;
            }
            else if (actualWidth >= grid.Width - this.Width)
            {
                actualWidth = grid.Width - this.Width;
            }

            columnLeft.Width = new GridLength(actualWidth);
        }

        static private double GetRowDefinitionActualHeight(RowDefinition row)
        {
            double actualHeight;
            if (row.Height.IsAbsolute)
            {
                actualHeight = row.Height.Value;
            }
            else
            {
                var property = row.GetType().GetRuntimeProperties().First((p) => p.Name == "ActualHeight");
                actualHeight = (double)property.GetValue(row);
            }
            return actualHeight;
        }

        static private double GetColumnDefinitionActualWidth(ColumnDefinition column)
        {
            double actualWidth;
            if (column.Width.IsAbsolute)
            {
                actualWidth = column.Width.Value;
            }
            else
            {
                var property = column.GetType().GetRuntimeProperties().First((p) => p.Name == "ActualWidth");
                actualWidth = (double)property.GetValue(column);
            }
            return actualWidth;
        }
        
        public static new BindableProperty ControlTemplateProperty = BindableProperty.Create(
               "ControlTemplate", typeof(DataTemplate), typeof(GridSplitter),
               null, BindingMode.Default, null, OnControlTemplateChanged);
        public new DataTemplate ControlTemplate
        {
            get { return (DataTemplate)GetValue(ControlTemplateProperty); }
            set { SetValue(ControlTemplateProperty, value); }
        }

        private static void OnControlTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var owner = bindable as GridSplitter;
            if (owner == null) return;

            var template = newValue as DataTemplate;
            if (template == null) return;

            owner.Content = (View)template.CreateContent();
        }

        //public static DataTemplate CreateDefaultTemplate()
        //{
        //    var template = new DataTemplate(() => {
        //        var box1 = new BoxView { IsEnabled = false, BackgroundColor = Color.Black, HeightRequest = 2, WidthRequest = 30 };
        //        var box2 = new BoxView { IsEnabled = false, BackgroundColor = Color.Black, HeightRequest = 2, WidthRequest = 30 };

        //        var layout = new StackLayout();
        //        layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
        //        layout.Margin = new Thickness(5);
        //        layout.Padding = new Thickness(2);

        //        layout.Children.Add(box1);
        //        layout.Children.Add(box2);

        //        //< StackLayout HorizontalOptions = "CenterAndExpand" Margin = "5" Padding = "1" >

        //        //           < BoxView HeightRequest = "2" WidthRequest = "25" BackgroundColor = "Black" />

        //        //                < BoxView HeightRequest = "2" WidthRequest = "25" BackgroundColor = "Black" />

        //        //                 </ StackLayout >

        //        //var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
        //        //var ageLabel = new BoxView();
        //        //var locationLabel = new Label { HorizontalTextAlignment = TextAlignment.End };

        //        //nameLabel.SetBinding(Label.TextProperty, "Name");
        //        //ageLabel.SetBinding(Label.TextProperty, "Age");
        //        //locationLabel.SetBinding(Label.TextProperty, "Location");

        //        //grid.Children.Add(nameLabel);
        //        //grid.Children.Add(ageLabel, 1, 0);
        //        //grid.Children.Add(locationLabel, 2, 0);

        //        return layout; //new ViewCell { View = layout };
        //    });
        ////var template = new DataTemplate();
        //template.CreateContent();
        //    return template;
        //}

    }
}
