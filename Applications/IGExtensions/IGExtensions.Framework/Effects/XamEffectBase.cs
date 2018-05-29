using System;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

// "C:\Program Files\Microsoft DirectX SDK (August 2009)\Utilities\bin\x86\fxc.exe" /T ps_2_0 /E main /Fo $(ProjectDir)OffsetShader.ps $(ProjectDir)OffsetShader.fx

namespace IGExtensions.Framework.Effects
{
    public class XamEffectBase : ShaderEffect
    {
        protected XamEffectBase()
        {
            var shaderPath = "Effects/Shaders/" + GetType().Name + ".ps";
            var assemblyShortName = GetType().Assembly.ToString().Split(',')[0];

#if WPF
            var uri = "pack://application:,,," + "/" + assemblyShortName + ";component/" + shaderPath;
#endif

#if SILVERLIGHT
            var uri = "/" + assemblyShortName + ";component/" + shaderPath;
#endif

            PixelShader = new PixelShader { UriSource = new Uri(uri, UriKind.RelativeOrAbsolute) };
        }

        protected static Brush NoiseBrush
        {
            get
            {
                if (_noiseBrush == null)
                {
                    var filePath = "Effects/Noise.png";
                    var assemblyShortName = typeof(XamEffectBase).Assembly.ToString().Split(',')[0];

#if WPF
                    var uri = "pack://application:,,," + "/" + assemblyShortName + ";component/" + filePath;
#endif

#if SILVERLIGHT
                    var uri = "/" + assemblyShortName + ";component/" + filePath;
#endif

                    _noiseBrush = new ImageBrush { ImageSource = new BitmapImage { UriSource = new Uri(uri, UriKind.Relative) } };
                }

                return _noiseBrush;
            }
        }
        private static Brush _noiseBrush;
    }
}
