using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamZoomBlurEffect : XamEffectBase
    {
        public XamZoomBlurEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CenterProperty);
            UpdateShaderValue(AmountProperty);
        }

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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamZoomBlurEffect), 0);

        public Point Center
        {
            get
            {
                return (Point)GetValue(CenterProperty);
            }
            set
            {
                SetValue(CenterProperty, value);
            }
        }
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register("Center", typeof(Point), typeof(XamZoomBlurEffect), new PropertyMetadata(new Point(0.5, 0.5), PixelShaderConstantCallback(0)));

        public double Amount
        {
            get
            {
                return (double)GetValue(AmountProperty);
            }
            set
            {
                SetValue(AmountProperty, value);
            }
        }
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(double), typeof(XamZoomBlurEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(1)));
    }
}
