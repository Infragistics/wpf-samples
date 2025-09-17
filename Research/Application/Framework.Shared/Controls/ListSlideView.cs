using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace Infragistics.Framework 
{     
    public class ListSlideView : Grid
    {
        public ListSlideView()
        {
            ValueLabel = new Label();
            ValueLabel.Margin = new Thickness(3, 2, 3, 2);
            ValueLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem") { Source = this });
            ValueLabel.SetBinding(Label.TextColorProperty, new Binding { Path = "ValueColor", Source = this });
            ValueLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            
            LeftLabel = new Label();
            LeftLabel.Text = "<";
            LeftLabel.HorizontalOptions = LayoutOptions.StartAndExpand;
            LeftLabel.Margin = new Thickness(10, 2, 10, 2);
            //LeftLabel.SetBinding(Label.TextColorProperty, new Binding("ArrowColor") { Source = this });
            LeftLabel.TextColor = Color.Accent;
            LeftLabel.FontAttributes = FontAttributes.Bold;
            LeftLabel.FontSize = 25;

            RightLabel = new Label();
            RightLabel.Text = ">";
            RightLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            RightLabel.Margin = new Thickness(10, 2, 10, 2);
            //RightLabel.SetBinding(Label.TextColorProperty, new Binding("ArrowColor") { Source = this });
            RightLabel.TextColor = Color.Accent;
            //RightLabel.TextColor = Color.FromHex("#FF007DFF");
            RightLabel.FontAttributes = FontAttributes.Bold;
            RightLabel.FontSize = 25;
            
            this.Labels = new List<Label>();
            Labels.Add(this.ValueLabel);
            Labels.Add(this.LeftLabel);
            Labels.Add(this.RightLabel);

            LeftBox = new Grid();
            LeftBox.Padding = new Thickness(0);
            LeftBox.Margin = new Thickness(0);
            //LeftBox.BackgroundColor = Color.Red;
            LeftBox.Children.Add(LeftLabel, 0, 0);

            RightBox = new Grid();
            RightBox.Padding = new Thickness(0);
            RightBox.Margin = new Thickness(0);
            //RightBox.BackgroundColor = Color.Red;
            RightBox.Children.Add(RightLabel, 0, 0);

            this.Padding = new Thickness(0);
            this.BackgroundColor = Color.Transparent;
            this.PropertyChanged += ListSlideView_PropertyChanged;

            ArrangeItems();
            SetCommands();
        }
        private void SetCommands()
        {
            var toggleView = new List<View> { RightBox, this };
            toggleView.Add(new Command(() =>
            {
                this.BackgroundColor = Color.FromHex("#FFCBCBCB");

                if (ItemsSource == null) return;
                if (ItemsSource.Count == 0) return;

                if (SelectedIndex == -1 || SelectedIndex >= ItemsSource.Count - 1)
                    SelectedIndex = 0;
                else
                    SelectedIndex++;

                this.BackgroundColor = Color.Transparent;
            }));

            LeftBox.Add(new Command(() =>
            {
                this.BackgroundColor = Color.FromHex("#FFCBCBCB");

                if (ItemsSource == null) return;
                if (ItemsSource.Count == 0) return;

                if (SelectedIndex == -1 || SelectedIndex == 0)
                    SelectedIndex = ItemsSource.Count - 1;
                else
                    SelectedIndex--;

                this.BackgroundColor = Color.Transparent;
            }));
        }

        private void ListSlideView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedIndex")
            {
                if (SelectedIndex >= 0 && SelectedIndex < ItemsSource.Count)
                    SelectedItem = ItemsSource[SelectedIndex];
            }
            else if (e.PropertyName == "ItemsSource")
            {
                if (ItemsSource != null && ItemsSource.Count > 0)
                    SelectedIndex = 0;
            }
            else if (e.PropertyName == "ValueStyle")
            {
                if (ValueLabel != null && this.ValueStyle != null)
                {
                    ValueLabel.SetBinding(StyleProperty, new Binding { Path = "ValueStyle", Source = this });
                }
            }
            //else if (e.PropertyName == "ValueColor")
            //{
            //    if (ValueLabel != null)
            //    {
            //        ValueLabel.SetBinding(Label.TextColorProperty, new Binding { Path = "ValueColor", Source = this });
            //    }
            //}
            else if (e.PropertyName == "ArrowVisible")
            {
                LeftBox.IsVisible = ArrowVisible;
                RightBox.IsVisible = ArrowVisible; 
            }
            
        }

        #region Properties
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
             "ItemsSource", typeof(IList), typeof(ListSlideView), null);
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
               "SelectedIndex", typeof(int), typeof(ListSlideView), -1);
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
             "SelectedItem", typeof(object), typeof(ListSlideView), null);
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty ValueStyleProperty = BindableProperty.Create(
             "ValueStyle", typeof(Style), typeof(ListSlideView), null);
        public Style ValueStyle
        {
            get { return (Style)GetValue(ValueStyleProperty); }
            set { SetValue(ValueStyleProperty, value); }
        }
        public static readonly BindableProperty ValueColorProperty = BindableProperty.Create(
           "ValueStyle", typeof(Color), typeof(ListSlideView), Color.Accent);
        public Color ValueColor
        {
            get { return (Color)GetValue(ValueColorProperty); }
            set { SetValue(ValueColorProperty, value); }
        }

        public static readonly BindableProperty ArrowColorProperty = BindableProperty.Create(
            "ArrowColor", typeof(Color), typeof(ListSlideView), Color.FromHex("#FFFF3100"));
        public Color ArrowColor
        {
            get { return (Color)GetValue(ArrowColorProperty); }
            set { SetValue(ArrowColorProperty, value); }
        }

        public static readonly BindableProperty ArrowVisibleProperty = BindableProperty.Create(
            "ArrowVisible", typeof(bool), typeof(ListSlideView), true);
        public bool ArrowVisible
        {
            get { return (bool)GetValue(ArrowVisibleProperty); }
            set { SetValue(ArrowVisibleProperty, value); }
        }
        #endregion

        protected Label LeftLabel;
        protected Label RightLabel;
        protected Label ValueLabel;
        protected List<Label> Labels;

        //public IEnumerable ItemsSource { get; set; }
        protected Grid RightBox;
        protected Grid LeftBox;

        protected void ArrangeItems()
        {
            foreach (var label in Labels)
            {
                //label.FontSize = 20;
                label.VerticalOptions = LayoutOptions.CenterAndExpand;
            }

            this.ColumnDefinitions.Clear();
            this.AddColumn(GridUnitType.Auto, 1);
            this.AddColumn(GridUnitType.Star, 1);
            this.AddColumn(GridUnitType.Auto, 1);
                       
            this.Children.Add(LeftBox, 0, 0);
            this.Children.Add(ValueLabel, 1, 0);
            this.Children.Add(RightBox, 2, 0);
        }
    }

    public class ListTemplateCell : ViewCell
    {
        public ListTemplateCell()
        {
            var titleLabel = new Label();
            titleLabel.Text = "ListTemplateCell";
            //titleLabel.SetBinding(Label.TextProperty, "Title");

            var s = new StackLayout();
            s.Children.Add(titleLabel);
            this.View = s;
        }
    }

    public class ListTemplatView : ListView
    {
        public ListTemplatView()
        {
            this.ItemTemplate = CreateDefaultTemplate();

            // ItemTemplate = new DataTemplate(typeof(SwitchListCell)); 
        }


        public DataTemplate CreateDefaultTemplate()
        {
            var template = new DataTemplate(() =>
            {

                var ViewCell = new ViewCell();

                var ValueLabel = new Label { Text = "DataTemplate", };
                var binding = new Binding { Source = ViewCell, Path = "BindingContext" };
                ValueLabel.SetBinding(Label.TextProperty, binding);
                ValueLabel.Margin = new Thickness(10, 0, 10, 0);
                ValueLabel.HorizontalOptions = LayoutOptions.StartAndExpand;
                ValueLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                //Label.SetBinding(Label.TextProperty, "BindingContext", BindingMode.OneWay, null, null,);

                var layout = new Grid();
                layout.Margin = new Thickness(5);
                layout.Padding = new Thickness(2);

                layout.AddColumn(GridUnitType.Auto, 1);


                layout.Children.Add(ValueLabel, 0, 0);


                ViewCell.View = layout;


                return ViewCell;
            });
            //template.CreateContent();
            return template;
        }


    }

}
