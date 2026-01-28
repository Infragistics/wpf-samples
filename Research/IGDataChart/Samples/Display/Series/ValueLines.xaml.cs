using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples;
using Infragistics.Samples.Shared.Models;


namespace IGDataChart.Samples.Display.Series
{
    public partial class ValueLines : Infragistics.Samples.Framework.SampleContainer
    {
        public WeatherData Data { get; set; }

        public ValueLines()
        {
            InitializeComponent();
            this.Data = new WeatherData();
            this.DataContext = this;
        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string content = rb.Content.ToString();

            ValueLayerValueMode mode = GetValueLayerEnum(content);

            ValueLayer val = chart.Series.Where(s => s is ValueLayer).FirstOrDefault() as ValueLayer;
            val.ValueMode = mode;
            val.RenderSeries(false);
        }

        //private void RadioButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    CheckBox cb = sender as CheckBox;
        //    string content = cb.Content.ToString();

        //    ValueLayerValueMode mode = GetValueLayerEnum(content);

        //    ValueLayer layer = chart.Series.Where(s => s is ValueLayer && ((ValueLayer)s).ValueMode == mode).FirstOrDefault() as ValueLayer;
        //    layer.ValueMode = mode;
        //}

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
