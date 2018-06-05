using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class CartesianPoint : ObservableModel
    {
        public CartesianPoint()
        {
            X = 0;
            Y = 0;
        }
        public CartesianPoint(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        #region Properties

        private double _x;
        /// <summary>
        /// Gets or sets X property 
        /// </summary>
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }
        private double _y;
        /// <summary>
        /// Gets or sets Y property 
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }
        #endregion

        public Point ToPoint()
        {
            return new Point(X, Y);
        }
        public new string ToString()
        {
            return ToPoint().ToString();
        }
    }
   
}