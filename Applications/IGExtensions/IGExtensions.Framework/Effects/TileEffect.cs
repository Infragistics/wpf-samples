using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace IGExtensions.Framework.Effects
{
    public class TileEffect : ShaderEffect
    {
        public static DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(TileEffect), 0);

        public static DependencyProperty VerticalTileCountProperty =
            DependencyProperty.Register(
                "VerticalTileCount",
                typeof(double),
                typeof(TileEffect),
                new PropertyMetadata(1.0, PixelShaderConstantCallback(1)));

        public static DependencyProperty HorizontalTileCountProperty =
            DependencyProperty.Register(
                "HorizontalTileCount",
                typeof(double),
                typeof(TileEffect),
                new PropertyMetadata(1.0, PixelShaderConstantCallback(2)));

        public TileEffect()
        {
            // Note: for your project you must decide how to use the generated ShaderEffect class (Choose A or B below).
            // B: Uncomment the following two lines - which load the *.ps file
            var ps = new Uri(@"/IGExtensions.Framework;component/Effects/Shaders/TileEffect.ps", UriKind.Relative);
            PixelShader = new PixelShader() { UriSource = ps };

            // Must initialize each DependencyProperty that's affiliated with a shader register
            // Ensures the shader initializes to the proper default value.
            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(VerticalTileCountProperty);
            this.UpdateShaderValue(HorizontalTileCountProperty);
        }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        public virtual System.Windows.Media.Brush Input
        {
            get
            {
                return ((System.Windows.Media.Brush)(GetValue(InputProperty)));
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the vertical tile count.
        /// </summary>
        /// <value>The vertical tile count.</value>
        public virtual double VerticalTileCount
        {
            get
            {
                return ((double)(GetValue(VerticalTileCountProperty)));
            }
            set
            {
                SetValue(VerticalTileCountProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the horizontal tile count.
        /// </summary>
        /// <value>The horizontal tile count.</value>
        public virtual double HorizontalTileCount
        {
            get
            {
                return ((double)(GetValue(HorizontalTileCountProperty)));
            }
            set
            {
                SetValue(HorizontalTileCountProperty, value);
            }
        }
    }
}