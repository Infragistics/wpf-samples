using System;
using System.Collections;
using System.Collections.Generic;

namespace IGExtensions.Framework.Tools
{
    public partial class Hierarchy
    {
        public Hierarchy()
        {
            Depth = depth;
        }

        public Func<object, object> Parent;
        public Func<object, int> Depth;

        public void Path(IList wayPoints, object o1, object o2)
        {
            int d1 = Depth(o1);
            int d2 = Depth(o2);

            var L2 = new List<object>() { Capacity = 2 * d2 };

            while (d1 > d2)
            {
                wayPoints.Add(o1);
                o1 = Parent(o1);

                --d1;
            }

            while (d2 > d1)
            {
                L2.Add(o2);
                o2 = Parent(o2);

                --d2;
            }

            if (o1 != o2)
            {
                while (o1 != o2)
                {
                    wayPoints.Add(o1);
                    o1 = Parent(o1);

                    L2.Add(o2);
                    o2 = Parent(o2);
                }

                if (o1 != null)
                {
                    wayPoints.Add(o1);
                }
            }

            for (int i = L2.Count - 1; i >= 0; --i)
            {
                wayPoints.Add(L2[i]);
            }
        }
        private int depth(object o1)
        {
            int d = 0;

            while (o1 != null)
            {
                o1 = Parent(o1);
                ++d;
            }

            return d;
        }
    }
}
