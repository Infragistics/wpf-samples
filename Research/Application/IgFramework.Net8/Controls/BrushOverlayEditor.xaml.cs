using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls;

namespace Infragistics.Framework
{ 
    public partial class BrushOverlayEditor : PropEditor
    {
        public BrushOverlayEditor()
        {
            InitializeComponent();
            UpdateBrushes();
        }

        #region Properties
        public Brush ActualAnnotationBackground
        {
            get { return (Brush)GetValue(ActualAnnotationBackgroundProperty); }
            set { SetValue(ActualAnnotationBackgroundProperty, value); }
        }
        public static readonly DependencyProperty ActualAnnotationBackgroundProperty = DependencyProperty.Register(
            "ActualAnnotationBackground", typeof(Brush), typeof(BrushOverlayEditor), 
            new PropertyMetadata(null, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));


        public Brush AnnotationBackground
        {
            get { return (Brush)GetValue(AnnotationBackgroundProperty); }
            set { SetValue(AnnotationBackgroundProperty, value); }
        }
        public static readonly DependencyProperty AnnotationBackgroundProperty = DependencyProperty.Register(
            "AnnotationBackground", typeof(Brush), typeof(BrushOverlayEditor), 
            new PropertyMetadata(null, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        public double AnnotationBackgroundShift
        {
            get { return (double)GetValue(AnnotationBackgroundShiftProperty); }
            set { SetValue(AnnotationBackgroundShiftProperty, value); }
        }
        public static readonly DependencyProperty AnnotationBackgroundShiftProperty = DependencyProperty.Register(
            "AnnotationBackgroundShift", typeof(double), typeof(BrushOverlayEditor), 
            new PropertyMetadata(double.NaN, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));


        public AnnotationAppearanceMode AnnotationBackgroundMode
        {
            get { return (AnnotationAppearanceMode)GetValue(AnnotationBackgroundModeProperty); }
            set { SetValue(AnnotationBackgroundModeProperty, value); }
        }
        public static readonly DependencyProperty AnnotationBackgroundModeProperty = DependencyProperty.Register(
            "AnnotationBackgroundMode", typeof(AnnotationAppearanceMode), typeof(BrushOverlayEditor),
            new PropertyMetadata(AnnotationAppearanceMode.Auto, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        public Brush ActualAnnotationTextColor
        {
            get { return (Brush)GetValue(ActualAnnotationTextColorProperty); }
            set { SetValue(ActualAnnotationTextColorProperty, value); }
        }
        public static readonly DependencyProperty ActualAnnotationTextColorProperty = DependencyProperty.Register(
            "ActualAnnotationTextColor", typeof(Brush), typeof(BrushOverlayEditor), 
            new PropertyMetadata(null, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        public Brush AnnotationTextColor
        {
            get { return (Brush)GetValue(AnnotationTextColorProperty); }
            set { SetValue(AnnotationTextColorProperty, value); }
        }
        public static readonly DependencyProperty AnnotationTextColorProperty = DependencyProperty.Register(
            "AnnotationTextColor", typeof(Brush), typeof(BrushOverlayEditor), 
            new PropertyMetadata(null, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        public double AnnotationTextColorShift
        {
            get { return (double)GetValue(AnnotationTextColorShiftProperty); }
            set { SetValue(AnnotationTextColorShiftProperty, value); }
        }
        public static readonly DependencyProperty AnnotationTextColorShiftProperty = DependencyProperty.Register(
            "AnnotationTextColorShift", typeof(double), typeof(BrushOverlayEditor), 
            new PropertyMetadata(double.NaN, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        public AnnotationAppearanceMode AnnotationTextColorMode
        {
            get { return (AnnotationAppearanceMode)GetValue(AnnotationTextColorModeProperty); }
            set { SetValue(AnnotationTextColorModeProperty, value); }
        }
        public static readonly DependencyProperty AnnotationTextColorModeProperty = DependencyProperty.Register(
            "AnnotationTextColorMode", typeof(AnnotationAppearanceMode), typeof(BrushOverlayEditor),
            new PropertyMetadata(AnnotationAppearanceMode.Auto, (sender, e) =>
            {
                ((BrushOverlayEditor)sender).RaisePropertyChanged(e);
            }));

        #endregion

        private bool IsUpdating = false;
        private void UpdateBrushes()
        {
            if (IsUpdating) return;

            IsUpdating = true;
            if (AnnotationBackground != null)
            {
                ActualAnnotationBackground = AnnotationBackground;
            }
            else
            {
                var defaultBrush = Brushes.White;
                ActualAnnotationBackground = Shift(defaultBrush, AnnotationBackgroundShift, AnnotationBackgroundMode);
            }

            if (AnnotationTextColor != null)
            {
                ActualAnnotationTextColor = AnnotationTextColor;
            }
            else
            {
                Brush bg = ActualAnnotationBackground;
                Brush fg = null;
                ActualAnnotationTextColor = GetForeground(bg, fg, AnnotationTextColorShift, AnnotationTextColorMode);

            }
            IsUpdating = false;
        }

        public static Color LightColor = Color.FromArgb(255, 0, 0, 0);
        public static Color DarkColor = Color.FromArgb(255, 255, 255, 255);

        public static Brush GetContrasting(Brush brush)
        {
            var solid = brush as SolidColorBrush;
            if (solid != null && (solid.Opacity < 0.5 || solid.Color.A < 100))
            {
                return new SolidColorBrush { Color = LightColor };
            }
            return BrushUtil.GetContrasting(brush, DarkColor, LightColor);
        }

        /// <summary>
        /// Get apply shift to text color or if not specified, find contrasting color to the text background 
        /// </summary> 
        public static Brush GetForeground(Brush textBacground, Brush textColor, double textShift, AnnotationAppearanceMode textColorMode)
        {
            if (textColor == null)
                return GetContrasting(textBacground);
            else
                return Shift(textColor, textShift, textColorMode);
        }


        internal static Brush Shift(Brush brush,
            double shiftAmount,
            AnnotationAppearanceMode mode)
        {
            if (brush != null && !double.IsNaN(shiftAmount) && shiftAmount != 0)
            {
                if (mode == AnnotationAppearanceMode.Auto ||
                    mode == AnnotationAppearanceMode.BrightnessShift)
                    return BrushUtil.GetLightened(brush, shiftAmount);
                else if (mode == AnnotationAppearanceMode.SaturationShift)
                    return BrushUtil.GetSaturated(brush, shiftAmount);
                else if (mode == AnnotationAppearanceMode.OpacityShift ||
                         mode == AnnotationAppearanceMode.DashPattern)
                {
                    if (shiftAmount < 0) shiftAmount = 0;
                    if (shiftAmount > 1) shiftAmount = 1;
                    return BrushUtil.ModifyOpacity(brush, shiftAmount);
                }
            }
            return brush;
        }

        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            System.Diagnostics.Debug.WriteLine("BrushOverlayEditor " + propertyName);

        }
        public override void RaisePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.RaisePropertyChanged(e);

            UpdateBrushes();
            System.Diagnostics.Debug.WriteLine("BrushOverlayEditor " + e.Property.Name);

            if (e.Property.Name == "Value" || e.Property.Name == "Precision")
            {
              //  var decimals = string.Concat(System.Linq.Enumerable.Repeat("0", Precision));
                //FormattedValue = Value.ToString("0." + decimals);
            }

            if (e.Property.Name == "Interval")
            {
              //  IsInteger = Interval == 1;
            }
        } 
    }

    





}
