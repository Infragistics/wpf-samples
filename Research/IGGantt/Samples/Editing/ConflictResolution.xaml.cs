using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using IGGantt.Resources;

namespace IGGantt.Samples.Editing
{
    public partial class ConflictResolution : SampleContainer
    {
        public ConflictResolution()
        {
            InitializeComponent();

            ShowSampleInformation();
        }

        private void ShowSampleInformation()
        {
            PringLog(GanttStrings.ConfResolSampleInfo05, false);
            PringLog(GanttStrings.ConfResolSampleInfo04, false);
            PringLog(GanttStrings.ConfResolSampleInfo03, false);
            PringLog(GanttStrings.ConfResolSampleInfo02, false);
            PringLog(GanttStrings.ConfResolSampleInfo01, false);
            PringLog(Environment.NewLine, false);
        }

        private void OnTaskConstraintWarning(object sender, ProjectTaskConstraintWarningEventArgs ptcwea)
        {
            string message = String.Format(
                "{2} {3} {0} {4} {1}",
                ptcwea.Task.TaskName, ptcwea.Action.ToString(), 
                GanttStrings.ConstraintWarning, GanttStrings.TaskAffected, GanttStrings.ActionNeeded);

            PringLog(message, true);
        }

        private void OnDependencyCircularityDetected(object sender, DependencyCircularityDetectedEventArgs dcdea)
        {
            string message = String.Format(
                "{2} {3} {0} {4} {1} ",
                dcdea.DependenciesCausingCircularity.Count.ToString(), dcdea.Action.ToString(), 
                GanttStrings.DependencyCircularity, GanttStrings.CircularitiesCount, GanttStrings.ActionNeeded);

            PringLog(message, true);
        }

        private void OnTaskInNonWorkingDayWarning(object sender, ProjectTaskInNonWorkingDayWarningEventArgs ptinwdwea)
        {
            string message = String.Format(
                "{2} {3} {0} {4} {1}",
                ptinwdwea.Task.TaskName, ptinwdwea.Action.ToString(), GanttStrings.TaskinNonWorkingDay, GanttStrings.TaskAffected, GanttStrings.ActionNeeded);

            PringLog(message, true);
        }

        private void PringLog(string message, bool includeTimeStamp)
        {
            string loggedMessage;

            if (includeTimeStamp)
            {
                 loggedMessage = string.Format("[{0}] {1} {2}", DateTime.Now.ToString("HH:mm:ss"), message, Environment.NewLine);
            }
            else
            {
                loggedMessage = string.Format("{0} {1}", message, Environment.NewLine);
            }

            ListBoxItem lbi = new ListBoxItem
            {
                Content = loggedMessage,
                Height = 20,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            lbWarnings.Items.Insert(0, lbi);
        }

        private void OnClear(object sender, RoutedEventArgs rea)
        {
            lbWarnings.Items.Clear();
        }
    }
}
