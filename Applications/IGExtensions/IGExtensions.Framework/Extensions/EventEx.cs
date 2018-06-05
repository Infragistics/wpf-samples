namespace System //IGExtensions.Framework.Extensions
{
    public static class EventEx
    {
        //http://houseofbilz.com/archives/2009/02/15/re-thinking-c-events/
        /// <summary>
        /// Performs check if a event has event handler (null reference) before raising the event
        /// <code> public event EventHandler<EventArgs<EventsPayloadType>> EventChangeName;
        ///        EventChangeName.Raise(this, new EventsPayloadType());</code>
        /// </summary>
        public static void Raise<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, T payload)
        {
            if (eventHandler != null)
                eventHandler(sender, new EventArgs<T>(payload));
        }
        //http://www.dailycoding.com/Posts/top_5_small_but_must_have_extension_methods.aspx
        /// <summary>
        /// Performs check if a event has event handler (null reference) before raising the event
        /// <code>  MyEvent.Raise(this, e);  </code>
        /// </summary>
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler != null)
                eventHandler(sender, e);
        }
        /// <summary>
        /// Performs check if a event has event handler (null reference) before raising the event
        /// <code> public event EventHandler<EventsPayloadType> EventChangeName; 
        ///        EventChangeName.Raise(this, new EventsPayloadType());</code>
        /// </summary>
        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler != null)
                eventHandler(sender, e);
        }
    }
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; private set; }
    }

    public class ResultEventArgs : EventArgs
    {
        public ResultEventArgs(Exception error)
        {
            this.Error = error;
            this.Result = string.Empty;
        }
        public ResultEventArgs(string result)
        {
            this.Error = null;
            this.Result = result;
        }
        public Exception Error { get; private set; }
        public string Result { get; private set; }
    }
}