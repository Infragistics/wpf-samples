using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Effects
{
    public class XamHollywoodEffect : XamEffectBase
    {
        public XamHollywoodEffect()
        {
            //CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1.0 / 30.0) };
            dispatcherTimer.Tick += (o, e) => CompositionTarget_Rendering(o, e);
            dispatcherTimer.Start();

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(Spot0PositionProperty);
            UpdateShaderValue(Spot0DirectionProperty);
            UpdateShaderValue(Spot1PositionProperty);
            UpdateShaderValue(Spot1DirectionProperty);

            Spot0Position = SpotPosition(-0.2, 1.2, 0.5);
            Spot1Position = SpotPosition(1.2, 1.2, 0.3);
        }

        #region Input Property
        public Brush Input
        {
            get
            {
                return (Brush)GetValue(InputProperty);
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamHollywoodEffect), 0);
        #endregion

        #region Autorun Property
        public bool Autorun
        {
            get { return autorun; }
            set
            {
                autorun = value;
            }
        }
        private bool autorun=true;
        #endregion

        #region Hotspot Property
        public Point Hotspot
        {
            get { return hotspot; }
            set
            {
                hotspot = value;
            }
        }
        private Point hotspot;

        DateTime then = DateTime.MinValue;
        Point V0 = new Point(0, 0);
        Point P0 = new Point(0, 0);
        Point V1 = new Point(0, 0);
        Point P1 = new Point(0, 0);

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            if (then != DateTime.MinValue)
            {
                if (Autorun)
                {
                    double z=now.TimeOfDay.TotalSeconds;

                    P0 = new Point(0.5 + MathTool.Noise(0.1, 0.2, z), 0.5 + MathTool.Noise(0.3, 0.4, z));
                    P1 = new Point(0.5 + MathTool.Noise(0.5, 0.6, z), 0.5 + MathTool.Noise(0.7, 0.8, z));
                }
                else
                {
                    double dt = (now - then).TotalSeconds;

                    Point A0 = new Point((hotspot.X - P0.X) * 30, (hotspot.Y - P0.Y) * 30);
                    V0 = new Point(V0.X * 0.9 + A0.X * dt, V0.Y * 0.9 + A0.Y * dt);
                    P0 = new Point(P0.X + V0.X * dt, P0.Y + V0.Y * dt);

                    Point A1 = new Point((hotspot.X - P1.X) * 35, (hotspot.Y - P1.Y) * 35);
                    V1 = new Point(V1.X * 0.7 + A1.X * dt, V1.Y * 0.7 + A1.Y * dt);
                    P1 = new Point(P1.X + V1.X * dt, P1.Y + V1.Y * dt);
                }

                Spot0Direction = SpotDirection(P0, Spot0Position);
                Spot1Direction = SpotDirection(P1, Spot1Position);
            }

            then = now;
        }
        private Color SpotDirection(Point hotspot, Color position)
        {
            double px = 3.0 * (double)(position.R) / 255.0 - 1.0;
            double py = 3.0 * (double)(position.G) / 255.0 - 1.0;
            double pz = 3.0 * (double)(position.B) / 255.0 - 1.0;

            double dx = hotspot.X - px;
            double dy = hotspot.Y - py;
            double dz = 0.0 - pz;

            {
                double m = MathTool.Hypot(dx, dy, dz);

                dx = dx / m;
                dy = dy / m;
                dz = dz / m;
            }

            dx = 255.0 * (0.5 + dx * 0.5);
            dy = 255.0 * (0.5 + dy * 0.5);
            dz = 255.0 * (0.5 + dz * 0.5);

            return new Color() { R = (byte)Math.Floor(dx), G = (byte)Math.Floor(dy), B = (byte)Math.Floor(dz), A = 255 };
        }
        private Color SpotPosition(double x, double y, double z)
        {
            return new Color()
            {
                R = (byte)Math.Floor(255.0 * ((x + 1.0) / 3.0)),
                G = (byte)Math.Floor(255.0 * ((y + 1.0) / 3.0)),
                B = (byte)Math.Floor(255.0 * ((z + 1.0) / 3.0)),
                A=255
            };
        }

        #endregion

        #region Spot0Position Property
        public Color Spot0Position
        {
            get
            {
                return (Color)GetValue(Spot0PositionProperty);
            }
            set
            {
                this.SetValue(Spot0PositionProperty, value);
            }
        }
        public static readonly DependencyProperty Spot0PositionProperty = DependencyProperty.Register("Spot0Position", typeof(Color), typeof(XamHollywoodEffect), new PropertyMetadata(PixelShaderConstantCallback(1)));
        #endregion

        #region Spot0Direction Property
        public Color Spot0Direction
        {
            get
            {
                return (Color)GetValue(Spot0DirectionProperty);
            }
            set
            {
                this.SetValue(Spot0DirectionProperty, value);
            }
        }
        public static readonly DependencyProperty Spot0DirectionProperty = DependencyProperty.Register("Spot0Direction", typeof(Color), typeof(XamHollywoodEffect), new PropertyMetadata(PixelShaderConstantCallback(2)));
        #endregion

        #region Spot1Position Property
        public Color Spot1Position
        {
            get
            {
                return (Color)GetValue(Spot1PositionProperty);
            }
            set
            {
                this.SetValue(Spot1PositionProperty, value);
            }
        }
        public static readonly DependencyProperty Spot1PositionProperty = DependencyProperty.Register("Spot1Position", typeof(Color), typeof(XamHollywoodEffect), new PropertyMetadata(PixelShaderConstantCallback(3)));
        #endregion

        #region Spot1Direction Property
        public Color Spot1Direction
        {
            get
            {
                return (Color)GetValue(Spot1DirectionProperty);
            }
            set
            {
                this.SetValue(Spot1DirectionProperty, value);
            }
        }
        public static readonly DependencyProperty Spot1DirectionProperty = DependencyProperty.Register("Spot1Direction", typeof(Color), typeof(XamHollywoodEffect), new PropertyMetadata(PixelShaderConstantCallback(4)));
        #endregion

     }
}
