using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public enum SlideTransitionDirection
    {
        Left,
        Down,
        Right,
        Up
    }

    public class SlideTransition: Transition
    {
        #region Direction Dependency Property
        /// <summary>
        /// Sets or gets the Direction for this Transition.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public SlideTransitionDirection Direction
        {
            get
            {
                return (SlideTransitionDirection)GetValue(DirectionProperty);
            }
            set
            {
                SetValue(DirectionProperty, value);
            }
        }

        internal const string DirectionPropertyName = "Direction";

        /// <summary>
        /// Identifies the Direction dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(DirectionPropertyName, typeof(SlideTransitionDirection), typeof(Transition),
            new PropertyMetadata((sender, e) =>
            {
                //(sender as Transition).OnPropertyChanged(new PropertyChangedEventArgs<SlideTransitionDirection>(DirectionPropertyName, (SlideTransitionDirection)e.OldValue, (SlideTransitionDirection)e.NewValue));
            }));
        #endregion

        public override TransitionState CreateTransitionState()
        {
            return new SlideTransitionState();
        }
    }

    internal class SlideTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;

                if (double.IsNaN(progress))
                {
                    // remove everything

                    UIElement.Opacity = 1.0;
                    UIElement.RenderTransform = null;

                }
                else
                {
                    IEasingFunction easingFunction = Transition.EasingFunction;
                    double p = easingFunction != null ? easingFunction.Ease(Progress) : Progress;

                    TranslateTransform transform = UIElement.RenderTransform as TranslateTransform;

                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        UIElement.RenderTransform = transform;
                    }

                    switch (((SlideTransition)Transition).Direction)
                    {
                        case SlideTransitionDirection.Left:
                            transform.X = (p - 1.0) * UIElement.RenderSize.Width;
                            transform.Y = 0.0;
                            break;

                        case SlideTransitionDirection.Right:
                            transform.X = (1.0 - p) * UIElement.RenderSize.Width;
                            transform.Y = 0.0;
                            break;

                        case SlideTransitionDirection.Down:
                            transform.X = 0.0;
                            transform.Y = (p - 1.0) * UIElement.RenderSize.Height;
                            break;

                        case SlideTransitionDirection.Up:
                            transform.Y = (1.0 - p) * UIElement.RenderSize.Height;
                            break;
                    }

                    UIElement.Opacity = MathTool.Clamp(p * 1.5, 0.0, 1.0);
                }
            }
        }
        private double progress;
    }
}
