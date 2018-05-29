using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamTwinCurtainEffect : XamEffectBase
    {
        public XamTwinCurtainEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ProgressProperty);
            UpdateShaderValue(OpeningProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamTwinCurtainEffect), 0);

        public double Progress
        {
            get
            {
                return (double)GetValue(ProgressProperty);
            }
            set
            {
                SetValue(ProgressProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(XamTwinCurtainEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));

        public double Opening
        {
            get
            {
                return (double)GetValue(OpeningProperty);
            }
            set
            {
                SetValue(OpeningProperty, value);
            }
        }
        public static readonly DependencyProperty OpeningProperty = DependencyProperty.Register("Opening", typeof(double), typeof(XamTwinCurtainEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(1)));
    }
}
