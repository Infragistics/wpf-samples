using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Controls.Gauges;

namespace IGRadialGauge.Samples.Editing
{
    public partial class GaugeEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeEvents()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void XamRadialGauge_AlignLabel(object sender, Infragistics.Controls.Gauges.AlignRadialGaugeLabelEventArgs args)
        {
            DisplayOutput("AlignLabel: " + args.Label.ToString());

        }

        private void XamRadialGauge_FormatLabel(object sender, Infragistics.Controls.Gauges.FormatRadialGaugeLabelEventArgs args)
        {
            DisplayOutput("FormatLabel: " + args.Label.ToString());
        }

        private void XamRadialGauge_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayOutput("SizeChanged: " + e.NewSize);
        }

        private void XamRadialGauge_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayOutput("Loaded");

        }

        private void XamRadialGauge_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplayOutput("MouseLeftButtonDown: " + e.GetPosition(sender as XamRadialGauge));
        }
        private void DisplayOutput(string content)
        {
            if (this.evtOutput != null)
            {
                this.evtOutput.Text += content + Environment.NewLine;
                this.svOutput.UpdateLayout();
                this.svOutput.ScrollToVerticalOffset(double.MaxValue);
            }
        }

        private void EraseOutputWindow()
        {
            this.evtOutput.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EraseOutputWindow();
        }

        private void XamRadialGauge_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DisplayOutput("MouseEnter");
        }

        private void XamRadialGauge_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DisplayOutput("MouseLeave");
        }

        private void XamRadialGauge_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplayOutput("MouseLeftButtonUp: " + e.GetPosition(sender as XamRadialGauge));
        }

        private void XamRadialGauge_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplayOutput("MouseRightButtonDown: " + e.GetPosition(sender as XamRadialGauge));
        }

        private void XamRadialGauge_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplayOutput("MouseRightButtonUp: " + e.GetPosition(sender as XamRadialGauge));
        }

        private void XamRadialGauge_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DisplayOutput("MouseMove: " + e.GetPosition(sender as XamRadialGauge));
        }

    }
}
