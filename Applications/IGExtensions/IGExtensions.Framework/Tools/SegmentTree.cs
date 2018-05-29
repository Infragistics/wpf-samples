using System;
using System.Collections.Generic;

namespace IGExtensions.Framework.Tools
{
    public class SegmentTree<T>
    {
        struct Todo
        {
            public Todo(int v, int bv, int ev)
            {
                this.v = v;
                this.Bv = bv;
                this.Ev = ev;
            }

            public readonly int v;
            public readonly int Bv;
            public readonly int Ev;
        }

        void Insert(int b, int e, Action<int> allocate)
        {
            var todo = new Stack<Todo>();

            todo.Push(new Todo(b, e, 1));

            while (todo.Count > 0)
            {
                Todo cur = todo.Pop();

                if (b <= cur.Bv && cur.Ev <= e)
                {
                    allocate(cur.v);
                }
                else
                {
                    int Mv = (cur.Bv + cur.Ev) / 2;

                    if (b < Mv)
                    {
                        todo.Push(new Todo(cur.Bv, Mv, 0 + 2 * cur.v));
                    }

                    if (Mv < e)
                    {
                        todo.Push(new Todo(Mv + 1, cur.Ev, 1 + 2 * cur.v));
                    }
                }
            }
        }
    }
}
