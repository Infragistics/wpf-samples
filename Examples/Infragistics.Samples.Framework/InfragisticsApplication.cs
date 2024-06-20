using System;

namespace Infragistics.Samples.Framework
{
    public abstract class InfragisticsApplication : System.Windows.Application
    {
        public Guid CurrentSampleId { get; set; }
        public int CurrentProductFamilyId { get; set; }
        public int CurrentComponentId { get; set; }

        public abstract string ProductFamilyName { get; }

        public InfragisticsApplication()
        {

        }
    }
}