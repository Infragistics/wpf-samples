using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Effects
{
    public class PageEffect : XamEffectBase
    {
        public PageEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderTable();
            UpdateShaderValue(OriginProperty);
            UpdateShaderValue(DirectionProperty);
            UpdateShaderValue(CurveProperty);
            UpdateShaderValue(BackgroundProperty);
            UpdateShaderValue(FresnelProperty);
            UpdateShaderValue(OpacityProperty);
        }

        public double FresnelStrength
        {
            get { return fresnelStrength; }
            set
            {
                if (fresnelStrength != value)
                {
                    fresnelStrength = value;
                    UpdateShaderTable();
                }
            }
        }
        private double fresnelStrength=0.15;

        public double FresnelPosition
        {
            get { return fresnelPosition; }
            set
            {
                if (fresnelPosition != value)
                {
                    fresnelPosition = value;
                    UpdateShaderTable();
                }
            }
        }
        private double fresnelPosition=0.85;

        private void UpdateShaderTable()
        {
            // this should depend on fresnel strength and fresnel position
            // it should also actually calculate fresnel hightlights

            int resolution = 256;
            //TODO implement WPF version
            var writeableBitmap = new WriteableBitmap(resolution, 1);

            for (int i = 0; i < resolution; ++i)
            {
                double α = ((double)i) / ((double)(resolution - 1));
                double w = 0.2;
                double d = 1.0 - Math.Min(Math.Abs(FresnelPosition - α), 0.5 * w) / (0.5 * w);  
 
                //double d = Math.Abs(c - MathTool.Clamp(α, c - 0.5 * w, c + 0.5 * w));

                int a = (int)Math.Floor(255.0 * Math.Asin(α) / (0.5 * Math.PI));    // arcsin 
                int r = (int)Math.Floor(255.0 * (1.0 - α));                         // frontshade
                int g = (int)Math.Floor(255.0 * (1.0 - 0.3 * α));                   // backshade
                int b = (int)Math.Floor(255.0 * FresnelStrength * d);              // backlight

                //TODO implement WPF version set pixel
                writeableBitmap.Pixels[i] = (a << 24) | (r << 16) | (g << 8) | b;
            }

            writeableBitmap.Invalidate();
            Table = new ImageBrush() { ImageSource = writeableBitmap };
        }

        #region Shader Input Property
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(PageEffect), 0);
        #endregion

        #region Table Property
        private Brush Table
        {
            get
            {
                return ((Brush)(this.GetValue(TableProperty)));
            }
            set
            {
                this.SetValue(TableProperty, value);
            }
        }
        private static readonly DependencyProperty TableProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Table", typeof(PageEffect), 1);
        #endregion

        #region Origin Property
        public Point Origin
        {
            get
            {
                return ((Point)(this.GetValue(OriginProperty)));
            }
            set
            {
                this.SetValue(OriginProperty, value);
            }
        }
        public static readonly DependencyProperty OriginProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(PageEffect), new PropertyMetadata(PixelShaderConstantCallback(0)));
        #endregion

        #region Direction Property
        public Point Direction
        {
            get
            {
                return ((Point)(this.GetValue(DirectionProperty)));
            }
            set
            {
                double m = MathTool.Hypot(value.X, value.Y);

                this.SetValue(DirectionProperty, new Point(value.X/m, value.Y/m));
            }
        }
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(Point), typeof(PageEffect), new PropertyMetadata(PixelShaderConstantCallback(1)));
        #endregion

        #region Curve Property
        public double Curve
        {
            get
            {
                return ((double)(this.GetValue(CurveProperty)));
            }
            set
            {
                this.SetValue(CurveProperty, value);
            }
        }
        public static readonly DependencyProperty CurveProperty = DependencyProperty.Register("Curve", typeof(double), typeof(PageEffect), new PropertyMetadata(0.5, PixelShaderConstantCallback(2)));
        #endregion

        #region Background Property
        public Color Background
        {
            get
            {
                return ((Color)(this.GetValue(BackgroundProperty)));
            }
            set
            {
                this.SetValue(BackgroundProperty, value);
            }
        }
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Color), typeof(PageEffect), new PropertyMetadata(PixelShaderConstantCallback(3)));
        #endregion

        #region Fresnel Property
        public Color Fresnel
        {
            get
            {
                return ((Color)(this.GetValue(FresnelProperty)));
            }
            set
            {
                this.SetValue(FresnelProperty, value);
            }
        }
        public static readonly DependencyProperty FresnelProperty = DependencyProperty.Register("Fresnel", typeof(Color), typeof(PageEffect), new PropertyMetadata(Colors.White, PixelShaderConstantCallback(4)));
        #endregion

        #region Opacity Property
        public double Opacity
        {
            get
            {
                return ((double)(this.GetValue(OpacityProperty)));
            }
            set
            {
                this.SetValue(OpacityProperty, value);
            }
        }
        public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register("Opacity", typeof(double), typeof(PageEffect), new PropertyMetadata(0.99, PixelShaderConstantCallback(5)));
        #endregion
    }
}
