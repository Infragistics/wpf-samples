using System;
using Xamarin.Forms;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Infragistics.Framework
{
    //TODO move title to .xaml  (see SettingsPopup.xaml)
    //TODO wrapped visuals in ScrollView
    public class PropertyGrid : Grid
    {
        protected bool IsUpdating = false;

        public PropertyGrid()
        {
            ArrangeCount = 0; 
            this.Labels = new ObservableCollection<Label>();
            this.Labels.CollectionChanged += OnPropertyCollectionChanged;
            this.Values = new ObservableCollection<View>();
            this.Values.CollectionChanged += OnPropertyCollectionChanged;
            this.Editors = new ObservableCollection<View>();
            this.Editors.CollectionChanged += OnPropertyCollectionChanged;
            this.Toggles = new ObservableCollection<View>();
            this.Toggles.CollectionChanged += OnPropertyCollectionChanged;
        }

        private void OnPropertyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Logs.Trace(this.GetType() + ".CollectionChanged '" + e.Action + "'");
            ArrangeItems();
        }


        #region Properties
        
        public static readonly BindableProperty ShowEditorsBelowLabelsProperty = BindableProperty.Create(
              "ShowEditorsBelowLabels", typeof(bool), typeof(PropertyGrid), false);
        public bool ShowEditorsBelowLabels
        {
            get { return (bool)GetValue(ShowEditorsBelowLabelsProperty); }
            set { SetValue(ShowEditorsBelowLabelsProperty, value); }
        }

        public static readonly BindableProperty LabelsProperty = BindableProperty.Create(
               "Labels", typeof(ObservableCollection<Label>), typeof(PropertyGrid), null);
        public ObservableCollection<Label> Labels
        {
            get { return (ObservableCollection<Label>)GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        public static readonly BindableProperty ValuesProperty = BindableProperty.Create(
                "Values", typeof(ObservableCollection<View>), typeof(PropertyGrid), null);
        public ObservableCollection<View> Values
        {
            get { return (ObservableCollection<View>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly BindableProperty EditorsProperty = BindableProperty.Create(
                "Editors", typeof(ObservableCollection<View>), typeof(PropertyGrid), null);
        public ObservableCollection<View> Editors
        {
            get { return (ObservableCollection<View>)GetValue(EditorsProperty); }
            set { SetValue(EditorsProperty, value); }
        }

        public static readonly BindableProperty TogglesProperty = BindableProperty.Create(
               "Toggles", typeof(ObservableCollection<View>), typeof(PropertyGrid), null);
        public ObservableCollection<View> Toggles
        {
            get { return (ObservableCollection<View>)GetValue(TogglesProperty); }
            set { SetValue(TogglesProperty, value); }
        }

        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(
              "LabelStyle", typeof(Style), typeof(PropertyGrid), null);
        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        public static readonly BindableProperty ValueStyleProperty = BindableProperty.Create(
              "ValueStyle", typeof(Style), typeof(PropertyGrid), null);
        public Style ValueStyle
        {
            get { return (Style)GetValue(ValueStyleProperty); }
            set { SetValue(ValueStyleProperty, value); }
        }

        public static readonly BindableProperty EditorStyleProperty = BindableProperty.Create(
              "EditorStyle", typeof(Style), typeof(PropertyGrid), null);
        public Style EditorStyle
        {
            get { return (Style)GetValue(EditorStyleProperty); }
            set { SetValue(EditorStyleProperty, value); }
        }

        public static readonly BindableProperty ToggleStyleProperty = BindableProperty.Create(
              "ToggleStyle", typeof(Style), typeof(PropertyGrid), null);
        public Style ToggleStyle
        {
            get { return (Style)GetValue(ToggleStyleProperty); }
            set { SetValue(ToggleStyleProperty, value); }
        }

        public static readonly BindableProperty SerperatorStyleProperty = BindableProperty.Create(
                     "SerperatorStyle", typeof(Style), typeof(PropertyGrid), null);
        public Style SerperatorStyle
        {
            get { return (Style)GetValue(SerperatorStyleProperty); }
            set { SetValue(SerperatorStyleProperty, value); }
        }

        public static readonly BindableProperty SerperatorVisibleProperty = BindableProperty.Create(
                     "SerperatorVisible", typeof(bool), typeof(PropertyGrid), false);
        public bool SerperatorVisible
        {
            get { return (bool)GetValue(SerperatorVisibleProperty); }
            set { SetValue(SerperatorVisibleProperty, value); }
        } 
        #endregion

        protected int ArrangeCount = 0;

        protected void ArrangeItems()
        {
            // MDD237371 - check if children is null
            if (this.Children == null) return;
            
            this.Children.Clear();
                      
            // check if all items are set
            if (Labels == null || Values == null || Editors == null || Toggles == null) return;

            if (Labels.Count == 0) return;
            if (Values.Count == 0 && Editors.Count == 0 && Toggles.Count == 0) return;
              
            if (ShowEditorsBelowLabels)
            {
                ArrangeItemsInRows();
            }
            else
            {
                ArrangeItemsInColumns();
            }
            
        }
        private void ArrangeItemsInRows()
        {
            //check if grid has enough rows
            var itemsCount = 0;
            itemsCount = Math.Max(itemsCount, Labels.Count);
            itemsCount = Math.Max(itemsCount, Values.Count);
            itemsCount = Math.Max(itemsCount, Editors.Count);
            itemsCount = Math.Max(itemsCount, Toggles.Count);

            var columnCount = 0;
            if (Labels.Count > 0) { columnCount++; }  
            if (Values.Count > 0) { columnCount++; }  
            if (Editors.Count > 0) { columnCount++; }
            if (Toggles.Count > 0) { columnCount++; }

            this.ColumnDefinitions.Clear();
            this.RowDefinitions.Clear();

            var rowMultipler = 1;
            
            for (int i = 0; i < itemsCount; i++)
            {
                double height = 0.0;
                height = Math.Max(Labels.Count > i ? Labels[i].HeightRequest : 0.0, height);
                height = Math.Max(Values.Count > i ? Values[i].HeightRequest : 0.0, height);
                height = Math.Max(Editors.Count > i ? Editors[i].HeightRequest : 0.0, height);
                height = Math.Max(Toggles.Count > i ? Toggles[i].HeightRequest : 0.0, height);

                //if (height > 0)
                this.AddRow(GridUnitType.Auto, 1);
                this.AddRow(GridUnitType.Auto, 1);
                //else
                //    this.AddRow(GridUnitType.Star, 1);

                if (SerperatorVisible)
                {
                    this.AddRow(GridUnitType.Auto, 1);
                    rowMultipler = 3;
                }
            }            

            var columnIndex = 0;
            if (Labels.Count > 0)
            {
                this.AddColumn(GridUnitType.Star);
                for (int i = 0; i < Labels.Count; i++)
                {
                    if (Labels[i] is Label && (LabelStyle != null || this.Style != null))
                    {
                        Labels[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "LabelStyle" });
                    }
                    else
                    {
                        Labels[i].Margin = new Thickness(10, 0, 0, 0);
                        Labels[i].HorizontalOptions = LayoutOptions.StartAndExpand;
                        Labels[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }

                    this.Children.Add(Labels[i], columnIndex, (i * rowMultipler));
                }
                columnIndex++;
            }

            if (Values.Count > 0)
            {
                this.AddColumn(GridUnitType.Auto);
                for (int i = 0; i < Values.Count; i++)
                {
                    if (Values[i] is Label)
                    {
                        if (ValueStyle != null || this.Style != null)
                        {
                            Values[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "ValueStyle" });
                        }
                        else
                        {
                            Values[i].Margin = new Thickness(0, 0, 0, 0);
                            Values[i].HorizontalOptions = LayoutOptions.CenterAndExpand;
                            Values[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                        }
                        this.Children.Add(Values[i], columnIndex, (i * rowMultipler) + 1);
                    }  
                }
                columnIndex++;
            }
            if (Editors.Count > 0)
            {
                //this.AddColumn(GridUnitType.Star);
                for (int i = 0; i < Editors.Count; i++)
                {
                    if (Editors[i] is Slider && (EditorStyle != null || this.Style != null))
                    {
                        Editors[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "EditorStyle" });
                    }
                    else
                    {
                        Editors[i].HorizontalOptions = LayoutOptions.FillAndExpand;
                        Editors[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }
                    this.Children.Add(Editors[i], 0, (i * rowMultipler) + 1);

                    if (Editors[i] is ListSlideView)
                    {
                        Grid.SetColumnSpan(Editors[i], this.ColumnDefinitions.Count);
                    }
                }
                columnIndex++;
            }

            if (Toggles.Count > 0)
            {
                for (int i = 0; i < Toggles.Count; i++)
                {
                    if (Toggles[i] is Switch && (ToggleStyle != null || this.Style != null))
                    {
                        Toggles[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "ToggleStyle" });
                    }
                    else
                    {
                        Toggles[i].Margin = new Thickness(0, 0, 10, 0);
                        Toggles[i].HorizontalOptions = LayoutOptions.FillAndExpand;
                        Toggles[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }
                    this.Children.Add(Toggles[i], 1, (i * rowMultipler));
                }
            }

            if (SerperatorVisible)
            {
                for (int i = 0; i < itemsCount - 1; i++)
                {
                    var serperator = new Grid(); //TODO boxview

                    if (SerperatorStyle != null || this.Style != null)
                    {
                        serperator.SetBinding(Grid.StyleProperty, new Binding { Source = this, Path = "SerperatorStyle" });
                    }
                    else
                    {
                        serperator.Margin = new Thickness(0, 2, 0, 2);
                        serperator.HorizontalOptions = LayoutOptions.FillAndExpand;
                        serperator.VerticalOptions = LayoutOptions.CenterAndExpand;
                        serperator.BackgroundColor = Color.FromHex("#EEEEEE");
                        serperator.HeightRequest = 1;
                    }

                    this.Children.Add(serperator, 0, (i * rowMultipler) + 2);
                    Grid.SetColumnSpan(serperator, this.ColumnDefinitions.Count);
                }
            }

            ArrangeCount++;
            //Logs.Trace(this.GetType() + ".ArrangeItems=" + ArrangeCount + ", " + itemsCount);

        }
        private void ArrangeItemsInColumns()
        {
            //check if grid has enough rows
            var itemsCount = 0;
            itemsCount = Math.Max(itemsCount, Labels.Count);
            itemsCount = Math.Max(itemsCount, Values.Count);
            itemsCount = Math.Max(itemsCount, Editors.Count);
            itemsCount = Math.Max(itemsCount, Toggles.Count);

            var columnCount = 0;
            if (Labels.Count > 0) columnCount++;
            if (Values.Count > 0) columnCount++;
            if (Editors.Count > 0) columnCount++;
            if (Toggles.Count > 0) columnCount++;

            this.ColumnDefinitions.Clear();
            this.RowDefinitions.Clear();

            var rowMultipler = 1;

            for (int i = 0; i < itemsCount; i++)
            {
                double height = 0.0;
                height = Math.Max(Labels.Count > i ? Labels[i].HeightRequest : 0.0, height);
                height = Math.Max(Values.Count > i ? Values[i].HeightRequest : 0.0, height);
                height = Math.Max(Editors.Count > i ? Editors[i].HeightRequest : 0.0, height);
                height = Math.Max(Toggles.Count > i ? Toggles[i].HeightRequest : 0.0, height);

                //if (height > 0)
                this.AddRow(GridUnitType.Auto, 1);
                //else
                //    this.AddRow(GridUnitType.Star, 1);

                if (SerperatorVisible)
                {
                    this.AddRow(GridUnitType.Auto, 1);
                    rowMultipler = 2;
                }
            }

            var columnIndex = 0;
            if (Labels.Count > 0)
            {
                this.AddColumn(GridUnitType.Auto);
                for (int i = 0; i < Labels.Count; i++)
                {
                    if (Labels[i] is Label && (LabelStyle != null || this.Style != null))
                    {
                        Labels[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "LabelStyle" });
                    }
                    else
                    {
                        Labels[i].Margin = new Thickness(10, 0, 0, 0);
                        Labels[i].HorizontalOptions = LayoutOptions.StartAndExpand;
                        Labels[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }

                    this.Children.Add(Labels[i], columnIndex, i * rowMultipler);
                }
                columnIndex++;
            }

            if (Values.Count > 0)
            {
                this.AddColumn(GridUnitType.Absolute, 35);
                for (int i = 0; i < Values.Count; i++)
                {
                    this.Children.Add(Values[i], columnIndex, i * rowMultipler);
                    if (Values[i] is Label)
                    {
                        if (ValueStyle != null || this.Style != null)
                        {
                            Values[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "ValueStyle" });
                        }
                        else
                        {
                            Values[i].Margin = new Thickness(0, 0, 0, 0); 
                        }
                        Values[i].HorizontalOptions = LayoutOptions.CenterAndExpand;
                        Values[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }
                    else if (Values[i] is ListSlideView)
                    {
                        Values[i].HorizontalOptions = LayoutOptions.FillAndExpand;
                        Values[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                        Grid.SetColumnSpan(Values[i], columnCount - 1);
                    } 
                }
                columnIndex++;
            }
            if (Editors.Count > 0)
            {
                this.AddColumn(GridUnitType.Star);
                for (int i = 0; i < Editors.Count; i++)
                {

                    if (Editors[i] is Slider && (EditorStyle != null || this.Style != null))
                    {
                        Editors[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "EditorStyle" });
                    }
                    else
                    {
                        Editors[i].HorizontalOptions = LayoutOptions.FillAndExpand;
                        Editors[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }
                    this.Children.Add(Editors[i], columnIndex, i * rowMultipler);

                    if (Editors[i] is ListSlideView)
                    {
                        Grid.SetColumnSpan(Editors[i], columnCount - 2);
                    }
                }
                columnIndex++;
            }

            if (Toggles.Count > 0)
            {
                if (Editors.Count > 0)
                    this.AddColumn(GridUnitType.Auto);
                else
                    this.AddColumn(GridUnitType.Star);

                for (int i = 0; i < Toggles.Count; i++)
                {
                    if (Toggles[i] is Switch && (ToggleStyle != null || this.Style != null))
                    {
                        Toggles[i].SetBinding(StyleProperty, new Binding { Source = this, Path = "ToggleStyle" });
                    }
                    else
                    {
                        Toggles[i].Margin = new Thickness(0, 0, 10, 0);
                        Toggles[i].HorizontalOptions = LayoutOptions.FillAndExpand;
                        Toggles[i].VerticalOptions = LayoutOptions.CenterAndExpand;
                    }
                    this.Children.Add(Toggles[i], columnIndex, i * rowMultipler);
                }
            }

            if (SerperatorVisible)
            {
                for (int i = 0; i < itemsCount - 1; i++)
                {
                    var serperator = new Grid();

                    if (SerperatorStyle != null || this.Style != null)
                    {
                        serperator.SetBinding(Grid.StyleProperty, new Binding { Source = this, Path = "SerperatorStyle" });
                    }
                    else
                    {
                        serperator.Margin = new Thickness(0, 2, 0, 2);
                        serperator.HorizontalOptions = LayoutOptions.FillAndExpand;
                        serperator.VerticalOptions = LayoutOptions.CenterAndExpand;
                        serperator.BackgroundColor = Color.FromHex("#EEEEEE");
                        serperator.HeightRequest = 1;
                    }

                    this.Children.Add(serperator, 0, (i * rowMultipler) + 1);
                    Grid.SetColumnSpan(serperator, this.ColumnDefinitions.Count);
                }
            }

            ArrangeCount++;
            //Logs.Trace(this.GetType() + ".ArrangeItems=" + ArrangeCount + ", " + itemsCount);

        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            //Logs.Trace(this.GetType() + ".OnPropertyChanged '" + propertyName + "'");
            base.OnPropertyChanged(propertyName);

            this.IsUpdating = true;

            if (propertyName == "Labels" ||
                propertyName == "Values" ||
                propertyName == "Editors" ||
                propertyName == "SerperatorVisible")
            {
                ArrangeItems();
            }       

            this.IsUpdating = false;
            
        }
    }

}
