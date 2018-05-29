using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class MonochromeEffect : ShaderEffect
    {

        public static DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", 
            typeof(MonochromeEffect), 0);

        public static DependencyProperty FilterColorProperty = DependencyProperty.Register("FilterColor", 
            typeof(Color), 
            typeof(MonochromeEffect), 
            new PropertyMetadata(new Color(), 
                PixelShaderConstantCallback(0)));

        public MonochromeEffect()
        {
            var u = new Uri(@"/IGExtensions.Framework;component/Effects/Shaders/Monochrome.ps", UriKind.Relative);
            this.PixelShader = new PixelShader() { UriSource = u };

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(FilterColorProperty);
        }

        public MonochromeEffect(PixelShader shader)
        {
            // Note: for your project you must decide how to use the generated ShaderEffect class (Choose A or B below).
            // A: Comment out the following line if you are not passing in the shader and remove the shader parameter from the constructor

            PixelShader = shader;

            // B: Uncomment the following two lines - which load the *.ps file
            // Uri u = new Uri(@"pack://application:,,,/bandedswirl.ps");
            // PixelShader = new PixelShader() { UriSource = u };

            // Must initialize each DependencyProperty that's affiliated with a shader register
            // Ensures the shader initializes to the proper default value.
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(FilterColorProperty);
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

        public virtual Color FilterColor
        {
            get
            {
                return ((Color)(GetValue(FilterColorProperty)));
            }
            set
            {
                SetValue(FilterColorProperty, value);
            }
        }
    }
}
