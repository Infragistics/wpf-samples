using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using IGDataChart.Samples.Helpers;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Styling
{
    public partial class LegendStyling : SampleContainer
    {  
        public LegendStyling()
        {
            InitializeComponent();

            //Hookup events
            titleForegroundCmbx.SelectionChanged += titleForegroundCmbx_SelectionChanged;
            titleFontSizeSlider.ValueChanged += titleFontSizeSlider_ValueChanged;
            titleFontFamilyCmBx.SelectionChanged += titleFontFamilyCmBx_SelectionChanged;
            titleFontStretchCmBx.SelectionChanged += titleFontStretchCmBx_SelectionChanged;
            itemForegroundCmbx.SelectionChanged += itemForegroundCmbx_SelectionChanged;
            itemFontSizeSlider.ValueChanged += itemFontSizeSlider_ValueChanged;
            itemFontFamilyCmBx.SelectionChanged += itemFontFamilyCmBx_SelectionChanged;
            itemFontStretchCmBx.SelectionChanged += itemFontStretchCmBx_SelectionChanged;
            itemFontWeightCmBx.SelectionChanged += itemFontWeightCmBx_SelectionChanged;
            titleFontWeightCmBx.SelectionChanged += titleFontWeightCmBx_SelectionChanged;

            //Select default values for all ComboBoxes
            titleFontFamilyCmBx.SelectedIndex = 0;
            titleForegroundCmbx.SelectedIndex = 0;
            titleFontStretchCmBx.SelectedIndex = 0;
            itemFontFamilyCmBx.SelectedIndex = 0;
            itemForegroundCmbx.SelectedIndex = 0;
            itemFontStretchCmBx.SelectedIndex = 0;
            itemFontWeightCmBx.SelectedIndex = 0;
            titleFontWeightCmBx.SelectedIndex = 0;
        }

        //FontWeight Events
        void titleFontWeightCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((titleFontWeightCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Bold":
                    {
                        TitleLegend.TitleFontWeight = FontWeights.Bold;
                        ItemLegend.TitleFontWeight = FontWeights.Bold;
                        ScaleLegend.TitleFontWeight = FontWeights.Bold;
                        break;
                    }
                case "Regular":
                    {
                        TitleLegend.TitleFontWeight = FontWeights.Normal;
                        ItemLegend.TitleFontWeight = FontWeights.Normal;
                        ScaleLegend.TitleFontWeight = FontWeights.Normal;
                        break;
                    }
                default:
                    break;
            }
        }
        void itemFontWeightCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((itemFontWeightCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Bold":
                    {
                        TitleLegend.ItemsFontWeight = FontWeights.Bold;
                        ItemLegend.ItemsFontWeight = FontWeights.Bold;
                        ScaleLegend.ItemsFontWeight = FontWeights.Bold;
                        break;
                    }
                case "Regular":
                    {
                        TitleLegend.ItemsFontWeight = FontWeights.Normal;
                        ItemLegend.ItemsFontWeight = FontWeights.Normal;
                        ScaleLegend.ItemsFontWeight = FontWeights.Normal;
                        break;
                    }
                default:
                    break;
            }
        }

        //Font Foreground Brush Events
        void itemForegroundCmbx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ItemColorConvert(itemForegroundCmbx.SelectedValue.ToString());
        }
        void titleForegroundCmbx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TitleColorConvert(titleForegroundCmbx.SelectedValue.ToString());
        }

        //Font Size Events
        void itemFontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TitleLegend.ItemsFontSize = itemFontSizeSlider.Value;
            ScaleLegend.ItemsFontSize = itemFontSizeSlider.Value;
            ItemLegend.ItemsFontSize = itemFontSizeSlider.Value;
        }
        void titleFontSizeSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            TitleLegend.TitleFontSize = titleFontSizeSlider.Value;
            ScaleLegend.TitleFontSize = titleFontSizeSlider.Value;
            ItemLegend.TitleFontSize = titleFontSizeSlider.Value;
        }

        //FontFamily Events
        void itemFontFamilyCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((itemFontFamilyCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Arial":
                    {
                        setLegendItemsFontFamily(new FontFamily("Arial"));
                        break;
                    }
                case "Calibri":
                    {
                        setLegendItemsFontFamily(new FontFamily("Calibri"));
                        break;
                    }
                case "Consolas":
                    {
                        setLegendItemsFontFamily(new FontFamily("Consolas"));
                        break;
                    }
                case "Courier New":
                    {
                        setLegendItemsFontFamily(new FontFamily("Courier New"));
                        break;
                    }
                case "Tahoma":
                    {
                        setLegendItemsFontFamily(new FontFamily("Tahoma"));
                        break;
                    }
                case "Times New Roman":
                    {
                        setLegendItemsFontFamily(new FontFamily("Times New Roman"));
                        break;
                    }
                case "Verdana":
                    {
                        setLegendItemsFontFamily(new FontFamily("Verdana"));
                        break;
                    }
                default:
                    break;
            }
        }
        void titleFontFamilyCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((titleFontFamilyCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Arial":
                    {
                        setLegendTitleFontFamily(new FontFamily("Arial"));
                        break;
                    }
                case "Calibri":
                    {
                        setLegendTitleFontFamily(new FontFamily("Calibri"));
                        break;
                    }
                case "Consolas":
                    {
                        setLegendTitleFontFamily(new FontFamily("Consolas"));
                        break;
                    }
                case "Courier New":
                    {
                        setLegendTitleFontFamily(new FontFamily("Courier New"));
                        break;
                    }
                case "Tahoma":
                    {
                        setLegendTitleFontFamily(new FontFamily("Tahoma"));
                        break;
                    }
                case "Times New Roman":
                    {
                        setLegendTitleFontFamily(new FontFamily("Times New Roman"));
                        break;
                    }
                case "Verdana":
                    {
                        setLegendTitleFontFamily(new FontFamily("Verdana"));
                        break;
                    }
                default:
                    break;
            }
        }

        //FontFamily Methods
        void setLegendTitleFontFamily(FontFamily font)
        {
            TitleLegend.TitleFontFamily = font;
            ScaleLegend.TitleFontFamily = font;
            ItemLegend.TitleFontFamily = font;
        }
        void setLegendItemsFontFamily(FontFamily font)
        {
            TitleLegend.ItemsFontFamily = font;
            ScaleLegend.ItemsFontFamily = font;
            ItemLegend.ItemsFontFamily = font;
        }
         
        //Legend Stretch Events
        void titleFontStretchCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((titleFontStretchCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Normal": 
                    {
                        setTitleLegendStretch(FontStretches.Normal);
                        break;
                    }
                case "Condensed":
                    {
                        setTitleLegendStretch(FontStretches.Condensed);
                        break;
                    }
                default:
                    break;
            }
        }
        void itemFontStretchCmBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch ((itemFontStretchCmBx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Normal":
                    {
                        setItemLegendStretch(FontStretches.Normal);
                        break;
                    }
                case "Condensed":
                    {
                        setItemLegendStretch(FontStretches.Condensed);
                        break;
                    }
                default:
                    break;
            }
        }

        //Legend Streth Methods
        void setTitleLegendStretch(FontStretch fontStretch)
        {
            TitleLegend.TitleFontStretch = fontStretch;
            ScaleLegend.TitleFontStretch = fontStretch;
            ItemLegend.TitleFontStretch = fontStretch;
        }
        void setItemLegendStretch(FontStretch fontStretch)
        {
            TitleLegend.ItemsFontStretch = fontStretch;
            ScaleLegend.ItemsFontStretch = fontStretch;
            ItemLegend.ItemsFontStretch = fontStretch;
        }

        //Legend Foreground Brush Converters
        SolidColorBrush TitleColorConvert(string Color)
        { 
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);

            switch ((titleForegroundCmbx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Black":
                    {
                        brush = new SolidColorBrush(Colors.Black);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Blue":
                    {
                        brush = new SolidColorBrush(Colors.Blue);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Magenta":
                    {
                        brush = new SolidColorBrush(Colors.Magenta);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Brown":
                    {
                        brush = new SolidColorBrush(Colors.Brown);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Orange":
                    {
                        brush = new SolidColorBrush(Colors.Orange);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Green":
                    {
                        brush = new SolidColorBrush(Colors.Green);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Purple":
                    {
                        brush = new SolidColorBrush(Colors.Purple);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Red":
                    {
                        brush = new SolidColorBrush(Colors.Red);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "Yellow":
                    {
                        brush = new SolidColorBrush(Colors.Yellow);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "White":
                    {
                        brush = new SolidColorBrush(Colors.White);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "LightGray":
                    {
                        brush = new SolidColorBrush(Colors.LightGray);
                        setLegendTitleBrush(brush);
                        break;
                    }
                case "DarkGray":
                    {
                        brush = new SolidColorBrush(Colors.DarkGray);
                        setLegendTitleBrush(brush);
                        break;
                    }
                default:
                    break;
            }
            return brush;
        }
        SolidColorBrush ItemColorConvert(string Color)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);

            switch ((itemForegroundCmbx.SelectedItem as System.Windows.Controls.ListBoxItem).Content.ToString())
            {
                case "Black":
                    {
                        brush = new SolidColorBrush(Colors.Black);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Blue":
                    {
                        brush = new SolidColorBrush(Colors.Blue);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Magenta":
                    {
                        brush = new SolidColorBrush(Colors.Magenta);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Brown":
                    {
                        brush = new SolidColorBrush(Colors.Brown);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Orange":
                    {
                        brush = new SolidColorBrush(Colors.Orange);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Green":
                    {
                        brush = new SolidColorBrush(Colors.Green);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Purple":
                    {
                        brush = new SolidColorBrush(Colors.Purple);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Red":
                    {
                        brush = new SolidColorBrush(Colors.Red);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "Yellow":
                    {
                        brush = new SolidColorBrush(Colors.Yellow);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "White":
                    {
                        brush = new SolidColorBrush(Colors.White);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "LightGray":
                    {
                        brush = new SolidColorBrush(Colors.LightGray);
                        setLegendItemsBrush(brush);
                        break;
                    }
                case "DarkGray":
                    {
                        brush = new SolidColorBrush(Colors.DarkGray);
                        setLegendItemsBrush(brush);
                        break;
                    }
                default:
                    break;
            }
            return brush;
        }

        //Legend Foreground Brush Methods
        void setLegendTitleBrush(SolidColorBrush brush)
        {
            TitleLegend.TitleForeground = brush;
            ScaleLegend.TitleForeground = brush;
            ItemLegend.TitleForeground = brush;
        }
        void setLegendItemsBrush(SolidColorBrush brush)
        {
            TitleLegend.ItemsForeground = brush;
            ScaleLegend.ItemsForeground = brush;
            ItemLegend.ItemsForeground = brush;
        }
    }
}