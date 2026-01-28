using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.SamplesBrowser.Controls
{
    /// <summary>
    /// A Button-derived control that adds an IsMenuExpanded property for custom template triggering.
    ///
    /// </summary>
    public class MenuExpansionButton : Button
    {
        static MenuExpansionButton()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuExpansionButton), new FrameworkPropertyMetadata(typeof(MenuExpansionButton)));
        }

        #region IsMenuExpanded
        /// <summary>
        /// Identifies the 'IsMenuExpanded' dependency property
        /// </summary>
        public static readonly DependencyProperty IsMenuExpandedProperty = DependencyProperty.Register("IsMenuExpanded",
        typeof(bool), typeof(MenuExpansionButton), new FrameworkPropertyMetadata());

        /// <summary>
        /// The image that was selected
        /// </summary>
        [Description("Indicates whether the menu is expanded or not.")]
        [Category("Behavior")]
        public bool IsMenuExpanded
        {
            get
            {
                return (bool)this.GetValue(MenuExpansionButton.IsMenuExpandedProperty);
            }
            set
            {
                this.SetValue(MenuExpansionButton.IsMenuExpandedProperty, value);
            }
        }

        #endregion IsMenuExpanded
    }
}
