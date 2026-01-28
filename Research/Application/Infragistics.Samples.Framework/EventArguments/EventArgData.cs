using System;

namespace Infragistics.Samples.Framework.EventArguments
{
    public class EventArgData<TValue> : EventArgs
    {
        private TValue value;

        public EventArgData(TValue value)
        {
            this.value = value;
        }

        public TValue Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
