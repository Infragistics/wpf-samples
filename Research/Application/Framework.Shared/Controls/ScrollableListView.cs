using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
        public abstract event EventHandler CanExecuteChanged;
    }
    public class ScrollableItemsView : ScrollView
    {
        public ScrollableItemsView()
        {
            //this.SizeChanged += ScrollableItemsView_SizeChanged;
            this.IsClippedToBounds = true;

            this.Orientation = ScrollOrientation.Horizontal;
            this.BackgroundColor = Colors.Brown;

            //this.HorizontalOptions = LayoutOptions.EndAndExpand;
            //this.VerticalOptions = LayoutOptions.EndAndExpand;

            //ContentLayout = new StackLayout();
            //ContentLayout.Orientation = StackOrientation.Horizontal;

            ContentLayout = new Grid();
             
            ContentLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            ContentLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            //ContentLayout.BackgroundColor = Colors.Red;
            ContentLayout.RowSpacing = 0;
            ContentLayout.ColumnSpacing = 0;
            ContentLayout.Padding = new Thickness(0, -18, 4, 0);

            this.Content = ContentLayout;

            this.Padding = new Thickness(0,0,0,0);
        }

        void ScrollableItemsView_SizeChanged(object sender, EventArgs e)
        {
            Logs.Trace(this, Width + "x" + Height + " " + WidthRequest + "x" + HeightRequest);
        }

        protected Grid ContentLayout;
        protected StackLayout ContentLayout22;

        public event EventHandler ItemsSourceUpdateStarted;
        public event EventHandler ItemsSourceUpdateCompleted;
        
        public void OnItemsSourceUpdateStarted()
        {
            if (ItemsSourceUpdateStarted != null)
                ItemsSourceUpdateStarted(this, EventArgs.Empty);
        }
        public void OnItemsSourceUpdateCompleted()
        {
            if (ItemsSourceUpdateCompleted != null)
                ItemsSourceUpdateCompleted(this, EventArgs.Empty);
        }

        #region ItemTapCommand

        public static BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
                "ItemTappedCommand", typeof(ICommand), typeof(ScrollableItemsView), null);
      //  BindableProperty.Create<ScrollableItemsView, ICommand>(o => o.ItemTappedCommand, null);

        /// <summary>  Gets or sets command for executing when an item is tapped  </summary>
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemTappedCommandParameterProperty = BindableProperty.Create(
                "ItemTappedCommandParameter", typeof(object), typeof(ScrollableItemsView), null);
      //  BindableProperty.Create<ScrollableItemsView, object>(o => o.ItemTappedCommandParameter, null);

        /// <summary>  Gets or sets command parameter for executing when an item is tapped  </summary>
        public object ItemTappedCommandParameter
        {
            get { return GetValue(ItemTappedCommandParameterProperty); }
            set { SetValue(ItemTappedCommandParameterProperty, value); }
        }
        
        /// <summary>  Occurs when an item is tapped  </summary>
        public event EventHandler<ItemTappedEventArgs> ItemTapped;

       
        public static BindableProperty ItemCommandMemberPathProperty = BindableProperty.Create(
                "ItemCommandMemberPath", typeof(string), typeof(ScrollableItemsView), "Command");
       // BindableProperty.Create<ScrollableItemsView, string>(o => o.ItemCommandMemberPath, "Command");

        /// <summary>  Gets or sets member path to a command for executing when an item is tapped  </summary>
        public string ItemCommandMemberPath
        {
            get { return (string)GetValue(ItemCommandMemberPathProperty); }
            set { SetValue(ItemCommandMemberPathProperty, value); }
        }

        protected void OnItemTapped(object itemView, EventArgs e)
        {
            var view = itemView as View;
            if (view == null) return;
            
            var item = view.BindingContext;
            Logs.Trace(this, " tapped item: " + item);
            
            this.SelectedItem = item;
      
            if (ItemTapped != null) 
                ItemTapped(this, new ItemTappedEventArgs(itemView, item));

            var command = item.GetPropertyValue(ItemCommandMemberPath) as ICommand;
            if (command != null)
            {
                if (command.CanExecute(item))
                {
                    Logs.Trace(this, " executing command on " + itemView.GetType().Name);
                    command.Execute(item);
                }
            }

            //if (this.ItemTappedCommand != null &&
            //    this.ItemTappedCommand.CanExecute(item))
            //{
            //    Logs.Trace(itemView.GetType() + " executing command on " + itemView.GetType());
            //    this.ItemTappedCommand.Execute(item);
            //}
        }
        #endregion

        #region SelectedItem
        //public static BindableProperty SelectedItemProperty =
        //  BindableProperty.Create<ScrollableItemsView, object>(o => o.SelectedItem, null);
        public static BindableProperty SelectedItemProperty = BindableProperty.Create(
                "SelectedItem", typeof(object), typeof(ScrollableItemsView), null, BindingMode.Default, null, OnSelectedItemChanged);
      //  BindableProperty.Create<ScrollableItemsView, object>(o => o.SelectedItem,
      //      null, BindingMode.Default, null, OnSelectedItemChanged);

        /// <summary>
        /// Gets or sets selected item in the <see cref="ItemsSource"/>
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        static void OnSelectedItemChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = obj as ScrollableItemsView;
            if (owner == null) return;

            //owner.UpdateItemsViews();
        }

        #endregion

        #region ItemTemplate
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                "ItemTemplate", typeof(DataTemplate), typeof(ScrollableItemsView), null, BindingMode.Default, null, OnItemTemplateChanged);
      //  BindableProperty.Create<ScrollableItemsView, DataTemplate>(o => o.ItemTemplate,
      //     null, BindingMode.Default, null, OnItemTemplateChanged);

        /// <summary>
        /// Gets or sets template for all items bound to <see cref="ItemsSource"/>
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        static void OnItemTemplateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var owner = obj as ScrollableItemsView;
            if (owner == null) return;

            Logs.Trace(owner, "is updating ItemTemplate...");
            owner.UpdateItemsViews(); 
        }

        #endregion

        #region ItemsSource
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                "ItemsSource", typeof(IEnumerable), typeof(ScrollableItemsView), default(IEnumerable), BindingMode.Default, null, OnItemsSourceChanged);
      //  BindableProperty.Create<ScrollableItemsView, IEnumerable>(o => o.ItemsSource,
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
            object oldValue, object newValue)
        {
            var owner = bindable as ScrollableItemsView;
            if (owner == null)  return;
             
            Logs.Trace("ScrollableItemsView is updating ItemsSource " + newValue);
            owner.OnItemsSourceUpdateStarted();
            owner.UpdateItemsViews();
            owner.OnItemsSourceUpdateCompleted();
        } 
        #endregion
      
        private bool IsHorizontal { get { return this.Orientation == ScrollOrientation.Horizontal; } }
        private bool IsVertical { get { return this.Orientation == ScrollOrientation.Vertical; } }

        public void ArrangeItems(double itemWidth, double itemHeight)
        {
            Logs.Trace(this, "arranging items");
            itemWidth -= (this.Padding.Left + this.Padding.Right);
            itemHeight -= (this.Padding.Top + this.Padding.Bottom);

            foreach (var itemView in this.ContentLayout.Children)
            {
                if (itemHeight > 0) itemView.HeightRequest = itemHeight;
                if (itemWidth > 0) itemView.WidthRequest = itemWidth;
            }
        }

        public void ArrangeItems(Size size)
        {
            ArrangeItems(size.Width, size.Height);
        }

        protected void UpdateItemsViews()
        {
            this.ContentLayout.Children.Clear();
            this.ContentLayout.RowDefinitions.Clear();
            this.ContentLayout.ColumnDefinitions.Clear();
              
            if (this.ItemsSource == null)
            {
                Logs.Trace(this, "detected null ItemsSource ");
                return;
                //ContentLayout.Children.Add(new Label { HeightRequest = 30, Text = "ItemsSource is null!" });
                //return; // this.StackLayout.Children.Add(CreateItemView("tmp"));
            }
            else
            {
                // clear rows/columns and add one star row/column
                var itemSize = 1.0;
                if (this.IsVertical)
                {
                    var col = new ColumnDefinition { Width = new GridLength(itemSize, GridUnitType.Star) };
                    this.ContentLayout.ColumnDefinitions.Add(col);
                    //this.ContentLayout.RowDefinitions.Clear();
                }
                else
                {
                    var row = new RowDefinition { Height = new GridLength(itemSize, GridUnitType.Star) };
                    this.ContentLayout.RowDefinitions.Add(row);
                    //this.ContentLayout.ColumnDefinitions.Clear();
                }
                // calculate row/column size
                var itemsCount = 0;
                var itemsActual  = this.ItemsSource.Cast<object>().Count();
                itemsCount = itemsActual == 0 ? 1 : itemsActual;
                itemSize = 1.0 / itemsCount;

                var itemType = this.ItemsSource.GetType();
               
                var index = 0;
                foreach (var item in this.ItemsSource)
                {
                    itemType = item.GetType();

                    var itemView = CreateItemView(item);
                    //itemView as ViewCell
                    //var layout = itemView as Layout<View>;
                    //if (layout != null)
                    //{
                    //    var itemChildren = layout.Children;
                    //}

                    if (this.IsVertical)
                    {
                        var row = new RowDefinition {Height = new GridLength(itemSize, GridUnitType.Auto)};
                        this.ContentLayout.RowDefinitions.Add(row);
                        Grid.SetRow(itemView, index);
                    }
                    else
                    {
                        var col = new ColumnDefinition { Width = new GridLength(itemSize, GridUnitType.Auto) };
                        this.ContentLayout.ColumnDefinitions.Add(col);
                        Grid.SetColumn(itemView, index);
                    }
                    index++;
                    this.ContentLayout.Children.Add(itemView);
                }
                Logs.Trace(this, "has ItemsSource with " + itemsActual + " items of " + itemType.Name + " type");
        
                //UpdateChildrenLayout();
                //InvalidateLayout();
            }
        }

        protected virtual View CreateItemView(object item)
        {
            var template = this.ItemTemplate; //GetTemplateFor(item.GetType());

            Logs.Trace(this, "creating item's content for " + item);
            object content;
            if (template == null)
            {
                content = new Label { Text = item.ToString() };
            }
            else
            {
               
                content = template.CreateContent();
            }
            //Logs.Trace(this, "checking item's content for " + item);
          
            //content = template != null ? 
            //              template.CreateContent() : new Label { Text = item.ToString()};

            if (!(content is View) && !(content is ViewCell))
                throw new Exception("ItemTemplate contains  " + content.GetType() + " instead of View or ViewCell");
            
            //var vc = (ViewCell)content;
            //vc.View.BindingContext = item;
            
            var tap = new TapGestureRecognizer();
            //tap.NumberOfTapsRequired = 1;
            tap.Tapped += OnItemTapped;
            //tap.Command = ItemTappedCommand;
            //tap.CommandParameter = item;
            Logs.Trace(this, "creating item's view for " + item);
            var view = (content is View) ? content as View : ((ViewCell)content).View;

            //var view = content as View;

            view.GestureRecognizers.Add(tap);
            view.BindingContext = item;
           // view.GestureRecognizers.Add(new TapGestureRecognizer { Command = ItemTappedCommand, CommandParameter = item });
            return view;
        }

         

        ///// <summary>
        ///// Select a datatemplate dynamically
        ///// Prefer the TemplateSelector then the DataTemplate
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //protected virtual DataTemplate GetTemplateFor(Type type)
        //{
        //    DataTemplate retTemplate = null;
        //    if (TemplateSelector != null)
        //        retTemplate = TemplateSelector.TemplateFor(type);
        //    return retTemplate ?? ItemTemplate;
        //}
    }
}