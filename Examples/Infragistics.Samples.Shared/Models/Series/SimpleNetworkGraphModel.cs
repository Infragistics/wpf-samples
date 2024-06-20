using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Infragistics.Samples.Shared.Models
{
    public class SimpleNetworkNode : ObservableModel
    {
        private double _x;
        public double X
        {
            get { return _x; }
            set
            {
                if (_x == value) return;
                _x = value;
                OnPropertyChanged("X");
            }
        }
        private double _y;
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y == value) return;
                _y = value;
                OnPropertyChanged("Y");
            }
        }
        private double _size;
        public double Size
        {
            get { return _size; }
            set
            {
                if (_size == value) return;
                _size = value;
                OnPropertyChanged("Size");
            }
        }
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged("Label");
            }
        }
        private List<List<Point>> _points;
        public List<List<Point>> Points
        {
            get { return _points; }
            set
            {
                if (_points == value) return;
                _points = value;
                OnPropertyChanged("Points");
            }
        }
    }
}
