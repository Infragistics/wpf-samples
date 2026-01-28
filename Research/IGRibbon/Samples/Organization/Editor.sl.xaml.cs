using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using IGRibbon.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGRibbon.Samples.Organization
{
    public partial class Editor : SampleContainer
    {
        private XamGrid MyDataGrid;
        ResourceDictionary xamGridRes;
        private XmlDataProvider xmlDataProvider;

        public Editor()
        {
            InitializeComponent();
        }

        #region ClipboardOperations

        private void Paste_Click(object sender, RibbonToolEventArgs e)
        {
            try
            {
                rtb.Selection.Text = Clipboard.GetText();
            }
            catch (System.Security.SecurityException)
            {
                ChildWindow errorWindow = new ErrorWindow(RibbonStrings.XWR_SilverlightClipboard_Title, RibbonStrings.XWR_SilverlightClipboard_Content);
                errorWindow.Show();
            }
            ReturnFocus();
        }

        private void Cut_Click(object sender, RibbonToolEventArgs e)
        {
            try
            {
                Clipboard.SetText(rtb.Selection.Text);
                rtb.Selection.Text = "";
            }
            catch (System.Security.SecurityException)
            {
                ChildWindow errorWindow = new ErrorWindow(RibbonStrings.XWR_SilverlightClipboard_Title, RibbonStrings.XWR_SilverlightClipboard_Content);
                errorWindow.Show();
            }
            ReturnFocus();
        }

        private void Copy_Click(object sender, RibbonToolEventArgs e)
        {
            try
            {
                Clipboard.SetText(rtb.Selection.Text);
            }
            catch (System.Security.SecurityException)
            {
                ChildWindow errorWindow = new ErrorWindow(RibbonStrings.XWR_SilverlightClipboard_Title, RibbonStrings.XWR_SilverlightClipboard_Content);
                errorWindow.Show();
            }
            ReturnFocus();
        }

        #endregion

        #region HelperFuntions
        private void ReturnFocus()
        {
            if (rtb != null)
                rtb.Focus();
        }
        #endregion

        #region FontOperations
        private void Bold_Click(object sender, RibbonToolEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontWeightProperty) is FontWeight && (
                    (FontWeight)rtb.Selection.GetPropertyValue(Run.FontWeightProperty)) == FontWeights.Normal)
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                else
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            }
            ReturnFocus();
        }

        private void Italic_Click(object sender, RibbonToolEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontStyleProperty) is FontStyle && (
                    (FontStyle)rtb.Selection.GetPropertyValue(Run.FontStyleProperty)) == FontStyles.Normal)
                    rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
                else
                    rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            }
            ReturnFocus();
        }

        private void Underline_Click(object sender, RibbonToolEventArgs e)
        {
            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (rtb.Selection.GetPropertyValue(Run.TextDecorationsProperty) == null)
                    rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
                else
                    rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            }
            ReturnFocus();
        }
        #endregion

        #region Gallery
        private void GalleryTool_ItemClicked(object sender, GalleryToolEventArgs e)
        {
            GalleryItem MyItem = e.Item;
            SolidColorBrush RedColor = new SolidColorBrush();
            RedColor.Color = Colors.Red;

            SolidColorBrush BlueColor = new SolidColorBrush();
            BlueColor.Color = Colors.Blue;

            SolidColorBrush PurpleColor = new SolidColorBrush();
            PurpleColor.Color = Colors.Purple;

            SolidColorBrush Cyan = new SolidColorBrush();
            Cyan.Color = Colors.Cyan;

            SolidColorBrush Pink = new SolidColorBrush();
            Pink.Color = Colors.Magenta;

            SolidColorBrush Green = new SolidColorBrush();
            Green.Color = Colors.Green;

            if (rtb != null && rtb.Selection.Text.Length > 0)
            {
                if (MyItem.Text.Equals(RibbonStrings.XWR_Normal))
                {
                    rtb.Selection.ApplyPropertyValue(Run.FontSizeProperty, 12);
                }

                else if (MyItem.Text.Equals(RibbonStrings.XWR_Bold))
                {
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Large))
                {
                    rtb.Selection.ApplyPropertyValue(Run.FontSizeProperty, 36);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Red))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, RedColor);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Blue))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, BlueColor);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Purple))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, PurpleColor);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Cyan))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, Cyan);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Pink))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, Pink);
                }
                else if (MyItem.Text.Equals(RibbonStrings.XWR_Green))
                {
                    rtb.Selection.ApplyPropertyValue(Run.ForegroundProperty, Green);
                }
            }
        }
        #endregion

        #region InsertItems

        private void InsertGrid_Click(object sender, RibbonToolEventArgs e)
        {
            MyDataGrid = new XamGrid();
            xamGridRes = new ResourceDictionary();
            xamGridRes.Source = new Uri("/Infragistics.Themes.IGTheme;component/IG.xamGrid.xaml", UriKind.Relative);
            MyDataGrid.Resources.MergedDictionaries.Add(xamGridRes);

            InlineUIContainer container = new InlineUIContainer();

            container.Child = getDataGrid();

            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private XamGrid getDataGrid()
        {
            MyDataGrid.AutoGenerateColumns = true;
            MyDataGrid.Width = 500;
            MyDataGrid.Height = 150;
            DownloadDataSource();

            return MyDataGrid;
        }

        private void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += xmlDataProvider_GetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("CustomersOrders.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Common\XElementExtension class. 
        /// </summary>
        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            if (gxdcea.Error != null)
            {
                return;
            }

            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").Value,
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  ContactTitle = d.Element("ContactTitle").Value,
                                  AddressOne = d.Element("Address").Value,
                                  City = d.Element("City").Value,
                                  Region = d.Element("Region").GetString(),
                                  Country = d.Element("Country").Value
                              });

            this.MyDataGrid.ItemsSource = dataSource.ToList();
        }

        private void InsertPicture_Click(object sender, RibbonToolEventArgs e)
        {
            InlineUIContainer container = new InlineUIContainer();

            container.Child = CreateImageFromUri(ImageFilePathProvider.GetImageLocalUri("Ribbon/Infragistics_logo.png"), 200, 150);

            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private void InsertChart_ItemClicked(object sender, GalleryToolEventArgs e)
        {
            GalleryItem MyItem = e.Item;
            InlineUIContainer container = new InlineUIContainer();

            switch (MyItem.Text)
            {
                case "Column":
                    container.Child = CreateImageFromUri(ImageFilePathProvider.GetImageLocalUri("Ribbon/ChartColumn32.png"), 125, 75);
                    break;
                case "Pie":
                    container.Child = CreateImageFromUri(ImageFilePathProvider.GetImageLocalUri("Ribbon/ChartPie32.png"), 125, 75);
                    break;
                case "Line":
                    container.Child = CreateImageFromUri(ImageFilePathProvider.GetImageLocalUri("Ribbon/ChartLine32.png"), 125, 75);
                    break;
                case "Bar":
                    container.Child = CreateImageFromUri(ImageFilePathProvider.GetImageLocalUri("Ribbon/ChartBar32.png"), 125, 75);
                    break;
                default:
                    break;
            }
            rtb.Selection.Insert(container);
            ReturnFocus();
        }

        private static Image CreateImageFromUri(Uri URI, double width, double height)
        {
            Image img = new Image();
            img.Stretch = Stretch.Uniform;
            img.Width = width;
            img.Height = height;
            BitmapImage bi = new BitmapImage(URI);
            img.Source = bi;
            img.Tag = bi.UriSource.ToString();
            return img;
        }
        #endregion

        #region FileOperations
        private void New_Click(object sender, RibbonToolEventArgs e)
        {
            rtb.Blocks.Clear();
        }

        private void Open_Click(object sender, RibbonToolEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Saved Files|*.sav|All Files|*.*";

            if (ofd.ShowDialog().Value)
            {
                FileInfo fi = ofd.File;
                StreamReader r = fi.OpenText();
                rtb.Xaml = r.ReadToEnd();
                r.Close();
            }
        }

        private void Save_Click(object sender, RibbonToolEventArgs e)
        {
            string ContentToSave = rtb.Xaml;

            //Check if the file contains any UIElements
            var res = from block in rtb.Blocks
                      from inline in (block as Paragraph).Inlines
                      where inline.GetType() == typeof(InlineUIContainer)
                      select inline;

            //If the file contains any UIElements, it will not be saved
            if (res.Count() != 0)
            {
                MessageBox.Show("Saving documents with UIElements is not supported");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".sav";
            sfd.Filter = "Saved Files|*.sav|All Files|*.*";

            if (sfd.ShowDialog().Value)
            {
                using (FileStream fs = (FileStream)sfd.OpenFile())
                {
                    System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                    byte[] buffer = enc.GetBytes(ContentToSave);
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }
        #endregion
    }
}