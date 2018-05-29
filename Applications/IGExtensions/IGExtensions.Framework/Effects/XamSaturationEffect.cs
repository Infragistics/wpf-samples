using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamSaturationEffect : XamEffectBase
    {
        public XamSaturationEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(SaturationProperty);
            UpdateShaderValue(TintProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamSaturationEffect), 0);

        public double Saturation
        {
            get
            {
                return ((double)(this.GetValue(SaturationProperty)));
            }
            set
            {
                this.SetValue(SaturationProperty, value);
            }
        }
        public static readonly DependencyProperty SaturationProperty = DependencyProperty.Register("Saturation", typeof(double), typeof(XamSaturationEffect), new PropertyMetadata(((double)(1.0)), PixelShaderConstantCallback(0)));

        public Color Tint
        {
            get
            {
                return ((Color)(this.GetValue(TintProperty)));
            }
            set
            {
                this.SetValue(TintProperty, value);
            }
        }
        public static readonly DependencyProperty TintProperty = DependencyProperty.Register("Tint", typeof(Color), typeof(XamSaturationEffect), new PropertyMetadata(Colors.White, PixelShaderConstantCallback(1)));
    }
}
