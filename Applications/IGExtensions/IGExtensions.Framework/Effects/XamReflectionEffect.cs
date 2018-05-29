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
using System.Windows.Media.Effects;
using System.Collections.Generic;

namespace IGExtensions.Framework.Effects
{
    public class XamReflectionEffect : XamEffectBase
    {
        #region auto rippling
        private static List<WeakReference> autoRipple = new List<WeakReference>();
        private static DateTime then = DateTime.Now;

        static XamReflectionEffect()
        {
            CompositionTarget.Rendering += (o, e) => Tick();
        }

        static void Tick()
        {
            DateTime now = DateTime.Now;

            double speed = 0.01;
            double delta = speed*(now - then).TotalSeconds;

            then = now;

            for (int i = 0; i < autoRipple.Count; )
            {
                if (!autoRipple[i].IsAlive)
                {
                    autoRipple.RemoveAt(i);
                }
                else
                {
                    XamReflectionEffect reflectionEffect = autoRipple[i].Target as XamReflectionEffect;

                    reflectionEffect.Progress = reflectionEffect.Progress + delta;
                    ++i;
                }
            }
        }

        #endregion

        public XamReflectionEffect()
        {
            Noise = NoiseBrush;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ProgressProperty);

            this.PaddingBottom = 720*0.35;
            this.PaddingLeft = 30;
            this.PaddingRight = 30;
            autoRipple.Add(new WeakReference(this));
        }

        #region Input property
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamReflectionEffect), 0);
        #endregion

        #region Noise Property
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
        public static readonly DependencyProperty NoiseProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Noise", typeof(XamReflectionEffect), 1);
        #endregion

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
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(XamReflectionEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    }
}
