using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace IGExtensions.Framework.Tools
{
    public class KDTree
    {
        #region Constructor and Initialisation
        public KDTree(List<int> indices, Func<int, double> x, Func<int, double> y)
        {
            X = x;
            Y = y;
            Root = Build(0, indices.Count - 1, indices, true);
        }
        public KDTree(int count, Func<int, double> x, Func<int, double> y)
        {
            List<int> indices=new List<int>() { Capacity=count };

            for(int i=0; i<count; ++i)
            {
                indices.Add(i);
            }

            X = x;
            Y = y;
            Root = Build(0, indices.Count - 1, indices, true);
        }

        private Node Build(int b, int e, List<int> indices, bool horizontal)
        {
            if (e < b)
            {
                return null;
            }

            if (e == b)
            {
                return new LeafNode() { Index = indices[b] };
            }

            int k = (b + e) / 2;
            Func<int, double> F = horizontal ? X : Y;
            indices.PartitionByOrder(k - b, b, e - b + 1, (i, j) => F(i).CompareTo(F(j)));

            double s = F(indices[k]);

            // make the partitioning strict

            int k1 = k + 1;

            for (int i = k + 1; i <= e; ++i)
            {
                if (F(indices[i]) == s)
                {
                    k = k1;
                    indices.Swap(i, k1);
                    ++k1;
                }
            }

            // recurse

            InternalNode node = new InternalNode()
            {
                Left = Build(b, k - 1, indices, !horizontal),
                Index = indices[k],
                Right = Build(k + 1, e, indices, !horizontal)
            };

            return node;
        }
        #endregion

        #region Query
        public IEnumerable<int> GetEnumerator(Rect range)
        {
            Stack<QueryTodo> todo = new Stack<QueryTodo>();

            Node node = Root;
            bool horizontal = true;

            while (node != null)
            {
                double x = X(node.Index);
                double y = Y(node.Index);

                if (range.Left <= x && range.Right >= x && range.Top <= y && range.Bottom >= y)
                {
                    yield return node.Index;
                }

                bool left = node.Left != null && ((horizontal && range.Left <= x) || (!horizontal && range.Top <= y));
                bool right = node.Right != null && ((horizontal && range.Right > x) || (!horizontal && range.Bottom > y));

                if (left && right)
                {
                    todo.Push(new QueryTodo(node.Right, !horizontal));
                }

                node = left ? node.Left : node.Right;
                horizontal = !horizontal;

                if(node==null && todo.Count > 0)
                {
                    node = todo.Peek().Node;
                    horizontal = todo.Peek().Horizontal;

                    todo.Pop();
                }
            }
        }

        private class QueryTodo
        {
            public QueryTodo(Node node, bool horizontal)
            {
                Node = node;
                Horizontal = horizontal;
            }
            public readonly Node Node;
            public readonly bool Horizontal;
        }
        #endregion

        private readonly Node Root;
        private readonly Func<int, double> X;
        private readonly Func<int, double> Y;

        private interface Node
        {
            int Index { get; set; }
            Node Left { get; }
            Node Right { get; }
        }
        private class LeafNode : Node
        {
            public int Index { get; set; }
            public Node Left { get { return null; } }
            public Node Right { get { return null; } }
        }
        private class InternalNode : Node
        {
            public int Index { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}
