using System.Collections.Generic;
using System.Windows;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Models
{
    public class WorldDrillDownViewModel : ObservableModel
    {
        public WorldDrillDownViewModel(WorldMapViewModel initialMapView)
        {
            this.CurrentDrillDownLevel = WorldDrillDownLevel.World;

            this.WorldMapViews = new List<WorldMapViewModel>();
            this.WorldMapViews.Add(initialMapView);

        }
        public void DrillDown(WorldMapViewModel newMapView)
        {
            this.CurrentDrillDownLevel = newMapView.WorldDrillDownLevel;
            this.WorldMapViews.Add(newMapView);
        }
        public void DrillUp()
        {
            if (this.WorldMapViews.Count > 1)
            {
                this.WorldMapViews.RemoveAt(this.WorldMapViews.Count - 1);
                this.CurrentDrillDownLevel = this.WorldMapViews[this.WorldMapViews.Count - 1].WorldDrillDownLevel;
                this.CurrentDrillDownView = this.WorldMapViews[this.WorldMapViews.Count - 1];
                
            }
        }
        private WorldDrillDownLevel _currentDrillDownLevel;
        public WorldDrillDownLevel CurrentDrillDownLevel
        {
            get
            {
                return _currentDrillDownLevel;
            }
            private set
            {
                _currentDrillDownLevel = value;
                OnPropertyChanged("CurrentDrillDownLevel");
            }
        }
        private WorldMapViewModel _currentDrillDownView;
        public WorldMapViewModel CurrentDrillDownView
        {
            get
            {
                return _currentDrillDownView;
            }
            private set
            {
                _currentDrillDownView = value;
                OnPropertyChanged("CurrentDrillDownView");
            }
        }

        private List<WorldMapViewModel> _worldViews;
        public List<WorldMapViewModel> WorldMapViews
        {
            get
            {
                return _worldViews;
            }
            private set
            {
                _worldViews = value;
                OnPropertyChanged("WorldMapViews");
            }
        }

    }

    public class WorldMapViewModel : ObservableModel
    {
        private WorldDrillDownLevel _worldDrillDownLevel;
        public WorldDrillDownLevel WorldDrillDownLevel
        {
            get
            {
                return _worldDrillDownLevel;
            }
            set
            {
                _worldDrillDownLevel = value;
                OnPropertyChanged("WorldDrillDownLevel");
            }
        }


        private string _worldIdentifier;
        public string WorldIdentifier
        {
            get
            {
                return _worldIdentifier;
            }
            set
            {
                _worldIdentifier = value;
                OnPropertyChanged("WorldIdentifier");
            }
        }

        private Rect _worldRect;
        public Rect WorldRect
        {
            get
            {
                return _worldRect;
            }
            set
            {
                _worldRect = value;
                OnPropertyChanged("WorldRect");
            }
        }
    }

    public enum WorldDrillDownLevel
    {
        World,
        Continents,
        Countries,
        CountryRegions,
        CountryStates,
        CountryCounties,
        CountryCities,
    }
}