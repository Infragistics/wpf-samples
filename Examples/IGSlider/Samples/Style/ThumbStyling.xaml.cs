using Infragistics.Samples.Framework;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace IGSlider.Samples.Style
{
    public partial class ThumbStyling : Infragistics.Samples.Framework.SampleContainer
    {
        private List<Image> _imgSequence = new List<Image>();

        public ThumbStyling()
        {
            InitializeComponent();

            this._imgSequence.Add(butterfly20);
            this._imgSequence.Add(butterfly40);
            this._imgSequence.Add(butterfly60);
            this._imgSequence.Add(butterfly80);
            this._imgSequence.Add(butterfly100);
            this._imgSequence.Add(butterfly120);
            this._imgSequence.Add(butterfly140);
            this._imgSequence.Add(butterfly160);
            this._imgSequence.Add(butterfly180);
            this._imgSequence.Add(butterfly200);
            this._imgSequence.Add(butterfly220);
            this._imgSequence.Add(butterfly240);
  
            
        }

        private void XamSliderNumericThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int imgNum = ((int)(11.0 * e.NewValue / 10.0) + 1) * 20;
            if (e.NewValue > 10)
                imgNum = 240;
            if (e.NewValue <= 0)
                imgNum = 20;

            foreach (Image img in this._imgSequence)
            {
                if (img.Name == "butterfly" + imgNum.ToString())
                {
                    img.Opacity = 1;
                }
                else
                {
                    img.Opacity = 0;
                }
            }
        }
    }
}
