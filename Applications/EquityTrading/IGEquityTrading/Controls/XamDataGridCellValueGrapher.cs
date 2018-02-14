//-------------------------------------------------------------------------
// <copyright file="StringComparisonToBoolValueConverter.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Windows.DataPresenter;

namespace IGTrading.Controls
{
    public class CellValueGrapher : Control
    {
        #region Member Variables

        private const double MIN_VALUE = 0.0d;
        private const double MAX_VALUE = 100.0d;

        #endregion Member Variables

        #region Constructor

        public CellValueGrapher()
        {
        }

        #endregion Constructor

        #region Properties

        #region NegativeSegmentBrush

        /// <summary>
        /// Identifies the <see cref="NegativeSegmentBrush"/> dependency property
        /// </summary>
        public static readonly DependencyProperty NegativeSegmentBrushProperty = DependencyProperty.Register("NegativeSegmentBrush",
            typeof(Brush), typeof(CellValueGrapher), new FrameworkPropertyMetadata(Brushes.Red));

        /// <summary>
        /// Returns/sets the brush to use when coloring Negative segments.
        /// </summary>
        public Brush NegativeSegmentBrush
        {
            get { return (Brush)this.GetValue(CellValueGrapher.NegativeSegmentBrushProperty); }
            set { this.SetValue(CellValueGrapher.NegativeSegmentBrushProperty, value); }
        }

        #endregion NegativeSegmentBrush

        #region NegativeTrendBrush

        /// <summary>
        /// Identifies the <see cref="NegativeTrendBrush"/> dependency property
        /// </summary>
        public static readonly DependencyProperty NegativeTrendBrushProperty = DependencyProperty.Register("NegativeTrendBrush",
            typeof(Brush), typeof(CellValueGrapher), new FrameworkPropertyMetadata(Brushes.LightPink));

        /// <summary>
        /// Returns/sets the brush to use when coloring Negative Trends.
        /// </summary>
        public Brush NegativeTrendBrush
        {
            get { return (Brush)this.GetValue(CellValueGrapher.NegativeTrendBrushProperty); }
            set { this.SetValue(CellValueGrapher.NegativeTrendBrushProperty, value); }
        }

        #endregion NegativeTrendBrush

        #region PositiveSegmentBrush

        /// <summary>
        /// Identifies the <see cref="PositiveSegmentBrush"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PositiveSegmentBrushProperty = DependencyProperty.Register("PositiveSegmentBrush",
            typeof(Brush), typeof(CellValueGrapher), new FrameworkPropertyMetadata(Brushes.Green));

        /// <summary>
        /// Returns/sets the brush to use when coloring positive segments.
        /// </summary>
        public Brush PositiveSegmentBrush
        {
            get { return (Brush)this.GetValue(CellValueGrapher.PositiveSegmentBrushProperty); }
            set { this.SetValue(CellValueGrapher.PositiveSegmentBrushProperty, value); }
        }

        #endregion PositiveSegmentBrush

        #region PositiveTrendBrush

        /// <summary>
        /// Identifies the <see cref="PositiveTrendBrush"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PositiveTrendBrushProperty = DependencyProperty.Register("PositiveTrendBrush",
            typeof(Brush), typeof(CellValueGrapher), new FrameworkPropertyMetadata(Brushes.LightGreen));

        /// <summary>
        /// Returns/sets the brush to use when coloring Positive Trends.
        /// </summary>
        public Brush PositiveTrendBrush
        {
            get { return (Brush)this.GetValue(CellValueGrapher.PositiveTrendBrushProperty); }
            set { this.SetValue(CellValueGrapher.PositiveTrendBrushProperty, value); }
        }

        #endregion PositiveTrendBrush

        #region ValueHistory

        public static readonly DependencyProperty ValueHistoryProperty = DependencyProperty.Register("ValueHistory",
            typeof(IList<DataValueInfo>), typeof(CellValueGrapher), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnValueHistoryChanged)));

        private static void OnValueHistoryChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var cellValueGrapher = target as CellValueGrapher;
            if (cellValueGrapher == null)
                return;

            INotifyCollectionChanged i;
            if (e.OldValue != null)
            {
                i = e.OldValue as INotifyCollectionChanged;
                if (i != null)
                    i.CollectionChanged -= new NotifyCollectionChangedEventHandler(cellValueGrapher.i_CollectionChanged);
            }

            i = e.NewValue as INotifyCollectionChanged;
            if (i != null)
                i.CollectionChanged += new NotifyCollectionChangedEventHandler(cellValueGrapher.i_CollectionChanged);
        }

        void i_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        public IList<DataValueInfo> ValueHistory
        {
            get { return (IList<DataValueInfo>)this.GetValue(CellValueGrapher.ValueHistoryProperty); }
            set { this.SetValue(CellValueGrapher.ValueHistoryProperty, value); }
        }

        #endregion ValueHistory

        #endregion Properties

        #region Base Class Overrides

        #region OnRender

        protected override void OnRender(DrawingContext drawingContext)
        {
            // Draw the background.
            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));

            // Need a ValueHistory to continue.
            if (this.ValueHistory == null)
                return;

            // Establish whether the trend is up or down based on the history we currently have.
            int trend = 0;
            for (int i = 1; i < this.ValueHistory.Count; i++)
            {
                if ((double)this.ValueHistory[i].Value > (double)this.ValueHistory[i - 1].Value)
                    trend++;
                else
                    trend--;
            }

            if (trend > 0)
                drawingContext.DrawRoundedRectangle(this.PositiveTrendBrush, null, new Rect(this.RenderSize), 4.0d, 4.0d);
            else
                if (trend < 0)
                    drawingContext.DrawRoundedRectangle(this.NegativeTrendBrush, null, new Rect(this.RenderSize), 4.0d, 4.0d);

            // Draw the midline.
            double renderHeight = this.RenderSize.Height;
            double renderWidth = this.RenderSize.Width;
            double midPointY = renderHeight / 2d;
            var midlinePen = new Pen(Brushes.DarkGray, 1);
            midlinePen.DashStyle = DashStyles.Dot;
            drawingContext.DrawLine(midlinePen, new Point(0, midPointY),
                                                new Point(renderWidth, midPointY));

            // Update our hi/lo values.
            double lowestValue = MAX_VALUE;
            double highestValue = MIN_VALUE;
            foreach (DataValueInfo dvi in this.ValueHistory)
            {
                lowestValue = Math.Min(lowestValue, (double)dvi.Value);
                highestValue = Math.Max(highestValue, (double)dvi.Value);
            }

            // Draw the graph segments.
            double midValue = lowestValue + ((highestValue - lowestValue) / 2d);
            double segmentWidth = 6d;
            int segmentNumber = 0;

            foreach (DataValueInfo dvi in this.ValueHistory)
            {
                var value = (double)dvi.Value;
                double pctHeight = Math.Abs(value - midValue) / Math.Abs(highestValue - midValue);
                double height = (renderHeight / 2d) * pctHeight;
                double left = Math.Min(renderWidth, (double)segmentNumber * segmentWidth);

                if ((left + segmentWidth) > renderWidth)
                    break;

                bool isPositiveSegment = value >= midValue;
                double top = isPositiveSegment ? (midPointY - height) : midPointY;
                var rect = new Rect(left, top, segmentWidth, height);
                drawingContext.DrawRectangle(isPositiveSegment ? this.PositiveSegmentBrush : this.NegativeSegmentBrush,
                                             null,
                                             rect);

                segmentNumber++;
            }
        }

        #endregion OnRender

        #endregion Base Class Overrides
    }
}