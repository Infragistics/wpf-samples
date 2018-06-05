using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamSphereEffect : XamEffectBase
    {
#if false
        public class Transform : GeneralTransform
        {
            public Transform(XamSphereEffect sphereEffect, bool forward)
            {
                SphereEffect = sphereEffect;
                Forward = forward;
            }

            private bool Forward;
            private XamSphereEffect SphereEffect;

            public override Rect TransformBounds(Rect rect)
            {
                throw new NotImplementedException();
            }

            public override GeneralTransform Inverse
            {
                get { return new Transform(SphereEffect, !Forward); }
            }

            private static double frac(double d)
            {
                return Math.Abs(d) - Math.Floor(Math.Abs(d));
            }
            private static double atan2(double a, double b)
            {
                return Math.Atan2(a, b);
            }
            private static double asin(double a)
            {
                return Math.Asin(a);
            }
            private static double sqrt(double a)
            {
                return Math.Sqrt(a);
            }

            public override bool TryTransform(Point inPoint, out Point outPoint)
            {
                const double rho = 0.5;
                const double pi = Math.PI;

                if (Forward)
                {
                    // given a point on the input, where does that go to on the output

                    Vector3D xyz = new Vector3D(inPoint.X - 0.5, inPoint.Y - 0.5, 0.0);

                    if (xyz.Length < rho)
                    {
                        xyz.Z = sqrt(rho * rho - xyz.X * xyz.X - xyz.Y * xyz.Y);

                        double latitude = asin(xyz.Y / rho);
                        double longitude = atan2(xyz.Z, xyz.X) + SphereEffect.Longitude;

                        outPoint = new Point(1.0 - frac(longitude / (2.0 * pi)), 0.5 + latitude / pi);
                        return true;
                    }

                    outPoint=inPoint;
                    return false;
                }
                else
                {
                    Vector3D xyz = new Vector3D(inPoint.X - 0.5, inPoint.Y - 0.5, 0.0);

                    if (xyz.Length < rho)
                    {
                        xyz.Z = sqrt(rho * rho - xyz.X * xyz.X - xyz.Y * xyz.Y);
                        
                        // given a point on the output, where does that come from on the input
                    }

                    outPoint=inPoint;
                    return false;
                }
            }
        }

        protected override GeneralTransform EffectMapping
        {
            get
            {
                return new Transform(this, true);
            }
        }
#endif
        public XamSphereEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(RProperty);
            UpdateShaderValue(CyProperty);
            UpdateShaderValue(SMinProperty);
            UpdateShaderValue(SMaxProperty);
            UpdateShaderValue(TMinProperty);
            UpdateShaderValue(TMaxProperty);
            UpdateShaderValue(AspectProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamSphereEffect), 0);
        #endregion

        #region R Property
        public double R
        {
            get
            {
                return (double)GetValue(RProperty);
            }
            set
            {
                SetValue(RProperty, value);
            }
        }
        public static readonly DependencyProperty RProperty = DependencyProperty.Register("R", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(0)));
        #endregion

        #region Cy Property
        public double Cy
        {
            get
            {
                return (double)GetValue(CyProperty);
            }
            set
            {
                SetValue(CyProperty, value);
            }
        }
        public static readonly DependencyProperty CyProperty = DependencyProperty.Register("Cy", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(1)));
        #endregion

        #region SMin Property
        public double SMin
        {
            get
            {
                return (double)GetValue(SMinProperty);
            }
            set
            {
                SetValue(SMinProperty, value);
            }
        }
        public static readonly DependencyProperty SMinProperty = DependencyProperty.Register("SMin", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(2)));
        #endregion

        #region SMax Property
        public double SMax
        {
            get
            {
                return (double)GetValue(SMaxProperty);
            }
            set
            {
                SetValue(SMaxProperty, value);
            }
        }
        public static readonly DependencyProperty SMaxProperty = DependencyProperty.Register("SMax", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(3)));
        #endregion

        #region TMin Property
        public double TMin
        {
            get
            {
                return (double)GetValue(TMinProperty);
            }
            set
            {
                SetValue(TMinProperty, value);
            }
        }
        public static readonly DependencyProperty TMinProperty = DependencyProperty.Register("TMin", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(4)));
        #endregion

        #region TMax Property
        public double TMax
        {
            get
            {
                return (double)GetValue(TMaxProperty);
            }
            set
            {
                SetValue(TMaxProperty, value);
            }
        }
        public static readonly DependencyProperty TMaxProperty = DependencyProperty.Register("TMax", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(5)));
        #endregion

        #region Aspect Property
        public double Aspect
        {
            get
            {
                return (double)GetValue(AspectProperty);
            }
            set
            {
                SetValue(AspectProperty, value);
            }
        }
        public static readonly DependencyProperty AspectProperty = DependencyProperty.Register("Aspect", typeof(double), typeof(XamSphereEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(6)));
        #endregion

    }
#if false
    public class _XamSphereEffect : XamEffectBase
    {
        public class Transform : GeneralTransform
        {
            public Transform(_XamSphereEffect sphereEffect, bool forward)
            {
                SphereEffect = sphereEffect;
                Forward = forward;
            }

            private bool Forward;
            private _XamSphereEffect SphereEffect;

            public override Rect TransformBounds(Rect rect)
            {
                throw new NotImplementedException();
            }

            public override GeneralTransform Inverse
            {
                get { return new Transform(SphereEffect, !Forward); }
            }

            private static double frac(double d)
            {
                return Math.Abs(d) - Math.Floor(Math.Abs(d));
            }
            private static double atan2(double a, double b)
            {
                return Math.Atan2(a, b);
            }
            private static double asin(double a)
            {
                return Math.Asin(a);
            }
            private static double sqrt(double a)
            {
                return Math.Sqrt(a);
            }

            public override bool TryTransform(Point inPoint, out Point outPoint)
            {
                const double rho = 0.5;
                const double pi = Math.PI;

                if (Forward)
                {
                    // given a point on the input, where does that go to on the output

                    Vector3D xyz = new Vector3D(inPoint.X - 0.5, inPoint.Y - 0.5, 0.0);

                    if (xyz.Length < rho)
                    {
                        xyz.Z = sqrt(rho * rho - xyz.X * xyz.X - xyz.Y * xyz.Y);

                        double latitude = asin(xyz.Y / rho);
                        double longitude = atan2(xyz.Z, xyz.X) + SphereEffect.Longitude;

                        outPoint = new Point(1.0 - frac(longitude / (2.0 * pi)), 0.5 + latitude / pi);
                        return true;
                    }

                    outPoint = inPoint;
                    return false;
                }
                else
                {
                    Vector3D xyz = new Vector3D(inPoint.X - 0.5, inPoint.Y - 0.5, 0.0);

                    if (xyz.Length < rho)
                    {
                        xyz.Z = sqrt(rho * rho - xyz.X * xyz.X - xyz.Y * xyz.Y);

                        // given a point on the output, where does that come from on the input
                    }

                    outPoint = inPoint;
                    return false;
                }
            }
        }

        protected override GeneralTransform EffectMapping
        {
            get
            {
                return new Transform(this, true);
            }
        }

        public _XamSphereEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(LongitudeProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(_XamSphereEffect), 0);
        #endregion

        public void Bogus()
        {
            // given some parameters, work out the shader parameters
            // for example, Longitude is the LongitudeMin+0.5*LongitudeRange
        }

        #region Longitude Property
        public double Longitude
        {
            get
            {
                return (double)GetValue(LongitudeProperty);
            }
            set
            {
                SetValue(LongitudeProperty, value);
            }
        }
        public static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(double), typeof(_XamSphereEffect),
            new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));
        #endregion

        #region Y Property
        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(_XamSphereEffect), 
            new PropertyMetadata(0.0, PixelShaderConstantCallback(1)));
        #endregion

        #region LongitudeMin Property
        public double LongitudeMin
        {
            get
            {
                return (double)GetValue(LongitudeMinProperty);
            }
            set
            {
                SetValue(LongitudeMinProperty, value);
            }
        }
        public static readonly DependencyProperty LongitudeMinProperty = DependencyProperty.Register("LongitudeMin", typeof(double), typeof(_XamSphereEffect), 
            new PropertyMetadata(-Math.PI, PixelShaderConstantCallback(2)));
        #endregion
 
        #region LongitudeRange Property
        public double LongitudeRange
        {
            get
            {
                return (double)GetValue(LongitudeRangeProperty);
            }
            set
            {
                SetValue(LongitudeRangeProperty, value);
            }
        }
        public static readonly DependencyProperty LongitudeRangeProperty = DependencyProperty.Register("LongitudeRange", typeof(double), typeof(_XamSphereEffect),
            new PropertyMetadata(2.0*Math.PI, PixelShaderConstantCallback(3)));
        #endregion

        #region LatitudeMin Property
        public double LatitudeMin
        {
            get
            {
                return (double)GetValue(LatitudeMinProperty);
            }
            set
            {
                SetValue(LatitudeMinProperty, value);
            }
        }
        public static readonly DependencyProperty LatitudeMinProperty = DependencyProperty.Register("LatitudeMin", typeof(double), typeof(_XamSphereEffect), 
            new PropertyMetadata(-0.5*Math.PI, PixelShaderConstantCallback(4)));
        #endregion

        #region LatitudeRange Property
        public double LatitudeRange
        {
            get
            {
                return (double)GetValue(LatitudeRangeProperty);
            }
            set
            {
                SetValue(LatitudeRangeProperty, value);
            }
        }
        public static readonly DependencyProperty LatitudeRangeProperty = DependencyProperty.Register("LatitudeRange", typeof(double), typeof(_XamSphereEffect),
            new PropertyMetadata(Math.PI, PixelShaderConstantCallback(5)));
        #endregion
    }
#endif
}
