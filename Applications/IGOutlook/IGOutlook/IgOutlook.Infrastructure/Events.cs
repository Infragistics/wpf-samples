using Prism.Events;
using System;

namespace IgOutlook.Infrastructure
{
    public class ViewActivateEvent : PubSubEvent<String> { }
    public class ViewItemsCountChangedEvent : PubSubEvent<int> { }
}
