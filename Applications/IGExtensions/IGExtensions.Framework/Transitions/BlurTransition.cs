using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public class BlurTransition : Transition
    {
        public override TransitionState CreateTransitionState()
        {
            return new BlurTransitionState();
        }
    }

    internal class BlurTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;

                if (double.IsNaN(Progress))
                {
                    UIElement.Opacity = 1.0;    // remove everything
                    UIElement.Effect = null;
                }
                else
                {
                    IEasingFunction easingFunction = Transition.EasingFunction;
                    double p = MathTool.Clamp(easingFunction != null ? easingFunction.Ease(Progress) : Progress, 0.0, 1.0);

                    UIElement.Opacity = p;

                    if (p == 0.0 || p == 1.0)
                    {
                        UIElement.Effect = null;
                    }
                    else
                    {
                        BlurEffect blurEffect = UIElement.Effect as BlurEffect;

                        if (blurEffect == null)
                        {
                            blurEffect = new BlurEffect();
                            UIElement.Effect = blurEffect;
                        }

                        blurEffect.Radius = 40.0 * (1.0 - p);
                    }
                }
            }
        }
        private double progress;
    }
}
