using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace IGExtensions.Common.Frameworks
{
    public static class AnimationFramework
    {
        /// <summary>
        /// Animate a property of <see cref="DependencyObject"/> using <see cref="DoubleAnimation"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <param name="targetValue"></param>
        /// <param name="seconds"></param>
        public static void AnimateProperty(DependencyObject target, string propertyName, double targetValue, double seconds = 0.2)
        {
            var da = new DoubleAnimation
            {
                To = targetValue,
                Duration = TimeSpan.FromSeconds(seconds)
            };
            var sb = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(seconds)
            };
            sb.Children.Add(da);
            Storyboard.SetTarget(da, target);
            Storyboard.SetTargetProperty(da, new PropertyPath(propertyName));

            sb.Begin();
        }
    }
}