using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

namespace System.Windows
{
    /// <summary>
    /// Provides extensions working with elements and descendents in VisualTree (DependencyObject)
    /// <remarks>source: http://www.scottlogic.co.uk/blog/colin/2010/03/linq-to-visual-tree/ </remarks>
    /// </summary>
    public static class VisualTreeEx
    {
        #region Ancestors
        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject item)
        {
            ILinqVisualTree<DependencyObject> adapter = new VisualTreeAdapter(item);

            var parent = adapter.Parent;
            while (parent != null)
            {
                yield return parent;
                adapter = new VisualTreeAdapter(parent);
                parent = adapter.Parent;
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var ancestor in item.Ancestors())
            {
                yield return ancestor;
            }
        }

        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors<T>(this DependencyObject item)
        {
            return item.Ancestors().Where(i => i is T);
        }
        public static T AncestorsFind<T>(this DependencyObject item) where T : DependencyObject
        {
            return item.Ancestors<T>().FirstOrDefault() as T;
        }
        /// <summary>
        /// Returns a collection containing this element and all ancestor elements
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf<T>(this DependencyObject item)
        {
            return item.AncestorsAndSelf().Where(i => i is T);
        }
        #endregion
        #region Descendents
        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendents(this DependencyObject item)
        {
            ILinqVisualTree<DependencyObject> adapter = new VisualTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;

                foreach (var grandChild in child.Descendents())
                {
                    yield return grandChild;
                }
            }
        }
        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendentsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var child in item.Descendents())
            {
                yield return child;
            }
        }
        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// <para>Usage:
        /// this.Descendents<XamDataChart>(); // all XamDataChart controls
        /// this.Descendents<XamDataChart>().FirstOrDefault(b => b.Name == "DataChart1");
        /// </para>
        /// </summary>
        public static IEnumerable<DependencyObject> Descendents<T>(this DependencyObject item)
        {
            return item.Descendents().Where(i => i is T);
            //return item.Descendents().Where(i => i is T).Cast<DependencyObject>();
        }
        public static T DescendentsFind<T>(this DependencyObject item) where T : DependencyObject
        {
             return item.Descendents<T>().FirstOrDefault() as T;
        }
        /// <summary>
        /// Returns a collection containing this element and all descendant elements
        /// which match the given type.
        /// <para>Usage:
        /// this.DescendentsAndSelf<XamDataChart>(); // all XamDataChart controls
        /// this.DescendentsAndSelf<XamDataChart>().FirstOrDefault(b => b.Name == "DataChart1");
        /// </para>
        /// </summary>
        public static IEnumerable<DependencyObject> DescendentsAndSelf<T>(this DependencyObject item)
        {
            return item.DescendentsAndSelf().Where(i => i is T);
        }

        //public static IEnumerable<DependencyObject> Descendents(this DependencyObject root, Type descendentsType)
        //{
        //    return root.Descendents().Where(i => i.GetType() == descendentsType).Cast<DependencyObject>();
        //}
        #endregion
        #region Elements

        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements(this DependencyObject item)
        {
            ILinqVisualTree<DependencyObject> adapter = new VisualTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of the sibling elements before this node, in document order.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsBeforeSelf(this DependencyObject item)
        {
            if (item.Ancestors().FirstOrDefault() == null)
                yield break;
            foreach (var child in item.Ancestors().First().Elements())
            {
                if (child.Equals(item))
                    break;
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of the after elements after this node, in document order.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAfterSelf(this DependencyObject item)
        {
            if (item.Ancestors().FirstOrDefault() == null)
                yield break;
            bool afterSelf = false;
            foreach (var child in item.Ancestors().First().Elements())
            {
                if (afterSelf)
                    yield return child;

                if (child.Equals(item))
                    afterSelf = true;
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var child in item.Elements())
            {
                yield return child;
            }
        }
        /// <summary>
        /// Returns a collection of the sibling elements before this node, in document order
        /// which match the given type.
        /// </summary>
        /// <para>Usage:
        /// this.ElementsBeforeSelf<XamDataChart>(); // all XamDataChart controls
        /// this.ElementsBeforeSelf<XamDataChart>().FirstOrDefault(b => b.Name == "DataChart1");
        /// </para>
        public static IEnumerable<DependencyObject> ElementsBeforeSelf<T>(this DependencyObject item)
        {
            return item.ElementsBeforeSelf().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection of the after elements after this node, in document order
        /// which match the given type.
        /// <para>Usage:
        /// this.ElementsAfterSelf<XamDataChart>(); // all XamDataChart controls
        /// this.ElementsAfterSelf<XamDataChart>().FirstOrDefault(b => b.Name == "DataChart1");
        /// </para>
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAfterSelf<T>(this DependencyObject item)
        {
            return item.ElementsAfterSelf().Where(i => i is T);
        }
        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
        /// <para>Usage:
        /// this.Elements<XamDataChart>(); // all XamDataChart controls
        /// </para>
        public static IEnumerable<DependencyObject> Elements<T>(this DependencyObject item)
        {
            return item.Elements().Where(i => i is T);
        }
        public static T ElementsFind<T>(this DependencyObject item) where T : DependencyObject
        {
            return item.Elements<T>().FirstOrDefault() as T;
        }
        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// <para>Usage:
        /// this.ElementsAndSelf<XamDataChart>(); // all XamDataChart controls
        /// </para>
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf<T>(this DependencyObject item)
        {
            return item.ElementsAndSelf().Where(i => i is T);
        }
        #endregion
    }
    /// <summary>
    /// Adapts a DependencyObject to provide methods required for generate
    /// a Linq To Tree API
    /// </summary>
    public class VisualTreeAdapter : ILinqVisualTree<DependencyObject>
    {
        private readonly DependencyObject _item;

        public VisualTreeAdapter(DependencyObject item)
        {
            _item = item;
        }

        public IEnumerable<DependencyObject> Children()
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(_item);
            for (int i = 0; i < childrenCount; i++)
            {
                yield return VisualTreeHelper.GetChild(_item, i);
            }
        }

        public DependencyObject Parent
        {
            get
            {
                return VisualTreeHelper.GetParent(_item);
            }
        }
    }
    /// <summary>
    /// Defines an interface that must be implemented to generate the LinqToTree methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILinqVisualTree<out T>
    {
        IEnumerable<T> Children();

        T Parent { get; }
    }
    /// <summary>
    /// Provides extensions working with list of elements and descendents in VisualTree (DependencyObject)
    /// <remarks>source: http://www.scottlogic.co.uk/blog/colin/2010/03/linq-to-visual-tree/ </remarks>
    /// </summary>
    public static class VisualTreeEnumerableEx
    {
        #region Operations
        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable.
        /// </summary>
        private static IEnumerable<DependencyObject> DrillDown(this IEnumerable<DependencyObject> items,
            Func<DependencyObject, IEnumerable<DependencyObject>> function)
        {
            foreach (var item in items)
            {
                foreach (var itemChild in function(item))
                {
                    yield return itemChild;
                }
            }
        }

        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable, which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> DrillDown<T>(this IEnumerable<DependencyObject> items,
            Func<DependencyObject, IEnumerable<DependencyObject>> function)
            where T : DependencyObject
        {
            foreach (var item in items)
            {
                foreach (var itemChild in function(item))
                {
                    if (itemChild is T)
                    {
                        yield return (T)itemChild;
                    }
                }
            }
        }
        public static T Find<T>(this IEnumerable<DependencyObject> items) where T : DependencyObject
        {
            return items.Where(i => i is T).FirstOrDefault() as T;
        }
        #endregion
        #region Descendents
        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendents(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Descendents());
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendentsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.DescendentsAndSelf());
        }

        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendents<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Descendents());
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendentsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.DescendentsAndSelf());
        }
        #endregion
        #region Ancestors
        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Ancestors());
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.AncestorsAndSelf());
        }

        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Ancestors());
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.AncestorsAndSelf());
        }

        #endregion
        #region Elements
        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Elements());
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.ElementsAndSelf());
        }

        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Elements());
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.ElementsAndSelf());
        }
        #endregion
    }

}
