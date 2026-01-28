using System.ComponentModel;
using System.Windows;

namespace IGDockManager.CustomControls
{
    public class MainMenuButton : System.Windows.Controls.Button
    {
        static MainMenuButton()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainMenuButton), new FrameworkPropertyMetadata(typeof(MainMenuButton)));
        }

        #region IsActiveItem
        /// <summary>
        /// Identifies the 'IsActiveItem' dependency property
        /// </summary>
        public static readonly DependencyProperty IsActiveItemProperty = DependencyProperty.Register("IsActiveItem",
        typeof(bool), typeof(MainMenuButton), new FrameworkPropertyMetadata());

        /// <summary>
        /// The image that was selected
        /// </summary>
        [Description("A property that notes whether this is the active menu item")]
        [Category("Behavior")]
        public bool IsActiveItem
        {
            get
            {
                return (bool)this.GetValue(MainMenuButton.IsActiveItemProperty);
            }
            set
            {
                this.SetValue(MainMenuButton.IsActiveItemProperty, value);
            }
        }

        #endregion IsActiveItem
    }
}
