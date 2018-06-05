
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using IGExtensions.Framework.Tools;
using System.Windows.Media;

namespace IGExtensions.Framework.Transitions
{
    public class TransitionService : DependencyObject
    {
        #region constructors
        private TransitionService()
        {
        }

        static TransitionService()
        {
            CompositionTarget.Rendering += (o, e) => Tick();
        }
        #endregion

        #region Appearance Attached Dependency Property
        private const string AppearancePropertyName = "Appearance";

        public static void SetAppearance(DependencyObject obj, Transition transition)
        {
            if (obj != null)
            {
                obj.SetValue(AppearanceProperty, transition);
            }
        }

        public static Transition GetAppearance(DependencyObject obj)
        {
            return obj != null?  (Transition)obj.GetValue(AppearanceProperty): null;
        }

        public static readonly DependencyProperty AppearanceProperty = DependencyProperty.RegisterAttached(AppearancePropertyName, typeof(Transition), typeof(TransitionService),
            new PropertyMetadata((o, e) =>
            {
            }));
        #endregion

        #region Disappearance Attached Dependency Property
        public static void SetDisappearance(UIElement obj, Transition transition)
        {
            if (obj != null)
            {
                obj.SetValue(DisappearanceProperty, transition);
            }
        }

        public static Transition GetDisappearance(UIElement obj)
        {
            return obj != null ? (Transition)obj.GetValue(DisappearanceProperty) : null;
        }

        public static readonly DependencyProperty DisappearanceProperty = DependencyProperty.RegisterAttached(DisappearancePropertyName, typeof(Transition), typeof(TransitionService),
            new PropertyMetadata((o, e) =>
            {
            }));

        private const string DisappearancePropertyName = "Disppearance";
        #endregion

        /// <summary>
        /// Triggers the appearance of a UIElement.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="done"></param>
        public static void Appear(UIElement uiElement, Action<UIElement> done)
        {
            DateTime startTime = DateTime.Now;
            Transition transition = GetAppearance(uiElement);

            TransitionState transitionState;

            if (activeDisappearances.TryGetValue(uiElement, out transitionState))
            {
                if (transition != null)
                {
                    startTime = startTime - TimeSpan.FromSeconds(transitionState.Progress * transition.Duration.TimeSpan.TotalSeconds);
                }

                transitionState.Progress = double.NaN;
                activeDisappearances.Remove(uiElement);
            }

            if (activeAppearances.TryGetValue(uiElement, out transitionState))
            {
                transitionState.Progress = double.NaN;
                startTime = transitionState.StartTime;
                activeAppearances.Remove(uiElement);
            }

            if (transition != null)
            {
                transitionState = transition.CreateTransitionState();

                activeAppearances.Add(uiElement, transitionState);

                transitionState.Transition = transition;
                transitionState.UIElement = uiElement;
                transitionState.StartTime = startTime;
                transitionState.EndTime = transitionState.StartTime + transition.Duration.TimeSpan;
                transitionState.Done = done;
                transitionState.Progress = 0.0;
            }
            else
            {
                if (done != null)
                {
                    done(uiElement);
                }
            }
        }

        /// <summary>
        /// Triggers the disappearance of a UIElement.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="done"></param>
        public static void Disappear(UIElement uiElement, Action<UIElement> done)
        {
            DateTime startTime = DateTime.Now;
            Transition transition = GetDisappearance(uiElement);
            TransitionState transitionState = null;

            if (activeDisappearances.TryGetValue(uiElement, out transitionState))
            {
                transitionState.Progress = double.NaN;
                activeDisappearances.Remove(uiElement);
            }

            if (activeAppearances.TryGetValue(uiElement, out transitionState))
            {
                if (transition != null)
                {
                    startTime = startTime - TimeSpan.FromSeconds((1.0 - transitionState.Progress) * transition.Duration.TimeSpan.TotalSeconds);
                }

                transitionState.Progress = double.NaN;
                activeAppearances.Remove(uiElement);
            }

            if (transition != null)
            {
                transitionState = transition.CreateTransitionState();
                activeDisappearances.Add(uiElement, transitionState);

                transitionState.Transition = transition;
                transitionState.UIElement = uiElement;
                transitionState.StartTime = startTime;
                transitionState.EndTime = transitionState.StartTime + transition.Duration.TimeSpan;
                transitionState.Done = done;
                transitionState.Progress = 1.0;
            }
            else
            {
                if (done != null)
                {
                    done(uiElement);
                }
            }
        }

        /// <summary>
        /// Cancels a UIElements appearance or disappearance.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        public static bool Cancel(UIElement uiElement, bool done)
        {
            Dictionary<UIElement, TransitionState> active = activeAppearances.ContainsKey(uiElement) ? activeAppearances :
                activeDisappearances.ContainsKey(uiElement) ? activeDisappearances :
                null;

            if (active != null)
            {
                TransitionState transitionState = active[uiElement];

                transitionState.Progress = double.NaN;

                if (done && transitionState.Done != null)
                {
                    transitionState.Done(uiElement);
                }

                active.Remove(uiElement);

                return true;
            }

            return false;
        }

        private static Dictionary<UIElement, TransitionState> activeAppearances = new Dictionary<UIElement, TransitionState>();
        private static Dictionary<UIElement, TransitionState> activeDisappearances = new Dictionary<UIElement, TransitionState>();

        private static void Tick()
        {
            DateTime now = DateTime.Now;
            List<UIElement> done = null;

            #region appearances
            foreach (TransitionState transitionState in activeAppearances.Values)
            {
                double p = MathTool.Clamp((now - transitionState.StartTime).TotalSeconds / (transitionState.EndTime - transitionState.StartTime).TotalSeconds, 0.0, 1.0);

                if (p == 1.0)
                {
                    done=done?? new List<UIElement>();
                    done.Add(transitionState.UIElement);

                    transitionState.Progress = double.NaN;

                    if (transitionState.Done != null)
                    {
                        transitionState.Done(transitionState.UIElement);
                    }
                }
                else
                {
                    transitionState.Progress = p;
                }
            }

            if(done!=null)
            {
                foreach (UIElement uiElement in done)
                {
                    activeAppearances.Remove(uiElement);
                }

                done.Clear();
            }
            #endregion

            #region disappearances
            foreach (TransitionState transitionState in activeDisappearances.Values)
            {
                double p = MathTool.Clamp((now - transitionState.StartTime).TotalSeconds / (transitionState.EndTime - transitionState.StartTime).TotalSeconds, 0.0, 1.0);

                if (p == 1.0)
                {
                    done=done?? new List<UIElement>();
                    done.Add(transitionState.UIElement);

                    transitionState.Progress = double.NaN;
                    
                    if (transitionState.Done != null)
                    {
                        transitionState.Done(transitionState.UIElement);
                    }
                }
                else
                {
                    transitionState.Progress = 1.0-p;
                }
            }

            if(done!=null)
            {
                foreach (UIElement uiElement in done)
                {
                    activeDisappearances.Remove(uiElement);
                }
            }
            #endregion
        }
    }
}

