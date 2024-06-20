using Infragistics.Controls.Charts;
using System.Windows;
using System.Windows.Media;

namespace IGPieChart.Samples.Display
{
    public partial class PieChartLabels : Infragistics.Samples.Framework.SampleContainer
    {
        public PieChartLabels()
        {
            InitializeComponent();
            this.labelsPositionCombo.ValueChanged += labelsPositionCombo_ValueChanged;
            this.labelsPositionCombo.Loaded += labelsPositionCombo_Loaded;
            innerLabelCmbx.SelectionChanged += labelInnerColor_SelectionChanged;
            outerLabelCmbx.SelectionChanged += labelOuterColor_SelectionChanged;
        }

        void labelsPositionCombo_Loaded(object sender, RoutedEventArgs e)
        {
            labelsPositionCombo.SelectedEnumIndex = 3;
        }

        void labelInnerColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ItemColorConvert(innerLabelCmbx.SelectedValue.ToString(), innerLabelCmbx.SelectedItem, innerLabelCmbx.Name);
        }

        void labelOuterColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ItemColorConvert(outerLabelCmbx.SelectedValue.ToString(), outerLabelCmbx.SelectedItem, outerLabelCmbx.Name);
        }

        void labelsPositionCombo_ValueChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            this.leaderLineOptions.Visibility = (LabelsPosition)e.NewValue == LabelsPosition.OutsideEnd ? Visibility.Visible : Visibility.Collapsed;
        }


        void ItemColorConvert(string Color, object SelectedItem, string Name)
        {
            switch ((SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Black":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Black), Name);
                        break;
                    }
                case "Blue":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Blue), Name);
                        break;
                    }
                case "Magenta":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Magenta), Name);
                        break;
                    }
                case "Brown":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Brown), Name);
                        break;
                    }
                case "Orange":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Orange), Name);
                        break;
                    }
                case "Green":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Green), Name);
                        break;
                    }
                case "Purple":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Purple), Name);
                        break;
                    }
                case "Red":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Red), Name);
                        break;
                    }
                case "Yellow":
                    {
                        setLabelColor(new SolidColorBrush(Colors.Yellow), Name);
                        break;
                    }
                case "White":
                    {
                        setLabelColor(new SolidColorBrush(Colors.White), Name);
                        break;
                    }
                case "LightGray":
                    {
                        setLabelColor(new SolidColorBrush(Colors.LightGray), Name);
                        break;
                    }
                case "DarkGray":
                    {
                        setLabelColor(new SolidColorBrush(Colors.DarkGray), Name);
                        break;
                    }
                default:
                    break;
            }
        }

        void setLabelColor(SolidColorBrush brush, string Name)
        {
            if (Name.Equals("innerLabelCmbx"))
            {
                pieChart.LabelInnerColor = brush;
            }
            else
                pieChart.LabelOuterColor = brush;
        }
    }
}

