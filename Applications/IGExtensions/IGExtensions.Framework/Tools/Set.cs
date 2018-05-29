using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGExtensions.Framework.Tools
{
    // The two classes have similar object models, and both have O(log n) retrieval. Where the two classes differ is
    // in memory use and speed of insertion and removal:
    //     SortedList<TKey, TValue> uses less memory than SortedDictionary<TKey, TValue>.
    //     SortedDictionary<TKey, TValue> has faster insertion and removal operations for unsorted data, O(log n) as opposed to O(n) for SortedList<TKey, TValue>.
    //     If the list is populated all at once from sorted data, SortedList<TKey, TValue> is faster than SortedDictionary<TKey, TValue>.
    //
    // Another difference between the SortedDictionary<TKey, TValue> and SortedList<TKey, TValue> classes is that 
    // SortedList<TKey, TValue> supports efficient indexed retrieval of keys and values through the collections returned
    // by the Keys and Values properties. It is not necessary to regenerate the lists when the properties are accessed, because
    // the lists are just wrappers for the internal arrays of keys and values. 

    //
    // the choice of implementation has an impact on performance which depends upon the use
    //

#if true
    class Set<T> : ISet<T>
    {
        Dictionary<T, object> dictionary = new Dictionary<T, object>();

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return dictionary.Keys.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dictionary.Keys.GetEnumerator();
        }
        #endregion

        #region ISet<T> Members

        public bool Add(T item)
        {
            if (!dictionary.ContainsKey(item))
            {
                dictionary.Add(item, null);
                return true;
            }

            return false;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            dictionary.Add(item, null);
        }

        public bool Contains(T item)
        {
            return dictionary.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dictionary.Keys.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return dictionary.Remove(item);
        }

        public void Clear()
        {
            dictionary.Clear();
        }
        #endregion
    }
#else
    class Set<T> : IEnumerable<T>
    {
        SortedList<T, int> list;

        public Set()
        {
            list = new SortedList<T, int>();
        }

        public void Add(T k)
        {
            if (!list.ContainsKey(k))
                list.Add(k, 0);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public void DeepCopy(Set<T> other)
        {
            list.Clear();

            foreach (T k in other.list.Keys)
            {
                Add(k);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return list.Keys.GetEnumerator();
        }

        public void Clear()
        {
            list.Clear();
        }


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
#endif
}
