using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGRibbon.Resources;
using Infragistics.Windows.Ribbon;
using Infragistics.Samples.Shared.Converters;

namespace IGRibbon.Display
{
    /// <summary>
    /// Interaction logic for xamRibbonWindow.xaml
    /// </summary>
    public partial class xamRibbonWindow : XamRibbonWindow
    {
        public xamRibbonWindow()
        {
            InitializeComponent();

            // Load text of sample
            string xamlText = "";

            // Get executable directory
            string appPath = null;
            string relativeSamplePath = null;
            try
            {
                appPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;  //get the executing assembly path
                appPath = appPath.Substring(8);  //remove the file:///
                appPath = System.IO.Path.GetDirectoryName(appPath); //Strip the file name
                DirectoryInfo dir = new DirectoryInfo(appPath);  //Figure out the proper parent directory
                appPath = dir.FullName;

                relativeSamplePath =
                    //[GE] TFS242703 - This is related to the improvement described in TFS242434. We no longer need the first "Samples" as we no longer copy everything to the Output path folder.
                    //"Samples" + System.IO.Path.DirectorySeparatorChar +
                    "IGRibbon" + System.IO.Path.DirectorySeparatorChar +
                    "Samples" + System.IO.Path.DirectorySeparatorChar +
                    "Display" + System.IO.Path.DirectorySeparatorChar + "xamRibbonWindow.xaml";

                //[GE] TFS242703 - This is related to the improvement described in TFS242434. As we no longer copy every project's 'Samples' folder to the Output path folder we need to get the code by navigating one folder up.
                xamlText = System.IO.File.ReadAllText(System.IO.Path.Combine(appPath, "..\\", relativeSamplePath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("appPath=" + appPath);
                Debug.WriteLine("ERROR loading xaml: " + ex.GetType().Name + "  MESSAGE: " + ex.ToString());

                // A problem occurred when trying to locate the source XAML file.
                xamlText = RibbonStrings.XamRibbonWindow_XamlLoadError;
            }

            XAMLColorizerConverter converter = new XAMLColorizerConverter();
            RichTextBox rtb = converter.Convert(xamlText, null, this.root, null) as RichTextBox;
            rtb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            rtb.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            rtb.Document.PageWidth = 1000;
            this.root.Child = rtb;
        }
    }
}
