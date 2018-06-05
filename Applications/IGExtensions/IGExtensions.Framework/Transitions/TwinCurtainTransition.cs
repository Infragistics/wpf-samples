using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using IGExtensions.Framework.Effects;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public class TwinCurtainTransition : Transition
    {
        public override TransitionState CreateTransitionState()
        {
            return new TwinCurtainTransitionState();
        }

        public bool Opening { get; set; }
    }

    internal class TwinCurtainTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;

                if (double.IsNaN(Progress))
                {
                    UIElement.Effect = null;
                }
                else
                {
                    IEasingFunction easingFunction = Transition.EasingFunction;

                    double p = easingFunction != null ? easingFunction.Ease(Progress) : Progress;

                    var effect = UIElement.Effect as XamTwinCurtainEffect;

                    if (effect == null)
                    {
                        effect = new XamTwinCurtainEffect();
                        UIElement.Effect = effect;
                    }

                    effect.Opening = (Transition as TwinCurtainTransition).Opening ? 1.0 : 0.0;
                    effect.Progress = p;
                }
            }
        }
        private double progress;
    }
}
