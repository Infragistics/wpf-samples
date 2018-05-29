using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace System.Windows.Media //IGExtensions.Framework.Models
{
    public class ColorCollection : ObservableCollection<Color>
    {
        /// <summary>Converts brushes in BrushCollection to LinearGradientBrush </summary>
        //public LinearGradientBrush ToGradientBrush()
        //{
        //    var colors = this.ToList();
        //    return colors.ToGradientBrush();
        //}
        public ColorCollection ReverseItems()
        {
            var newCollection = new ColorCollection();
            for (int i = this.Count - 1; i >= 0; i--)
            {
                var brush = this[i];
                newCollection.Add(brush);
            }
            return newCollection;
        }
        public string ToText()
        {
            var result = string.Empty;
            foreach (var item in this)
            {
                result += item.ToHex();
                result += " ";
            }
            return result;
        }
    }


}