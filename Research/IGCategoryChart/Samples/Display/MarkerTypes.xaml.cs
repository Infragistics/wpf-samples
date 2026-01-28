using IGCategoryChart.Samples.ViewModels;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;
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

namespace IGCategoryChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for MarkerTypes.xaml
    /// </summary>
    public partial class MarkerTypes : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkerTypes()
        {
            InitializeComponent();
            chart1.ItemsSource = new EnergyProductionDataSample();
            
            List<MarkerTypeChoice> MarkerChoices = new List<MarkerTypeChoice>();
            MarkerChoices.Add(new MarkerTypeChoice(new string[1]{"None"}));
            MarkerChoices.Add(new MarkerTypeChoice(new string[1] { "Automatic" }));           
            MarkerChoices.Add(new MarkerTypeChoice(new string[1] { "Circle" }));
            MarkerChoices.Add(new MarkerTypeChoice(new string[3] { "Circle","Square","Triangle" }));
            MarkerChoices.Add(new MarkerTypeChoice(new string[4] { "Diamond", "Hexagon", "Pentagon", "Pyramid" }));
            MarkerChoices.Add(new MarkerTypeChoice(new string[6] { "Circle", "Diamond", "Hexagon", "Pentagon", "Pyramid", "Square" }));
            MarkerChoices.Add(new MarkerTypeChoice(new string[6] { "Square", "Tetragram", "Triangle", "Circle", "Pentagon", "Diamond" }));

            markerTypesCombo.ItemsSource = MarkerChoices;           
            markerTypesCombo.SelectedIndex = 5;
            
        }

        private void MarkerTypes_Loaded(object sender, RoutedEventArgs e)
        {
            MarkerTypeCollection markerTypeCollection = new MarkerTypeCollection();

            MarkerType diamondMarker = MarkerType.Diamond;
            MarkerType circleMarker = MarkerType.Circle;
            MarkerType squareMarker = MarkerType.Square;
            MarkerType hexMarker = MarkerType.Hexagon;
            MarkerType pyramidMarker = MarkerType.Pyramid;

            markerTypeCollection.Add(diamondMarker);
            markerTypeCollection.Add(circleMarker);
            markerTypeCollection.Add(squareMarker);
            markerTypeCollection.Add(hexMarker);
            markerTypeCollection.Add(pyramidMarker);

            chart1.MarkerTypes = markerTypeCollection;

            
        }

        private void OnMarkerTypesChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var sel = e.NewValue;
            //chart1.MarkerTypes = 
        }


    }

    public class MarkerTypeChoice
    {
        public string Description { get; set; }
        public MarkerTypeCollection MarkerTypes { get; set; }
        public MarkerTypeChoice(string[] markers)
        {
            MarkerTypes = new MarkerTypeCollection();
            
            foreach(var s in markers)
            {
                switch(s)
                {
                    case "Automatic":
                        MarkerTypes.Add(MarkerType.Automatic);
                        AddDescription(s);
                        break;
                    case "Circle":
                        MarkerTypes.Add(MarkerType.Circle);
                        AddDescription(s);
                        break;
                    case "Diamond":
                        MarkerTypes.Add(MarkerType.Diamond);
                        AddDescription(s);
                        break;
                    case "Hexagon":
                        MarkerTypes.Add(MarkerType.Hexagon);
                        AddDescription(s);
                        break;
                    case "Hexagram":
                        MarkerTypes.Add(MarkerType.Hexagram);
                        AddDescription(s);
                        break;
                    case "None":
                        MarkerTypes.Add(MarkerType.None);
                        AddDescription(s);
                        break;
                    case "Pentagon":
                        MarkerTypes.Add(MarkerType.Pentagon);
                        AddDescription(s);
                        break;
                    case "Pyramid":
                        MarkerTypes.Add(MarkerType.Pyramid);
                        AddDescription(s);
                        break;
                    case "Square":
                        MarkerTypes.Add(MarkerType.Square);
                        AddDescription(s);
                        break;
                    case "Tetragram":
                        MarkerTypes.Add(MarkerType.Tetragram);
                        AddDescription(s);
                        break;
                    case "Triangle":
                        MarkerTypes.Add(MarkerType.Triangle);
                        AddDescription(s);
                        break;
                   
                }
                
            }
        }
        private void AddDescription(string s)
        {
            if (Description != null && Description.Length >0)
                Description = Description + ", ";
            Description = Description + s;
        }
    }
}
