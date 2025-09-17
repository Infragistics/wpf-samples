using Infragistics.Framework;
using System.Windows.Controls;

namespace Infragistics.Controls.Maps
{ 
    public interface IImageryModel
    {
        Control GetImageryControl();
    }
    /// <summary>
    /// Represents a base class for all geo-imagery models.
    /// </summary>
    public abstract class ImageryModel<T> : ObservableModel, IImageryModel  
    {
        public ImageryModel()
        {
        }
        public ImageryModel(T style)
        {
            this.ImageryStyle = style;
        }

        private T _ImageryStyle;
        public T ImageryStyle
        {
            get { return _ImageryStyle; }
            set { if (_ImageryStyle.Equals(value)) return; _ImageryStyle = value; OnPropertyChanged("ImageryStyle"); }
        }
        public string ImageryName { get { return this.ToString(); } }
        
        public override string ToString()
        {
            if (ImageryStyle == null)
            {
                return GetImageryName();
            }
            return GetImageryName() + "." + ImageryStyle; 
        }

        public string GetImageryName()
        {
            return this.GetType().Name.ToString().Replace("ImageryModel", "");
        }

        public abstract Control GetImageryControl();
    }
      
    
    /// <summary>
    /// Represents a model for the BingMaps geo-imagery.
    /// </summary>
    public class BingMapsImageryModel : ImageryModel<BingMapsImageryStyle> //GeoImageryModel
    { 
        public BingMapsImageryModel(BingMapsImageryStyle style) : base(style)
        { 
        } 

        public override Control GetImageryControl()
        {
            return new BingMapsMapImagery
            {
                ImageryStyle = ImageryStyle,
                ApiKey = "Aj2inbXEmvSbAbbVUKH1-DWlUeWIGjFTY3K2K8d1lqE5mEwI3_1oFX2U2tqONiHF",
                IsDeferredLoad = false
            };
        }
    }
     
    /// <summary>
    /// Represents a model for the ArcGISOnlineMap geo-imagery.
    /// </summary>
    public class EsriImageryModel : ImageryModel<EsriImageryStyle> //: GeoImageryModel
    {
        public EsriImageryModel(EsriImageryStyle style) : base(style)
        {
        } 

        public override Control GetImageryControl()
        {
            return new EsriMapImagery(this.ImageryStyle);
        }
    }


    /// <summary>
    /// Represents a model for the OpenStreetMap geo-imagery.
    /// </summary>
    public class OpenStreetImageryModel : ImageryModel<object>
    {
        public override Control GetImageryControl()
        {
            return new OpenStreetMapImagery();
        }
    }

    public class ThunderForestImageryModel : ImageryModel<ThunderForestIStyle>
    {
        public ThunderForestImageryModel(ThunderForestIStyle style) : base(style)
        {
        }

        public override Control GetImageryControl()
        {
            var imagery = new ThunderForestImagery();
            imagery.MapStyle = ImageryStyle;
            imagery.MapKey = "4087c6a4943d412ab143fa8d0e49c28a";
            return imagery;
        }
    }
}
