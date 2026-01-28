using Infragistics.Samples.Framework;
using System.Windows;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ControllingTabStopEnabledReadonly.xaml
    /// </summary>
    public partial class ControllingTabStopEnabledReadonly : SampleContainer
    {
        public ControllingTabStopEnabledReadonly()
        {
            InitializeComponent();
        }

        private void Btn_DisableSelected_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var selectedCell in dataGrid.RecordManager.DataPresenter.SelectedItems.Cells)
            {
                selectedCell.IsEnabled = false;
            }
        }
    }
}
