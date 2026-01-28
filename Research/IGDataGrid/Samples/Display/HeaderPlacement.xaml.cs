using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for HeaderPlacement.xaml
    /// </summary>
    public partial class HeaderPlacement : SampleContainer
    {
        public HeaderPlacement()
        {
            InitializeComponent();

            Type[] enumTypes = new Type[]
            {
                typeof( Infragistics.Windows.DataPresenter.HeaderPlacement ),
                typeof( HeaderPlacementInGroupBy ),
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));
        }

        void Page_Loaded(object o, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);

            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
