using Infragistics.Samples.Shared.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace IGDiagram.ViewModels
{
    public class PascalTriangleViewModel : ObservableModel
    {
        ObservableCollection<PascalNode> _nodes = new ObservableCollection<PascalNode>();
        public ObservableCollection<PascalNode> Nodes
        {
            get { return _nodes; }
            set
            {
                this._nodes = value;
                this.OnPropertyChanged("Nodes");
            }
        }

        ObservableCollection<PascalConnection> _connections = new ObservableCollection<PascalConnection>();
        public ObservableCollection<PascalConnection> Connections
        {
            get { return _connections; }
            set
            {
                this._connections = value;
                this.OnPropertyChanged("Connections");
            }
        }

        private int _numberOfRows;
        public int NumberOfRows
        {
            get
            {
                return this._numberOfRows;
            }
            set
            {
                if (value != this._numberOfRows)
                {
                    this._numberOfRows = value; 
                    this.OnPropertyChanged("NumberOfRows");
                }
            }
        }

        private int _buffer;
        public int Buffer
        {
            get
            {
                return this._buffer;
            }
            set
            {
                if (value != this._buffer)
                {
                    this._buffer = value;
                    this.OnPropertyChanged("Buffer");
                }
            }
        }


        public PascalTriangleViewModel()
        {
            _numberOfRows = 7;
            _buffer = 50;
            LoadPascalRows();
            this.PropertyChanged += PascalTriangleViewModel_PropertyChanged;
        }

        void PascalTriangleViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Buffer" || e.PropertyName == "NumberOfRows")
            {
                LoadPascalRows();
            }
        }

        void LoadPascalRows()
        {
            this._nodes = new ObservableCollection<PascalNode>();
            this._connections = new ObservableCollection<PascalConnection>();

            for (int y = 0; y < _numberOfRows; y++)
            {
                int c = 1;
                for (int x = 0; x <= y; x++)
                {
                    _nodes.Add(new PascalNode() { Value = c, X = x, Y = y, Position = new Point((_numberOfRows - y) * _buffer + 2 * x * _buffer, 50 + y * _buffer * 1.5) });
                    c = c * (y - x) / (x + 1);
                }
                if (y > 0)
                {
                    var previousRow = Nodes.Where(node => node.Y == y - 1).ToList();
                    var currentRow = Nodes.Where(node => node.Y == y).ToList();
                    for (int x = 0; x < previousRow.Count; x++)
                    {
                        _connections.Add(new PascalConnection() { Start = previousRow[x], End = currentRow[x] });
                        _connections.Add(new PascalConnection() { Start = previousRow[x], End = currentRow[x + 1] });
                    }
                }
            }
        }
    }

    public class PascalNode : ObservableModel
    {
        private int _value;
        public int Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if (value != this._value)
                {
                    this._value = value;
                    this.OnPropertyChanged("Value");
                }
            }
        }

        private Point _position;
        public Point Position
        {
            get
            {
                return this._position;
            }
            set
            {
                if (value != this._position)
                {
                    this._position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }


        private int _x;
        public int X
        {
            get
            {
                return this._x;
            }
            set
            {
                if (value != this._x)
                {
                    this._x = value;
                    this.OnPropertyChanged("X");
                }
            }
        }

        private int _y;
        public int Y
        {
            get
            {
                return this._y;
            }
            set
            {
                if (value != this._y)
                {
                    this._y = value;
                    this.OnPropertyChanged("Y");
                }
            }
        }
    }

    public class PascalConnection : ObservableModel
    {
        private PascalNode _start;
        public PascalNode Start
        {
            get
            {
                return this._start;
            }
            set
            {
                if (value != this._start)
                {
                    this._start = value;
                    this.OnPropertyChanged("Start");
                }
            }
        }

        private PascalNode _end;
        public PascalNode End
        {
            get
            {
                return this._end;
            }
            set
            {
                if (value != this._end)
                {
                    this._end = value;
                    this.OnPropertyChanged("End");
                }
            }
        }
    }
}
