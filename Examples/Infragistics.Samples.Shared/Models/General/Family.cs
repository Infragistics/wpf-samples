using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class Family
    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                }
            }
        }

        IEnumerable<Parent> _parents;
        public IEnumerable<Parent> Parents
        {
            get
            {
                return this._parents;
            }
            set
            {
                if (this._parents != value)
                {
                    this._parents = value;
                }
            }
        }

        IEnumerable<Child> _children;
        public IEnumerable<Child> Children
        {
            get
            {
                return this._children;
            }
            set
            {
                if (this._children != value)
                {
                    this._children = value;
                }
            }
        }
        IEnumerable<CarLayout> _carlayouts;
        public IEnumerable<CarLayout> CarLayouts
        {
            get
            {
                return this._carlayouts;
            }
            set
            {
                if (this._carlayouts != value)
                {
                    this._carlayouts = value;
                }
            }
        }
    }
}
