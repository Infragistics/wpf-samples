using Infragistics.Controls.Charts;
using System;
using System.Windows.Controls;

namespace IGCategoryChart.Samples
{ 
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
