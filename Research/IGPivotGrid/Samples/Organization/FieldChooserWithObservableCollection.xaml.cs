using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGPivotGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Organization
{
    public partial class FieldChooserWithObservableCollection : SampleContainer
    {
        public FieldChooserWithObservableCollection()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            FieldChooser chooser = pivotGrid.FieldChooser;
            chooser.FieldUniqueNameMemberPath = "UniqueName";
            chooser.DisplayMemberPath = "Caption";

            chooser.ColumnsGroupHeader = new TextBlock() { Text = PivotGridStrings.XPG_Columns, FontWeight = FontWeights.Bold };
            chooser.RowsGroupHeader = new TextBlock() { Text = PivotGridStrings.XPG_Rows, FontWeight = FontWeights.Bold };
            chooser.FiltersGroupHeader = new TextBlock() { Text = PivotGridStrings.XPG_Filters, FontWeight = FontWeights.Bold };
            chooser.MeasuresGroupHeader = new TextBlock() { Text = PivotGridStrings.XPG_Measures, FontWeight = FontWeights.Bold };

            chooser.ColumnsItemsSource = GetHierarchiesCollection(new string[] { "[Product]", "[Quarter]" });
            chooser.RowsItemsSource = GetHierarchiesCollection(new string[] { "[City]", "[Seller]" });
            chooser.FiltersItemsSource = GetHierarchiesCollection(new string[] { "[Date]" });
            chooser.MeasuresItemsSource = GetMeasuresCollection(new string[] { "AmountOfSale", "NumberOfUnits", "UnitPrice", "Value" });

            chooser.IsOpen = true;
        }

        /// <summary>
        /// Creates and returns an ObservableCollection of all hierarchies that are from the specified dimensions.
        /// </summary>
        private ObservableCollection<IHierarchy> GetHierarchiesCollection(string[] dimensionUniqueNames)
        {
            ObservableCollection<IHierarchy> outputCollection = new ObservableCollection<IHierarchy>();
            foreach (var dimension in (pivotGrid.DataSource.Cube.Dimensions))
            {
                if (dimensionUniqueNames.Contains(dimension.UniqueName))
                    outputCollection.Add(dimension.Hierarchies[0]);
            }
            return outputCollection;
        }

        /// <summary>
        /// Creates and returns an ObservableCollection of all measures in the data source that have the specified unique names.
        /// </summary>
        private ObservableCollection<IMeasure> GetMeasuresCollection(string[] uniqueNames)
        {
            ObservableCollection<IMeasure> outputCollection = new ObservableCollection<IMeasure>();
            foreach (var measure in (pivotGrid.DataSource.Cube.Measures))
            {
                outputCollection.Add(measure);
            }
            return outputCollection;
        }
    }
}
