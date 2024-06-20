using System.Linq;
using Infragistics;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Tools
{
    public class MyCustomSummary : SynchronousSummaryCalculator
    {
        public override SummaryExecution? SummaryExecution
        {
            get
            {
                return Infragistics.SummaryExecution.PriorToFilteringAndPaging;
            }
        }

        public override object Summarize(IQueryable data, string fieldKey)
        {
            IQueryable<Product> convertedData = (IQueryable<Product>)data;
            return convertedData.Count<Product>();
        }
    }

    public class MySummaryOperand : SummaryOperandBase
    {
        MyCustomSummary _myCalc;

        protected override string DefaultRowDisplayLabel
        {
            get { return "Count"; }
        }

        protected override string DefaultSelectionDisplayLabel
        {
            get { return "Count"; }
        }

        public override SummaryCalculatorBase SummaryCalculator
        {
            get
            {
                if (_myCalc == null)
                {
                    this._myCalc = new MyCustomSummary();
                }
                return this._myCalc;
            }
        }
    }
}
