using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamSpotlightEffect : XamEffectBase
    {
        public XamSpotlightEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(BackBufferProperty);
            UpdateShaderValue(MouseProperty);
            UpdateShaderValue(HotspotProperty);
            UpdateShaderValue(BurnProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamSpotlightEffect), 0);

        public Brush BackBuffer
        {
            get
            {
                return ((Brush)(this.GetValue(BackBufferProperty)));
            }
            set
            {
                this.SetValue(BackBufferProperty, value);
            }
        }
        public static readonly DependencyProperty BackBufferProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("BackBuffer", typeof(XamSpotlightEffect), 1);

        public Point Mouse
        {
            get
            {
                return ((Point)(this.GetValue(MouseProperty)));
            }
            set
            {
                this.SetValue(MouseProperty, value);
            }
        }
        public static readonly DependencyProperty MouseProperty = DependencyProperty.Register("Mouse", typeof(Point), typeof(XamSpotlightEffect), new PropertyMetadata(PixelShaderConstantCallback(0)));

        public Point Hotspot
        {
            get
            {
                return ((Point)(this.GetValue(HotspotProperty)));
            }
            set
            {
                this.SetValue(HotspotProperty, value);
            }
        }
        public static readonly DependencyProperty HotspotProperty = DependencyProperty.Register("Hotspot", typeof(Point), typeof(XamSpotlightEffect), new PropertyMetadata(PixelShaderConstantCallback(1)));

        public double Burn
        {
            get
            {
                return ((double)(this.GetValue(BurnProperty)));
            }
            set
            {
                this.SetValue(BurnProperty, value);
            }
        }
        public static readonly DependencyProperty BurnProperty = DependencyProperty.Register("Burn", typeof(double), typeof(XamSpotlightEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(2)));

    }
}
