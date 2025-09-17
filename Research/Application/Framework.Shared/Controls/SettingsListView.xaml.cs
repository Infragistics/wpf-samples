using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Infragistics.Framework 
{ 
    public partial class SettingsListView : ScrollView
    {
		public SettingsListView()
		{
			InitializeComponent(); 

            this.PropertyChanged += SettingsListView_PropertyChanged;
		}

        private void SettingsListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource" ||
                e.PropertyName == "ItemTemplate" ||
                e.PropertyName == "ItemMemberPath")
            {
                ArrangeItems();

                //if (ItemsSource != null && ItemsSource.Count > 0)
                //   SelectedIndex = 0;
            } 
            else if(e.PropertyName == "SelectedIndex")
            {
                if (SelectedIndex >= 0 && SelectedIndex < ItemsSource.Count)
                    SelectedItem = ItemsSource[SelectedIndex];
            }
            else if (e.PropertyName == "SelectedItem")
            {
                UpdateSelection();
                //TODO this.ScrollToAsync(0, 0, true);
            }
        }
        // Event that is raised when an item is tapped.
        public event EventHandler<ItemTappedEventArgs> ItemTapped;
        public void OnItemTapped(object item)
        {
            SelectedItem = item;
            ItemTapped?.Invoke(this, new ItemTappedEventArgs(null, item));
        }

        protected List<ViewCell> ItemCells = new List<ViewCell>();

        public void ArrangeItems()
        {
            ItemsLayout.Children.Clear();
            ItemCells.Clear();

            if (ItemsSource == null || ItemsSource.Count == 0) return;

            var template = ItemTemplate;
            if (template == null)
            {
                template = CreateDefaultTemplate();
            }

            foreach (var item in ItemsSource)
            {
                var itemCell = template.CreateContent() as SettingsListCell;
                if (itemCell == null)
                    throw new Exception("SettingsListView.ItemTemplate does not contain SettingsListCell");

                itemCell.BindingContext = item;
                var itemView = itemCell.View;
                itemView.Add(new Command(() =>
                {
                    OnItemTapped(item);
                }));

                ItemsLayout.Children.Add(itemCell.View);
                ItemCells.Add(itemCell);

            }
            UpdateSelection();
        } 

        public void UpdateSelection()
        {
            foreach (SettingsListCell itemCell in ItemCells)
            { 
                if (itemCell.BindingContext == null) continue;
                
                itemCell.Text = itemCell.BindingContext.ToString();
               
                if (itemCell.BindingContext.Equals(this.SelectedItem))
                {
                    itemCell.BackgroundColor = Color.White;
                    itemCell.SelectionColor = SelectedSperatorColor; 
                    itemCell.TextColor = SelectedTextColor; 
                    itemCell.IsSelected = true;
                }
                else
                {
                    itemCell.BackgroundColor = Color.White;
                    itemCell.SelectionColor = ItemSperatorColor;
                    itemCell.TextColor = ItemTextColor;
                    itemCell.IsSelected = false;
                }
            }
        } 

        public DataTemplate CreateDefaultTemplate()
        {
            var template = new DataTemplate(() =>
            { 
                var viewCell = new SettingsListCell(); 
                var label = new Label { Text = "DataTemplate", };
                
                label.SetBinding(Label.TextColorProperty, new Binding { Source = viewCell, Path = "TextColor" });
                label.BackgroundColor = Color.Transparent;
                label.SetBinding(MarginProperty, new Binding { Source = this, Path = "ItemMargin" });
                label.SetBinding(HorizontalOptionsProperty, new Binding { Source = this, Path = "ItemHorizontalOptions" });
                label.SetBinding(VerticalOptionsProperty, new Binding { Source = this, Path = "ItemVerticalOptions" });
                //label.SetBinding(Label.TextProperty, new Binding { Source = viewCell, Path = "BindingContext" });
                label.SetBinding(Label.TextProperty, new Binding { Source = viewCell, Path = "Text" });
                
                var top = new BoxView();
                top.SetBinding(BackgroundColorProperty, new Binding { Source = viewCell, Path = "SelectionColor" });
                top.HorizontalOptions = LayoutOptions.FillAndExpand;
                top.Margin = new Thickness(0);
                top.HeightRequest = 1;

                var bottom = new BoxView();
                bottom.SetBinding(BackgroundColorProperty, new Binding { Source = viewCell, Path = "SelectionColor" });
                bottom.HorizontalOptions = LayoutOptions.FillAndExpand;
                bottom.Margin = new Thickness(0);
                bottom.HeightRequest = 1;

                var layout = new StackLayout();
                layout.Spacing = 0;
                layout.Margin = new Thickness(0);
                layout.Padding = new Thickness(0);
                layout.SetBinding(BackgroundColorProperty, new Binding { Source = viewCell, Path = "BackgroundColor" });
                
                layout.Children.Add(top);
                layout.Children.Add(label);
                layout.Children.Add(bottom);

                viewCell.View = layout; 

                return viewCell;
            }); 
            return template;
        }

        #region Properties
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                    "ItemsSource", typeof(IList), typeof(SettingsListView), null);
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemMemberPathProperty = BindableProperty.Create(
                     "ItemMemberPath", typeof(string), typeof(SettingsListView), null);
        public string ItemMemberPath
        {
            get { return (string)GetValue(ItemMemberPathProperty); }
            set { SetValue(ItemMemberPathProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                           "ItemTemplate", typeof(DataTemplate), typeof(SettingsListView), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemHorizontalOptionsProperty = BindableProperty.Create(
                            "ItemHorizontalOptions", typeof(LayoutOptions), typeof(SettingsPopup), LayoutOptions.CenterAndExpand);
        public LayoutOptions ItemHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(ItemHorizontalOptionsProperty); }
            set { SetValue(ItemHorizontalOptionsProperty, value); }
        }

        public static readonly BindableProperty ItemVerticalOptionsProperty = BindableProperty.Create(
                            "ItemVerticalOptions", typeof(LayoutOptions), typeof(SettingsPopup), LayoutOptions.CenterAndExpand);
        public LayoutOptions ItemVerticalOptions
        {
            get { return (LayoutOptions)GetValue(ItemVerticalOptionsProperty); }
            set { SetValue(ItemVerticalOptionsProperty, value); }
        }

        public static readonly BindableProperty ItemSperatorColorProperty = BindableProperty.Create(
                    "ItemSperatorColor", typeof(Color), typeof(SettingsListView), Color.FromHex("#F9CBC9C9"));
        public Color ItemSperatorColor
        {
            get { return (Color)GetValue(ItemSperatorColorProperty); }
            set { SetValue(ItemSperatorColorProperty, value); }
        }

        public static readonly BindableProperty ItemTextColorProperty = BindableProperty.Create(
                    "ItemTextColor", typeof(Color), typeof(SettingsListView), Color.FromHex("#F96F6F6F"));
        public Color ItemTextColor
        {
            get { return (Color)GetValue(ItemTextColorProperty); }
            set { SetValue(ItemTextColorProperty, value); }
        }

        static Thickness DefaultItemMargin = new Thickness(8);
        public static readonly BindableProperty ItemMarginProperty = BindableProperty.Create(
                        "ItemMargin", typeof(Thickness), typeof(SettingsListView), DefaultItemMargin);
        public Thickness ItemMargin
        {
            get { return (Thickness)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
                  "SelectedIndex", typeof(int), typeof(SettingsListView), -1);
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                     "SelectedItem", typeof(object), typeof(SettingsListView), null);
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedSperatorColorProperty = BindableProperty.Create(
                    "SelectedSperatorColor", typeof(Color), typeof(SettingsListView), Color.Accent);
        public Color SelectedSperatorColor
        {
            get { return (Color)GetValue(SelectedSperatorColorProperty); }
            set { SetValue(SelectedSperatorColorProperty, value); }
        }

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
                    "SelectedTextColor", typeof(Color), typeof(SettingsListView), Color.Accent);
        public Color SelectedTextColor
        {
            get { return (Color)GetValue(SelectedTextColorProperty); }
            set { SetValue(SelectedTextColorProperty, value); }
        }
        #endregion

    }
     
    public class SettingsListCell : ViewCell
    {
        public void RaisePropertyChanged(string name)
        {
            this.OnPropertyChanged(name);
        }

        private bool _IsSelected;
        /// <summary> Gets or sets IsSelected </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (_IsSelected == value) return; _IsSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private Color _BackgroundColor = Color.White;
        /// <summary> Gets or sets BackgroundColor </summary>
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { if (_BackgroundColor == value) return; _BackgroundColor = value; OnPropertyChanged("BackgroundColor"); }
        }

        private Color _SelectionColor = Color.Accent;
        /// <summary> Gets or sets SelectionColor </summary>
        public Color SelectionColor
        {
            get { return _SelectionColor; }
            set { if (_SelectionColor == value) return; _SelectionColor = value; OnPropertyChanged("SelectionColor"); }
        }

        private Color _TextColor;
        /// <summary> Gets or sets TextColor </summary>
        public Color TextColor
        {
            get { return _TextColor; }
            set { if (_TextColor == value) return; _TextColor = value; OnPropertyChanged("TextColor"); }
        }

        private string _Text;
        /// <summary> Gets or sets Text </summary>
        public string Text 
        {
            get { return _Text; }
            set { if (_Text == value) return; _Text = value; OnPropertyChanged("Text"); }
        }
    }
}
