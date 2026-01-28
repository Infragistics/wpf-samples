using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Infragistics.Framework
{

    public class LabelsDataColumn : DataColumnBase
    {

        public override View CreateItemView(object item, bool isSelected)
        {
            var content = new Label();
            //content.BindingContext = item;
            content.HorizontalOptions = HorizontalOptions;
            content.VerticalOptions = LayoutOptions.Center;
            content.HorizontalTextAlignment = TextAlignment.Center;
            content.VerticalTextAlignment = TextAlignment.Center;
          
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
                var content = Cells[i].Content as Label;
                if (content == null) return;

                Cells[i].BindingContext = items[i];

                UpdateBinding(content, items[i]);
            }
        }

        public void UpdateBinding(Label content, object item)
        {
            if (string.IsNullOrEmpty(MemberPath))
            {
                content.Text = "---";
            }
            else
            {
                Logs.Trace(this, "updating binding for item: " + item);
                
                var text = item.GetPropertyValue(MemberPath);
                content.Text = text == null ? "NUL" : text.ToString();

                //var binding = new Binding { Path = MemberPath, Source = item };
                //content.SetBinding(Label.TextProperty, binding);
            }
        }

        public override void UpdateSelection(DataCell cell, bool isSelected)
        {
            if (cell == null) return;

            var content = cell.Content as Label;
            if (content == null) return;

            UpdateApperance(content, isSelected);
        }
        public void UpdateApperance(Label content, bool isSelected)
        {
            if (isSelected)
            {
                content.TextColor = SelectionForeground;
                content.FontAttributes = SelectionFontAttributes;
                if (!double.IsNaN(SelectionFontSize)) content.FontSize = SelectionFontSize;
                if (!string.IsNullOrEmpty(SelectionFontFamily)) content.FontFamily = SelectionFontFamily;
            }
            else
            {
                content.TextColor = CellForeground;
                content.FontAttributes = CellFontAttributes;
                if (!double.IsNaN(CellFontSize)) content.FontSize = CellFontSize;
                if (!string.IsNullOrEmpty(CellFontFamily)) content.FontFamily = CellFontFamily;
            }
        }


    }

    
}