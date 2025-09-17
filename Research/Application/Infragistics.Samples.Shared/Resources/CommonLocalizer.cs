namespace Infragistics.Samples.Shared.Resources
{
    /// <summary>
    /// Represents common assets localizer that provides access to the <see cref="CommonStrings"/> resources.
    /// </summary>
    public class CommonLocalizer
    {
        private static readonly OlapStrings OlapStringResources = new OlapStrings();
        private static readonly CommonStrings CommonStringResources = new CommonStrings();
        private static readonly ModelStrings ModelStringResources = new ModelStrings();
        private static readonly FinancialStrings FinancialStringResources = new FinancialStrings();

        public FinancialStrings FinancialStrings
        {
            get { return FinancialStringResources; }
        }
        public ModelStrings ModelStrings
        {
            get { return ModelStringResources; }
        }
        public CommonStrings CommonStrings
        {
            get { return CommonStringResources; }
        }
        public OlapStrings OlapStrings
        {
            get { return OlapStringResources; }
        }
    }
}