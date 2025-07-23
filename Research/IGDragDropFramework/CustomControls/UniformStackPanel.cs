using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace IGDragDropFramework.CustomControls
{
    public class UniformStackPanel : Panel
    {
        #region Fields
        private TimeSpan _defaultStartupAnimationDuration;
        private CubicEase _defaultEasingFunction;
        #endregion Fields

        public UniformStackPanel()
        {
            this.StartupAnimationDuration = TimeSpan.FromMilliseconds(750);
            this._defaultStartupAnimationDuration = TimeSpan.FromMilliseconds(750);
            this._defaultEasingFunction = new CubicEase();
            this._defaultEasingFunction.EasingMode = EasingMode.EaseOut;
            this.IsInitialLayout = true;
            this.AnimateItemsOnLoad = true;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.OverrideDimensions = false;
        }

        #region Public Methods

        #endregion Public Methods

        #region Private Methods
        private void CreateClippingPath()
        {
            RectangleGeometry clippingRect = new RectangleGeometry();
            clippingRect.Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);

            this.Clip = clippingRect;
            this.UpdateLayout();
        }

        private void GenerateStartupAnimations(UIElement element, int itemIndex, Storyboard targetStoryboard)
        {
            element.UpdateLayout();

            // Add transform group to element
            TransformGroup tg = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            SkewTransform skt = new SkewTransform();
            RotateTransform rt = new RotateTransform();
            TranslateTransform tt = new TranslateTransform();
            tg.Children.Add(st);
            tg.Children.Add(skt);
            tg.Children.Add(rt);
            tg.Children.Add(tt);

            element.RenderTransform = tg;

            // Calculate KeyTimes
            double timeSegment = (this.StartupAnimationDuration.TotalMilliseconds / this.Children.Count);
            double offset = (timeSegment * this.ItemAnimationOverlapPercentage);
            if (itemIndex == 0)
                offset = 0;

            TimeSpan initTime, startTime, endTime;
            initTime = TimeSpan.FromMilliseconds(0);

            // Start Time
            if (this.StartupAnimationDuration != null)
            {			
                startTime = TimeSpan.FromMilliseconds((timeSegment * itemIndex) - offset + this.StartupAnimationBeginTime.TotalMilliseconds);
                Debug.WriteLine("startOpacity.KeyTime: " + startTime.ToString());
            }
            else
            {
                startTime = TimeSpan.FromMilliseconds(0);
            }

            // End Time
            if (this.StartupAnimationDuration != null)
            {
                endTime = TimeSpan.FromMilliseconds(((int)((double)this.StartupAnimationDuration.TotalMilliseconds / (double)this.Children.Count) * (itemIndex + 1))
                    + this.StartupAnimationBeginTime.TotalMilliseconds);
            }
            else
            {
                endTime = this._defaultStartupAnimationDuration;
            }

            // Opacity			
            if (this.FadeInItemsOnLoad)
            {
                DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame initOpacityFrame = new EasingDoubleKeyFrame();
                EasingDoubleKeyFrame startOpacityFrame = new EasingDoubleKeyFrame();
                EasingDoubleKeyFrame endOpacityFrame = new EasingDoubleKeyFrame();

                this.InitializeKeyFrames(opacityAnimation, targetStoryboard, element,
                    "Opacity",
                    null,
                    0, 1,
                    initTime, startTime, endTime);
            }

            if (this.StartupAnimationTransform is TransformGroup)
            {
                TransformGroup group = this.StartupAnimationTransform as TransformGroup;
                for (int i = 0; i < group.Children.Count; i++)
                {
                    Transform t = group.Children[i];

                    if (t is ScaleTransform)
                    {
                        ScaleTransform scaleT = new ScaleTransform();

                        if (scaleT.ScaleX != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames scaleXAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(scaleXAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)",
                                this.EasingFunction,
                                scaleT.ScaleX, 1,
                                initTime, startTime, endTime);
                        }

                        if (scaleT.ScaleY != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames scaleYAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(scaleYAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)",
                                this.EasingFunction,
                                scaleT.ScaleY, 1,
                                initTime, startTime, endTime);
                        }

                    }
                    else if (t is SkewTransform)
                    {
                        SkewTransform skewT = t as SkewTransform;

                        if (skewT.AngleX != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames skewXAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(skewXAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[1].(SkewTransform.AngleX)",
                                this.EasingFunction,
                                skewT.AngleX, 0,
                                initTime, startTime, endTime);
                        }

                        if (skewT.AngleY != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames skewYAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(skewYAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[1].(SkewTransform.AngleY)",
                                this.EasingFunction,
                                skewT.AngleY, 0,
                                initTime, startTime, endTime);
                        }

                    }
                    else if (t is RotateTransform)
                    {
                        RotateTransform rotateT = t as RotateTransform;
                        DoubleAnimationUsingKeyFrames rotationAnimation = new DoubleAnimationUsingKeyFrames();
                        this.InitializeKeyFrames(rotationAnimation, targetStoryboard, element,
                            "(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)",
                            this.EasingFunction,
                            rotateT.Angle, 0,
                            initTime, startTime, endTime);


                    }
                    else if (t is TranslateTransform)
                    {
                        TranslateTransform transT = t as TranslateTransform;

                        if (transT.X != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames translateXAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(translateXAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)",
                                this.EasingFunction,
                                transT.X, 0,
                                initTime, startTime, endTime);
                        }

                        if (transT.Y != Double.NaN)
                        {
                            DoubleAnimationUsingKeyFrames translateYAnimation = new DoubleAnimationUsingKeyFrames();
                            this.InitializeKeyFrames(translateYAnimation, targetStoryboard, element,
                                "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)",
                                this.EasingFunction,
                                transT.Y, 0,
                                initTime, startTime, endTime);
                        }
                    }
                }
            }
        }

        private void InitializeKeyFrames(DoubleAnimationUsingKeyFrames animation, Storyboard targetStoryboard, UIElement targetElement, string propertyPath, 
            IEasingFunction easingFunction, double initValue, double finalValue, TimeSpan initTime, TimeSpan startTime, TimeSpan endTime)
        {
            EasingDoubleKeyFrame initFrame = new EasingDoubleKeyFrame();
            EasingDoubleKeyFrame startFrame = new EasingDoubleKeyFrame();
            EasingDoubleKeyFrame endFrame = new EasingDoubleKeyFrame();

            initFrame.KeyTime = initTime;
            startFrame.KeyTime = startTime;
            endFrame.KeyTime = endTime;

            initFrame.Value = startFrame.Value = initValue;
            endFrame.Value = finalValue;

            animation.KeyFrames.Add(initFrame);
            animation.KeyFrames.Add(startFrame);
            animation.KeyFrames.Add(endFrame);

            if (easingFunction != null)
            {
                endFrame.EasingFunction = easingFunction;
            }


            // Set Storyboard Target Element and Property
            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyPath));

            animation.BeginTime = TimeSpan.FromMilliseconds(0);

            // Add Animation to Parent Storyboard
            targetStoryboard.Children.Add(animation);
        }
        #endregion Private Methods

        #region Properties

        #region EasingFunction (Dependency Property)

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(UniformStackPanel), null);

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        #endregion

        #region StartupAnimationDuration (Dependency Property)

        public static readonly DependencyProperty StartupAnimationDurationProperty =
            DependencyProperty.Register("StartupAnimationDuration", typeof(TimeSpan), typeof(UniformStackPanel), null);

        public TimeSpan StartupAnimationDuration
        {
            get { return (TimeSpan)GetValue(StartupAnimationDurationProperty); }
            set { SetValue(StartupAnimationDurationProperty, value); }
        }


        #endregion

        #region StartupAnimationBeginTime (Dependency Property)

        public static readonly DependencyProperty StartupAnimationBeginTimeProperty =
            DependencyProperty.Register("StartupAnimationBeginTime", typeof(TimeSpan), typeof(UniformStackPanel), null);

        public TimeSpan StartupAnimationBeginTime
        {
            get { return (TimeSpan)GetValue(StartupAnimationBeginTimeProperty); }
            set { SetValue(StartupAnimationBeginTimeProperty, value); }
        }

        #endregion

        #region AnimateItemsOnLoad (Dependency Property)

        public static readonly DependencyProperty AnimateItemsOnLoadProperty =
            DependencyProperty.Register("AnimateItemsOnLoad", typeof(bool), typeof(UniformStackPanel), null);

        public bool AnimateItemsOnLoad
        {
            get { return (bool)GetValue(AnimateItemsOnLoadProperty); }
            set { SetValue(AnimateItemsOnLoadProperty, value); }
        }

        #endregion

        #region FadeInItemsOnLoad (Dependency Property)

        public static readonly DependencyProperty FadeInItemsOnLoadProperty =
            DependencyProperty.Register("FadeInItemsOnLoad", typeof(bool), typeof(UniformStackPanel), null);

        public bool FadeInItemsOnLoad
        {
            get { return (bool)GetValue(FadeInItemsOnLoadProperty); }
            set { SetValue(FadeInItemsOnLoadProperty, value); }
        }
        #endregion

        #region ItemAnimationOverlapPercentage (Dependency Property)

        public static readonly DependencyProperty ItemAnimationOverlapPercentageProperty =
            DependencyProperty.Register("ItemAnimationOverlapPercentage", typeof(double), typeof(UniformStackPanel), null);

        public double ItemAnimationOverlapPercentage
        {
            get { return (double)GetValue(ItemAnimationOverlapPercentageProperty); }
            set { SetValue(ItemAnimationOverlapPercentageProperty, value); }
        }
        #endregion

        #region StartupAnimationTransform (Dependency Property)

        public static readonly DependencyProperty StartupAnimationTransformProperty =
            DependencyProperty.Register("StartupAnimationTransform", typeof(Transform), typeof(UniformStackPanel), null);

        public Transform StartupAnimationTransform
        {
            get { return (Transform)GetValue(StartupAnimationTransformProperty); }
            set { SetValue(StartupAnimationTransformProperty, value); }
        }

        #endregion

        #region IsInitialLayout (Dependency Property)

        public static readonly DependencyProperty IsInitialLayoutProperty =
            DependencyProperty.Register("IsInitialLayout", typeof(bool), typeof(UniformStackPanel), null);

        public bool IsInitialLayout
        {
            get { return (bool)GetValue(IsInitialLayoutProperty); }
            set { SetValue(IsInitialLayoutProperty, value); }
        }

        #endregion

        #region HorizontalContentAlignment (Dependency Property)

        public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(UniformStackPanel), null);

        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        #endregion

        #region OverrideDimensions (Dependency Property)

        public static readonly DependencyProperty OverrideDimensionsProperty =
            DependencyProperty.Register("OverrideDimensions", typeof(bool), typeof(UniformStackPanel), null);

        public bool OverrideDimensions
        {
            get { return (bool)GetValue(OverrideDimensionsProperty); }
            set { SetValue(OverrideDimensionsProperty, value); }
        }

        #endregion


        #endregion Properties

        #region Overrides
        #region MeasureOverride
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Double.IsInfinity(availableSize.Width))
            {
                Size totalSize = new Size(0, 0);
                foreach (UIElement el in this.Children)
                {
                    el.Measure(availableSize);
                    totalSize.Height += el.DesiredSize.Height;
                    totalSize.Width += el.DesiredSize.Width;
                }

                return base.MeasureOverride(totalSize);
            }
            else
            {
                return base.MeasureOverride(availableSize);
            }
        }
        #endregion MeasureOverride

        private double initialSize = 0;

        #region ArrangeOverride
        protected override Size ArrangeOverride(Size finalSize)
        {
            int x = 0;
            int y = 0;

            int itemHeight = (int)Math.Round(finalSize.Height);
            int itemWidth = (int)Math.Round(finalSize.Width / (double)this.Children.Count);

            UIElement element;
            Storyboard startupStoryboard = new Storyboard();

            double desiredWidth = 0;
            foreach (UIElement el in this.Children)
            {
                el.Measure(finalSize);
                desiredWidth += el.DesiredSize.Width;
            }


            if (this.Children.Count == 1)
            {
                this.initialSize = this.Children[0].DesiredSize.Width;
            }

            for (int i = 0; i < this.Children.Count; i++)
            {
                element = this.Children[i];

                // y = currentRow * itemHeight;
                itemWidth = (int)Math.Round(finalSize.Width / (double)this.Children.Count);

                if (this.HorizontalContentAlignment == HorizontalAlignment.Left && initialSize * this.Children.Count <= finalSize.Width)
                {
                    itemWidth = (int)initialSize;
                }

                x = i * itemWidth;

                element.Arrange(new Rect(x, y, itemWidth, itemHeight));
                FrameworkElement el = element as FrameworkElement;
                if (el != null && this.OverrideDimensions)
                {
                    el.Width = itemWidth;
                    el.Height = itemHeight;
                }

                if (this.IsInitialLayout)
                {
                    this.GenerateStartupAnimations(element, i, startupStoryboard);
                }
            }

            if (this.AnimateItemsOnLoad)
            {
                startupStoryboard.Begin();
                Debug.WriteLine("storyboard: " + startupStoryboard.ToString());
                foreach (Timeline t in startupStoryboard.Children)
                {
                    Debug.WriteLine("   > " + Storyboard.GetTargetName(t));
                    Debug.WriteLine("        > " + Storyboard.GetTargetProperty(t).Path);
                    Debug.WriteLine("            > BeginTime: " + t.BeginTime.Value.ToString());
                    DoubleAnimationUsingKeyFrames anim = t as DoubleAnimationUsingKeyFrames;
                    if (anim != null)
                    {
                        // Debug.WriteLine("                  anim.TargetName: " + Storyboard.GetTargetName(anim).ToString());						
                        Debug.WriteLine("                  anim.TargetProp: " + Storyboard.GetTargetProperty(anim).Path.ToString());

                        foreach (EasingDoubleKeyFrame kf in anim.KeyFrames)
                        {
                            Debug.WriteLine("                  kf.KeyTime: " + kf.KeyTime.TimeSpan.ToString());
                            Debug.WriteLine("                  kf.TargetValue: " + kf.Value.ToString());
                            if (kf.EasingFunction != null)
                                Debug.WriteLine("                  kf.EasingFunction: " + kf.EasingFunction.ToString());

                        }
                    }
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        #endregion ArrangeOverride
        #endregion Overrides

    }
}