using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryScatterPolygonSeries : SampleContainer
    {
        public GalleryScatterPolygonSeries()
        {
            InitializeComponent();            
        }  
    }

    public class Room
    {
        public string Label { get; set; }
        public List<List<Point>> Points { get; set; }
    }
    public class RoomList : List<Room>
    {
        public RoomList()
        {
            var mainFloor = new Room();
            mainFloor.Points = new List<List<Point>>();
            mainFloor.Points.Add(new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 7),
                new Point(7, 7),
                new Point(7, 10),
                new Point(0, 10),
            });
            this.Add(mainFloor);

            var bedroom1 = new Room() { Label = "Master Bedroom" };
            bedroom1.Points = new List<List<Point>>();
            bedroom1.Points.Add(new List<Point>()
            {
                new Point(0, 0),
                new Point(4, 0),
                new Point(4, 5),
                new Point(0, 5),
            });
            this.Add(bedroom1);

            var bedroom2 = new Room() { Label = "Guest Bedroom" };
            bedroom2.Points = new List<List<Point>>();
            bedroom2.Points.Add(new List<Point>()
            {
                new Point(2, 10),
                new Point(7, 10),
                new Point(7, 7),
                new Point(2, 7),
            });
            this.Add(bedroom2);

            var bathroom = new Room() { Label = "Bath" };
            bathroom.Points = new List<List<Point>>();
            bathroom.Points.Add(new List<Point>()
            {
                new Point(0, 10),
                new Point(2, 10),
                new Point(2, 7),
                new Point(0, 7),
            });
            this.Add(bathroom);

            var kitchen = new Room() { Label = "Kitchen" };
            kitchen.Points = new List<List<Point>>();
            kitchen.Points.Add(new List<Point>()
            {
                new Point(6, 7),
                new Point(10, 7),
                new Point(10, 4),
                new Point(6, 4),
            });
            this.Add(kitchen);

            var livingroom = new Room() { Label = "Living Room" };
            livingroom.Points = new List<List<Point>>();
            livingroom.Points.Add(new List<Point>()
            {
                new Point(6, 4),
                new Point(10, 4),
                new Point(10, 0),
                new Point(6, 0),
            });
            this.Add(livingroom);
        }
    }
}
