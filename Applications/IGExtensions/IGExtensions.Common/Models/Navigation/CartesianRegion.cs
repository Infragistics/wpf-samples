using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class CartesianRegion : ObservableModel
    {
        public CartesianRegion() : this(0, 0, 1, 1)
        { }
        public CartesianRegion(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        public CartesianRegion(Rect rect)
            : this(rect.X, rect.Y, rect.Width, rect.Height)
        { }
        public CartesianRegion(Point position, Size size)
            : this(position.X, position.Y, size.Width, size.Height)
        { }
        public CartesianRegion(Point position, double width, double height)
            : this(position.X, position.Y, width, height)
        { }

        public Rect ToRect()
        {
            return new Rect(X, Y, Width, Height);
        }
        public new string ToString()
        {
            return ToRect().ToString();
        }
        public void Update(double x, double y, double w, double h)
        {
            _x = x;
            _y = y;
            _width = w;
            _height = h;
            OnPropertyChanged("CenterX"); OnPropertyChanged("CenterY");
            OnPropertyChanged("X"); OnPropertyChanged("Y");
            OnPropertyChanged("Width"); OnPropertyChanged("Height");
        }
        public void Update(Rect rect)
        {
            Update(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void Update(Point position, Size size)
        {
            Update(position.X, position.Y, size.Width, size.Height);
        }
        public void Update(Point position, double width, double height)
        {
            Update(position.X, position.Y, width, height);
        }
        private void UpdateCenter()
        {
            var rect = this.ToRect();
            if (rect.IsValid())
            {
                _centerX = rect.Center().X;
                _centerY = rect.Center().Y;
            }
            OnPropertyChanged("CenterX"); OnPropertyChanged("CenterY");
            //UpdateCenter(_centerX, _centerY);
        }
        private void UpdatePosition()
        {
            UpdatePosition(_centerX, _centerY);
      
        }
        private void UpdatePosition(double centerX, double centerY)
        {
            //_center = newCenter;
            _x = centerX - (this.Width / 2);
            _y = centerY - (this.Width / 2);
            OnPropertyChanged("X"); OnPropertyChanged("Y");
        }
        private void UpdatePosition(CartesianPoint newCenter)
        {
            //_center = newCenter;
            UpdatePosition(newCenter.X, newCenter.Y);
        }
        private void UpdatePosition(Point newCenter)
        {
            UpdatePosition(newCenter.X, newCenter.Y);
            //UpdateCenter(new CartesianPoint(newCenter));
        }
        #region Properties

        //private CartesianPoint _center = new CartesianPoint();
        ///// <summary>
        ///// Gets or sets Center property 
        ///// </summary>
        //public CartesianPoint Center
        //{
        //    get { UpdateCenter(); return _center; }
        //    set { if (_center == value) return; UpdateCenter(value); OnPropertyChanged("Center"); }
        //}

        private double _centerX;
        /// <summary>
        /// Gets or sets Center X property 
        /// </summary>
        public double CenterX
        {
            get { return _centerX; }
            set { if (_centerX == value) return; _centerX = value; UpdatePosition(); OnPropertyChanged("CenterX"); }
        }
        private double _centerY;
        /// <summary>
        /// Gets or sets Center Y property 
        /// </summary>
        public double CenterY
        {
            get { return _centerY; }
            set { if (_centerY == value) return; _centerY = value; UpdatePosition(); OnPropertyChanged("CenterY"); }
        }

        private double _x;
        /// <summary>
        /// Gets or sets X property 
        /// </summary>
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; UpdateCenter(); OnPropertyChanged("X"); }
        }
        private double _y;
        /// <summary>
        /// Gets or sets Y property 
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; UpdateCenter(); OnPropertyChanged("Y"); }
        }
        private double _width;
        /// <summary>
        /// Gets or sets Width property 
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { if (_width == value) return; _width = value; UpdateCenter(); OnPropertyChanged("Width"); }
        }
        private double _height;
        /// <summary>
        /// Gets or sets Height property 
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { if (_height == value) return; _height = value; UpdateCenter(); OnPropertyChanged("Height"); }
        }
        #endregion

    }
  
 
}