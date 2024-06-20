using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Editing
{
    public partial class ThumbInteractionModes : Infragistics.Samples.Framework.SampleContainer
    {
        public ThumbInteractionModes()
        {
            InitializeComponent();
  
            
        }

        private void SetInteractionMode(XamSliderNumericThumb currentThumb, string mode)
        {
            if (currentThumb != null)
            {
                switch (mode)
                {
                    case "Free":
                        currentThumb.InteractionMode = SliderThumbInteractionMode.Free;
                        break;
                    case "Push":
                        currentThumb.InteractionMode = SliderThumbInteractionMode.Push;
                        break;
                    case "Lock":
                        currentThumb.InteractionMode = SliderThumbInteractionMode.Lock;
                        break;
                }
            }
        }

        private void redRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.slider != null)
            {
                foreach (XamSliderNumericThumb thumb in this.slider.Thumbs)
                {
                    if (thumb.Tag.Equals("red"))
                    {
                        SetInteractionMode(thumb, (sender as RadioButton).Content as string);
                        break;
                    }
                }
            }
        }

        private void greenRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.slider != null)
            {
                foreach (XamSliderNumericThumb thumb in this.slider.Thumbs)
                {
                    if (thumb.Tag.Equals("green"))
                    {
                        SetInteractionMode(thumb, (sender as RadioButton).Content as string);
                        break;
                    }
                }
            }
        }

        private void blueRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.slider != null)
            {
                foreach (XamSliderNumericThumb thumb in this.slider.Thumbs)
                {
                    if (thumb.Tag.Equals("blue"))
                    {
                        SetInteractionMode(thumb, (sender as RadioButton).Content as string);
                        break;
                    }
                }
            }
        }

    }
}
