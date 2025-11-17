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
        } 
        public double CurrentVersion { get; set; }
        public List<TocControl> Controls { get; set; }
        //public int ControlsCount { get; set; }
        //public int SamplesCount { get; set; }
    }

    public class TocControl
    {
        public TocControl()
        { 
            Categories = new List<TocCategory>();
        }
        public string Control { get; set; }
        //public int SamplesCount { get; set; }
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

    public static class TocScript
    {

        private static void Initialize()
        {

        }

        public static void PortToJson()
        {
            // System.Threading.Tasks.Extensions
            Initialize();

            var xml = "/;component/TableOfContents.xml";
            var xmlTOC = TableOfContentsViewModel.Create(xml);

            var tocRoot = new TocRoot();
            //tocRoot.ReleaseVersion = xmlTOC.ReleaseVersion.ToString();
            tocRoot.CurrentVersion = xmlTOC.CurrentVersion;
            

            foreach (var xc in xmlTOC.Children) // Controls
            {
                var tocControl = new TocControl();
                tocControl.Control = xc.Name.Replace("Xam", "");

                foreach (var xcat in xc.Children) // Categories
                {
                    var tocCat = new TocCategory();
                    tocCat.Category = xcat.Name;
                    foreach (var xs in xcat.Children) // Samples
                    {
                        var tocSample = new TocSample();
                        tocSample.NameEN = xs.Name;
                        tocSample.NameJP = xs.NameJP;
                        tocSample.Version = xs.ReleaseVersion;
                        tocSample.Assembly = xs.AssemblyName;
                        tocSample.Path = xs.CodeFiles[0];
                        tocSample.Status = xs.Status;
                        tocSample.DescriptionEN = xs.Description;
                        tocSample.DescriptionJP = xs.DescriptionJP;

                        if (!tocSample.Path.StartsWith("/" + tocSample.Assembly) &&
                            !tocSample.Path.StartsWith(tocSample.Assembly))
                        {
                            System.Diagnostics.Debug.WriteLine("WARNING " + tocSample.Assembly + " " + tocSample.Path);
                        }

                        tocCat.Samples.Add(tocSample);
                    }
                    tocCat.Samples.Sort((toc1, toc2) => SortToc(toc1, toc2));

                    tocControl.Categories.Add(tocCat);
                }

                tocControl.Categories.Sort((toc1, toc2) => SortToc(toc1, toc2));

                tocRoot.Controls.Add(tocControl);

                //if (tocRoot.Controls.Count > 1) break;

            }

            tocRoot.Controls.Sort((toc1, toc2) => SortToc(toc1, toc2));

            var testTOC = new TocRoot();

            var jsonTab = "    ";

            var jset = new JsonSerializerSettings();
            jset.Formatting = Formatting.Indented;
            jset.Formatting = Formatting.None;  
           
            //var json = JsonConvert.SerializeObject(testTOC, jset);
            var json = JsonConvert.SerializeObject(tocRoot, jset);

            //var json = System.Text.Json.JsonSerializer.Serialize(testTOC);

            json = json.Replace("\"Path\":\"IG", "\"Path\":\"/IG");


            var NL = "\r\n";
            json = json.Replace("{\"CurrentVersion\":", NL + Tabs() + "{\n" + Indent() + "\"CurrentVersion\": ");
            json = json.Replace("\"Controls\":", NL + Tabs() + "\"Controls\": ");

            json = json.Replace("{\"Control\":", NL + Indent() + "{\n" + Indent() + "\"Control\": ");
            json = json.Replace("\"Categories\":", NL + Tabs() + "\"Categories\": ");

            json = json.Replace("{\"Category\":", NL + Indent() + "{\n" + Indent() + "\"Category\": ");
            json = json.Replace("\"Samples\":", NL + Tabs() + "\"Samples\": ");

            json = json.Replace("{\"NameEN\":", NL + Indent() + "{\n" + Indent() + "\"NameEN\": ");
            json = json.Replace("\"NameJP\":", NL + Tabs() + "\"NameJP\": ");
            json = json.Replace("\"DescriptionEN\":", NL + Tabs() + "\"DescriptionEN\": ");
            json = json.Replace("\"DescriptionJP\":", NL + Tabs() + "\"DescriptionJP\": ");
            json = json.Replace("\"Status\":", NL + Tabs() + "\"Status\": ");
            json = json.Replace("\"Version\":", NL + Tabs() + "\"Version\": ");
            json = json.Replace("\"Assembly\":", NL + Tabs() + "\"Assembly\": ");
            json = json.Replace("\"Path\":", NL + Tabs() + "\"Path\": ");

            json = json.Replace(".xaml\"},", ".xaml\"" + NL + Undent() + "},");
            json = json.Replace(".xaml\"}", ".xaml\"" + NL + Tabs() + "}");

            json = json.Replace("}]}]}", "}X1]X2}X3]X4}X5");
            json = json.Replace("}]},", "}N1]N2}N3,");

            json = json.Replace("}N1", "" + "}");
            json = json.Replace("]N2", NL + Undent() + "]");
            json = json.Replace("}N3,", NL + Undent() + "},");

            json = json.Replace("}X1", "" + "}");
            json = json.Replace("]X2", NL + Indent() + "]");
            json = json.Replace("}X3", NL + Undent() + "}");
            json = json.Replace("]X4", NL + Undent() + "]");
            json = json.Replace("}X5", NL + Undent() + "}");

            json = json.Replace("}]}", "}E1]E2}E3");
            json = json.Replace("}E1", "" + "}");
            json = json.Replace("]E2", NL + Undent() + "]");
            json = json.Replace("}E3", NL + Undent() + "}");
             
            System.Diagnostics.Debug.WriteLine(json);

            File.WriteAllText("C:\\WORK\\wpf-samples\\Research\\Application\\IgTestApp.Net4\\TableOfContents.json", json);
        }

        private static int tabIndent = 0;
        public static string Tabs()
        {
            return string.Concat(Enumerable.Repeat("    ", tabIndent));
        }

        public static string Indent()
        {
            tabIndent++; return Tabs();
        }

        public static string Undent()
        {
            tabIndent--; return Tabs();
        }


        public static int SortToc(TocControl toc1, TocControl toc2)
        {
            string name1 = toc1.Control ?? string.Empty;
            string name2 = toc2.Control ?? string.Empty;

            return name1.CompareTo(name2);
        }

        public static int SortToc(TocCategory toc1, TocCategory toc2)
        {  
            string name1 = toc1.Category ?? string.Empty;
            string name2 = toc2.Category ?? string.Empty;

            return name1.CompareTo(name2);
        }

        public static int SortToc(TocSample toc1, TocSample toc2)
        {
            //if (toc1.ReleaseVersion == CurrentVersion ||
            //    toc2.ReleaseVersion == CurrentVersion)
            //{

            // MT skip sorting by status
            //if (toc1.IsNew != toc2.IsNew)
            //    return toc1.IsNew ? -1 : 1;
            //if (toc1.IsUpdated != toc2.IsUpdated)
            //    return toc1.IsUpdated ? -1 : 1;
            //if (toc1.IsPreview != toc2.IsPreview)
            //    return toc1.IsPreview ? -1 : 1;

            //if (toc1.IsBETA != toc2.IsBETA)
            //    return toc1.IsBETA ? -1 : 1;
            //}

            string name1 = toc1.NameEN ?? string.Empty;
            string name2 = toc2.NameEN ?? string.Empty;

            return name1.CompareTo(name2);
        }
    }
}
