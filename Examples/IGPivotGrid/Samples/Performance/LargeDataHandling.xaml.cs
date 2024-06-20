using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using IGPivotGrid.Resources;
using Infragistics.Olap;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGPivotGrid.Samples.Performance
{
    public partial class LargeDataHandling : SampleContainer
    {
        private BackgroundWorker loaderbw;
        Storyboard hideLoadText;

        public LargeDataHandling()
        {
            InitializeComponent();
            InitializeLoaderBw();
            hideLoadText = this.Resources["hideMessages"] as Storyboard;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loaderbw.IsBusy) return;

            //Generate data
            GenerateDataButton.IsEnabled = false;
            messageContainer.Visibility = Visibility.Visible;
            loaderbw.RunWorkerAsync();

            int numberOfSalesToGenerate = (int)numberOfSalesToGenerateComboBox.SelectedItem;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender2, e2) =>
            {
                e2.Result = SalesDataGenerator.GenerateSales(numberOfSalesToGenerate);
            };
            bw.RunWorkerCompleted += (sender2, e2) =>
            {
                IEnumerable GeneratedData = e2.Result as IEnumerable;

                generatingText.Visibility = Visibility.Collapsed;
                loadingText.Visibility = Visibility.Visible;

                //Create a DataSource from that data
                FlatDataSource dataSource = new FlatDataSource()
                {
                    Cube = DataSourceBase.GenerateInitialCube("Sale"),
                    Rows = DataSourceBase.GenerateInitialItems("[City].[City]"),
                    Columns = DataSourceBase.GenerateInitialItems("[Product].[Product]"),
                    Measures = DataSourceBase.GenerateInitialItems("AmountOfSale"),
                    ItemsSource = GeneratedData
                };

                dataSource.CubesSettings.Add(GenerateCubeMetadata());
                dataSource.AddHierarchyDescriptor(GenerateSellerHierarchy());
                dataSource.AddHierarchyDescriptor(GenerateProductHierarchy());
                dataSource.AddHierarchyDescriptor(GenerateLocationHierarchy());

                //Assign the DataSource to the xamPivotGrid and xamPivotDataSelector
                BackgroundWorker dataBw = new BackgroundWorker();
                dataBw.DoWork += (sender3, e3) =>
                {
                    this.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        this.pivotGrid.DataSource = dataSource;
                        this.dataSelector.DataSource = dataSource;
                    }));
                };
                dataBw.RunWorkerCompleted += (sender3, e3) =>
                {
                    // when data is fully loaded in the datasource, ResultChanged will be raised
                    dataSource.ResultChanged += DataSource_ResultChanged;
                };
                dataBw.RunWorkerAsync();
            };
            bw.RunWorkerAsync();
        }

        private void DataSource_ResultChanged(object sender, ResultChangedEventArgs e)
        {
            pivotGrid.DataSource.ResultChanged -= DataSource_ResultChanged;
            GenerateDataButton.IsEnabled = true;
            loaderbw.CancelAsync();
            hideLoadText.Begin();
            pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }

        #region HelperMethods
        private void InitializeLoaderBw()
        {
            loaderbw = new BackgroundWorker();
            loaderbw.WorkerSupportsCancellation = true;
            loaderbw.DoWork += (sender, e) =>
            {
                int dots = 0;
                while (!loaderbw.CancellationPending)
                {
                    string generateMessage = PivotGridStrings.XPG_LargeDataHandling_GeneratingData;

                    if (dots > 3) dots = 0;
                    for (int i = 0; i < dots; i++)
                    {
                        generateMessage += ".";
                    }
                    dots++;

                    if (!e.Cancel)
                    {
                        loadingText.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            generatingText.Text = generateMessage;
                        }));

                        Thread.Sleep(400);
                    }
                }
            };
        }

        private static HierarchyDescriptor<Sale> GenerateLocationHierarchy()
        {
            HierarchyDescriptor<Sale> locationHierarchy = new HierarchyDescriptor<Sale>(p => p.City);
            locationHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_All
                );
            locationHierarchy.AddLevel(p =>
                p.City,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Location_Name
                );
            return locationHierarchy;
        }

        private static HierarchyDescriptor<Sale> GenerateProductHierarchy()
        {
            HierarchyDescriptor<Sale> productHierarchy = new HierarchyDescriptor<Sale>(p => p.Product);
            productHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_All
                );
            productHierarchy.AddLevel(p =>
                p.Product.Name,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Product_Name
                );
            return productHierarchy;
        }

        private static HierarchyDescriptor<Sale> GenerateSellerHierarchy()
        {
            HierarchyDescriptor<Sale> sellerHierarchy = new HierarchyDescriptor<Sale>(p => p.Seller);
            sellerHierarchy.AddLevel(p =>
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_All,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_All
                );
            sellerHierarchy.AddLevel(p =>
                p.Seller.Name,
                PivotGridStrings.XPG_Data_HierarchyDescriptors_Seller_Name
                );
            return sellerHierarchy;
        }

        private CubeMetadata GenerateCubeMetadata()
        {
            CubeMetadata cubeMetadata = new CubeMetadata();
            cubeMetadata.DisplayName = PivotGridStrings.XPG_Data_CubeMetadata_DisplayName;
            cubeMetadata.DataTypeFullName = typeof(Sale).FullName;
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "AmountOfSale",
                DisplayName = PivotGridStrings.XPG_Data_AmountOfSale_DisplayName,
                DisplayFormat = PivotGridStrings.XPG_Data_AmountOfSale_DisplayFormat,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "NumberOfUnits",
                DisplayName = PivotGridStrings.XPG_Data_NumberOfUnits_DisplayName,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "UnitPrice",
                DisplayName = PivotGridStrings.XPG_Data_UnitPrice_DisplayName,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Value",
                DisplayName = PivotGridStrings.XPG_Data_Value_DisplayName,
                DimensionType = DimensionType.Measure
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Product",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Product
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "City",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_City
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Seller",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Seller
            });
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName = "Date",
                DisplayName = PivotGridStrings.XPG_Data_PropertyNames_Date
            });

            return cubeMetadata;
        }

        #endregion HelperMethods

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.Processing ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
