namespace Infragistics.Samples.Shared.Models.General
{
    public class Parent
    {
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }
    }
}