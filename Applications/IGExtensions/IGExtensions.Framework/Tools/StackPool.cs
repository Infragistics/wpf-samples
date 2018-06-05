using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents a pool of reusable objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class StackPool<T>
    {
        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        /// <remarks>
        /// The pool will either Create a new object or Activate one which was previously returned
        /// and Deactivated. If DeferDisactivate is set there may be pending active objects
        /// available which will be reused directly).
        /// </remarks>
        /// <returns>An object.</returns>
        public T Pop()
        {
            T t;

            if (_limbo.Count != 0)
            {
                t = _limbo.Pop();
            }
            else
            {
                t = _inactive.Count != 0 ? _inactive.Pop() : Create();
                Activate(t);
            }

            _active.Add(t, null);
            return t;
        }

        /// <summary>
        /// Returns an object to the pool for recycling.
        /// </summary>
        /// <remarks>
        /// All references to objects which have been returned to the pool should be destroyed. 
        /// <para>
        /// When an object is returned to the pool it will be immediately Deactivated (unless
        /// DeferDisactivate is set) and may also be Destroyed either immediately or some time
        /// later. 
        /// </para>
        /// </remarks>
        /// <param name="t"></param>
        public void Push(T t)
        {
            _active.Remove(t);

            if (DeferDisactivate)
            {
                _limbo.Push(t);
            }
            else
            {
                Disactivate(t);

                int inactiveCount = RoundUp(_active.Count);

                if (_inactive.Count < inactiveCount)
                {
                    Destroy(t);
                }
                else
                {
                    _inactive.Push(t);
                }
            }
        }

        /// <summary>
        /// Sets or gets the DeferDisactivate flag.
        /// </summary>
        /// <remarks>
        /// When the pool is marked to defer deactivation, objects returned to the pool are
        /// not immediately deactivated, instead remaining in a limbo state where they are
        /// available for reuse without Activation. Resetting DeferDisactivate causes all of 
        /// these limbo objects to be deactivated and potentially destroyed.
        /// <para>
        /// Deferred deactivation is useful where the activation/deactivation cycle is costly
        /// (such as add/remove a VisualElement from a ParentItem Panel).
        /// </para>
        /// </remarks>
        public bool DeferDisactivate
        {
            get { return deferDisactivate; }
            set
            {
                if (deferDisactivate != value)
                {
                    deferDisactivate = value;

                    if (!deferDisactivate)
                    {
                        int inactiveCount = RoundUp(_active.Count);

                        while (_limbo.Count > 0 && _inactive.Count <= inactiveCount)
                        {
                            T t = _limbo.Pop();

                            Disactivate(t);
                            _inactive.Push(t);
                        }

                        while (_limbo.Count > 0)
                        {
                            T t = _limbo.Pop();

                            Disactivate(t);
                            Destroy(t);
                        }

                        while (_inactive.Count > inactiveCount)
                        {
                            Destroy(_inactive.Pop());
                        }
                    }
                }
            }
        }
        private bool deferDisactivate = false;

        public void Clear()
        {
            if (DeferDisactivate)
            {
                foreach (T t in _active.Keys)
                {
                    _limbo.Push(t);
                }

                _active.Clear();
            }
            else
            {
                foreach (T t in _active.Keys)
                {
                    Disactivate(t);

                    int inactiveCount = RoundUp(_active.Count);

                    if (_inactive.Count < inactiveCount)
                    {
                        Destroy(t);
                    }
                    else
                    {
                        _inactive.Push(t);
                    }
                }

                _active.Clear();
            }
        }

        public bool Contains(T t)
        {
            return _active.ContainsKey(t);
        }

        /// <summary>
        /// Gets the number of active items in the current StackPool object.
        /// </summary>
        public int ActiveCount { get { return _active.Count; } }

        /// <summary>
        /// Gets the number of inactive (not including limbo) items in the
        /// current StackPool object.
        /// </summary>
        public int InactiveCount { get { return _inactive.Count; } }

        /// <summary>
        /// Gets or sets the function used to create new items.
        /// </summary>
        public Func<T> Create { get; set; }

        /// <summary>
        /// Gets or sets the function used to deactivate items.
        /// </summary>
        public Action<T> Disactivate { get; set; }

        /// <summary>
        /// Gets or sets the function used to activate items.
        /// </summary>
        public Action<T> Activate { get; set; }

        /// <summary>
        /// Gets or sets the function used to destroy old items.
        /// </summary>
        public Action<T> Destroy { get; set; }

        private static int RoundUp(int a)
        {
            int p = 2;

            while (a > p)
            {
                p <<= 1;
            }

            return p;
        }

        /// <summary>
        /// The active object collection.
        /// </summary>
        readonly Dictionary<T, object> _active = new Dictionary<T, object>();

        /// <summary>
        /// The limbo object collection.
        /// </summary>
        readonly Stack<T> _limbo = new Stack<T>();

        /// <summary>
        /// The inactive object collection.
        /// </summary>
        readonly Stack<T> _inactive = new Stack<T>();
    }

}
