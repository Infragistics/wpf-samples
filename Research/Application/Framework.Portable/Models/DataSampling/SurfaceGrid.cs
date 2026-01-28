using Infragistics.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace Infragistics.Framework 
{
    /*  G is grid point or gird cell's center 
     *  # is gird cell's location/index 
     *  X is gird cell's X size or interval between grid points
     *  Y is gird cell's Y size or interval between grid points
     *  Dots show data points that can be snapped to grid points
     *    
     *     |-----X-----|-----X-----|-----X-----| 
     *     |           |           |           |
     * +---+-----------+-----------+-----------+---+
     * |   |           |           |           |   |
     * |   |           |           |           |   |
     * 2   |     G-----+-----G-----+-----G     |   Y
     * |   |     |.....|.....|.....|.....|     |   |
     * |   |     |.....|.....|.....|.....|     |   |
     * +---+-----+-----+-----+-----+-----+-----+---+
     * |   |     |.....|.....|.....|.....|     |   |
     * |   |     |.....|.....|.....|.....|     |   |
     * 1   |     G-----+-----G-----+-----G     |   Y
     * |   |     |.....|.....|.....|.....|     |   |
     * |   |     |.....|.....|.....|.....|     |   |
     * +---+-----+-----+-----+-----+-----+-----+---+
     * |   |     |.....|.....|.....|.....|     |   |
     * |   |     |.....|.....|.....|.....|     |   |
     * 0   |     G-----+-----G-----+-----G     |   Y
     * |   |           |           |           |   |
     * |   |           |           |           |   |
     * +---+-----------+-----------+-----------+---+
     *     |           |           |           |
     *     |-----0-----|-----1-----|-----2-----|
     */
    /// <summary>
    /// Represents surface grid with data points snapped to grid points that are equally spaced
    /// </summary>
    public class SurfaceGrid : ObservableModel
    {
        public SurfaceGrid()
        {  
            GridCells = new GridDictionary();
           
            RangeX = new DataNumericColumn();
            RangeY = new DataNumericColumn();
            RangeZ = new DataNumericColumn();

            MaximumPointsX = MinimumPointsInSpace;
            MaximumPointsY = MinimumPointsInSpace;
            //TODO remove because MaximumPoints X/Y is more flexible
            MaximumPoints = MinimumPointsTotal;

            // settings optimized for performance
            DataInterpolation = DataInterp.AverageLocalNeighbors;
            DistanceMeasure = DistanceMeasure.CityBlock;
            DataSplitMode = DataSplitMode.FullSplit;

            // settings optimized for accuracy
            //DataInterpolation = DataInterp.MaxClosestNeighbors;
            //DistanceMeasure = DistanceMeasure.CityBlock;
            //DataSplitMode = DataSplitMode.FullSplit;

            DataInterpolator = new DataInterpolator(this);
        }
        
        #region Properties
        /// <summary> Gets tolerance for comparing equality of intervals 
        /// between grid points </summary>
        protected const double SpaceTolerance = 0.0001;

        /// <summary> Gets minimum points in X or Y space </summary>
        protected const int MinimumPointsInSpace = 2;

        /// <summary> Gets minimum points in X and Y space </summary>
        protected const int MinimumPointsTotal = MinimumPointsInSpace * MinimumPointsInSpace;

        private int _maximumPoints;
        //TODO remove because MaximumPoints X/Y is more flexible
        /// <summary> Gets or sets Maximum Points on X and Y axis </summary>
        public int MaximumPoints
        {
            get { return _maximumPoints; }
            set
            {
                if (value == _maximumPoints ||
                    value < MinimumPointsTotal) return;
                _maximumPoints = value;
                _maximumPointsX = (int)Math.Sqrt(value);
                _maximumPointsY = (int)Math.Sqrt(value);
                UpdateIntervals();
                OnPropertyChanged("MaximumPoints");
            }
        }

        private int _maximumPointsX = -1; // ensures an user must set max points
        /// <summary> Gets or sets Maximum Points on X axis </summary>
        public int MaximumPointsX
        {
            get { return _maximumPointsX; }
            set
            {
                if (value == _maximumPointsX ||
                    value < MinimumPointsInSpace) return;
                _maximumPointsX = value;
                UpdateIntervals();
                OnPropertyChanged("MaximumPointsX");
            }
        }

        private int _maximumPointsY = -1; // ensures an user must set max points
        /// <summary> Gets or sets Maximum Points on Y axis </summary>
        public int MaximumPointsY
        {
            get { return _maximumPointsY; }
            set
            {
                if (value == _maximumPointsY ||
                    value < MinimumPointsInSpace) return;
                _maximumPointsY = value;
                UpdateIntervals();
                OnPropertyChanged("MaximumPointsY");
            }
        }

        private double _intervalX = double.NaN;
        /// <summary> Gets or sets IntervalX </summary>
        public double GridIntervalX
        {
            get { return _intervalX; }
            private set
            {
                if (IsEqual(value, _intervalX)) return; _intervalX = value;
                OnPropertyChanged("GridIntervalX");
            }
        }

        private double _intervalY = double.NaN;
        /// <summary> Gets or sets IntervalY </summary>
        public double GridIntervalY
        {
            get { return _intervalY; }
            private set
            {
                if (IsEqual(value, _intervalY)) return; _intervalY = value;
                OnPropertyChanged("GridIntervalY");
            }
        }

        /// <summary> Gets or sets max distance between grid points (Intervals) </summary>
        public double DistanceMax { get; private set; }

        protected DataNumericColumn RangeX { get; set; }
        protected DataNumericColumn RangeY { get; set; }
        protected DataNumericColumn RangeZ { get; set; }

        // TODO use FastItemsSource and add member path properties: X, Y, Z
        private Shape3D _dataSource;
        /// <summary> Gets or sets Data Points </summary>
        public Shape3D DataSource
        {
            get { return _dataSource; }
            set
            {
                if (_dataSource != null)
                    _dataSource.CollectionChanged -= OnDataSourceChanged;

                if (_dataSource == value) return;
                _dataSource = value;

                if (_dataSource != null)
                    _dataSource.CollectionChanged += OnDataSourceChanged;

                OnPropertyChanged("DataSource");
            }
        }

        /// <summary> Gets or sets Grid Points Dictionary stored using cell locations </summary>
        protected GridDictionary GridCells { get; set; }

        /// <summary> Gets Grid Points </summary>
        public List<GridPoint> GridPoints
        {
            get { return this.GridCells.Values.ToList(); } 
        }

        private DataInterpolatorBase _dataInterpolator;
        /// <summary> Gets or sets DataInterpolator </summary>
        public DataInterpolatorBase DataInterpolator
        {
            get { return _dataInterpolator;}
            set
            { 
                if (_dataInterpolator == value) return;
                _dataInterpolator = value; OnPropertyChanged("DataInterpolator"); 
            }
        }

        private DataInterp _dataInterpolation;
        /// <summary> Gets or sets type of data interpolation </summary>
        public DataInterp DataInterpolation
        {
            get { return _dataInterpolation; }
            set
            {
                if (_dataInterpolation == value) return;
                _dataInterpolation = value; OnPropertyChanged("DataInterpolation");
            }
        }

        private DataSplitMode _dataSplitMode;
        /// <summary> 
        /// Gets or sets split mode of data points that fall exactly between grid points
        /// For example, a data point (2.0,0.0) in grid with points between 0-4x and 0-4y
        /// </summary>
        public DataSplitMode DataSplitMode
        {
            get { return _dataSplitMode;}
            set
            {
                if (_dataSplitMode == value) return;
                _dataSplitMode = value; OnPropertyChanged("DataSplitMode");
            }
        }
         
        private DistanceMeasure _distanceMeasure;
        /// <summary> 
        /// Gets or sets measure used to in calculation of distance between data points 
        /// </summary>
        /// <remarks> This property affects only data interpolations:
        /// - <see cref="DataInterp.MaxClosestNeighbors"/> 
        /// - <see cref="DataInterp.InverseDistanceWeight"/> 
        /// </remarks>
        public DistanceMeasure DistanceMeasure
        {
            get { return _distanceMeasure;}
            set
            {
                if (_distanceMeasure == value) return; 
                _distanceMeasure = value; OnPropertyChanged("DistanceMeasure");
            }
        }

        #endregion
        protected void UpdateIntervals()
        {
            if (IsReady())
            {
                GridIntervalX = DataSource.X.Range / (MaximumPointsX - 1);
                GridIntervalY = DataSource.Y.Range / (MaximumPointsY - 1);

                // recalculating max distance for DistanceMeasure
                var distance = GetDistance(
                    new Point3D(0, 0),
                    new Point3D(GridIntervalX, GridIntervalY));

                DistanceMax = distance.Length;
            }
        }
        public bool IsReady()
        {
            if (DataSource == null ||
                DataSource.Count == 0) return false;

            if (DataInterpolator == null) return false;
            if (MaximumPointsX < MinimumPointsInSpace) return false;
            if (MaximumPointsY < MinimumPointsInSpace) return false;
            if (GridIntervalX < SpaceTolerance) return false;
            if (GridIntervalY < SpaceTolerance) return false;
          
            return true;
        }

        public bool IsEqual(double value1, double value2)
        {
            return Math.Abs(value1 - value2) < SpaceTolerance;
        } 

        /// <summary>
        /// Generate the whole grid based on current data source and properties
        /// </summary>
        protected void GenerateGrid()
        {
            if (IsReady())
            {
                UpdateIntervals(); 
                GridCells.Clear();
          
                var xStart = DataSource.X.Min;
                var yStart = DataSource.Y.Min;

                var xEnd = DataSource.X.Max;
                var yEnd = DataSource.Y.Max;

                var xStep = GridIntervalX;
                var yStep = GridIntervalY;

                #region Create grid cells and points
                // "End + Step" ensures that we capture all cells
                for (var x = xStart; x < xEnd + xStep; x += xStep)
                {
                    for (var y = yStart; y < yEnd + yStep; y += yStep)
                    {
                        var dataX = x;
                        var dataY = y;

                        if (dataX > xEnd) dataX = xEnd;
                        if (dataY > yEnd) dataY = yEnd;

                        var location = GetLocation(dataX, dataY);

                        if (!GridCells.Contains(location))
                        {
                            var cell = new GridPoint(location, xStep, yStep);

                            //var point = new GridPoint(cell, dataX, dataY, double.NaN);

                            GridCells.Add(location, cell); 
                        }
                    }
                }
                #endregion

                DistributeData();
                //DataInterpolator.Distribute(this.DataSource);
                
                // update range
                RangeX = DataSource.X;
                RangeY = DataSource.Y;
                RangeZ = DataSource.Z;

                UpdateGrid();

                //OnPropertyChanged("GridPoints");
                //OnPropertyChanged("DataPoints");
            }
        }

        /// <summary>
        /// Safely add data point to the grid cells at specified location exists
        /// </summary>
        public void Add(GridLocation location, Point3D dataPoint)
        {
            if (GridCells.Contains(location))
            {
                GridCells[location].Neighbors.Add(dataPoint);
            }
        }

        /// <summary>
        /// Gets a location of data point on the grid and finds split location if point falls between grid points
        /// </summary>  
        public GridLocation GetLocation(double dataX, double dataY)
        {
            var xRatio = (dataX - DataSource.X.Min) / GridIntervalX;
            var yRatio = (dataY - DataSource.Y.Min) / GridIntervalY;
           
            var xIndex = Math.Round(xRatio, 0, MidpointRounding.ToEven);
            var yIndex = Math.Round(yRatio, 0, MidpointRounding.ToEven);

            var loc = new GridLocation();
            loc.Col = (int)xIndex;
            loc.Row = (int)yIndex;
            loc.X = dataX;
            loc.Y = dataY;

            var hasColSplit = xRatio % 1 == 0.5;
            var hasRowSplit = yRatio % 1 == 0.5;

            #region Check for split locations

            /* find split locations if data point (P) falls exactly between grid points (G)
             * so that data is evenly distributed to these grid points, where:
             * - G is grid point
             * - P is data point  
             * - L is snapped  location of data point to one of grid points
             * - O is diagonal location opposite to L
             * 
             * DATA POINT BETWEEN 4 GRID POINTS 
             *   G-------+-------G  G-------+-------G  G-------+-------G  G-------+-------G
             *   |       |       |  |       |       |  |       |       |  |       |       |
             *   |   L   |       |  |   O   |       |  |       |   O   |  |       |   L   |
             *   |       |       |  |       |       |  |       |       |  |       |       |
             *   +-------P-------+  +-------P-------+  +-------P-------+  +-------P-------+
             *   |       |       |  |       |       |  |       |       |  |       |       |
             *   |       |   O   |  |       |   L   |  |   L   |       |  |   O   |       |
             *   |       |       |  |       |       |  |       |       |  |       |       |
             *   G-------+-------G  G-------+-------G  G-------+-------G  G-------+-------G
             *  1.0     1.5     2.0
             *             
             * DATA POINT BETWEEN VERTICAL 2 GRID POINTS:
             *   G-------+-------G  G-------+-------G   
             *   |       |       |  |       |       |   
             *   |   O   |       |  |       |   L   |   
             *   |       |       |  |       |       |   
             *   P-------+-------+  +-------+-------P   
             *   |       |       |  |       |       |   
             *   |   L   |       |  |       |   O   |   
             *   |       |       |  |       |       |   
             *   G-------+-------G  G-------+-------G   
             *  1.0     1.5     2.0
             *               
             * DATA POINT BETWEEN HORIZONTAL 2 GRID POINTS:
             *   G-------P-------G  G-------+-------G   
             *   |       |       |  |       |       |   
             *   |   O   |   L   |  |       |       |   
             *   |       |       |  |       |       |   
             *   +-------+-------+  +-------+-------+   
             *   |       |       |  |       |       |   
             *   |       |       |  |   L   |   O   |   
             *   |       |       |  |       |       |   
             *   G-------+-------G  G-------P-------G   
             *  1.0     1.5     2.0
             *              
             */

            var hasTopSnapping  = loc.Row * GridIntervalY > dataY;
            var hasLeftSnapping = loc.Col * GridIntervalX < dataX;

            if (hasRowSplit && hasColSplit)
            {
                loc.Split.Add(loc);  

                loc.Split.Add(hasTopSnapping ? loc.Below() : loc.Above());
                loc.Split.Add(hasLeftSnapping ? loc.Right() : loc.Left());

                GridLocation diagnal;

                diagnal = hasTopSnapping  ? loc.Below() : loc.Above();
                diagnal = hasLeftSnapping ? diagnal.Right() : diagnal.Left();
                loc.Split.Add(diagnal);
             
            }
            else if (hasRowSplit)
            {
                loc.Split.Add(loc);
                loc.Split.Add(hasTopSnapping ? loc.Below() : loc.Above());
            }
            else if (hasColSplit)
            {
                loc.Split.Add(loc);
                loc.Split.Add(hasLeftSnapping ? loc.Right() : loc.Left()); 
            }
            #endregion

            return loc;
        }

        /// <summary>
        /// Gets a distance of grid point to data point 
        /// </summary>  
        public GridDistance GetDistance(Point3D gridPoint, Point3D dataPoint)
        {
            var distance = new GridDistance(this.DistanceMeasure); 
            distance.GridPoint = gridPoint;
            distance.DataPoint = dataPoint; 
            return distance;
        }
        /// <summary>
        /// Gets a weight of grid point based on its distance to data point 
        /// </summary>  
        public GridWeight GetWeight(Point3D gridPoint, Point3D dataPoint)
        {
            var distance = GetDistance(gridPoint, dataPoint);
            //TODO move logic to GridWeight class
            var weight = 1 / (distance.Length / this.DistanceMax);
            weight = Math.Min(weight, 1);
            if (double.IsNaN(weight) || double.IsInfinity(weight))
                weight = 1;

            return new GridWeight(weight, distance);
        }

        /// <summary>
        /// Get data points that are neighboring the grid point at specified cell step/distance 
        /// </summary>  
        protected List<Point3D> GetNeighbors(GridPoint point, int xStep, int yStep)
        {
            var x = point.Location.Col;
            var y = point.Location.Row;

            var neighors = new List<Point3D>();
            var location = new GridLocation();

            for (var row = y - yStep; row <= y + yStep; row++)
            {
                location.Col = x - xStep;
                location.Row = row;

                if (GridCells.ContainsKey(location))
                {
                    neighors.AddRange(GridCells[location].Neighbors);
                }

                location.Col = x + xStep;
                location.Row = row;
                if (GridCells.ContainsKey(location))
                {
                    neighors.AddRange(GridCells[location].Neighbors);
                }
            }
            for (var col = x - xStep; col <= x + xStep; col++)
            {
                location.Col = col;
                location.Row = y - yStep;
                if (GridCells.ContainsKey(location))
                {
                    neighors.AddRange(GridCells[location].Neighbors);
                }
                location.Col = col;
                location.Row = y + yStep;
                if (GridCells.ContainsKey(location))
                {
                    neighors.AddRange(GridCells[location].Neighbors);
                }
            }
            // search for data points in one cell farther from the current cell
            if (neighors.Count == 0)
            {
                neighors = GetNeighbors(point, xStep + 1, yStep + 1);
            }
            return neighors;
        }

        /// <summary>
        /// Get data points that are neighboring the grid point
        /// </summary>  
        public List<Point3D> GetNeighbors(GridPoint point)
        {
            var neighors = new List<Point3D>();
            var location = point.Location;

            neighors.AddRange(GridCells[location].Neighbors);

            //NOTE DataInterp.AverageAllNeighbors is faster because it is not doing the following
            if (neighors.Count == 0 &&
               (this.DataInterpolation == DataInterp.MaxClosestNeighbors ||
                this.DataInterpolation == DataInterp.InverseDistanceWeight))
            {
                // search nearby cells if no data points are neighboring this grid point
                neighors = GetNeighbors(point, 1, 1);
            }
            
            return neighors;
        }
       
        /// <summary>
        /// Distribute data points by snapping them to grid points/cells
        /// </summary>
        public void DistributeData()
        {
            if (IsReady())
            {
                foreach (var cell in GridCells.Values)
                {
                    cell.Neighbors.Clear();
                }
                  
                foreach (var point in this.DataSource)
                {
                    var location = GetLocation(point.X, point.Y);

                    if (location.Split.Count == 0)
                    {
                        Debug.WriteLine("Snap  " + point + " -> " + location);

                        Add(location, point);
                    }
                    else
                    {
                        foreach (var neighbor in location.Split)
                        {
                            var splitPoint = new Point3D(point);
                            if (DataSplitMode == DataSplitMode.EvenSplit)
                            {
                                splitPoint.Z = point.Z / location.Split.Count;
                            }
                            else if (DataSplitMode == DataSplitMode.FullSplit)
                            {
                                splitPoint.Z = point.Z;
                            }

                            if (DataSplitMode != DataSplitMode.NoSplit)
                            {
                                Debug.WriteLine(DataSplitMode + " " + point + " -> " + neighbor);

                                Add(neighbor, splitPoint);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateGrid()
        {
            if (IsReady())
            {
                Debug.WriteLine("Interpolating: " + DataInterpolation + " " + DistanceMeasure);
              
                DataInterpolator.Update(this.GridPoints);

                OnPropertyChanged("GridPoints");
            }
        }
        
        #region Event Handlers
        protected override void OnPropertyChanged(string propertyName)
        {  
            if (propertyName == "DataSource" || 
                propertyName == "MaximumPoints" || //TODO remove
                propertyName == "MaximumPointsX" ||
                propertyName == "MaximumPointsY")
            {
                GenerateGrid();
            }
            else if (propertyName == "DataSplitMode")
            {
                DistributeData();
                UpdateGrid();
            }
            else if (propertyName == "DistanceMeasure")
            {
                UpdateIntervals();
                UpdateGrid();
            }
            else if (propertyName == "DataInterpolator" ||
                     propertyName == "DataInterpolation")
            {
                UpdateGrid();
            }

            base.OnPropertyChanged(propertyName);
        }

       
        private void OnDataSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO add support of updating grid when data points or their values are changing 
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                GenerateGrid();
            }
            else
            {
                // re-generate grid only if range of X,Y has changed
                if (IsEqual(RangeX.Min, DataSource.X.Min) &&
                    IsEqual(RangeX.Max, DataSource.X.Max) &&
                    IsEqual(RangeY.Min, DataSource.Y.Min) &&
                    IsEqual(RangeY.Max, DataSource.Y.Max))
                {
                    DistributeData();
                    UpdateGrid();
                }
                else
                {
                    GenerateGrid();
                }
            }
        }

        #endregion
    }

}