using System;
using System.Collections;
using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class BulletDataColumn : DataColumnBase
    { 
        public string CompareValue { get; set; }
        public string CompareTarget { get; set; }

        public BulletDataColumn()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            
            CellPadding = new Thickness(0, 2, 0, 2);
        }

        public override View CreateItemView(object item, bool isSelected)
        {
            Logs.Trace(this, "CreateItemView for item " + item);

            var content = new BulletDataColumnView(); 
             
            content.HorizontalOptions = HorizontalOptions;
            content.VerticalOptions = VerticalOptions;

            UpdateBinding(content, item);
            UpdateApperance(content, isSelected);

            return content;
        }

        public override void UpdateBindings(IList items)
        {
            if (items == null) return;
            if (items.Count != Cells.Count) return;

            for (var i = 0; i < items.Count; i++)
            {
                var content = Cells[i].Content as BulletDataColumnView;
                if (content == null) return;

                Cells[i].BindingContext = items[i];

                UpdateBinding(content, items[i]);
            }
      
        }
        public void UpdateBinding(BulletDataColumnView content, object item)
        {
            if (string.IsNullOrEmpty(CompareValue))
            {
                Logs.Warning(this, "missing value for CompareValue property");
            }
            else if (string.IsNullOrEmpty(CompareTarget))
            {
                Logs.Warning(this, "missing value for CompareTarget property");
            }
            else
            {
                Logs.Trace(this, "updating binding for item: " + item);

                var itemValue = item.GetPropertyValue(CompareValue);
                content.CompareValue = itemValue is double ? (double)itemValue : 0.2;

                var itemTarget = item.GetPropertyValue(CompareTarget);
                content.CompareTarget = itemTarget is double ? (double)itemTarget : 1;
            }
        }

        public override void UpdateSelection(DataCell cell, bool isSelected)
        {
            if (cell == null) return;

            var content = cell.Content as BulletDataColumnView;
            if (content == null) return;

            UpdateApperance(content, isSelected);
        }

        public void UpdateApperance(BulletDataColumnView content, bool isSelected)
        {
            if (isSelected)
            {
                content.LabelForeground = SelectionForeground;
                content.LabelFontAttributes = SelectionFontAttributes;
                if (!double.IsNaN(SelectionFontSize)) content.LabelFontSize = SelectionFontSize;
                //if (!string.IsNullOrEmpty(SelectionFontFamily)) 
                //content.CompareFontFamily = SelectionFontFamily;
            }
            else
            {
                content.LabelForeground = CellForeground;
                content.LabelFontAttributes = CellFontAttributes;
                if (!double.IsNaN(CellFontSize)) content.LabelFontSize = CellFontSize;
                //if (!string.IsNullOrEmpty(CellFontFamily)) 
                //content.CompareFontFamily = CellFontFamily;
            }
        }
    } 


}