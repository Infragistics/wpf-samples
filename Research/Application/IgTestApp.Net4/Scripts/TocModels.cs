using Infragistics.SamplesBrowser.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infragistics.Samples
{
    public class TocRoot
    {
        public TocRoot()
        { 
            Controls = new List<TocControl>();
            SamplesTotal = 0;
            ControlsCount = 0;
        } 
        public double CurrentVersion { get; set; }
        public int SamplesTotal { get; set; }
        public int ControlsCount { get; set; }
        public List<TocControl> Controls { get; set; }
    }

    public class TocControl
    {
        public TocControl()
        { 
            Categories = new List<TocCategory>();
            SamplesCount = 0;
        }
        public string Control { get; set; }
        public int SamplesCount { get; set; }
        public List<TocCategory> Categories { get; set; } 
    }

    public class TocCategory
    {
        public TocCategory()
        { 
            Samples = new List<TocSample>();
        }
        public string Category { get; set; }
        public List<TocSample> Samples { get; set; } 
    }

    public class TocSample
    {
        public TocSample()
        { 
        }

        //[JsonProperty]
        public string NameEN { get; set; }
        public string NameJP { get; set; }

        public string DescriptionEN { get; set; }
        public string DescriptionJP { get; set; }

        public string Status { get; set; }
        public double Version { get; set; }

        public string Assembly { get; set; }
        public string Path { get; set; } 

    }
     
}
