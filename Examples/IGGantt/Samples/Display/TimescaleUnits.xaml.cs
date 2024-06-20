using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using IGGantt.Samples.DataSource;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;

namespace IGGantt.Samples.Display
{
    public partial class TimescaleUnits : SampleContainer
    {
        public TimescaleUnits()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            GetProjectData();
            FillUnitTypes();
        }

        private void GetProjectData()
        {
            DataContext = ProjectDataHelper.GenerateProjectData();
        }

        private void xamnuUnitCountOnValueChanged(object sender, EventArgs ea)
        {
            if (xamGantt == null)
            {
                return;
            }

            XamNumericEditor xamnu = sender as XamNumericEditor;
            Timescale ts = (xamGantt.TimescaleResolved as Timescale);

            if (ts.Bands.Count > 0 && xamnu.Value != null)
            {
                if (xamnu.Name == "xamnuUnitCountTop")
                {
                    ts.Bands[0].UnitCount = Math.Max(1, int.Parse(xamnu.Value.ToString()));
                }
                else if (xamnu.Name == "xamnuUnitCountMiddle")
                {
                    ts.Bands[1].UnitCount = Math.Max(1, int.Parse(xamnu.Value.ToString()));
                }
            }

            xamGantt.ViewSettings = new ProjectViewSettings();
            xamGantt.ViewSettings.Timescale = ts;
        }

        private void UnitTypesOnSelectionChanged(object sender, SelectionChangedEventArgs scea)
        {
            ComboBox cb = sender as ComboBox;
            TimescaleUnit tu = (TimescaleUnit)cb.SelectedIndex;
            if (cb.Name == "cbUnitTypesTop")
            {
                if (cb.SelectedIndex <= cbUnitTypesMiddle.SelectedIndex)
                {
                    ChangeUnitType(tu, 0);
                }
                else if (cbUnitTypesMiddle.SelectedIndex != -1)
                {
                    cb.SelectedIndex = cbUnitTypesMiddle.SelectedIndex;
                }
            }
            else if (cb.Name == "cbUnitTypesMiddle")
            {
                if (cb.SelectedIndex >= cbUnitTypesTop.SelectedIndex)
                {
                    ChangeUnitType(tu, 1);
                }
                else if(cbUnitTypesTop.SelectedIndex != -1)
                {
                    cb.SelectedIndex = cbUnitTypesTop.SelectedIndex;
                }
            }
        }

        private void ChangeUnitType(TimescaleUnit tu, int bandIndex)
        {
            ((Timescale)xamGantt.TimescaleResolved).Bands[bandIndex].Unit = tu;
        }

        private void FillUnitTypes()
        {
            List<string> unitTypes = new List<string>();
            foreach (TimescaleUnit tu in Enum.GetValues(typeof(TimescaleUnit)))
            {
                unitTypes.Add(tu.ToString());
            }

            cbUnitTypesTop.ItemsSource = unitTypes;
            cbUnitTypesMiddle.ItemsSource = unitTypes.ToArray();
            cbUnitTypesTop.SelectedIndex = 5;
            cbUnitTypesMiddle.SelectedIndex = 6;
        }
    }
}
