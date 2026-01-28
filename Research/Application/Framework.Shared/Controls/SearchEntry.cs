using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class SearchEntry : ContentView
    {
        public SearchEntry()
        {
            SearchStrategy = SearchStrategy.StartsWithEntry;

            SuggestionsMax = 5;
          
            ContentLayout = new StackLayout();
            TextEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Colors.Black, 
                BackgroundColor = Colors.White,
                //Text = "Enter",
            };
            var suggestionLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                //Font = AppFonts.Small,
                BindingContext = this,

            };
            suggestionLabel.SetBinding(Label.TextProperty, new Binding("SuggestionText"));

            btnSearch = new Button
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Search"
            };

            //var height = 300;
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    height = 600;
            //}

            SuggestionsView = new ListView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                //HeightRequest = height,
                //HasUnevenRows = true
            };
              
            var innerLayout = new StackLayout();
            innerLayout.BackgroundColor = Colors.Black;
            innerLayout.Padding = 0;
            innerLayout.Children.Add(TextEntry);
            //innerLayout.Children.Add(btnSearch);

            SuggestionsLayout = new StackLayout();
            SuggestionsLayout.Padding = new Thickness(2,0,2,0);
            SuggestionsLayout.Children.Add(suggestionLabel);
            SuggestionsLayout.Children.Add(SuggestionsView);
 
            ContentLayout.Children.Add(innerLayout);
            ContentLayout.Children.Add(SuggestionsLayout);

            this.Content = ContentLayout;
            TextEntry.Focused += (s, e) =>
            {
                TextEntry.Text = "";
                OnSearchFocused();  
            };
            TextEntry.TextChanged += (s, e) =>
            { 
                this.SearchText = e.NewTextValue;
            };
            btnSearch.Clicked += (s, e) =>
            {
                if (this.SearchCommand != null && 
                    this.SearchCommand.CanExecute(this.SearchText))
                    this.SearchCommand.Execute(SearchText);
            };
            SuggestionsView.ItemSelected += (s, e) =>
            {
                Logs.Trace(this.GetType() + " SuggestionsView.ItemSelected event firing");
                this.SearchSelectedItem = e.SelectedItem;
                //TextEntry.Text = GetItemValue(e.SelectedItem, this.SearchItemsMemberPath);

                //SuggestionItems = new List<object>();
                ////SuggestionItems.Clear();
                ////ToggleSuggestionsView(false);

                //if (this.SearchSelectedCommand != null &&
                //    this.SearchSelectedCommand.CanExecute(e))
                //    this.SearchSelectedCommand.Execute(e);

                //if (this.ExecuteOnSugestionClick &&
                //    this.SearchCommand != null &&
                //    this.SearchCommand.CanExecute(SearchText))
                //{
                //    this.SearchCommand.Execute(e);
                //}
            };
            SearchItems = new List<object>();
            SuggestionItems = new List<object>();
            SuggestionsView.ItemsSource = this.SuggestionItems;
            SuggestionsLayout.IsVisible = false;
            //SuggestionsView.ItemTemplate = this.SugestionItemDataTemplate;
        }

        protected internal Entry TextEntry { get; private set; }
        protected internal ListView SuggestionsView { get; private set; }
        protected internal StackLayout SuggestionsLayout { get; private set; } 
        protected internal StackLayout ContentLayout { get; private set; }
        protected internal Button btnSearch; 
      
        public event EventHandler SearchFocused;
        public void OnSearchFocused()
        {
            if (SearchFocused != null)
                SearchFocused(this, EventArgs.Empty);
        }

        public event EventHandler SearchItemSelected;

        public void OnSearchItemSelected()
        {
            if (SearchItemSelected != null)
            {
                Logs.Trace(this.GetType() + " SearchItemSelected event firing");
                SearchItemSelected(this, EventArgs.Empty);
            }
        }
         
        #region Bindable Properties

        public static readonly BindableProperty SearchItemsMemberPathProperty = BindableProperty.Create(
                "SearchItemsMemberPath", typeof(string), typeof(SearchEntry), null);
     //   BindableProperty.Create<SearchEntry, string>(o => o.SearchItemsMemberPath,
      //      null, BindingMode.Default, null, null); //OnSearchItemsMemberPathChanged);

        public string SearchItemsMemberPath
        {
            get { return (string)GetValue(SearchItemsMemberPathProperty); }
            set { SetValue(SearchItemsMemberPathProperty, value); }
        }

        public static readonly BindableProperty SearchItemsProperty = BindableProperty.Create(
                "SearchItems", typeof(IEnumerable), typeof(SearchEntry), null);
     //   BindableProperty.Create<SearchEntry, IEnumerable>
     //       (p => p.SearchItems, null);

        /// <summary>
        /// Get or sets source for search items that are searched when entering search text
        /// </summary>
        public IEnumerable SearchItems
        {
            get { return (IEnumerable)GetValue(SearchItemsProperty); }
            set { SetValue(SearchItemsProperty, value); }
        }

        public static readonly BindableProperty SearchSelectedItemProperty = BindableProperty.Create(
           nameof(SearchSelectedItem), typeof(object), typeof(SearchEntry), null, 
           BindingMode.OneWay, null, OnSearchSelectedItemChanged); 
        //  BindableProperty.Create(
        //          "SearchSelectedItem", typeof(object), typeof(SearchEntry), null);
        //   BindableProperty.Create<SearchEntry, object>(p => p.SearchSelectedItem,
        //       null, BindingMode.OneWay, null, OnSearchSelectedItemChanged, null, null);

        /// <summary>
        /// Get or sets selected item from search items
        /// </summary>
        public object SearchSelectedItem
        {
            get { return GetValue(SearchSelectedItemProperty); }
            set { SetValue(SearchSelectedItemProperty, value); }
        }

        private static void OnSearchSelectedItemChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;

            Logs.Trace(owner.GetType() + ".OnSearchSelectedItemChanged " + newValue);
            
            var searchText = "";
            if (newValue != null)
            {
                searchText = GetItemValue(newValue, owner.SearchItemsMemberPath);
            }

            owner.TextEntry.Text = ""; // searchText;
            owner.SuggestionItems = new List<object>();
            //SuggestionItems.Clear();
            //ToggleSuggestionsView(false);

            if (owner.SearchSelectedCommand != null &&
                owner.SearchSelectedCommand.CanExecute(searchText))
            {
                Logs.Trace(owner.GetType() + " SearchSelectedCommand executing with parameter " + searchText);
                owner.SearchSelectedCommand.Execute(searchText);
            }

            if (owner.ExecuteOnSugestionClick &&
                owner.SearchCommand != null &&
                owner.SearchCommand.CanExecute(searchText))
            {
                Logs.Trace(owner.GetType() + " SearchCommand executing with parameter " + searchText);
                owner.SearchCommand.Execute(searchText);
            }

            owner.OnSearchItemSelected();
               
        }

        public static readonly BindableProperty SuggestionItemsProperty = BindableProperty.Create(
                "SuggestionItems", typeof(IEnumerable), typeof(SearchEntry), null, BindingMode.TwoWay, null, OnSuggestionItemsChanged);
      //  BindableProperty.Create<SearchEntry, IEnumerable>(p => p.SuggestionItems,
      //    null, BindingMode.TwoWay, null, OnSuggestionItemsChanged, null, null);

        /// <summary>
        /// Get or sets source for suggestions that are displayed when entering search text
        /// </summary>
        public IEnumerable SuggestionItems
        {
            get { return (IEnumerable)GetValue(SuggestionItemsProperty); }
            set { SetValue(SuggestionItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets PropertyName
        /// </summary>
        public int SuggestionsMax { get; set; }

        static void OnSuggestionItemsChanged(BindableObject obj,
           object oldValue,
           object newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;

            Logs.Trace(owner.GetType() + ".OnSuggestionItemsChanged " + newValue);
            if (newValue == null)
            {
                owner.SuggestionsView.ItemsSource = new List<object>();
                owner.SuggestionsLayout.IsVisible = false;
            }
            else
            {
                var suggestions = new List<object>();
                var items = (IEnumerable)newValue;
                var list = items.Cast<object>().ToList();
                var count = Math.Min(owner.SuggestionsMax, list.Count);
                for (var i = 0; i < count; i++)
                {
                    suggestions.Add(list[i]);
                }

                Logs.Trace(owner.GetType() + ".OnSuggestionItemsChanged with  " + suggestions.Count + " suggestions");
                owner.SuggestionsView.ItemsSource = suggestions;
                if (list.Count > 0)
                    owner.SuggestionsLayout.IsVisible = true;
                else
                    owner.SuggestionsLayout.IsVisible = false;
            } 

            //owner.SuggestionsView.BackgroundColor = newValue;
        }
        


        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(
                "SearchText", typeof(string), typeof(SearchEntry), "", BindingMode.TwoWay, null, OnSearchTextChanged);
    //    BindableProperty.Create<SearchEntry, string>(p => p.SearchText, "",
    //        BindingMode.TwoWay, null, OnSearchTextChanged, null, null);

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        private static void OnSearchTextChanged(BindableObject obj,
            object oldValue, object newVal)
        {
            var newValue = (string)newVal;
            var owner = (obj as SearchEntry);
            if (owner == null) return;
            
            Logs.Trace(owner.GetType() + ".OnSearchTextChanged " + newValue);
           
            owner.btnSearch.IsEnabled = !string.IsNullOrEmpty(newValue);

            if (owner.SearchItems == null)
            {
                owner.SuggestionItems = new List<object>();
                return;
            }
            
            var list = owner.SearchItems.Cast<object>().ToList();

            var newText = Regex.Replace((newValue ?? "").ToLowerInvariant(), @"\s+", string.Empty);
            if (!string.IsNullOrEmpty(newText))
            {
                var matches = GetMatchingItems(list, 
                    owner.SearchItemsMemberPath, newText, owner.SearchStrategy);
  
                owner.SuggestionItems = matches;
            }
            else
            {
                if (list.Count > 0)
                {
                    owner.SuggestionItems = new List<object>();
                    //owner.SuggestionItems.Clear();
                    //owner.ToggleSuggestionsView(false);
                }
            }
        }
        public static string GetSearchString(object item)
        {
            return item.ToString();
        }
         
        protected internal static string GetItemValue(object item, string itemsMemberPath)
        {
            if (item == null) return "Null";

            if (item is string || string.IsNullOrEmpty(itemsMemberPath)) 
                return item.ToString();

            // support multiple data mappings
            var propertyNames = new List<string> { itemsMemberPath };
            if (itemsMemberPath.Contains(","))
                propertyNames = itemsMemberPath.Split(',').ToList();
            if (itemsMemberPath.Contains("|"))
                propertyNames = itemsMemberPath.Split('|').ToList();

            var itemValue = item.GetPropertyValue(propertyNames[0].Trim()) as string;

            return itemValue;
        }

        private static bool IsMatchingText(object item, string matchText, 
            SearchStrategy strategy)
        {
            if (item == null || matchText == null) return false;

            var itemValue = item as string;
            if (itemValue == null) return false;

            itemValue = Regex.Replace(itemValue.ToLowerInvariant(), @"\s+", string.Empty);

            if (strategy == SearchStrategy.StartsWithEntry && 
                itemValue.StartsWith(matchText))
            {
                return true;
            }

            if (strategy == SearchStrategy.ContainsEntry &&
                itemValue.Contains(matchText))
            {
                return true;
            }

            return false;
        }
        public static List<object> GetMatchingItems(IList<object> items,
            string itemsMemberPath, string matchText, SearchStrategy strategy)
        {
            var matches = new List<object>();
            if (items == null || string.IsNullOrEmpty(matchText)) 
                return matches;

            if (string.IsNullOrEmpty(itemsMemberPath))
            {
                foreach (var item in items)
                {
                    if (IsMatchingText(item, matchText, strategy))
                    {
                        matches.Add(item);
                    }
                } 
                matches = matches.OrderBy(o => o.ToString()).ToList();
            }
            else
            {
                // support multiple data mappings
                var propertyNames = new List<string> { itemsMemberPath };
                if (itemsMemberPath.Contains(","))
                    propertyNames = itemsMemberPath.Split(',').ToList();
                if (itemsMemberPath.Contains("|"))
                    propertyNames = itemsMemberPath.Split('|').ToList();

                foreach (var item in items)
                {
                    foreach (var propertyName in propertyNames)
                    {
                        var propValue = item.GetPropertyValue(propertyName.Trim()) as string;
                        if (IsMatchingText(propValue, matchText, strategy))
                        {
                            matches.Add(item);
                            break;
                        }
                    }
                }
                if (propertyNames.Count > 0)
                {
                    matches = matches.OrderBy(o =>
                        o.GetPropertyValue(propertyNames[0]).ToString()).ToList();
                    //matches = matches.OrderByDescending(o =>
                    //    o.GetPropertyValue(propertyNames[0]).ToString().StartsWith(matchText)).ToList();
                }
            }
            return matches;
        }
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
                "Placeholder", typeof(string), typeof(SearchEntry), "");
     //  BindableProperty.Create<SearchEntry, string>(
     //          p => p.Placeholder, "", BindingMode.TwoWay, null,OnPlaceHolderChanged);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        static void OnPlaceHolderChanged(BindableObject obj, string oldValue, string newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;
            owner.TextEntry.Placeholder = newValue;
        }

        public static readonly BindableProperty ShowSearchProperty = BindableProperty.Create(
                "ShowSearchButton", typeof(bool), typeof(SearchEntry), true, BindingMode.TwoWay, null, OnShowSearchChanged);
     //   BindableProperty.Create<SearchEntry, bool>(
     //           p => p.ShowSearchButton, true, BindingMode.TwoWay, null, OnShowSearchChanged);

        public bool ShowSearchButton
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }

        static void OnShowSearchChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;
            owner.btnSearch.IsVisible = (bool)newValue;
        }
        
        
        /// <summary>
        /// Gets or sets SearchStrategy
        /// </summary>
        public SearchStrategy SearchStrategy { get; set; }

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
                "SearchCommand", typeof(ICommand), typeof(SearchEntry), null);
       // BindableProperty.Create<SearchEntry, ICommand>(
       //         p => p.SearchCommand, null);

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public static readonly BindableProperty SearchSelectedCommandProperty = BindableProperty.Create(
                "SearchSelectedCommand", typeof(ICommand), typeof(SearchEntry), null);
      //  BindableProperty.Create<SearchEntry, ICommand>(
      //          p => p.SearchSelectedCommand, null);

        public ICommand SearchSelectedCommand
        {
            get { return (ICommand)GetValue(SearchSelectedCommandProperty); }
            set { SetValue(SearchSelectedCommandProperty, value); }
        }

        public static readonly BindableProperty SugestionItemDataTemplateProperty = BindableProperty.Create(
                "SuggestionItemDataTemplate", typeof(DataTemplate), typeof(SearchEntry), null, BindingMode.TwoWay, null, OnSuggestionItemDataTemplateChanged);
     //   BindableProperty.Create<SearchEntry, DataTemplate>(p => p.SuggestionItemDataTemplate, null,
     //           BindingMode.TwoWay, null, OnSuggestionItemDataTemplateChanged, null, null);

        public DataTemplate SuggestionItemDataTemplate
        {
            get { return (DataTemplate)GetValue(SugestionItemDataTemplateProperty); }
            set { SetValue(SugestionItemDataTemplateProperty, value); }
        }

        static void OnSuggestionItemDataTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;

            var template = (newValue as DataTemplate);
            if (template == null) return;

            owner.SuggestionsView.ItemTemplate = template;
        }
        
        public static readonly BindableProperty SearchBackgroundColorProperty = BindableProperty.Create(
                "SearchBackgroundColor", typeof(Color), typeof(SearchEntry), Color.Blue);
     //   BindableProperty.Create<SearchEntry, Color>(p => p.SearchBackgroundColor, 
     //       Color.Blue, BindingMode.TwoWay, null, SearchBackgroundColorChanged, null, null);

        public Color SearchBackgroundColor
        {
            get { return (Color)GetValue(SearchBackgroundColorProperty); }
            set { SetValue(SearchBackgroundColorProperty, value); }
        }

        static void SearchBackgroundColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;
            owner.ContentLayout.BackgroundColor = newValue;
        }
        //SuggestionText
        public static readonly BindableProperty SuggestionTextProperty = BindableProperty.Create(
                "SuggestionText", typeof(string), typeof(SearchEntry), "SUGGESTIONS");
      //  BindableProperty.Create<SearchEntry, string>(p => p.SuggestionText,
      //     "SUGGESTIONS", BindingMode.Default, null);

        public string SuggestionText
        {
            get { return (string)GetValue(SuggestionTextProperty); }
            set { SetValue(SuggestionTextProperty, value); }
        }

        public static readonly BindableProperty SuggestionsBackgroundColorProperty = BindableProperty.Create(
                "SuggestionsBackgroundColor", typeof(Color), typeof(SearchEntry), Color.Transparent, BindingMode.TwoWay, null, OnSugestionBackgroundColorChanged);
      //  BindableProperty.Create<SearchEntry, Color>(p => p.SuggestionsBackgroundColor, 
      //      Color.Transparent, BindingMode.TwoWay, null, OnSugestionBackgroundColorChanged, null, null);

        public Color SuggestionsBackgroundColor
        {
            get { return (Color)GetValue(SuggestionsBackgroundColorProperty); }
            set { SetValue(SuggestionsBackgroundColorProperty, value); }
        }

        static void OnSugestionBackgroundColorChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = (obj as SearchEntry);
            if (owner == null) return;
            owner.SuggestionsView.BackgroundColor = (Color)newValue;
        }
        
        public static readonly BindableProperty ExecuteOnSugestionClickProperty = BindableProperty.Create(
                "ExecuteOnSugestionClick", typeof(bool), typeof(SearchEntry), false);
     //   BindableProperty.Create<SearchEntry, bool>(p => p.ExecuteOnSugestionClick, false);

        public bool ExecuteOnSugestionClick
        {
            get { return (bool)GetValue(ExecuteOnSugestionClickProperty); }
            set { SetValue(ExecuteOnSugestionClickProperty, value); }
        }



        #endregion
    }
    public enum SearchStrategy
    {
        ContainsEntry,
        StartsWithEntry
    }

}