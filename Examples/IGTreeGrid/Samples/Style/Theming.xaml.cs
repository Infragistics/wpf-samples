using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;
using System.Windows.Controls;

namespace IGTreeGrid.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        private void OnInitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            if (e.Record is DataRecord)
            {
                var dr = (DataRecord)e.Record;
                if (!bool.Parse(dr.Cells["IsFolder"].Value.ToString()))
                {
                    object obj = dr.Cells["FileSize"].Value;
                    dr.Cells["calcSize"].Value = obj;
                }
            }
        }
    }
}
