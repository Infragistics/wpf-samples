using System.IO;
using System.Windows.Controls;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningImages : SampleContainer
    {
        private ImageNode imgNode;

        public DefiningImages()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            // create top paragraph
            ParagraphNode pn = new ParagraphNode();
            pn.SetText("Text at top");
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);

            // create paragraph for the left text, image and right text
            pn = new ParagraphNode();
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);

            // create the text at left
            RunNode rn = new RunNode();
            rn.SetText("Text at left");
            pn.ChildNodes.Add(rn);

            // create the run node for the image
            rn = new RunNode();
            pn.ChildNodes.Add(rn);

            // obtain the image
            Stream fs = FilesProvider.GetStreamForFile("laptop.jpg");
            RichTextImage rtImage = RichTextImage.CreateFrom(fs, RichTextImageFormat.Jpeg);

            // create the image node and set the rich text image
            imgNode = new ImageNode();
            imgNode.Image = rtImage;
            rn.ChildNodes.Add(imgNode);

            // create the text at right
            rn = new RunNode();
            rn.SetText("Text at right");
            pn.ChildNodes.Add(rn);

            // create bottom paragraph
            pn = new ParagraphNode();
            pn.SetText("Text at bottom");
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }

        private RichTextImageTransform getImageTransform()
        {
            RichTextImageTransform rtit = imgNode.Transform;
            if (rtit == null)
            {
                rtit = new RichTextImageTransform();
                imgNode.Transform = rtit;
            }
            return rtit;
        }

        private void cb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RichTextImageTransform rtit;
            CheckBox cb = (CheckBox)sender;
            switch (cb.Name)
            {
                case "cbFlipHorizontal":
                    rtit = getImageTransform();
                    rtit.FlipHorizontally = cb.IsChecked.Value;
                    break;
                case "cbFlipVertical":
                    rtit = getImageTransform();
                    rtit.FlipVertically = cb.IsChecked.Value;
                    break;
            }
        }

        private void slAngle_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            RichTextImageTransform rtit = getImageTransform();
            rtit.RotationAngle = (float)slider.Value;
        }

        private RectOffsets getCropping()
        {
            RectOffsets? offsets = this.imgNode.Cropping;
            if (!offsets.HasValue)
            {
                offsets = new RectOffsets();
                imgNode.Cropping = offsets;
            }
            return offsets.Value;
        }

        private void slCrop_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            RectOffsets offsets = getCropping();
            switch (slider.Name)
            {
                case "slCropTop":
                    offsets.Top = slider.Value;
                    break;
                case "slCropBottom":
                    offsets.Bottom = slider.Value;
                    break;
                case "slCropLeft":
                    offsets.Left = slider.Value;
                    break;
                case "slCropRight":
                    offsets.Right = slider.Value;
                    break;
            }
            imgNode.Cropping = offsets;
        }

        private RichTextMargin getRichTextMargin()
        {
            RichTextMargin? rtm = this.imgNode.RenderEffectsMargin;
            if (!rtm.HasValue)
            {
                rtm = new RichTextMargin();
                this.imgNode.RenderEffectsMargin = rtm;
            }
            return rtm.Value;
        }

        private void slMargin_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            RichTextMargin margin = getRichTextMargin();
            switch (slider.Name)
            {
                case "slMarginTop":
                    margin.Top = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                    break;
                case "slMarginBottom":
                    margin.Bottom = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                    break;
                case "slMarginLeft":
                    margin.Left = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                    break;
                case "slMarginRight":
                    margin.Right = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                    break;
            }
            this.imgNode.RenderEffectsMargin = margin;
        }

        private void slStretchSize_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            if (this.imgNode != null)
            {
                RichTextImageTransform rtit = getImageTransform();

                if (rtit.StretchSize == null)
                {
                    rtit.StretchSize = new RichTextSize(this.imgNode.RenderSizeResolved.Value.Width, this.imgNode.RenderSizeResolved.Value.Height);
                }

                RichTextSize rts = rtit.StretchSize.Value;
                switch (slider.Name)
                {
                    case "slStretchWidth":
                        rts.Width = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                        break;
                    case "slStretchHeight":
                        rts.Height = new Extent(slider.Value, ExtentUnitType.LogicalPixels);
                        break;
                }
                this.imgNode.Transform.StretchSize = rts;
            }
        }
    }
}
