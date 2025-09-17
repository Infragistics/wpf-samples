using System;
using System.Collections;
using System.Collections.Generic; 
using Xamarin.Forms;

namespace Infragistics.Framework 
{
    /// <summary>
    /// Represents behavior that changes visibility of <see cref="VisualElement"/> depending on
    /// <para>orientations Landscape|Portrait </para> 
    /// <para>screen types Phone|Tablet|Desktop </para> 
    /// <example> 
    /// <ix:VisibilityBehavior IsVisibleInLandscape="True" IsVisibleInPortiait="True" 
    ///                        IsVisibleOnPhone="True"     IsVisibleOnTablet="True"/>
    /// </example>
    /// </summary>
    public class VisibilityBehavior : Behavior<VisualElement>
    {
        public VisibilityBehavior()
        {
            Dictionary = new Dictionary<Guid, VisibilityInfo>();

            //Mobile.OrientationChanged += OnMobileOrientationChanged;
        }

        void OnMobileOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            foreach (var info in Dictionary.Values)
            {
                info.UpdateView();
            }
        }
        protected static Dictionary<Guid, VisibilityInfo> Dictionary;
        #region Properties

        static readonly BindableProperty IsVisibleOnPhoneProperty =
            BindableProperty.CreateAttached("IsVisibleOnPhone", typeof(bool),
            typeof(VisibilityBehavior), true, BindingMode.TwoWay, null,
            IsVisibleOnPhoneChanged, null, null);

        public bool IsVisibleOnPhone
        {
            get { return (bool)GetValue(IsVisibleOnPhoneProperty); }
            set { SetValue(IsVisibleOnPhoneProperty, value); }
        } 
        #endregion

        #region Event Handlers
        protected override void OnAttachedTo(VisualElement view)
        {
            Logs.Trace(view, "attaching visibility behavior...");
            Logs.Trace(view, "IsVisibleOnPhone: " + IsVisibleOnPhone);
            
            if (!Dictionary.ContainsKey(view.Id))
            {
                var info = new VisibilityInfo { View = view };
                info.IsVisibleOnPhone = this.IsVisibleOnPhone;

                info.UpdateView();

                Dictionary.Add(view.Id, info);
            }
        }

        protected override void OnDetachingFrom(VisualElement view)
        {
            Logs.Trace(view, "detaching visibility behavior...");

            if (Dictionary.ContainsKey(view.Id))
                Dictionary.Remove(view.Id); 
        }

        private static void IsVisibleOnPhoneChanged(BindableObject bindable, object oldValue, object newValue)
        {
        
            
        }
        
        #endregion

        #region Methods
        protected static void UpdateVisibility(VisualElement element)
        {
            var isVisible = true;
            var isVisibleOnPhone = (bool)element.GetValue(IsVisibleOnPhoneProperty);

            if (Device.Idiom == TargetIdiom.Phone)
            {
                isVisible = isVisibleOnPhone;
            }

            element.IsVisible = isVisible;
        } 
        #endregion
  
    }
    
    /// <summary>
    /// Represents info about visibility of a visual element depending on:
    /// <para>Device orientation: Landscape|Portrait </para> 
    /// <para>Device screen type: Phone|Tablet|Desktop </para> 
    /// </summary>
    public class VisibilityInfo
    {
        public VisibilityInfo()
        {
            IsVisibleOnPhone = true;
            IsVisibleOnTablet = true;
            IsVisibleOnDesktop = true;

            IsVisibleInLandscape = true;
            IsVisibleInPortrait = true;
        }
        #region Properties
        public VisualElement View { get; set; }

        /// <summary> Gets or sets whether element is visible on phone </summary>
        public bool IsVisibleOnPhone { get; set; }
        /// <summary> Gets or sets whether element is visible on Tablet </summary>
        public bool IsVisibleOnTablet { get; set; }
        /// <summary> Gets or sets whether element is visible on Desktop </summary>
        public bool IsVisibleOnDesktop { get; set; }

        /// <summary> Gets or sets whether element is visible in Landscape orientation</summary>
        public bool IsVisibleInLandscape { get; set; }
        /// <summary> Gets or sets whether element is visible in Portrait orientation</summary>
        public bool IsVisibleInPortrait { get; set; }

        #endregion 
        public void UpdateView()
        {
            if (View == null) return;

            var visibleOnScreen = true;
            switch (Mobile.Idiom)
            {
                case TargetIdiom.Phone: visibleOnScreen = IsVisibleOnPhone; break;
                case TargetIdiom.Tablet: visibleOnScreen = IsVisibleOnTablet; break;
                case TargetIdiom.Desktop: visibleOnScreen = IsVisibleOnDesktop; break;
            }

            var visibleInOrientation = true;
            //switch (Mobile.Orientation)
            //{
            //    case DeviceOrientation.Landscape: visibleInOrientation = IsVisibleOnPhone; break;
            //    case DeviceOrientation.Portrait: visibleInOrientation = IsVisibleOnTablet; break;
            //}

            View.IsVisible = visibleOnScreen && visibleInOrientation;
        }
    }
     
}