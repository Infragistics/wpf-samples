using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGFinancialChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for DataTooltip.xaml
    /// </summary>
    public partial class ValueLines : Infragistics.Samples.Framework.SampleContainer
    {
        public ValueLines()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string content = rb.Content.ToString();
            Chart.ValueLines.Clear();
            Chart.ValueLines.Add(GetValueLayerEnum(content));
        }

        public ValueLayerValueMode GetValueLayerEnum(string content)
        {
            switch (content)
            {
                case "Auto":
                    {
                        return ValueLayerValueMode.Auto;
                    }
                case "Average":
                    {
                        return ValueLayerValueMode.Average;
                    }
                case "Global Average":
                    {
                        return ValueLayerValueMode.GlobalAverage;
                    }
                case "Global Maximum":
                    {
                        return ValueLayerValueMode.GlobalMaximum;
                    }
                case "Global Minimum":
                    {
                        return ValueLayerValueMode.GlobalMinimum;
                    }
                case "Maximum":
                    {
                        return ValueLayerValueMode.Maximum;
                    }
                case "Minimum":
                    {
                        return ValueLayerValueMode.Minimum;
                    }
            }

            return ValueLayerValueMode.Auto;
        }
    }
}
