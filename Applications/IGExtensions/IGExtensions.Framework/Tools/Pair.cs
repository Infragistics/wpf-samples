namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents an immutable pair of values.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public T1 First { get; set; }
        public T2 Second { get; set; }
    }
}
