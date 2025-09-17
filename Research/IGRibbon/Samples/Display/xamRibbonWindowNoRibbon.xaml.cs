using IGRibbon.Resources;
using Infragistics.Samples.Shared.Converters;
using Infragistics.Windows.Ribbon;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGRibbon.Samples.Display
{
    public partial class xamRibbonWindowNoRibbon : XamRibbonWindow
    {
        public xamRibbonWindowNoRibbon()
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
                appPath = dir.FullName; // dir.Parent.Parent.FullName;

                relativeSamplePath =
                    //[GE] TFS242703 - This is related to the improvement described in TFS242434. We no longer need the first "Samples" as we no longer copy everything to the Output path folder.
                    //"Samples" + System.IO.Path.DirectorySeparatorChar +
                    "IGRibbon" + System.IO.Path.DirectorySeparatorChar +
                    "Samples" + System.IO.Path.DirectorySeparatorChar +
                    "Display" + System.IO.Path.DirectorySeparatorChar + "xamRibbonWindowNoRibbon.xaml";

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
            RichTextBox rtb = converter.Convert(xamlText, null, this.RootGrid, null) as RichTextBox;
            rtb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            rtb.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            rtb.Document.PageWidth = 1000;
            xamlContent.Child = rtb;
        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            Color? color = null;

            var selcted = ((RadioButton)sender).Content as string;

            switch (selcted)
            {
                case "Excel":
                    color = Color.FromArgb(255, 33, 115, 70);
                    break;
                case "Powerpoint":
                    color = Color.FromArgb(255, 210, 71, 38);
                    break;
                case "Word":
                    color = Color.FromArgb(255, 43, 87, 154);
                    break;
                case "Outlook":
                    color = Color.FromArgb(255, 0, 114, 198);
                    break;
            }

            if (color != null)
            {
                RibbonWindowContentHost.SetApplicationAccentColor(contentHost, color.Value);
            }
        }
    }
}
