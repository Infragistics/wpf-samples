using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infragistics.Olap;

namespace IGPivotGrid.Samples.Controls
{
    public class TopCountAggregator<T> : IAggregator<T, TopCountCache<T>>
    {
        public AggregatorType AggregatorType
        {
            get { return AggregatorType.Calculated; }
        }

        public string Identity
        {
            get;
            set;
        }

        public IAggregationResult<T, TopCountCache<T>> Evaluate(
            IAggregationResult<T, TopCountCache<T>> oldResult,
            IAggregateable aggregateable,
            IEnumerable items
            )
        {
            var result = new TopCountResult<T>();
            foreach (var item in items)
            {
                var value = (T)aggregateable.GetValue(item);
                result.Cache.AddCount(value);
            }

            return result;
        }

        public IAggregationResult Evaluate(
            IAggregationResult oldResult,
            IAggregateable aggregateable,
            IEnumerable items
            )
        {
            return this.Evaluate((oldResult as TopCountResult<T>), aggregateable, items);
        }

        public IAggregationResult<T, TopCountCache<T>> Evaluate(
            IAggregationResult<T, TopCountCache<T>> oldResult,
            T value
            )
        {
            throw new NotImplementedException();
        }

        public IAggregationResult Evaluate(IAggregationResult oldResult, object value)
        {
            throw new NotImplementedException();
        }
    }

    public class TopCountResult<T> : IAggregationResult<T, TopCountCache<T>>
    {
        private readonly TopCountCache<T> _cache;

        public TopCountResult()
        {
            this._cache = new TopCountCache<T>();
        }

        public TopCountCache<T> Cache
        {
            get
            {
                return this._cache;
            }
        }

        public T Value
        {
            get
            {
                return this.Cache.TopItem;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        object IAggregationResult.Value
        {
            get
            {
                return this.Value;
            }
        }

        object IAggregationResult.Cache
        {
            get
            {
                return this.Cache;
            }
        }
    }

    public class TopCountCache<T>
    {
        private IDictionary<T, int> _itemsCache = new Dictionary<T, int>();

        public T TopItem
        {
            get
            {
                int maxValue = this._itemsCache.Max(kvp => kvp.Value);
                return this._itemsCache.Where(kvp => kvp.Value == maxValue).First().Key;
            }
        }

        public void AddCount(T item)
        {
            ValueType value = item as ValueType;
            if (value == null && item == null)
            {
                return;
            }

            if (!this._itemsCache.ContainsKey(item))
            {
                this._itemsCache.Add(item, 1);
            }
            else
            {
                this._itemsCache[item]++;
            }
        }
    }


    public class TopCountOfStringAggregator : TopCountAggregator<string>
    {
    }
}
