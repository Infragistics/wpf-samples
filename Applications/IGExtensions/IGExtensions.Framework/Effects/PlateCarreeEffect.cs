using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Effects
{
    public class PlateCarreEffect : XamEffectBase
    {
        class EffectTransform : GeneralTransform
        {
            public EffectTransform(PlateCarreEffect effect)
            {
                PlateCarreEffect = effect;
            }

            private PlateCarreEffect PlateCarreEffect;
            public bool IsInverse;

            public override GeneralTransform Inverse
            {
                get { return new EffectTransform(PlateCarreEffect) { IsInverse = !IsInverse }; }
            }

            public override bool TryTransform(Point uv, out Point outPoint)
            {
                double R = 0.5;
                double u = double.NaN;
                double v = double.NaN;

                if (IsInverse)
                {
                    Vector3D xyz = new Vector3D((uv.X - 0.5) * 2.0, uv.Y - 0.5, 0.0);
                    double z2 = R * R - xyz.X * xyz.X - xyz.Y * xyz.Y;

                    if (z2 >= 0)
                    {
                        xyz.Z = Math.Sqrt(z2);
                        xyz = PlateCarreEffect.InverseMatrix3D.Transform(xyz);

                        u = 0.995*MathTool.Frac(0.5 * Math.Atan2(xyz.X, xyz.Z) / Math.PI - 0.5);
                        v = 0.5 + Math.Asin(xyz.Y / R) / Math.PI;
                    }
                }
                else
                {
                }

                outPoint = new Point(u, v);

                return !(double.IsNaN(u) || double.IsNaN(v));
            }
            public override Rect TransformBounds(Rect rect)
            {
                throw new NotImplementedException();
            }

#if WPF
            /// <summary>
            /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable"/> derived class. 
            /// </summary>
            /// <returns>
            /// The new instance.
            /// </returns>
            protected override Freezable CreateInstanceCore()
            {
                throw new NotImplementedException("WPF CreateInstanceCore method not implemented");
            } 
#endif
        }

        public PlateCarreEffect()
        {
            UpdateShaderValue(InputProperty);
            Rotation = Matrix3D.Identity;

            UpdateShaderValue(R0Property);
            UpdateShaderValue(R1Property);
            UpdateShaderValue(R2Property);
        }

        #region Rotation Dependency Property
        /// <summary>
        /// Sets or gets the Rotation property.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public Matrix3D Rotation
        {
            get
            {
                return (Matrix3D)GetValue(RotationProperty);
            }
            set
            {
                SetValue(RotationProperty, value);
            }
        }

        internal const string RotationPropertyName = "Rotation";

        /// <summary>
        /// Identifies the Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(RotationPropertyName, typeof(Matrix3D), typeof(PlateCarreEffect),
            new PropertyMetadata(new Matrix3D(),(sender, e) =>
            {
#if true
                PlateCarreEffect plateCarre = sender as PlateCarreEffect;
                Matrix3D matrix = plateCarre.Rotation;

                plateCarre.R0 = ColorTool.FromVector(new Vector3D(matrix.M11, matrix.M12, matrix.M13) * 0.5);
                plateCarre.R1 = ColorTool.FromVector(new Vector3D(matrix.M21, matrix.M22, matrix.M23) * 0.5);
                plateCarre.R2 = ColorTool.FromVector(new Vector3D(matrix.M31, matrix.M32, matrix.M33) * 0.5);
                plateCarre.Matrix3D = matrix;

                plateCarre.InverseMatrix3D = matrix;
                plateCarre.InverseMatrix3D.Invert();
                
#else
                // only care about rotation around Y
#endif
            }));
        internal Matrix3D Matrix3D;
        internal Matrix3D InverseMatrix3D;
        #endregion

        protected override GeneralTransform EffectMapping
        {
            get
            {
                return new EffectTransform(this) { IsInverse = false };
            }
        }

        #region Input Property
        public const string InputPropertyName = "Input";
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(InputPropertyName, typeof(PlateCarreEffect), 0);
        #endregion

#if false
        #region S0 Property
        public const string S0PropertyName = "S0";
        public double S0
        {
            get
            {
                return (double)GetValue(S0Property);
            }
            set
            {
                SetValue(S0Property, value);
            }
        }
        public static readonly DependencyProperty S0Property = DependencyProperty.Register(S0PropertyName, typeof(double), typeof(PlateCarreEffect), new PropertyMetadata(1.0, PixelShaderConstantCallback(0)));
        #endregion

        #region L Property
        public const string LPropertyName = "L";
        public Color L
        {
            get
            {
                return (Color)GetValue(LProperty);
            }
            set
            {
                SetValue(LProperty, value);
            }
        }
        public static readonly DependencyProperty LProperty = DependencyProperty.Register(LPropertyName, typeof(Color), typeof(PlateCarreEffect), new PropertyMetadata(Color.FromArgb(0, 128, 128, 0), PixelShaderConstantCallback(1)));
        #endregion
#else
        #region R0 Property
        private const string R0PropertyName = "R0";
        private Color R0
        {
            get
            {
                return (Color)GetValue(R0Property);
            }
            set
            {
                SetValue(R0Property, value);
            }
        }
        private static readonly DependencyProperty R0Property = DependencyProperty.Register(R0PropertyName, typeof(Color), typeof(PlateCarreEffect), new PropertyMetadata(Color.FromArgb(0, 0xff, 0x80, 0x80), PixelShaderConstantCallback(2)));
        #endregion

        #region R1 Property
        private const string R1PropertyName = "R1";
        private Color R1
        {
            get
            {
                return (Color)GetValue(R1Property);
            }
            set
            {
                SetValue(R1Property, value);
            }
        }
        private static readonly DependencyProperty R1Property = DependencyProperty.Register(R1PropertyName, typeof(Color), typeof(PlateCarreEffect), new PropertyMetadata(Color.FromArgb(0, 0x80, 0xff, 0x80), PixelShaderConstantCallback(3)));
        #endregion

        #region R2 Property
        private const string R2PropertyName = "R2";
        private Color R2
        {
            get
            {
                return (Color)GetValue(R2Property);
            }
            set
            {
                SetValue(R2Property, value);
            }
        }
        private static readonly DependencyProperty R2Property = DependencyProperty.Register(R2PropertyName, typeof(Color), typeof(PlateCarreEffect), new PropertyMetadata(Color.FromArgb(0, 0x80, 0x80, 0xff), PixelShaderConstantCallback(4)));
        #endregion
#endif
    }

}
