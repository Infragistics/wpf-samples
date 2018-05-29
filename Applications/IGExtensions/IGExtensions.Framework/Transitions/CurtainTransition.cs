using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using IGExtensions.Framework.Effects;
using IGExtensions.Framework.Tools;

namespace IGExtensions.Framework.Transitions
{
    public class CurtainTransition : Transition
    {
        public override TransitionState CreateTransitionState()
        {
            return new CurtainTransitionState();
        }

        public bool Opening { get; set; }
    }

    internal class CurtainTransitionState : TransitionState
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

                    var effect = UIElement.Effect as XamCurtainEffect;

                    if (effect == null)
                    {
                        effect = new XamCurtainEffect();
                        UIElement.Effect = effect;
                    }

                    effect.Opening = (Transition as CurtainTransition).Opening ? 1.0 : 0.0;
                    effect.Progress = p;
                }
            }
        }
        private double progress;
    }
}
