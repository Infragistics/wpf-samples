using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Infragistics.Web.SampleBrowser.Core.Framework.Model;
using System.Xml;

namespace FilterXPlatformToc
{
    class Program
    {
        static Dictionary<string, string> featureCategories;

        static string xSourcePath;
        static string xDestinationPath;
        static void Close()
        {
            LOG.Close();
        }
        static void Main(string[] args)
        {
            // enable to generate combined TOC format with fields and locals 'displayName' >> 'displayNameEN' + 'displayNameJA'
            var useCombinedFormat = false;
            if (useCombinedFormat)
            {
                args = new string[] {
                "WPF",
                "ALL",
                @"C:\WORK\xaml-samples\XAML\TableOfContents\TOC.xml",
                @"C:\WORK\xaml-samples\XAML\Infragistics.SamplesBrowser.WPF\TableOfContents.xml" };
            }
            else
            {
                //args = new string[] {
                //"WPF",
                //"EN",
                //@"C:\WORK\xaml-samples\XAML\TableOfContents\TOC.xml",
                //@"C:\WORK\xaml-samples\XAML\Infragistics.SamplesBrowser.WPF\TableOfContents.xml" };
            }

            LOG.Info(string.Join(" ", args));

            if (args.Length != 4)
            {
                LOG.Warn("Please provide the Flag value, the Lang, the Source TOC path and Destination TOC path");
                Close(); return;
            }
            if (!File.Exists(args[2]))
            {
                LOG.Info("Missing file " + args[2]);
                Close(); return;
            }

            var components = GetComponents();

            featureCategories = new Dictionary<string, string>();

            string Platform = args[0];
            string Lang = args[1];
            string xSourceTocPath = args[2];
            string xDestinationTocPath = args[3];

            if (Lang == "JA")
            {
                featureCategories.Add("Data", "アーキテクチャ");
                featureCategories.Add("Localization", "アーキテクチャ");
                featureCategories.Add("Performance", "アーキテクチャ");
                featureCategories.Add("Navigation", "操作");
                featureCategories.Add("Organization", "操作");
                featureCategories.Add("Display", "ビジュアル");
                featureCategories.Add("Style", "ビジュアル");
                featureCategories.Add("Editing & Selection", "操作");
                featureCategories.Add("Printing", "ビジュアル");
            }
            else
            {
                featureCategories.Add("Data", "Architecture");
                featureCategories.Add("Localization", "Architecture");
                featureCategories.Add("Performance", "Architecture");
                featureCategories.Add("Navigation", "Interaction");
                featureCategories.Add("Organization", "Interaction");
                featureCategories.Add("Display", "Visual");
                featureCategories.Add("Style", "Visual");
                featureCategories.Add("Editing & Selection", "Interaction");
                featureCategories.Add("Printing", "Visual");
            }
            try
            {
                xSourcePath = (new FileInfo(xSourceTocPath)).DirectoryName;
                xDestinationPath = (new FileInfo(xDestinationTocPath)).DirectoryName;
            }
            catch (Exception ex)
            {
                LOG.Error(ex.Message);
                return;
            }

            XNamespace xs = "urn:toc-schema";
            XDocument xToc;
            try
            {
                LOG.Info("Loading " + xSourceTocPath);
                xToc = XDocument.Load(xSourceTocPath);
            }
            catch (Exception ex)
            {
                LOG.Error(ex.Message);
                return;
            }

            if (!useCombinedFormat)
            {
                xToc.AddFirst(new XComment(@" ===================================================================== "));
                xToc.AddFirst(new XComment(@" Please edit its master file - \TableOfContents\TOC.xml"));
                xToc.AddFirst(new XComment(@" This is a Auto-Generated File that should not be changed by manaully."));
                xToc.AddFirst(new XComment(@" Attention!"));
                xToc.AddFirst(new XComment(@" ===================================================================== "));
            }

            LOG.Info("Removing flag attributes not matching " + Platform + " platform");
            xToc.Descendants(xs + "Flags").Remove();

            xToc.Descendants()
                .Where(c => c.Attribute("flag") != null && c.Attribute("flag").Value != Platform)
                .Remove();

            if (!useCombinedFormat)
            {
                LOG.Info("Removing lang attributes not matching " + Lang + " local");
                xToc.Descendants()
                   .Where(c => c.Attribute("lang") != null && c.Attribute("lang").Value != Lang)
                   .Remove();
            }

            //xToc.Descendants(xs + "route")
            //    .Where(c => c.Attribute("flag") != null && c.Attribute("flag").Value != Platform)
            //    .Remove();

            //xToc.Descendants(xs + "Control")
            //    .Where(c => c.Attribute("flag") != null && c.Attribute("flag").Value != Platform)
            //    .Remove();

            //xToc.Descendants(xs + "Sample")
            //    .Where(c => c.Attribute("flag") != null && c.Attribute("flag").Value != Platform)
            //    .Remove();

            //xToc.Descendants(xs + "HtmlData")
            //   .Where(c => c.Attribute("flag") != null && c.Attribute("flag").Value != Platform)
            //   .Remove();

            xToc.Descendants().Attributes("flag").Remove();

            if (!useCombinedFormat)
            {
                xToc.Descendants().Attributes("lang").Remove();
            }

            var sbElement = xToc.Element(xs + "SamplesBrowser");

            sbElement.DescendantsAndSelf().Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
            var ele = sbElement.DescendantsAndSelf();
            foreach (var el in ele)
            {
                el.Name = el.Name.LocalName;
            }

            if (!xDestinationTocPath.EndsWith("LocalStorage.xml"))
            {
                if (!useCombinedFormat)
                {
                    LOG.Info("Saving  " + xDestinationTocPath);
                    xToc.Save(xDestinationTocPath, SaveOptions.None);
                }
                else
                {
                    var tmpToc = xToc.ToString();

                    LOG.Info("Saving  " + xDestinationTocPath);

                    var tocContent = tmpToc.ToString();
                    tocContent = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" + tocContent;

                    // replacing old field names with combined name and local 'displayName' >> 'displayNameEN'
                    var oldFields = new List<string> { "displayName", "description", "controlPage", "FeaturesPage", "LinksPage", "ShowcasesPage" };
                    var tocLines = tocContent.Split('\n');

                    var descLineChangedEN = false;
                    var descLineChangedJA = false;
                    for (int i = 0; i < tocLines.Length; i++)
                    {
                        var line = tocLines[i];
                        foreach (var field in oldFields)
                            {
                                if (line.Contains("lang=\"EN\""))
                                {
                                    line = line.Replace("<"+ field +" lang=\"EN\"", "<" + field + "EN");
                                    line = line.Replace("</" + field + ">", "</" + field + "EN>");

                                    if (line.Trim().EndsWith("<descriptionEN>"))
                                    {
                                        descLineChangedEN = true;
                                    }
                                }
                                if (line.Contains("</description>") && descLineChangedEN)
                                {
                                    line = line.Replace("</description>", "</descriptionEN>");
                                    descLineChangedEN = false;
                                }

                                if (line.Contains("lang=\"JA\""))
                                {
                                    line = line.Replace("<" + field + " lang=\"JA\"", "<" + field + "JA");
                                    line = line.Replace("</" + field + ">", "</" + field + "JA>");

                                    if (line.Trim().EndsWith("<descriptionJA>"))
                                    {
                                        descLineChangedJA = true;
                                    }
                                }
                                if (line.Contains("</description>") && descLineChangedJA)
                                {
                                    line = line.Replace("</description>", "</descriptionJA>");
                                    descLineChangedJA = false;
                                }
                            }
                        tocLines[i] = line;
                    }

                    tocContent = string.Join("\r\n", tocLines);
                    var tocDoc = XDocument.Parse(tocContent);
                    tocDoc.Save(xDestinationTocPath, SaveOptions.None);
                }
            }
            else
            {
                LOG.Prefix = "CMS";
                LOG.Info("Generating " + Platform + " platform file: " + xDestinationTocPath);
                //TODO Language
                int LangID = Lang == "EN" ? 1033 : (Lang == "JA" ? 1041 : 0);
                //string tocPath = "~/TableOfContents." + Lang + ".xml";
                var sb = xToc.Element("SamplesBrowser");
                var CurVersion = sb.AttributeValue("releaseVersion");
                var productFamily_new = new ProductFamily();

                if (Platform == "WPF")
                {
                    //<ProductFamily ProductFamilyId="6" Name="NetAdvantage for WPF" RouteName="wpf" CmsContentID="773" TechnologyType="Win">
                    productFamily_new.Name = "WPF";
                    productFamily_new.RouteName = "wpf";
                    productFamily_new.TechnologyType = "Win";
                    productFamily_new.Components = new List<Component>();
                    productFamily_new.ProductFamilyId = 6;
                    productFamily_new.CmsContentID = 773;
                }
                else if (Platform == "SL")
                {
                    productFamily_new.Name = "Silverlight";
                    productFamily_new.RouteName = "silverlight/#";
                    productFamily_new.TechnologyType = "Silverlight";
                    productFamily_new.Components = new List<Component>();
                    productFamily_new.ProductFamilyId = 4;
                    productFamily_new.CmsContentID = 770;
                }
                //CmsContents
                //<FeaturesPage>/WebPages/SL/featuresPage.EN.html</FeaturesPage>

                productFamily_new.CmsContents = new CmsContentCollection();

                var featuresPage = sbElement.ElementValue("FeaturesPage");
                featuresPage = featuresPage.Replace("/", @"\");
                featuresPage = Environment.CurrentDirectory + featuresPage;
                if (File.Exists(featuresPage))
                {
                    LOG.Info("Loading Feature Pages...");
                    LoadFeaturesPage(featuresPage, LangID, productFamily_new);
                }
                else
                {
                    LOG.Warn("FeaturesPage is missing: " + featuresPage);
                }

                //<ShowcasesPage>/WebPages/SL/showcasePage.EN.html</ShowcasesPage>
                productFamily_new.AppSamplesOverviewCmsContent = new CmsContentCollection();

                var showcasesPage = sbElement.ElementValue("ShowcasesPage");
                showcasesPage = showcasesPage.Replace("/", @"\");
                showcasesPage = Environment.CurrentDirectory + showcasesPage;
                if (File.Exists(showcasesPage))
                {
                    LOG.Info("Loading Showcase Pages...");
                    LoadShowcasesPage(showcasesPage, LangID, productFamily_new);
                }
                else
                {
                    LOG.Warn("ShowcasesPage is missing: " + showcasesPage);
                }

                int componentCount = 1;
                int featureCount = 1;
                foreach (var xControl in sb.Descendants("Control"))
                {
                    var controlName = xControl.AttributeValue("name");
                    LOG.Info("Parsing control: " + controlName);
                    var controlGroup = xControl.Ancestors("ControlGroup").First();
                    var component = new Component
                    {
                        CmsContentID = 1,
                        ComponentId = componentCount,
                        FeatureCategories = new List<FeatureCategory>(),
                        GroupName = controlGroup.ElementValue("displayName"),
                        GroupID = controlGroup.ElementValue("displayName").GetHashCode(),
                        IgName = xControl.ElementValue("displayName"),
                        Name = controlName,
                        RouteName = xControl.AttributeValue("route")
                    };
                    var dbComponent = components.SingleOrDefault(tt =>
                        tt.RouteName == component.RouteName &&
                        tt.ProductFamilyID == productFamily_new.ProductFamilyId);

                    if (dbComponent != null)
                    {
                        component.CmsContentID = dbComponent.CmsContentID;
                        component.ComponentId = dbComponent.ComponentID;
                    }
                    else
                    {
                        LOG.Warn("Missing '" + component.RouteName + "' component in '" + productFamily_new.Name + "' product family");
                    }

                    component.ComponentXap = xControl.Descendants("Sample").Select(s => s.ElementValue("assembly"))
                        .GroupBy(a => a).Select(a => new { a.Key, count = a.Count() })
                        .OrderByDescending(a => a.count).First().Key + ".xap";

                    foreach (var xCategory in xControl.Descendants("Category"))
                    {
                        var featureCategory = new FeatureCategory
                        {
                            FeatureCategoryId = featureCount++,
                            LocalizedFeatures = new List<Feature>
                        {
                           new Feature
                           {
                               Category=featureCategories[xCategory.Attribute("name").Value],
                               Name=xCategory.Element("displayName").Value,
                               LanguageId=LangID
                           }
                        },
                            Samples = new List<Sample>()
                        };
                        foreach (var xSample in xCategory.Descendants("Sample"))
                        {
                            var sampleName = xSample.ElementValue("displayName");
                            //LOG.Info("Parsing sample: " + sampleName);
                            Content content;
                            try
                            {
                                content = new Content
                                {
                                    CultureCode = LangID,
                                    Description = xSample.ElementValue("description"),
                                    Title = sampleName,
                                    FallbackUrl = xSample.ElementValue("fallback"),
                                    Url = sb.ElementValue("route") + "/" + xControl.Attribute("route").Value +
                                            (Platform == "SL" ? "/#/" : "/") +
                                            xSample.Attribute("route").Value
                                };
                            }
                            catch (Exception ex)
                            {
                                var msg = "Error occured: " + ex.Message + " \n while parsing sample: " + sampleName;
                                LOG.Error(msg);
                                continue;
                            }

                            var sample = new Sample
                            {
                                Contents = new List<Content> { content },
                                FileReferences = new List<FileReference>()
                            };
                            CopyFile(sample.Contents.First().FallbackUrl);
                            if (xSample.Attribute("outputType") == null)
                            {
                                sample.ContentTypeID = 4;
                            }
                            else
                            {
                                switch (xSample.Attribute("outputType").Value)
                                {
                                    case "FeatureSample":
                                        sample.ContentTypeID = 4;
                                        break;
                                    case "ExtendedFeatureSample":
                                        sample.ContentTypeID = 11;
                                        break;
                                    case "WindowLessFeatureSample":
                                        sample.ContentTypeID = 12;
                                        break;
                                }
                            }
                            sample.SampleId = Guid.NewGuid();
                            sample.ParentFeatureCategoryId = featureCount;
                            if (xSample.Attribute("status") == null)
                            {
                                if (xSample.Attribute("releaseVersion") != null && CurVersion == xSample.Attribute("releaseVersion").Value)
                                    sample.Status = "NEW";
                            }
                            else
                            {
                                sample.Status = xSample.Attribute("status").Value;
                            }
                            sample.FileReferences.AddRange(xSample.Descendants("CodeFile").ToList().Select(cf =>
                               new FileReference
                               {
                                   Id = Guid.NewGuid(),
                                   FileOriginalPath = cf.Attribute("path").Value,
                                   IsStartupFile = bool.Parse(cf.Attribute("isStartUp").Value),
                                   Name = Path.GetFileName(cf.Attribute("path").Value),
                                   FilePath = cf.Attribute("path").Value
                               }));
                            sample.ProductReleaseFamilies = new List<ProductReleaseFamily>();
                            sample.ProductReleaseFamilies.Add(
                                new ProductReleaseFamily
                                {
                                    Name = xSample.Attribute("releaseVersion") == null ? "2012.2" : (xSample.Attribute("releaseVersion").Value.Length == 4 ? ("20" + xSample.Attribute("releaseVersion").Value) : ("200" + xSample.Attribute("releaseVersion").Value))
                                }
                                );
                            featureCategory.Samples.Add(sample);
                            GetImagesAndCopy(new CmsContent { ImageThumbnail = sample.Contents[0].FallbackUrl });
                        }
                        component.FeatureCategories.Add(featureCategory);
                    }
                    //CMS

                    try
                    {
                        var controlPage = xControl.Element("controlPage");
                        var controlPageName = controlPage.Value.Replace("/", @"\");
                        var controlPagePath = Environment.CurrentDirectory + controlPageName;
                        if (!File.Exists(controlPagePath))
                        {
                            LOG.Warn("Missing file: " + controlPagePath);
                        }
                        else
                        {
                            var cmsXControl = XDocument.Load(controlPagePath);
                            var entControl = new CmsContent()
                            {
                                CultureCode = LangID
                            };

                            foreach (var secControl in cmsXControl.Element("section").Elements("section"))
                            {
                                switch (secControl.Attribute("id").Value)
                                {
                                    case "title":
                                        entControl.Title = secControl.Value.Trim();
                                        break;
                                    case "icon":
                                        entControl.ImageThumbnail = "/samplesbrowser/" + secControl.Value.Trim();
                                        break;
                                    case "link":
                                        entControl.HyperLink = secControl.Value.Trim();
                                        break;
                                    case "html":
                                        entControl.Html = string.Join(string.Empty, secControl.Elements()).Trim();
                                        break;
                                }
                            }
                            component.CmsContents = new CmsContentCollection();
                            component.CmsContents.Add(entControl);
                            GetImagesAndCopy(entControl);
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = "File corrupped: " + xControl.Element("controlPage").Value;
                        msg += "\n " + ex.Message;
                        LOG.Warn(msg);
                    }
                    productFamily_new.Components.Add(component);
                    componentCount++;
                }

                var TableOfContent = new TableOfContent();
                TableOfContent.ProductFamilies = new List<ProductFamily>();
                TableOfContent.ProductFamilies.Add(productFamily_new);
                var serializer = new XmlSerializer(typeof(TableOfContent));

                LOG.Info("Saving " + xDestinationTocPath);

                FileStream fs = new FileStream(xDestinationTocPath, FileMode.Create);
                TextWriter writer = new StreamWriter(fs, new UTF8Encoding(true, false));

                serializer.Serialize(writer, TableOfContent);
                writer.Flush();
                writer.Close();
            }

            Close();
        }

        static void LoadShowcasesPage(string showcasesPage, int LangID, ProductFamily product)
        {
            var xApp = XDocument.Load(showcasesPage);

            foreach (var priApp in xApp.Element("section").Elements("section"))
            {
                var entApp = new CmsContent()
                {
                    CultureCode = LangID
                };
                foreach (var secApp in priApp.Elements("section"))
                {
                    switch (secApp.Attribute("id").Value)
                    {
                        case "title":
                            entApp.Title = secApp.Value.Trim();
                            break;
                        case "icon":
                            entApp.ImageThumbnail = secApp.Value.Trim();
                            break;
                        case "link":
                            entApp.HyperLink = secApp.Value.Trim();
                            break;
                        case "html":
                            entApp.Html = string.Join(string.Empty, secApp.Elements()).Trim();
                            break;
                    }
                }
                product.AppSamplesOverviewCmsContent.Add(entApp);
                GetImagesAndCopy(entApp);
            }

        }

        static void LoadFeaturesPage(string featuresPage, int LangID, ProductFamily product)
        {
             var xCms = XDocument.Load(featuresPage);

                foreach (var priCms in xCms.Element("section").Elements("section"))
                {
                    var entCms = new CmsContent()
                      {
                          CultureCode = LangID
                      };

                    foreach (var secCms in priCms.Elements("section"))
                    {
                        switch (secCms.Attribute("id").Value)
                        {
                            case "title":
                                entCms.Title = secCms.Value.Trim();
                                break;
                            case "icon":
                                entCms.ImageThumbnail = secCms.Value.Trim();
                                break;
                            case "link":
                                entCms.HyperLink = secCms.Value.Trim();
                                break;
                            case "html":
                                if (secCms.Elements().Count() > 0)
                                    entCms.Html = string.Join(string.Empty, secCms.Elements()).Trim();
                                else
                                    entCms.Html = string.Join(string.Empty, secCms.Value).Trim();
                                break;
                        }
                    }
                    product.CmsContents.Add(entCms);
                    GetImagesAndCopy(entCms);
                }
        }

        static void GetImagesAndCopy(CmsContent ret)
        {
            if (ret.ImageThumbnail != null)
            {
                var imgThumb = @"\" + ret.ImageThumbnail.Replace("/samplesbrowser/", "").Replace(@"/", @"\");
                CopyFile(imgThumb);

            }
            if (!string.IsNullOrWhiteSpace(ret.Html))
            {
                int i = 0;
                int j = 0;
                string foundPath = "";
                i = ret.Html.IndexOf(@"""WebImages/");
                while (i > 0)
                {
                    j = ret.Html.IndexOf('"', i + 1);
                    foundPath = @"\" + ret.Html.Substring(i + 1, j - i - 1).Replace(@"/", @"\");
                    CopyFile(foundPath);
                    i = ret.Html.IndexOf(@"""WebImages/", i + 1);
                }
            }
        }

        private static void CopyFile(string path)
        {
            try
            {
                path = path.TrimStart(@"/\".ToCharArray());
                var destFileName  = Path.Combine(xDestinationPath.Replace(@"\App_Data", "") , path);
                destFileName = destFileName.Replace("/", "\\");

                var destDir = Path.GetDirectoryName(destFileName);
                if (!Directory.Exists(destDir))
                     Directory.CreateDirectory(destDir);

                if (File.Exists(destFileName))
                    return;

                var sourceFileName = Path.Combine(xSourcePath, path);
                sourceFileName = sourceFileName.Replace("/", "\\");

                if (File.Exists(sourceFileName))
                    File.Copy(sourceFileName, destFileName, true);
                else
                    LOG.Warn("Cannot copy missing file: " + sourceFileName);
            }
            catch (Exception ex)
            {
                LOG.Warn(ex.Message);
            }
        }

        private static List<CGComponent> GetComponents()
        {
            var ret = new List<CGComponent>();
            ret.Add(new CGComponent(206, "barcode", 4, 532, 2529, 2530));
            ret.Add(new CGComponent(207, "bullet-graph", 4, 533, 2532, 2533));
            ret.Add(new CGComponent(209, "segmented-display", 4, 536, 2538, 2539));
            ret.Add(new CGComponent(210, "map", 4, 538, 2541, 2542));
            ret.Add(new CGComponent(211, "pivot-grid", 4, 540, 2544, 2545));
            ret.Add(new CGComponent(212, "timeline", 4, 9336, 2547, 2548));
            ret.Add(new CGComponent(213, "treemap", 4, 542, 2550, 2551));
            ret.Add(new CGComponent(214, "zoombar", 4, 544, 2553, 2554));
            ret.Add(new CGComponent(215, "drag-drop-framework", 4, 664, 2572, 2573));
            ret.Add(new CGComponent(216, "infragistics-compression", 4, 668, 2575, 2576));
            ret.Add(new CGComponent(217, "infragistics-excel", 4, 669, 2578, 2579));
            ret.Add(new CGComponent(218, "persistence-framework", 4, 672, 2581, 2582));
            ret.Add(new CGComponent(219, "virtual-collection", 4, 680, 2584, 2585));
            ret.Add(new CGComponent(220, "combo-editor", 4, -1, 2587, 2588));
            ret.Add(new CGComponent(221, "context-menu", 4, 660, 2590, 2591));
            ret.Add(new CGComponent(222, "data-tree", 4, 9334, 2593, 2594));
            ret.Add(new CGComponent(223, "dialog-window", 4, 662, 2596, 2597));
            ret.Add(new CGComponent(224, "combo-box", 4, 665, 2587, 2588));
            ret.Add(new CGComponent(225, "grid", 4, 666, 2602, 2603));
            ret.Add(new CGComponent(226, "html-viewer", 4, 667, 2605, 2606));
            ret.Add(new CGComponent(227, "menu", 4, 670, 2608, 2609));
            ret.Add(new CGComponent(228, "outlook-bar", 4, 671, 2611, 2612));
            ret.Add(new CGComponent(229, "ribbon", 4, 673, 2614, 2615));
            ret.Add(new CGComponent(230, "schedule", 4, 674, 2617, 2618));
            ret.Add(new CGComponent(231, "slider", 4, 675, 2620, 2621));
            ret.Add(new CGComponent(232, "spell-checker", 4, 676, 2623, 2624));
            ret.Add(new CGComponent(233, "tag-cloud", 4, 677, 2626, 2627));
            ret.Add(new CGComponent(234, "tile-view", 4, 678, 2629, 2630));
            ret.Add(new CGComponent(235, "tree", 4, 679, 2632, 2633));
            ret.Add(new CGComponent(236, "map", 6, 552, 2666, 2667));
            ret.Add(new CGComponent(237, "barcode", 6, 545, 2669, 2670));
            ret.Add(new CGComponent(238, "bullet-graph", 6, 546, 2672, 2673));
            ret.Add(new CGComponent(240, "segmented-display", 6, 550, 2678, 2679));
            ret.Add(new CGComponent(241, "pivot-grid", 6, 554, 2681, 2682));
            ret.Add(new CGComponent(242, "timeline", 6, 9339, 2684, 2685));
            ret.Add(new CGComponent(243, "treemap", 6, 556, 2687, 2688));
            ret.Add(new CGComponent(244, "zoombar", 6, 557, 2690, 2691));
            ret.Add(new CGComponent(245, "carousel-panel", 6, 683, 2715, 2716));
            ret.Add(new CGComponent(246, "carousel-listbox", 6, 682, 2718, 2719));
            ret.Add(new CGComponent(247, "data-grid", 6, 689, 2721, 2722));
            ret.Add(new CGComponent(248, "data-carousel", 6, 688, 2724, 2725));
            ret.Add(new CGComponent(249, "data-presenter", 6, 690, 2727, 2728));
            ret.Add(new CGComponent(250, "combo-box", 6, 696, 2730, 2731));
            ret.Add(new CGComponent(251, "ribbon", 6, 9337, 2733, 2734));
            ret.Add(new CGComponent(252, "chart", 6, 684, 2736, 2737));
            ret.Add(new CGComponent(253, "dock-manager", 6, 694, 2739, 2740));
            ret.Add(new CGComponent(254, "outlook-bar", 6, 700, 2742, 2743));
            ret.Add(new CGComponent(255, "tiles-control", 6, 707, 2745, 2746));
            ret.Add(new CGComponent(256, "data-cards", 6, 687, 2748, 2749));
            ret.Add(new CGComponent(257, "month-calendar", 6, 699, 2751, 2752));
            ret.Add(new CGComponent(258, "schedule", 6, 702, 2754, 2755));
            ret.Add(new CGComponent(259, "data-tree", 6, 9338, 2757, 2758));
            ret.Add(new CGComponent(260, "tab-control", 6, 705, 2760, 2761));
            ret.Add(new CGComponent(261, "wpf-reporting", 6, 708, 2763, 2764));
            ret.Add(new CGComponent(262, "color-picker", 6, 685, 2766, 2767));
            ret.Add(new CGComponent(263, "slider", 6, 703, 2769, 2770));
            ret.Add(new CGComponent(264, "menu", 6, 698, 2772, 2773));
            ret.Add(new CGComponent(265, "context-menu", 6, 686, 2775, 2776));
            ret.Add(new CGComponent(266, "dialog-window", 6, 693, 2778, 2779));
            ret.Add(new CGComponent(267, "spell-checker", 6, 704, 2781, 2782));
            ret.Add(new CGComponent(268, "tag-cloud", 6, 706, 2784, 2785));
            ret.Add(new CGComponent(338, "org-chart", 4, 539, 2562, 2563));
            ret.Add(new CGComponent(341, "web-chart", 4, 681, 2638, 2639));
            ret.Add(new CGComponent(342, "masked-editor", 4, 0, 2641, 2642));
            ret.Add(new CGComponent(343, "numeric-editor", 4, 0, 2644, 2645));
            ret.Add(new CGComponent(344, "day-view", 4, 0, 2647, 2648));
            ret.Add(new CGComponent(345, "month-view", 4, 0, 2650, 2651));
            ret.Add(new CGComponent(346, "schedule-view", 4, 0, 2653, 2654));
            ret.Add(new CGComponent(347, "color-picker", 4, 659, 2656, 2657));
            ret.Add(new CGComponent(348, "dock-manager", 4, 663, 2659, 2660));
            ret.Add(new CGComponent(353, "org-chart", 6, 553, 2705, 2706));
            ret.Add(new CGComponent(355, "masked-editor", 6, 0, 2787, 2788));
            ret.Add(new CGComponent(356, "date-time-editor", 6, 0, 2790, 2791));
            ret.Add(new CGComponent(357, "currency-editor", 6, 0, 2793, 2794));
            ret.Add(new CGComponent(358, "numeric-editor", 6, 0, 2796, 2797));
            ret.Add(new CGComponent(359, "combo-editor", 6, 696, 2799, 2800));
            ret.Add(new CGComponent(360, "check-editor", 6, 0, 2802, 2803));
            ret.Add(new CGComponent(361, "text-editor", 6, 0, 2805, 2806));
            ret.Add(new CGComponent(362, "infragistics-excel", 6, 697, 2808, 2809));
            ret.Add(new CGComponent(363, "xls-export", 6, 0, 2811, 2812));
            ret.Add(new CGComponent(364, "xls-import", 6, 0, 2814, 2815));
            ret.Add(new CGComponent(365, "data-presenter-excel-exporter", 6, 691, 2817, 2818));
            ret.Add(new CGComponent(366, "day-view", 6, 0, 2820, 2821));
            ret.Add(new CGComponent(367, "month-view", 6, 0, 2823, 2824));
            ret.Add(new CGComponent(368, "schedule-view", 6, 0, 2826, 2827));
            ret.Add(new CGComponent(369, "drag-drop-framework", 6, 695, 2829, 2830));
            ret.Add(new CGComponent(374, "network-node", 4, 2641, 3116, 3115));
            ret.Add(new CGComponent(375, "network-node", 6, 2645, 3161, 3160));
            ret.Add(new CGComponent(376, "barcode-reader", 4, 2639, 3119, 3118));
            ret.Add(new CGComponent(377, "barcode-reader", 6, 2643, 3164, 3163));
            ret.Add(new CGComponent(389, "calendar", 4, 2667, 3137, 3136));
            ret.Add(new CGComponent(390, "calendar", 6, 2671, 3182, 3181));
            ret.Add(new CGComponent(393, "infragistics-word", 4, 2673, 3140, 3139));
            ret.Add(new CGComponent(394, "infragistics-word", 6, 2676, 3185, 3184));
            ret.Add(new CGComponent(404, "geographic-map", 6, 5546, 3170, 3169));
            ret.Add(new CGComponent(405, "pie-chart", 6, 5540, 3173, 3172));
            ret.Add(new CGComponent(406, "funnel-chart", 6, 5653, 3176, 3175));
            ret.Add(new CGComponent(409, "geographic-map", 4, 5429, 3125, 3124));
            ret.Add(new CGComponent(413, "calculation-manager", 4, 5494, 3143, 3142));
            ret.Add(new CGComponent(414, "multicolumn-combo", 4, 5504, 3146, 3145));
            ret.Add(new CGComponent(415, "resource-washer", 4, 5506, 3149, 3148));
            ret.Add(new CGComponent(416, "infragistics-math", 4, 4890, 3152, 3151));
            ret.Add(new CGComponent(417, "date-picker", 4, 5497, 3155, 3154));
            ret.Add(new CGComponent(418, "tile-manager", 4, 5508, 3158, 3157));
            ret.Add(new CGComponent(419, "calculation-manager", 6, 5512, 3188, 3187));
            ret.Add(new CGComponent(420, "multicolumn-combo", 6, 5521, 3191, 3190));
            ret.Add(new CGComponent(421, "grid", 6, 5527, 3194, 3193));
            ret.Add(new CGComponent(423, "resource-washer", 6, 5525, 3197, 3196));
            ret.Add(new CGComponent(424, "infragistics-math", 6, 4893, 3200, 3199));
            ret.Add(new CGComponent(425, "date-picker", 6, 5515, 3203, 3202));
            ret.Add(new CGComponent(426, "persistence-framework", 6, 5523, 3206, 3205));
            ret.Add(new CGComponent(427, "tile-manager", 6, 5529, 3209, 3208));
            ret.Add(new CGComponent(474, "formula-editor", 4, 5500, 3499, 3498));
            ret.Add(new CGComponent(475, "formula-editor", 6, 5517, 3532, 3531));
            ret.Add(new CGComponent(476, "sparkline", 4, 5427, 3493, 3492));
            ret.Add(new CGComponent(477, "sparkline", 6, 5542, 3526, 3525));
            ret.Add(new CGComponent(480, "inputs", 4, 5502, 3502, 3501));
            ret.Add(new CGComponent(481, "inputs", 6, 5519, 3535, 3534));
            ret.Add(new CGComponent(483, "data-chart", 4, 5492, 3505, 3504));
            ret.Add(new CGComponent(484, "data-chart", 6, 5510, 3538, 3537));
            ret.Add(new CGComponent(490, "funnel-chart", 4, 5595, 3508, 3507));
            ret.Add(new CGComponent(492, "pie-chart", 4, 5631, 3511, 3510));
            ret.Add(new CGComponent(494, "sparkline-chart", 4, 0, 0, 0));
            ret.Add(new CGComponent(495, "sparkline-chart", 6, 0, 0, 0));
            ret.Add(new CGComponent(513, "math-calculations", 4, -1, 3514, 3513));
            ret.Add(new CGComponent(514, "math-calculations", 6, -1, 3547, 3546));
            ret.Add(new CGComponent(530, "color-tuner", 4, 7161, 3517, 3516));
            ret.Add(new CGComponent(531, "color-tuner", 6, 8181, 3550, 3549));
            ret.Add(new CGComponent(532, "gantt", 4, 8482, 3520, 3519));
            ret.Add(new CGComponent(533, "gantt", 6, 8487, 3553, 3552));
            ret.Add(new CGComponent(534, "undo", 4, 7159, 3523, 3522));
            ret.Add(new CGComponent(535, "undo", 6, 7164, 3556, 3555));
            ret.Add(new CGComponent(544, "syntax-editor", 6, 9061, 4006, 4005));
            ret.Add(new CGComponent(545, "syntax-editor", 4, 9316, 3733, 3732));
            ret.Add(new CGComponent(621, "syntax-parsing-engine", 4, 8585, 4000, 3999));
            ret.Add(new CGComponent(622, "syntax-parsing-engine", 6, 8760, 4009, 4008));
            ret.Add(new CGComponent(635, "data-grid", 4, -1, -1, -1));
            ret.Add(new CGComponent(638, "richtext-editor", 4, 11119, -1, -1));
            ret.Add(new CGComponent(639, "richtext-editor", 6, 11120, -1, -1));
            ret.Add(new CGComponent(642, "radial-gauge", 4, 10131, 3997, 3996));
            ret.Add(new CGComponent(643, "radial-gauge", 6, 10325, 4003, 4002));
            ret.Add(new CGComponent(645, "doughnut-chart", 4, 10123, 3958, 3957));
            ret.Add(new CGComponent(646, "doughnut-chart", 6, 10311, 3961, 3960));
            ret.Add(new CGComponent(677, "linear-gauge", 4, 11419, -1, -1));
            ret.Add(new CGComponent(678, "linear-gauge", 6, 11420, -1, -1));
            ret.Add(new CGComponent(688, "radial-menu", 4, 11514, 4157, 4156));
            ret.Add(new CGComponent(689, "radial-menu", 6, 11512, 4154, 4153));
            ret.Add(new CGComponent(692, "diagram", 6, 11540, 4163, 4162));
            ret.Add(new CGComponent(694, "spreadsheet", 6, 11574, -1, -1));
            ret.Add(new CGComponent(695, "infragistics-theme-manager", 6, -1, -1, -1));
            ret.Add(new CGComponent(696, "property-grid", 6, -1, -1, -1));

            return ret;
        }


    }


}