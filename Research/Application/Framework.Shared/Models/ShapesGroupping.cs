using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows; 

namespace Infragistics.Framework
{
    public class ShapesGroupCollection : ObservableCollection<ShapesGroupping>
    {
        public ShapesGroupCollection()
        {
            this.CollectionChanged += OnCollectionChanged;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var index = 0;
            foreach (var item in this)
            {
                item.Index = index;
                index++;
            }
        } 
    }

    public class ShapesGroupping : DependencyObject, INotifyPropertyChanged 
    {
        public ShapesGroupping()
        {
            _shapesFilled = new List<List<Point>>();
            _shapesLines = new List<List<Point>>();

            _shapesPoints = new ObservableCollection<Point>();
        }

        /// <summary> Gets or sets Index used to for matching fill brush </summary>
        public int Index { get; set; }


        //NOTE: don't use ObservableCollection<> here 
        private List<List<Point>> _shapesFilled;
        /// <summary> Gets or sets ShapesFilled </summary>
        public List<List<Point>> ShapesFilled
        {
            get { return _shapesFilled;}
            set { if (_shapesFilled == value) return; _shapesFilled = value; OnPropertyChanged("ShapesFilled"); }
        }

        //NOTE: don't use ObservableCollection<> here 
        private List<List<Point>> _shapesLines;
        /// <summary> Gets or sets ShapesLines </summary>
        public List<List<Point>> ShapesLines
        {
            get { return _shapesLines;}
            set { if (_shapesLines == value) return; _shapesLines = value; OnPropertyChanged("ShapesLines"); }
        }
        
        private ObservableCollection<Point> _shapesPoints;
        /// <summary> Gets or sets ShapePoint </summary>
        public ObservableCollection<Point> ShapesPoints
        {
            get { return _shapesPoints;}
            set { if (_shapesPoints == value) return; _shapesPoints = value; OnPropertyChanged("ShapesPoints"); }
        }

        protected Random Rand = new Random();

        public void AddShapeAt(double x, double y, 
            double fillHeight = double.NaN, double fillWidth = double.NaN,
            double lineHeight = double.NaN)
        {

            this.ShapesPoints.Add(new Point(x, y));

            if (double.IsNaN(fillWidth)) fillWidth = Rand.Next(250, 300);
            if (double.IsNaN(fillHeight)) fillHeight = 75;
             
            var shape = new List<Point>();
            //TODO provide logic for creating custom shape by adding points that define contours of your shape
            //NOTE currently this code creates rectangle shape but you can add as many point as you need to create other shapes
            shape.Add(new Point(x, y));
            shape.Add(new Point(x, y - fillHeight));
            shape.Add(new Point(x + fillWidth, y - fillHeight));
            shape.Add(new Point(x + fillWidth, y));
             // NOTE first and last point should be the same so that shape is closed
            shape.Add(shape.Last());


            this.ShapesFilled.Add(shape);

            double lineTop, lineBottom;

            if (double.IsNaN(lineHeight))  
            {
                lineTop     = y + Rand.Next(30, 50);
                lineBottom  = y - Rand.Next(100, 125);
            }
            else
            {
                lineTop = y + (lineHeight/2.0);
                lineBottom = y - (lineHeight / 2.0);
            }
           
            var line = new List<Point>();
            line.Add(new Point(x, lineTop));
            line.Add(new Point(x, lineBottom));
            this.ShapesLines.Add(line);
             

            //OnPropertyChanged("ShapesPoints");
            
            OnPropertyChanged("ShapesLines");

            OnPropertyChanged("ShapesFilled");
        }

        /// <summary>
        /// Event raised whenever a property on this ShapefileRecord is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}