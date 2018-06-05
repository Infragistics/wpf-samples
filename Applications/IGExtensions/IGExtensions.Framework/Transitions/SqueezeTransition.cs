using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public enum SqueezeTransitionDirection
    {
        Left,
        Down,
        Right,
        Up
    }

    public class SqueezeTransition : Transition
    {
        #region Direction Dependency Property
        /// <summary>
        /// Sets or gets the Direction for this Transition.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public SqueezeTransitionDirection Direction
        {
            get
            {
                return (SqueezeTransitionDirection)GetValue(DirectionProperty);
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
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(DirectionPropertyName, typeof(SqueezeTransitionDirection), typeof(Transition),
            new PropertyMetadata((sender, e) =>
            {
                //(sender as Transition).OnPropertyChanged(new PropertyChangedEventArgs<SqueezeTransitionDirection>(DirectionPropertyName, (SqueezeTransitionDirection)e.OldValue, (SqueezeTransitionDirection)e.NewValue));
            }));
        #endregion

        public override TransitionState CreateTransitionState()
        {
            return new SqueezeTransitionState();
        }
    }

    internal class SqueezeTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;

                if (double.IsNaN(Progress))
                {
                    // remove everything

                    UIElement.RenderTransform = null;
                    UIElement.Opacity = 1.0;
                }
                else
                {
                    IEasingFunction easingFunction = Transition.EasingFunction;
                    double p = easingFunction != null ? easingFunction.Ease(Progress) : Progress;

                    ScaleTransform transform = UIElement.RenderTransform as ScaleTransform;

                    if (transform == null)
                    {
                        transform = new ScaleTransform();
                        UIElement.RenderTransform = transform;
                    }

                    switch (((SqueezeTransition)Transition).Direction)
                    {
                        case SqueezeTransitionDirection.Left:
                            transform.ScaleX = p;
                            transform.ScaleY = 1.0;
                            transform.CenterX = 0.0;
                            transform.CenterY = 0.0;
                            break;

                        case SqueezeTransitionDirection.Right:
                            transform.ScaleX = p;
                            transform.ScaleY = 1.0;
                            transform.CenterX = UIElement.RenderSize.Width;
                            transform.CenterY = 0.0;
                            break;

                        case SqueezeTransitionDirection.Down:
                            transform.ScaleX = 1.0;
                            transform.ScaleY = p;
                            transform.CenterX = 0.0;
                            transform.CenterY = 0.0;
                            break;

                        case SqueezeTransitionDirection.Up:
                            transform.ScaleX = 1.0;
                            transform.ScaleY = p;
                            transform.CenterX = 0.0;
                            transform.CenterY = UIElement.RenderSize.Height;
                            break;
                    }

                    UIElement.Opacity = MathTool.Clamp(p * 1.5, 0.0, 1.0);
                }
            }
        }
        private double progress;
    }
}
