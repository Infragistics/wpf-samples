using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace IgWord.Infrastructure
{
    public class ViewBaseWithAnimations : ViewBase
    {
        private TranslateTransform translateTransform;

        public bool EnableOpacityAnimation { get; set; }
        public bool EnableSlideAnimation { get; set; }
        public double TranslateXAnimationFrom { get; set; }
        public double AnimationDuration { get; set; }

        public ViewBaseWithAnimations()
        {
            EnableOpacityAnimation = true;
            EnableSlideAnimation = true;
            TranslateXAnimationFrom = 50;
            AnimationDuration = 250;

            this.translateTransform = new TranslateTransform();
            this.RenderTransform = translateTransform;
            this.Loaded += ViewBaseWithAnimations_Loaded;
        }

        void ViewBaseWithAnimations_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (EnableOpacityAnimation)
            {
                var opacityAnimation = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromMilliseconds(AnimationDuration) };
                this.BeginAnimation(UserControl.OpacityProperty, opacityAnimation);
            }
            if (EnableSlideAnimation)
            {
                var translateXAnimation = new DoubleAnimation { From = TranslateXAnimationFrom, To = 0, Duration = TimeSpan.FromMilliseconds(AnimationDuration) };
                this.translateTransform.BeginAnimation(TranslateTransform.XProperty, translateXAnimation);
            }
        }
    }
}
