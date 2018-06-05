using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class XamSmokeEffect : XamEffectBase
    {
        public XamSmokeEffect()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OldInputProperty);
            UpdateShaderValue(NoiseProperty);
            UpdateShaderValue(ProgressProperty);

            Noise = NoiseBrush;
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(InputPropertyName, typeof(XamSmokeEffect), 0);
        #endregion

        #region OldInput Property
        public const string OldInputPropertyName = "OldInput";
        public Brush OldInput
        {
            get
            {
                return (Brush)GetValue(OldInputProperty);
            }
            set
            {
                this.SetValue(OldInputProperty, value);
            }
        }
        public static readonly DependencyProperty OldInputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(OldInputPropertyName, typeof(XamSmokeEffect), 1);
        #endregion

        #region Noise Property
        public const string NoisePropertyName = "Noise";
        public Brush Noise
        {
            get
            {
                return (Brush)GetValue(NoiseProperty);
            }
            set
            {
                this.SetValue(NoiseProperty, value);
            }
        }
        public static readonly DependencyProperty NoiseProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(NoisePropertyName, typeof(XamSmokeEffect), 2);
        #endregion

        #region Progress Property
        public const string ProgressPropertyName = "Progress";
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
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(ProgressPropertyName, typeof(double), typeof(XamSmokeEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));
        #endregion
    }

}
