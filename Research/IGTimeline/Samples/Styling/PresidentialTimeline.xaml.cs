using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Infragistics.Controls.Timelines;
using Infragistics.Samples.Shared.DataProviders;

namespace IGTimeline.Samples
{
    public partial class PresidentialTimeline : Infragistics.Samples.Framework.SampleContainer
    {
        XmlDataProvider _dataSource;

        public PresidentialTimeline()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this._dataSource = new XmlDataProvider();
            this._dataSource.GetXmlDataCompleted += OnGetXmlDataCompleted;
            this._dataSource.GetXmlDataAsync("Presidents.xml");
        }
        void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.HasError) return;

            string path = ImageFilePathProvider.GetImageLocalPath() + "Presidents/";
            // parse xml data
            var doc = e.Result;
            var presidents =
              from president in doc.Element("Presidents").Elements("President")
              let photoUri = new Uri(path + president.Attribute("PhotoUri").Value, UriKind.RelativeOrAbsolute)
              select new
              {
                  IsRepublican = (bool)president.Attribute("IsRepublican"),
                  Name = (string)president.Attribute("Name"),
                  YearsInOffice = (int)president.Attribute("YearsInOffice"),
                  FirstYearInOffice = (int)president.Attribute("FirstYearInOffice"),
                  PhotoSource = new BitmapImage(photoUri)
              };

            var republicanSeries = this.timeline.Series[0] as NumericTimeSeries;
            var democratSeries = this.timeline.Series[1] as NumericTimeSeries;

            var republicanTitleStyle = this.Resources["EventTitleBottomStyle"] as Style;
            var republicanDetailStyle = this.Resources["EventDetailsBottomStyle"] as Style;
            var democratTitleStyle = this.Resources["EventTitleTopStyle"] as Style;
            var democratDetailStyle = this.Resources["EventDetailsTopStyle"] as Style;

            foreach (var president in presidents)
            {
                var entry = new NumericTimeEntry
                {
                    Title = president.Name,
                    Details = president.PhotoSource,
                    Duration = president.YearsInOffice,
                    Time = president.FirstYearInOffice
                };

                if (president.IsRepublican)
                {
                    entry.TitleStyle = republicanTitleStyle;
                    entry.DetailsStyle = republicanDetailStyle;
                    republicanSeries.Entries.Add(entry);
                }
                else
                {
                    entry.TitleStyle = democratTitleStyle;
                    entry.DetailsStyle = democratDetailStyle;
                    democratSeries.Entries.Add(entry);
                }
            }
        }
    }
}
