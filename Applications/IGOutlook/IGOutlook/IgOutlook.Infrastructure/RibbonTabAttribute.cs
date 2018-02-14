using System;

namespace IgOutlook.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RibbonTabAttribute : Attribute
    {
        public Type Type { get; private set; }

        public RibbonTabAttribute(Type ribbonTabType)
        {
            if (ribbonTabType == null) throw new ArgumentNullException("ribbonTabType");
            if (ribbonTabType.BaseType != typeof(RibbonTabItem))
                throw new ArgumentOutOfRangeException("ribbonTabType", "Ribbon Tab Type does not derive from RibbonTabItem");
            Type = ribbonTabType;
        }
    }
}
