using System;
using Xamarin.Forms;

namespace Infragistics.Framework 
{
    public class ImageMapper
    {
        public Size ImageSize { get; set; }
        public Size ContainerSize { get; set; }

        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }

        public double ImageLeft { get; set; }
        public double ImageTop { get; set; }

        public void UpdateImageBounds()
        {
            var imageContainerWidth = (int)Math.Round((ImageSize.Width / ImageSize.Height) * ContainerSize.Height);
            var imageContainerHeightForWidth = (int)Math.Round(imageContainerWidth / (ImageSize.Width / ImageSize.Height));

            var imageContainerHeight = (int)Math.Round((ImageSize.Height / ImageSize.Width) * ContainerSize.Width);
            var imageContainerWidthForHeight = (int)Math.Round(imageContainerHeight / (ImageSize.Height / ImageSize.Width));

            double imageWidth;
            double imageHeight;

            var containerWidth = (int)Math.Round(ContainerSize.Width);
            var containerHeight = (int)Math.Round(ContainerSize.Height);

            if (imageContainerWidth >= containerWidth && imageContainerHeightForWidth >= containerHeight)
            {
                imageWidth = imageContainerWidth;
                imageHeight = imageContainerHeightForWidth;
            }
            else
            {
                imageWidth = imageContainerWidthForHeight;
                imageHeight = imageContainerHeight;
            }

            ImageWidth = imageWidth;
            ImageHeight = imageHeight;

            var left = (containerWidth - ImageWidth) / 2.0;
            var top = (containerHeight - ImageHeight) / 2.0;

            ImageLeft = left;
            ImageTop = top;
        }

        public Point MapPoint(Point point)
        {
            UpdateImageBounds();

            var x = point.X / ImageSize.Width;
            var y = point.Y / ImageSize.Height;

            x = ImageLeft + (ImageWidth * x);
            y = ImageTop + (ImageHeight * y);

            return new Point(x, y);
        }

        public bool IsValid()
        {
            return ImageSize.Width > 0 &&
                ImageSize.Height > 0 &&
                ContainerSize.Width > 0 &&
                ContainerSize.Height > 0;
        }

        public double MapLength(double length)
        {
            UpdateImageBounds();

            var len = length / ImageSize.Width;
            len = len * ImageWidth;

            return len;
        }
    }

}