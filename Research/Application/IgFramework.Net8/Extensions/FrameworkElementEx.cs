using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows.Media.Animation;

namespace System.Windows
{
    public static class FrameworkElementEx
    {
        public static void AnimateBySize(this FrameworkElement target, 
            double deltaWidth, double deltaHeight, double seconds = 2.0,
            EventHandler onCompleted = null)
        {
            var h = target.ActualHeight + deltaHeight;
            var w = target.ActualWidth + deltaWidth;

            target.AnimateToSize(w, h, seconds, onCompleted);
        }

        public static void AnimateByRatio(this FrameworkElement target, 
            double ratioWidth, double ratioHeight, double seconds = 2.0,
            EventHandler onCompleted = null)
        {
            var h = target.ActualHeight * ratioHeight;
            var w = target.ActualWidth * ratioWidth;

            target.AnimateToSize(w, h, seconds, onCompleted);
        }

        public static void AnimateToSize(this FrameworkElement target, 
            double newWidth, double newHeight, double seconds = 2.0,
            EventHandler onCompleted = null)
        {
            var span = TimeSpan.FromMilliseconds(seconds * 1000);
            //var span2 = TimeSpan.FromMilliseconds(200);

            var sb = new Storyboard { }; // Duration = new Duration(span) };

            var animateW = new DoubleAnimationUsingKeyFrames();
            var animateH = new DoubleAnimationUsingKeyFrames();


            //animateW.Duration  = new Duration(span);
            //animateH.Duration = new Duration(span);

            var spanStart = TimeSpan.FromMilliseconds(0);
            var spanEnd = span; // TimeSpan.FromMilliseconds(2000);

            var keyStart = KeyTime.FromTimeSpan(spanStart);
            var keyEnd = KeyTime.FromTimeSpan(spanEnd);

            animateH.KeyFrames.Add(new EasingDoubleKeyFrame(target.ActualHeight, keyStart));
            animateH.KeyFrames.Add(new EasingDoubleKeyFrame(newHeight, keyEnd));
            animateW.KeyFrames.Add(new EasingDoubleKeyFrame(target.ActualWidth, keyStart));
            animateW.KeyFrames.Add(new EasingDoubleKeyFrame(newWidth, keyEnd));

            Storyboard.SetTarget(animateW, target);
            Storyboard.SetTargetProperty(animateW, new PropertyPath(FrameworkElement.WidthProperty));

            Storyboard.SetTarget(animateH, target);
            Storyboard.SetTargetProperty(animateH, new PropertyPath(FrameworkElement.HeightProperty));

            sb.Children.Add(animateW);
            sb.Children.Add(animateH);
             
            if (onCompleted != null)
            {
                sb.Completed += onCompleted;
               
            }
            //sb.Completed += (s, a) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Completed Event Fired");
            //    ///sbFadeIn.Stop(this);    //<-- This does not cause the storyboard animation to stop
            //};

            sb.Begin();
        }

        private static void Sb_Completed(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
