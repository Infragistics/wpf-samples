using System;
using System.Windows;
using System.Windows.Controls;

namespace IGDockManager.CustomControls 
{
    public class XdmStartPage : ContentControl
    {
        static XdmStartPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XdmStartPage), new FrameworkPropertyMetadata(typeof(XdmStartPage)));
        }

        #region Dependency Properties
        #region Header (Dependency Property)

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(XdmStartPage),
               new FrameworkPropertyMetadata((string)String.Empty));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion Header

        #endregion Dependency Properties
    }
}
