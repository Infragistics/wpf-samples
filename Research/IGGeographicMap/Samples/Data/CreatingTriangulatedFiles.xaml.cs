using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Threading;
using System.Globalization;

namespace IGGeographicMap.Samples.Data
{
    public partial class CreatingTriangulatedFiles : Infragistics.Samples.Framework.SampleContainer
    {
        public CreatingTriangulatedFiles()
        {
            InitializeComponent();            
            this.Loaded += OnSampleLoaded;
        }
        protected long RequiredSpaceForTriangulatedFile = 3 * 1024 * 1024; 
        protected ItfConverter ItfConverter;
        protected ShapefileConverter ShapefileConverter;
        protected IList<ShapefileRecord> ShapefileRecords;
        protected TriangulationSource TriangulationData;
        protected string TriangulationFileName = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".itf";

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.PerformTriangulationButton.IsEnabled = false;
            this.SaveTriangulationFileButton.IsEnabled = false;
            this.LoadTriangulationFileButton.IsEnabled = false;

            // show and set bounds of geo-map world 
            var mapRegion = new GeoRegion(new GeoLocation(-110, 50), new GeoLocation(-80, 15));
            Rect geoRect = mapRegion.ToGeoRect();
            this.GeoMap.WorldRect = geoRect;
        }

        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        private void OnLoadShapeFileButtonClick(object sender, RoutedEventArgs e)
        {           
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            this.PerformTriangulationButton.IsEnabled = false;
            this.SaveTriangulationFileButton.IsEnabled = false;
            this.LoadTriangulationFileButton.IsEnabled = false;
            
            System.Diagnostics.Debug.WriteLine("loading shape file...");

            var shapeFileProvider = new ShapeFileProvider();
            shapeFileProvider.ShapeFileRelativePath = "/Weather/Precipitation/nws_precip_2011091820.shp";
            shapeFileProvider.ShapeDatabaseRelativePath = "/Weather/Precipitation/nws_precip_2011091820.dbf";
             
            ShapefileConverter = new ShapefileConverter();
            ShapefileConverter.ImportCompleted += OnShapeFileConverterImportCompleted;
            ShapefileConverter.ShapefileSource = new Uri(shapeFileProvider.ShapeFileAbsolutePath, UriKind.RelativeOrAbsolute);
            ShapefileConverter.DatabaseSource = new Uri(shapeFileProvider.ShapeDatabaseAbsolutePath, UriKind.RelativeOrAbsolute);
        }
        private void OnShapeFileConverterImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ShapefileRecords = (IList<ShapefileRecord>)ShapefileConverter;
            this.PerformTriangulationButton.IsEnabled = true;
                
            System.Diagnostics.Debug.WriteLine("loading shape file has been completed.");
        }
        private void OnPerformTriangulationButtonClick(object sender, RoutedEventArgs e)
        {

           
            
            this.SaveTriangulationFileButton.IsEnabled = false;
            this.LoadTriangulationFileButton.IsEnabled = false;
     
            if( ShapefileRecords != null && ShapefileRecords.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("performing triangulation...");
                // perform triangulation on loaded shape file
                TriangulationData = TriangulationSource.Create(ShapefileRecords.Count,
                    (i) => ShapefileRecords[i].Points[0][0],
                    (i) => Convert.ToSingle(ShapefileRecords[i].Fields["Globvalue"]));
                this.SaveTriangulationFileButton.IsEnabled = true;
 
                System.Diagnostics.Debug.WriteLine("performing triangulation has been completed.");

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

            }
        }
        private void OnSaveTriangulationFileButtonClick(object sender, RoutedEventArgs e)
        {
            this.LoadTriangulationFileButton.IsEnabled = false;
            this.TriangulationStatusTextBlock.Text = CommonStrings.XW_SampleStatus_Loading;
            this.TriangulationStatusOverlay.Visibility = Visibility.Visible;

            // check if there is enough space for saving Triangulated files in IsolatedStorage
            if (!IsolatedStorageProvider.IsEnoughSpaceInIsoStorage(this.RequiredSpaceForTriangulatedFile))
            {
                System.Diagnostics.Debug.WriteLine("detected not enough space in Isolated Storage for saving triangulated file.");

                IsolatedStorageIncreaseRespond respond = IsolatedStorageProvider.IncreaseIsoStorage(this.RequiredSpaceForTriangulatedFile);
                if (respond == IsolatedStorageIncreaseRespond.Declined) return;
            }
            #region Save Triangulation
            try
            {
                // save triangulation as an ITF file.
                using (IsolatedStorageFile iso = IsolatedStorageProvider.GetIsolatedStorageFile())
                {
                    System.Diagnostics.Debug.WriteLine("saving triangulation...");
                    
                    if (!iso.DirectoryExists("TriangulatedFiles"))
                    {
                        iso.CreateDirectory("TriangulatedFiles");
                    }
                    string filePath = "TriangulatedFiles/" + TriangulationFileName;
                    using (var stream = new IsolatedStorageFileStream(filePath, FileMode.Create, iso))
                    {
                        // save Triangulation as an ITF file 
                        TriangulationData.SaveItf(stream);
                        stream.Close();
                        System.Diagnostics.Debug.WriteLine("saving triangulation has been completed.");
                        this.TriangulationStatusOverlay.Visibility = Visibility.Collapsed;
                        this.LoadTriangulationFileButton.IsEnabled = true;
                    }
                }
            }
            catch (IsolatedStorageException ex)
            {
                // Isolated storage not enabled or an error occurred
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            #endregion
        }
        private void OnLoadTriangulationFileButtonClick(object sender, RoutedEventArgs e)
        {
            this.TriangulationStatusTextBlock.Text = CommonStrings.XW_SampleStatus_Loading;
            this.TriangulationStatusOverlay.Visibility = Visibility.Visible;
            
            TriangulationSource triangulationData;
             // load triangulation from an ITF file.
            using (IsolatedStorageFile iso = IsolatedStorageProvider.GetIsolatedStorageFile())
            {
                System.Diagnostics.Debug.WriteLine("loading triangulation file...");
                string filePath = "TriangulatedFiles/" + TriangulationFileName;
                if (iso.FileExists(filePath))
                {
                    using (var stream = new IsolatedStorageFileStream(filePath, FileMode.Open, iso))
                    {
                        // save Triangulation as an ITF file 
                        triangulationData = TriangulationSource.LoadItf(stream);
                        stream.Close();
                        System.Diagnostics.Debug.WriteLine("loading triangulation file has been completed.");

                        if (triangulationData != null)
                        {
                            System.Diagnostics.Debug.WriteLine("binding triangulation...");
                            var series = this.GeoMap.Series[0] as GeographicScatterAreaSeries;
                            series.ItemsSource = triangulationData.Points;
                            series.TrianglesSource = triangulationData.Triangles;
                            this.TriangulationStatusOverlay.Visibility = Visibility.Collapsed;
                            System.Diagnostics.Debug.WriteLine("binding triangulation has been completed.");
                        }
                    }
                }    
            }
         
        }

        void OnItfConverterPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TriangulationSource"))
            {
                System.Diagnostics.Debug.WriteLine("loading triangulation...");
                var series = GeoMap.Series[0] as GeographicScatterAreaSeries;
                series.ItemsSource = ItfConverter.TriangulationSource.Points;
                series.TrianglesSource = ItfConverter.TriangulationSource.Triangles;

            }
        }

       
   }
   
}
