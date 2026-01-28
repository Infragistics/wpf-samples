using System.Windows;
using System.Windows.Input;
using Infragistics;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public partial class MovingEventEntries : Infragistics.Samples.Framework.SampleContainer
    {
        public MovingEventEntries()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            Timeline.MouseDownTools.Add(new EventPointMovingTool(Timeline));
        }

    }
    #region EventPointMovingTool
    /// <summary>
    /// Represents a tool that is used to move event points.
    /// </summary>
    public class EventPointMovingTool : Tool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventPointMovingTool"/> class.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        public EventPointMovingTool(XamTimeline timeline)
            : base(timeline)
        {
            this.Timeline = timeline;
        }
        #region Private Properties
        /// <summary>
        /// The timeline.
        /// </summary>
        private XamTimeline Timeline { get; set; }

        /// <summary>
        /// The handled event point.
        /// </summary>
        private EventPoint HandledPoint { get; set; }

        /// <summary>
        /// The timeline axis.
        /// </summary>
        private NumericTimeAxis Axis
        {
            get
            {
                return this.Timeline.Axis as NumericTimeAxis;
            }
        }
        #endregion
        /// <summary>
        /// Determines whether this tool can start.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this tool can start; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanStart(MouseButtonEventArgs e)
        {
            IInputElement inputElement = this.Timeline.InputHitTest(e.GetPosition(this.Timeline));
            FrameworkElement element = inputElement as FrameworkElement;

            while (element != null)
            {
                this.HandledPoint = element as EventPoint;

                if (this.HandledPoint != null)
                {
                    return true;
                }

                if (element.Parent != null)
                {
                    element = element.Parent as FrameworkElement;
                }
                else
                {
                    element = element.TemplatedParent as FrameworkElement;
                }
            }

            return false;
        }
        /// <summary>
        /// Called when this tools is started and the mouse is moved.
        /// </summary>
        public override void MouseMove()
        {
            double value =
            this.Timeline.Axis.GetWindowBasedPixelValue(this.LastInput.DocMousePosition.X);

            double newTime = System.Math.Round(value);
            newTime = System.Math.Min(1000000, System.Math.Max(-1000000, newTime));

            (this.HandledPoint.EventEntry as NumericTimeEntry).Time = newTime;
        }
        /// <summary>
        /// Represents a tool that is used for scrolling the timeline.
        /// </summary>
        /// <param name="e">A <see cref="MouseButtonEventArgs"/> object.</param>
        public override void MouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.StopTool();
        }
    }
    #endregion
}
