using System;
using Infragistics.Controls.Charts;

namespace IGPieChart.Model
{
    public class LabelsPositionMode
    {
        public LabelsPositionMode()
        {
        }

        public LabelsPosition LabelsPosition
        { get; set; }

        public string LabelsPositionName
        { get; set; }

        public override string ToString()
        {
            return LabelsPositionName;
        }

        public LabelsPositionMode(LabelsPosition labelsPosition, string name)
        {
            LabelsPosition = labelsPosition;
            LabelsPositionName = name;
        }
    }
}
