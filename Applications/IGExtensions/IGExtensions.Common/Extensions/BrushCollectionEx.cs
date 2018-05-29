using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Infragistics;

namespace Infragistics
{
    public static class BrushCollectionEx
    {
        #region AddRange
        /// <summary>Adds a list of Brush to the current BrushCollection</summary>
        public static BrushCollection AddRange(this BrushCollection brushCollection, List<Brush> brushes)
        {
            foreach (var brush in brushes)
            {
                brushCollection.Add(brush);
            }
            return brushCollection;
        }
        /// <summary>Adds a list of SolidColorBrush to the current BrushCollection</summary>
        public static BrushCollection AddRange(this BrushCollection brushCollection, List<SolidColorBrush> brushes)
        {
            foreach (var brush in brushes)
            {
                brushCollection.Add(brush);
            }
            return brushCollection;
        }
        /// <summary>Adds a list of LinearGradientBrush to the current BrushCollection</summary>
        public static BrushCollection AddRange(this BrushCollection brushCollection, List<LinearGradientBrush> brushes)
        {
            foreach (var brush in brushes)
            {
                brushCollection.Add(brush);
            }
            return brushCollection;
        }
        /// <summary>Adds a list of Color as SolidColorBrush to the current BrushCollection</summary>
        public static BrushCollection AddRange(this BrushCollection brushCollection, List<Color> colors)
        {
            foreach (var color in colors)
            {
                brushCollection.Add(color.ToBrush());
            }
            return brushCollection;
        }
        /// <summary>Adds a list of NamedColor as SolidColorBrush to the current BrushCollection</summary>
        public static BrushCollection AddRange(this BrushCollection brushCollection, List<NamedColor> colors)
        {
            foreach (var color in colors)
            {
                brushCollection.Add(color.ToBrush());
            }
            return brushCollection;
        } 
        #endregion

        /// <summary>Creates BrushCollection from a list of Brushes </summary>
        public static BrushCollection FromBrushes(this BrushCollection brushCollection, List<Brush> brushes)
        {
            return brushCollection.AddRange(brushes);
        }
        /// <summary>Creates BrushCollection from a list of SolidColorBrush </summary>
        public static BrushCollection FromBrushes(this BrushCollection brushCollection, List<SolidColorBrush> brushes)
        {
            return brushCollection.AddRange(brushes);
        }
        /// <summary>Creates BrushCollection from a list of LinearGradientBrush </summary>
        public static BrushCollection FromBrushes(this BrushCollection brushCollection, List<LinearGradientBrush> brushes)
        {
            return brushCollection.AddRange(brushes);
        }
        /// <summary>Creates BrushCollection from a list of Brushes </summary>
        public static BrushCollection FromColors(this BrushCollection brushCollection, List<Color> colors)
        {
            return brushCollection.AddRange(colors);
        }
        /// <summary>Creates BrushCollection from a list of Brushes </summary>
        public static BrushCollection FromColors(this BrushCollection brushCollection, List<NamedColor> colors)
        {
            return brushCollection.AddRange(colors);
        }
        /// <summary>Converts brushes in BrushCollection to LinearGradientBrush </summary>
        public static LinearGradientBrush ToGradientBrush(this BrushCollection brushCollection)
        {
            var brushes = brushCollection.ToList();
            return brushes.ToGradientBrush();
        }
        /// <summary>Blends brushes in BrushCollection to SolidColorBrush </summary>
        public static SolidColorBrush Blend(this BrushCollection brushCollection)
        {
            var brushes = brushCollection.ToList();
            return brushes.Blend();
        }
        public static BrushCollection ReverseItems(this BrushCollection brushCollection)
        {
            var newBrushCollection = new BrushCollection();
            for (int i = brushCollection.Count - 1; i >= 0; i--)
            {
                var brush = brushCollection[i];
                newBrushCollection.Add(brush);
            }
            return newBrushCollection;
        }
        public static string ToText(this BrushCollection brushCollection)
        {
            var result = string.Empty;
            foreach (var brush in brushCollection)
            {
                result += brush.ToColor().ToHex();
                if (brush is GradientBrush)
                    result +=  "^";
                result += " ";
            }
            return result;
        }
    }
    public class BrushCollectionList : ObservableCollection<BrushCollection> //List<BrushCollection>
    {
        public BrushCollectionList ReverseItems()
        {
            var list = new BrushCollectionList();
            foreach (var collection in this)
            {
                list.Add(collection.ReverseItems());
            }
            return list;
        }
    }
}
namespace System.Windows.Media
{
    public static class BrushListEx
    {
        /// <summary>Converts a list of Color to BrushCollection </summary>
        public static BrushCollection ToBrushCollection(this List<Color> colors)
        {
            return new BrushCollection().FromColors(colors);
        }
        /// <summary>Converts a list of Color to BrushCollection </summary>
        public static BrushCollection ToBrushCollection(this List<Brush> brushes)
        {
            return new BrushCollection().FromBrushes(brushes);
        }
        /// <summary>Converts a list of SolidColorBrush to BrushCollection </summary>
        public static BrushCollection ToBrushCollection(this List<SolidColorBrush> brushes)
        {
            return new BrushCollection().FromBrushes(brushes);
        }
        /// <summary>Converts a list of LinearGradientBrush to BrushCollection </summary>
        public static BrushCollection ToBrushCollection(this List<LinearGradientBrush> brushes)
        {
            return new BrushCollection().FromBrushes(brushes);
        }
        /// <summary>Converts a list of NamedColor to BrushCollection </summary>
        public static BrushCollection ToBrushCollection(this List<NamedColor> brushes)
        {
            return new BrushCollection().FromColors(brushes);
        }
    }
}