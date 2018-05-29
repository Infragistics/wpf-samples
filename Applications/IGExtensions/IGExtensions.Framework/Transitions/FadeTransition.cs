using System.Windows;
using System.Windows.Media.Animation;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public class FadeTransition : Transition
    {
        public override TransitionState CreateTransitionState()
        {
            return new FadeTransitionState();
        }
    }

    internal class FadeTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;

                if (double.IsNaN(Progress))
                {
                    UIElement.Opacity = 1.0;
                }
                else
                {
                    IEasingFunction easingFunction = Transition.EasingFunction;
                    double p = MathTool.Clamp(easingFunction != null ? easingFunction.Ease(Progress) : Progress, 0.0, 1.0);

                    UIElement.Opacity = p;
                }
            }
        }
        private double progress;
    }
}
