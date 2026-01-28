using System.Data;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FieldWidthAndHeight.xaml
    /// </summary>
    public partial class FieldWidthAndHeight : SampleContainer
    {
        private DataView _data;
        private bool _fieldsInitialized;

        public FieldWidthAndHeight()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this._data = this.InitializeData();
            this.XamDataGrid1.DataSource = this._data;

            // Load up the Fields comboxbox with one entry for each Field in our DataSource.
            if (this._fieldsInitialized == false)
            {
                foreach (Field field in this.XamDataGrid1.DefaultFieldLayout.Fields)
                {
                    this.cboFields.Items.Add(field);
                }
                this.cboFields.SelectedIndex = 0;

                this._fieldsInitialized = true;
            }
        }

        private DataView InitializeData()
        {
            DataTable dt = new DataTable("AdvertisingOptions");

            dt.Columns.Add(new DataColumn("Publication", typeof(string)));
            dt.Columns.Add(new DataColumn("Circulation", typeof(string)));
            dt.Columns.Add(new DataColumn("AdRate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CostPerThousand", typeof(decimal)));

            dt.Rows.Add(new object[] { "Moonwriting", 6500000000D, 123500000D, 19D });
            dt.Rows.Add(new object[] { "India Monthly", 900000000D, 909000D, 1.01D });
            dt.Rows.Add(new object[] { "USA Yesterday", 151000000D, 2447710D, 16.21D });
            dt.Rows.Add(new object[] { "Das neue Objectiv", 75900000D, 1687257D, 22.23D });
            dt.Rows.Add(new object[] { "Osteichthyes & Fauna", 14300000D, 1430286D, 100.02D });
            dt.Rows.Add(new object[] { "The Times Square Times", 7000300D, 3807113.155D, 543.85D });
            dt.Rows.Add(new object[] { "Women’s Gourmet Daily", 1310000D, 1354867.50D, 1034.25D });
            dt.Rows.Add(new object[] { "New England Student Life", 988000D, 253669D, 256.75D });
            dt.Rows.Add(new object[] { "Independence Sentinel Post", 122000D, 4094.32D, 33.56D });
            dt.Rows.Add(new object[] { "Antique Furniture Illustrated", 101160D, 1997.91D, 19.75D });
            dt.Rows.Add(new object[] { "Wastewater Engineers Weekly", 88800D, 732.60D, 8.25D });
            dt.Rows.Add(new object[] { "Appleton Community Observer", 41900D, 20.53D, 0.49D });
            dt.Rows.Add(new object[] { "Database Administrators Journal", 19991D, 436.80D, 21.85D });
            dt.Rows.Add(new object[] { "Fantasy Football Scouting Annual", 7500D, 942.60D, 125.68D });
            dt.Rows.Add(new object[] { "The Sugar Bull Financial Newsletter", 1000D, 2048.32D, 2048.32D });
            dt.Rows.Add(new object[] { "Numerological Association Bulletin", 999D, 11488.50D, 11500D });
            dt.Rows.Add(new object[] { "Ruby On Rails Programmers Journal", 128D, 1663.36D, 12995D });
            dt.Rows.Add(new object[] { "Northeast High School Football Watch", 87D, 9396D, 108000D });
            dt.Rows.Add(new object[] { "Int'l Society of Dermatoglyphics Journal", 14D, 3584D, 256000D });
            dt.Rows.Add(new object[] { "Winchester-on-the-Severn World Monitor", 6D, 6030.15D, 1005025D });
            dt.Rows.Add(new object[] { "My Imaginary Friends Network Newspaper", 1D, 2500.00D, 2500000.00D });
            dt.Rows.Add(new object[] { "Correspondence on the 3/5ths Compromise ", 0.6D, 199.95D, 333250.00D });
            dt.Rows.Add(new object[] { "Esperanto Literary Digest of Autobiographies", 0.11D, 19.75D, 179545.45D });
            dt.Rows.Add(new object[] { "One-in-One Hundred Who's Who in Field Hockey", 0.01D, 1.21D, 121000.00D });
            dt.Rows.Add(new object[] { "Journal of Nanotechnology for Microscopic Scientists", 0.001D, 0.01D, 10000.00D });

            return dt.DefaultView;
        }

        private void ApplyWidthToField()
        {
            Field field = this.cboFields.SelectedItem as Field;
            if (field != null)
            {
                if (this.cboFieldLengthUnitTypes.Value == null)
                {
                    field.Width = null;
                    return;
                }


                FieldLengthUnitType selectedFieldLengthUnitType = (FieldLengthUnitType)this.cboFieldLengthUnitTypes.Value;
                if (selectedFieldLengthUnitType == FieldLengthUnitType.Auto)
                    field.Width = FieldLength.Auto;
                else
                    if (selectedFieldLengthUnitType == FieldLengthUnitType.InitialAuto)
                        field.Width = FieldLength.InitialAuto;
                    else
                        field.Width = new FieldLength((double)(txtWidthValue.Value ?? (double)0), selectedFieldLengthUnitType);
            }
        }

        private void InitializeUIWithSelectedFieldWidthData()
        {
            Field field = this.cboFields.SelectedItem as Field;
            if (field != null)
            {
                if (field.Width.HasValue)
                {
                    this.txtWidthValue.Value = field.Width.Value.Value;
                    this.cboFieldLengthUnitTypes.Value = field.Width.Value.UnitType;
                }
                else
                {
                    this.txtWidthValue.Value = 0;
                    this.cboFieldLengthUnitTypes.Value = null;
                }
            }
        }

        private void cboFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.InitializeUIWithSelectedFieldWidthData();
        }

        private void txtWidthValue_EditModeEnded(object sender, Infragistics.Windows.Editors.Events.EditModeEndedEventArgs e)
        {
            this.ApplyWidthToField();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Field field = this.cboFields.SelectedItem as Field;
            if (field != null)
            {
                field.Width = null;
                this.InitializeUIWithSelectedFieldWidthData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = null;
            this._data = this.InitializeData();
            this.XamDataGrid1.DataSource = this._data;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Field field = this.cboFields.SelectedItem as Field;
            if (field != null)
                field.Settings.ClearValue(FieldSettings.CellMinWidthProperty);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Field field = this.cboFields.SelectedItem as Field;
            if (field != null)
                field.Settings.ClearValue(FieldSettings.CellMaxWidthProperty);
        }

        private void cboFieldLengthUnitTypes_SelectionChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.cboFieldLengthUnitTypes.Value == null)
                return;

            FieldLengthUnitType selectedFieldLengthUnitType = (FieldLengthUnitType)this.cboFieldLengthUnitTypes.Value;
            switch (selectedFieldLengthUnitType)
            {
                case FieldLengthUnitType.Auto:
                case FieldLengthUnitType.InitialAuto:
                    this.txtWidthValue.IsEnabled = false;
                    break;
                default:
                    this.txtWidthValue.IsEnabled = true;
                    break;
            }

            this.ApplyWidthToField();
        }
    }
}
