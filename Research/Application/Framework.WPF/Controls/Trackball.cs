using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Infragistics.Framework
{
    public class Trackball : Control
    {
        static Trackball()
        {
            //RotationDefault = (Rotation3D)XamScatterSurface3D.RotationProperty.DefaultMetadata.DefaultValue;
            //if (RotationDefault == null)
            //{
            //    var quaternion = new Quaternion(double.NaN, double.NaN, double.NaN, double.NaN);
            //    RotationDefault = new QuaternionRotation3D(quaternion);
            //}
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Trackball), 
                new FrameworkPropertyMetadata(typeof(Trackball)));
        }

        public event EventHandler RotationChanged;

        private void OnRotationChanged()
        {
            if (RotationChanged == null) return;

            RotationChanged(this, EventArgs.Empty);
        }
        private bool Trackballing { get; set; }
        private Vector3D TrackballStart { get; set; }
        private Quaternion Ball { get; set; }

        protected static Quaternion DefaultQuaternion = new Quaternion(double.NaN, double.NaN, double.NaN, double.NaN);
        protected static Rotation3D DefaultRotation = new QuaternionRotation3D(DefaultQuaternion);
        private const string RotationPropertyName = "Rotation";

        //public static readonly DependencyProperty RotationProperty = 
        //    DependencyProperty.Register(RotationPropertyName,
        //    typeof(Rotation3D), typeof(Trackball),
        //    new PropertyMetadata(DefaultRotation, (oo, ee) =>
        //{
        //    /* _ */
        //}));

        public static readonly DependencyProperty RotationProperty = 
            DependencyProperty.Register(RotationPropertyName, 
            typeof(Rotation3D), typeof(Trackball), 
            new PropertyMetadata(Rotation3D.Identity, (oo, ee) =>
        {
            /* _ */
        }));

        public Rotation3D Rotation
        {
            get
            {
                return this.GetValue(RotationProperty) as Rotation3D;
            }
            set
            {
                this.SetValue(RotationProperty, value);
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!e.Handled)
            {
                Point pt = e.GetPosition(this);
                double x = (2.0 * pt.X - this.ActualWidth) / this.ActualWidth;
                double y = -(2.0 * pt.Y - this.ActualHeight) / this.ActualHeight;
                double s = x * x + y * y;

                Trackballing = s <= 1.0 && CaptureMouse();

                if (Trackballing)
                {
                    double z = System.Math.Sqrt(1.0 - s);
                    double m = Hypot(x, y, z);

                    TrackballStart = new Vector3D(x / m, y / m, z / m);
                    e.Handled = true;
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Trackballing)
            {
                Point pt = e.GetPosition(this);
                double x = (2.0 * pt.X - this.ActualWidth) / this.ActualWidth;
                double y = -(2.0 * pt.Y - this.ActualHeight) / this.ActualHeight;
                double s = x * x + y * y;

                if (s <= 1.0)
                {
                    var z = System.Math.Sqrt(1.0 - s);
                    var m = Hypot(x, y, z);
                    var p1 = new Vector3D(x / m, y / m, z / m);

                    var angle = System.Math.Acos(Clamp(Vector3D.DotProduct(TrackballStart, p1), -1.0, 1.0));
                    var axis = Vector3D.CrossProduct(TrackballStart, p1);

                    if (axis.Length > 0.0)
                    {
                        axis.Normalize();

                        Ball = new Quaternion(axis, 180.0 * angle / System.Math.PI) * Ball;
                        Rotation = new QuaternionRotation3D(Ball);
                        OnRotationChanged();
                    }

                    TrackballStart = p1;
                }
            }

            base.OnMouseMove(e);
        }
        public static double Clamp(double value, double minimum, double maximum)
        {
            return System.Math.Min(maximum, System.Math.Max(minimum, value));
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (Trackballing)
            {
                Trackballing = false;
                ReleaseMouseCapture();
                e.Handled = true;
            }
           
            base.OnMouseLeftButtonUp(e);
        }
        private static double Hypot(params double[] a)
        {
            var m = 0.0;

            foreach (var d in a)
            {
                m += d * d;
            }

            return m;
        }
    }
}
