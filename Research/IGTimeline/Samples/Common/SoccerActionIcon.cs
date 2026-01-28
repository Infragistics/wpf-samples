using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IGTimeline.Samples.Common
{
    [TemplateVisualState(GroupName = "ActionStates", Name = "GoalState")]
    [TemplateVisualState(GroupName = "ActionStates", Name = "RedCardState")]
    [TemplateVisualState(GroupName = "ActionStates", Name = "YellowCardState")]
    [TemplateVisualState(GroupName = "ActionStates", Name = "FoulState")]
    public class SoccerActionIcon : Control, INotifyPropertyChanged
    {
        public SoccerActionIcon()
        {
            base.DefaultStyleKey = typeof(SoccerActionIcon);
            this.Loaded += new RoutedEventHandler(SoccerActionIcon_Loaded);
        }

        void SoccerActionIcon_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "GoalState", true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #region FootballAction (Dependency Property)

        public static readonly DependencyProperty FootballActionProperty =
            DependencyProperty.Register("FootballAction", typeof(FootballEntryType), typeof(SoccerActionIcon), new PropertyMetadata(new PropertyChangedCallback(FootballAction_Changed)));

        public FootballEntryType FootballAction
        {
            get { return (FootballEntryType)GetValue(FootballActionProperty); }
            set { SetValue(FootballActionProperty, value); }
        }

        private static void FootballAction_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SoccerActionIcon ownerObj = (SoccerActionIcon)obj;
            ownerObj.OnPropertyChanged("FootballAction");

            VisualStateManager.GoToState(ownerObj, e.NewValue.ToString() + "State", true);
        }

        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

    }
}
