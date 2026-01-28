using System.Collections.ObjectModel;
using Infragistics.Controls.Maps;       // XamMap, ProjectionType

namespace IGMap.Models
{
    public class MapProjectionModel
    {

        public MapProjectionModel()
        {
            this.ProjectionType = ProjectionType.SphericalMercator;
        }
        public MapProjectionModel(XamMap projectionMap)
        {
            this.ProjectionType = projectionMap.MapProjectionType;
            this.ProjectionMap = projectionMap;
        }

        public string ProjectionName { get { return this.ProjectionType.ToString(); } }
        public ProjectionType ProjectionType { get; set; }
        public XamMap ProjectionMap { get; private set; }

        public new string ToString()
        {
            return this.ProjectionName;
        }
    }
    public class MapProjections
    {
        public static ObservableCollection<MapProjectionModel> List
        {
            get
            {
                var list = new ObservableCollection<MapProjectionModel>();
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Balthasart,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Behrmann,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Equirectangular,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.GallOrthographic,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Lambert,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Mercator,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Miller37,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Miller43,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Miller50,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.MillerCylindrical,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.ObliqueMercator, });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.Peters,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.SphericalMercator,  });
                list.Add(new MapProjectionModel { ProjectionType = ProjectionType.TristanEdwards,  });
                return list;
            }
        }
    }
}