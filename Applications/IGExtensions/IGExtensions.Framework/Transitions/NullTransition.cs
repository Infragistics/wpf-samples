
namespace IGExtensions.Framework.Transitions
{
    public class NullTransition : Transition
    {
        public override TransitionState CreateTransitionState()
        {
            return new NullTransitionState();
        }
    }

    internal class NullTransitionState : TransitionState
    {
        public override double Progress
        {
            get { return progress; }
            set
            {
                progress = value;
            }
        }
        private double progress;
    }
}
