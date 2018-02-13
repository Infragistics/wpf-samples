using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Prism.Regions;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamRibbonRegionBehavior : RegionBehavior
    {
        /// <summary>
        /// The key of this behavior.
        /// </summary>
        public const String BehaviorKey = "XamRibbonRegionBehavior";

        protected override void OnAttach()
        {
            if (Region.Name == RegionNames.ContentRegion)
                Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //TODO: I have access to the region manager here, maybe I should just create and inject the ribbon tabs here
            //and take the region manager dependency out of the out of the view completely
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    //make sure we are dealing with the right type of view
                    var view = newView as IViewBase;
                    if (view == null)
                        continue;

                    //if we already have ribbons no need on checking again
                    if (view.RibbonTabs.Count > 0)
                        continue;

                    //loop through all the ribbon tab attributes and create them for the view
                    foreach (var atr in GetCustomAttributes<RibbonTabAttribute>(newView.GetType()))
                    {
                        var ribbonTabItem = (IRibbonTabItem)Activator.CreateInstance(atr.Type);
                        ribbonTabItem.ViewModel = view.ViewModel;

                        // the ribbon tab needs access to a rich text editor on the view.
                        // if they both implement ISupportRichText the assignment is accomplished below
                        var tabISupportRichText = ribbonTabItem as ISupportRichText;
                        var viewISupportRichText = view as ISupportRichText;
                        if (tabISupportRichText != null && viewISupportRichText != null)
                            tabISupportRichText.RichTextEditor = viewISupportRichText.RichTextEditor;

                        view.RibbonTabs.Add(ribbonTabItem);
                    }
                }
            }
        }

        IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
