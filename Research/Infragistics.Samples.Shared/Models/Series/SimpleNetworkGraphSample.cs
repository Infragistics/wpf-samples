using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Infragistics.Samples.Shared.Models
{
    public class SimpleNetworkGraph
    {
        private Random _random;
        public int _seed { get; set; }

        public List<SimpleNetworkNode> Departments { get; set; }
        public List<SimpleNetworkNode> Employees { get; set; }
        public List<SimpleNetworkNode> Graph { get; set; }

        public SimpleNetworkGraph() : this(3, 4, 3, 25972937)
        {

        }
        public SimpleNetworkGraph(int numberParentNodes, int numberChildNodes, int deviation, int seed)
        {
            _seed = seed;
            _random = new Random(_seed);

            Departments = new List<SimpleNetworkNode>();
            Employees = new List<SimpleNetworkNode>();
            Graph = new List<SimpleNetworkNode>();

            var departments = new string[] { "Engineering", "HR", "Sales", "Marketing" };
            var people = new string[] { "Eric", "Phil", "Charlene", "Tedd", "Erica", "Matthew", "Julie", "Marie", "Joe", "Graham", "Martin" };

            if (numberParentNodes > departments.Length)
                numberParentNodes = departments.Length;

            var parentDistance = 10.0;
            var childDistance = 5.0;

            var start = new Point(0, 0);
            for (var i = 0; i < numberParentNodes; i++)
            {
                var parentNode = new SimpleNetworkNode();
                var nodeDirection = Normalize(new Point(_random.NextDouble() - 0.5, _random.NextDouble() - 0.5));

                parentNode.X = i == 0 ? start.X : start.X + nodeDirection.X * parentDistance;
                parentNode.Y = i == 0 ? start.Y : start.Y + nodeDirection.Y * parentDistance;
                parentNode.Size = 2.0;
                parentNode.Label = departments[i];
                Departments.Add(parentNode);

                start.X = parentNode.X;
                start.Y = parentNode.Y;

                var childCount = _random.Next(numberChildNodes - deviation, numberChildNodes + deviation);
                for (var j = 0; j < childCount; j++)
                {
                    var childNode = new SimpleNetworkNode();
                    nodeDirection = Normalize(new Point(_random.NextDouble() - 0.5, _random.NextDouble() - 0.5));
                    childNode.X = parentNode.X + nodeDirection.X * childDistance;
                    childNode.Y = parentNode.Y + nodeDirection.Y * childDistance;
                    childNode.Size = 1.0;
                    childNode.Label = people[_random.Next(0, people.Length - 1)];
                    Employees.Add(childNode);
                }
            }

            CheckCollisions();

            for (var i = 0; i < Departments.Count; i++)
            {
                var graph = new SimpleNetworkNode();
                graph.X = Departments[i].X;
                graph.Y = Departments[i].Y;
                graph.Points = new List<List<Point>>();

                for (var j = 0; j < Employees.Count; j++)
                {
                    if (Magnitude(new Point(graph.X - Employees[j].X, graph.Y - Employees[j].Y)) <= childDistance + 1.0)
                    {
                        graph.Points.Add(new List<Point>() { new Point(graph.X, graph.Y), new Point(Employees[j].X, Employees[j].Y) });
                    }
                }

                Graph.Add(graph);
            }
        }

        public Point Normalize(Point point)
        {
            var mag = Magnitude(point);
            return new Point(point.X / mag, point.Y / mag);
        }
        public double Magnitude(Point point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }

        public void CheckCollisions()
        {
            for (var i = 0; i < Employees.Count; i++)
            {
                for (var j = 0; j < Employees.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var overlap = Magnitude(new Point(Math.Abs(Employees[i].X - Employees[j].X), Math.Abs(Employees[i].Y - Employees[j].Y))) - ((Employees[i].Size / 2) + (Employees[j].Size / 2));
                    if (overlap < 0)
                    {
                        var pushDirection = Normalize(new Point(Employees[i].X - Employees[j].X, Employees[i].Y - Employees[j].Y));
                        Employees[i].X += pushDirection.X * -(overlap / 2);
                        Employees[i].Y += pushDirection.Y * -(overlap / 2);

                        Employees[j].X += pushDirection.X * (overlap / 2);
                        Employees[j].Y += pushDirection.Y * (overlap / 2);
                    }
                }
            }
        }
    }
}
