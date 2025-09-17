using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq; 
using System.Windows.Input;
using Xamarin.Forms;

namespace Infragistics.Framework
{

    public class ListViewEx : ListView
    {  
		public ListViewEx()
		{
            IsRowHeigthSyncWithView = false;
            IsItemSelectedOnItemTapped = false;

            //this.MeasureInvalidated += ListViewEx_MeasureInvalidated;
            //this.ItemAppearing += ListViewEx_ItemAppearing;

            this.BindingContextChanged += ListViewEx_BindingContextChanged;
            this.ItemTapped += OnListItemTapped;
            this.SizeChanged += OnListViewSizeChanged;
		}

        void ListViewEx_BindingContextChanged(object sender, EventArgs e)
        {
            Logs.Trace(this, "BindingContextChanged... ");
        }
         
        void ListViewEx_ChildAdded(object sender, ElementEventArgs e)
        {
            Logs.Trace(this, "ChildAdded... " + e.Element);
        }

        void ListViewEx_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            Logs.Trace(this, "ItemAppearing... " + e.Item);
        }

        void ListViewEx_MeasureInvalidated(object sender, EventArgs e)
        {
            Logs.Trace(this, "MeasureInvalidated... ");
        }

	    /// <summary> Gets or sets TappedItem </summary>
	    public object TappedItem { get; private set; }

        private void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            TappedItem = e.Item;
            Logs.Trace(this, " tapped item: " + TappedItem + " - " + ItemCommandMemberPath);

            if (!IsItemSelectedOnItemTapped)
                this.SelectedItem = null;
                                  
            var command = TappedItem.GetPropertyValue(ItemCommandMemberPath) as ICommand;
            if (command != null)
            {
                Logs.Trace(this, "executing command on " + TappedItem + " ...");
                if (command.CanExecute(TappedItem))
                { 
                    command.Execute(TappedItem);
                    Logs.Trace(this, "executing command on " + TappedItem + " ... completed");
                }
                else
                    Logs.Trace(this, "executing command on " + TappedItem + " ... forbidden"); 
            }
        }

        private void OnListViewSizeChanged(object sender, EventArgs e)
        {
            Logs.Trace(this, "SizeChanged... ");
            if (IsRowHeigthSyncWithView)
            {
                var height = (int)Math.Ceiling(this.Height);

                if (height > 0 && height > this.RowHeight)
                { 
                    Logs.Trace(this, "synchronizing row/list height: " + height + " ... ");

                    this.RowHeight = height;
                    Logs.Trace(this, "synchronizing row/list height: " + height + " ... completed");
                }
            }
        }
           

        public void ScrollAndSelect(object targetItem)
        {
            if (this.ItemsSource == null) return;
          
            var listSource = this.ItemsSource as IEnumerable<object>;
            
            if (listSource.Contains(targetItem))
            {
                //TODO select target item
                
                this.ScrollTo(targetItem, ScrollToPosition.Center, false);
            }
        }

		#region Properties

		#region ItemCommandMemberPath
		public static BindableProperty ItemCommandMemberPathProperty = BindableProperty.Create(
                "ItemCommandMemberPath", typeof(string), typeof(ListViewEx), "Command");
        //BindableProperty.Create<ListViewEx, string>(o => o.ItemCommandMemberPath, "Command");

		/// <summary>  Gets or sets member path to a command for executing when an item is tapped  </summary>
		public string ItemCommandMemberPath
		{
			get { return (string)GetValue(ItemCommandMemberPathProperty); }
			set { SetValue(ItemCommandMemberPathProperty, value); }
		}
        #endregion


	    /// <summary> Gets or sets SyncRowHeigthToView </summary>
        public bool IsRowHeigthSyncWithView { get; set; }

        public bool IsItemSelectedOnItemTapped { get; set; }


		#endregion  
	}
}
