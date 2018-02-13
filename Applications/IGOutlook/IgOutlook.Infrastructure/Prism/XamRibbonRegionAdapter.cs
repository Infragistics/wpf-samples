using Infragistics.Windows.Ribbon;
using Prism.Regions;
using System;
using System.Collections.Specialized;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamRibbonRegionAdapter : RegionAdapterBase<XamRibbon>
    {
        public XamRibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            if (region == null) throw new ArgumentNullException("region");
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");

            region.ActiveViews.CollectionChanged += (s, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (Object view in args.NewItems)
                            {
                                AddViewToRegion(view, regionTarget);
                            }
                            break;
                        }
                    case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (Object view in args.OldItems)
                            {
                                RemoveViewFromRegion(view, regionTarget);
                            }
                            break;
                        }
                    default:
                        {
                            // Do nothing.
                            break;
                        }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }

        static void AddViewToRegion(Object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItem;
            if (ribbonTabItem != null)
            {
                if (ribbonTabItem.TabIndex == 0)
                {
                    xamRibbon.Tabs.Insert(0, ribbonTabItem);
                    ribbonTabItem.IsSelected = true;
                }
                else
                    xamRibbon.Tabs.Add(ribbonTabItem);
            }
        }

        static void RemoveViewFromRegion(Object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItem;
            if (ribbonTabItem != null)
                xamRibbon.Tabs.Remove(ribbonTabItem);
        }
    }
}
