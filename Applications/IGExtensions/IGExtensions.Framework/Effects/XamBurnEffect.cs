using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Resources;

namespace IGExtensions.Framework.Effects
{
    public class XamBurnEffect : XamEffectBase
    {
        public XamBurnEffect()
        {
            Noise = NoiseBrush;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(NoiseProperty);
            UpdateShaderValue(ProgressProperty);
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamBurnEffect), 0);

        public Brush Noise
        {
            get
            {
                return ((Brush)(this.GetValue(NoiseProperty)));
            }
            set
            {
                this.SetValue(NoiseProperty, value);
            }
        }
        public static readonly DependencyProperty NoiseProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Noise", typeof(XamBurnEffect), 1);

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
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(XamBurnEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    }
}
