using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class ColorToneEffect : ShaderEffect
    {
        public static DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColorToneEffect), 0);

        public static DependencyProperty DesaturationProperty = DependencyProperty.Register("Desaturation", typeof(double), typeof(ColorToneEffect), new PropertyMetadata(new double(), PixelShaderConstantCallback(0)));

        public static DependencyProperty TonedProperty = DependencyProperty.Register("Toned", typeof(double), typeof(ColorToneEffect), new PropertyMetadata(new double(), PixelShaderConstantCallback(1)));

        public static DependencyProperty LightColorProperty = DependencyProperty.Register("LightColor", typeof(Color), typeof(ColorToneEffect), new PropertyMetadata(new Color(), PixelShaderConstantCallback(2)));

        public static DependencyProperty DarkColorProperty = DependencyProperty.Register("DarkColor", typeof(Color), typeof(ColorToneEffect), new PropertyMetadata(new Color(), PixelShaderConstantCallback(3)));

        public ColorToneEffect()
        {
            var u = new Uri(@"/IGExtensions.Framework;component/Effects/Shaders/ColorTone.ps", UriKind.Relative);
            this.PixelShader = new PixelShader() { UriSource = u };

            // Must initialize each DependencyProperty that's affiliated with a shader register
            // Ensures the shader initializes to the proper default value.
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(DesaturationProperty);
            this.UpdateShaderValue(TonedProperty);
            this.UpdateShaderValue(LightColorProperty);
            this.UpdateShaderValue(DarkColorProperty);
        }

        public virtual Brush Input
        {
            get
            {
                return ((Brush)(GetValue(InputProperty)));
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }

        public virtual double Desaturation
        {
            get
            {
                return ((double)(GetValue(DesaturationProperty)));
            }
            set
            {
                SetValue(DesaturationProperty, value);
            }
        }

        public virtual double Toned
        {
            get
            {
                return ((double)(GetValue(TonedProperty)));
            }
            set
            {
                SetValue(TonedProperty, value);
            }
        }

        public virtual Color LightColor
        {
            get
            {
                return ((Color)(GetValue(LightColorProperty)));
            }
            set
            {
                SetValue(LightColorProperty, value);
            }
        }

        public virtual Color DarkColor
        {
            get
            {
                return ((Color)(GetValue(DarkColorProperty)));
            }
            set
            {
                SetValue(DarkColorProperty, value);
            }
        }
    }
}
