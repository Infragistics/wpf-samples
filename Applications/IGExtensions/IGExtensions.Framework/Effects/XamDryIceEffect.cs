using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace IGExtensions.Framework.Effects
{
    public class XamDryIceEffect : XamEffectBase
    {
        public XamDryIceEffect()
        {
            //CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(NoiseProperty);
            UpdateShaderValue(ProgressProperty);
            UpdateShaderValue(FrequencyXProperty);
            UpdateShaderValue(FrequencyYProperty);

            Noise = NoiseBrush;
        }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1.0 / 30.0) }; 
        public bool AutoRun
        {
            get { return autoRun; }
            set
            {
                autoRun=value;

                if(AutoRun)
                {
                    dispatcherTimer.Start();
                }
                else
                {
                    dispatcherTimer.Stop();
                }
            }
        }
        private bool autoRun;

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Progress = Progress + 0.02;
        }

        #region Input Property
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
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(XamDryIceEffect), 0);
        #endregion

        #region Noise Property
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
        public static readonly DependencyProperty NoiseProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Noise", typeof(XamDryIceEffect), 1);
        #endregion

        #region Progress Property
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
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(XamDryIceEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));
        #endregion

        #region FrequencyX Property
        public double FrequencyX
        {
            get
            {
                return (double)GetValue(FrequencyXProperty);
            }
            set
            {
                SetValue(FrequencyXProperty, value);
            }
        }
        public static readonly DependencyProperty FrequencyXProperty = DependencyProperty.Register("FrequencyX", typeof(double), typeof(XamDryIceEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(1)));
        #endregion

        #region FrequencyY Property
        public double FrequencyY
        {
            get
            {
                return (double)GetValue(FrequencyYProperty);
            }
            set
            {
                SetValue(FrequencyYProperty, value);
            }
        }
        public static readonly DependencyProperty FrequencyYProperty = DependencyProperty.Register("FrequencyY", typeof(double), typeof(XamDryIceEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(2)));
        #endregion

    }
}
