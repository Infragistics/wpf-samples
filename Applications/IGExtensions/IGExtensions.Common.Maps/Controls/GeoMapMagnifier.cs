using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Controls
{

    //TODO: create a new instance of GeoMap and bind changes to MapControl
    [TemplatePart(Name = GeoMapMagnifier.MagnificationMapName, Type = typeof(XamGeographicMap))]
    [TemplatePart(Name = GeoMapMagnifier.MagnificationMapContainerName, Type = typeof(Grid))]
    [TemplatePart(Name = GeoMapMagnifier.MagnificationMapMapVisibilityButtonName, Type = typeof(Button))]
    //MapImagerySyncToggleButton
    public class GeoMapMagnifier : GeoMapPane 
    {
        public GeoMapMagnifier()
        {
            this.MagnificationMapImageryView = new OpenStreetMapImageryView();

            DefaultStyleKey = typeof(GeoMapMagnifier);

        }
        public const string MagnificationMapMapVisibilityButtonName = "MagnificationMapMapVisibilityButton";
        public const string MagnificationMapContainerName = "MagnificationMapContainer";
        public const string MagnificationMapName = "MagnificationMap";

        protected Button MagnificationMapMapVisibilityButton = null;

        #region Event Handlers
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //if (this.MagnificationMap != null)
            //{
                
            //}

            //var templateMap = GetTemplateChild(MagnificationMapName) as XamGeographicMap;
            this.MagnificationMapMapVisibilityButton = GetTemplateChild(MagnificationMapMapVisibilityButtonName) as Button;
          
            if (this.MagnificationMapMapVisibilityButton != null)
            {
                this.MagnificationMapMapVisibilityButton.Click += new RoutedEventHandler(MagnificationMapMapVisibilityButton_Click);
            }
          
            if (this.MagnificationMap == null)
                this.MagnificationMap = GetTemplateChild(MagnificationMapName) as XamGeographicMap;
            else
            {
                //var templateMap = GetTemplateChild(MagnificationMapName) as XamGeographicMap;
                //templateMap = this.MagnificationMap;

                var mapContainer = GetTemplateChild(MagnificationMapContainerName) as Grid;
                if (mapContainer != null)
                {
                    mapContainer.Children.Clear();
                    mapContainer.Children.Add(this.MagnificationMap);
                }
            }

            if (this.MagnificationMap != null)
            {
                this.MagnificationMap.IncreaseImageryZoomMaxLevel();
                this.MagnificationMap.Zoomable = false;
                //this.MagnificationMap.VerticalZoomable = false;
                this.MagnificationMap.IsHitTestVisible = false;
                this.MagnificationMap.OverviewPlusDetailPaneVisibility = Visibility.Collapsed;
                this.MagnificationMap.MouseWheel += new MouseWheelEventHandler(MagnificationMap_MouseWheel);
                this.MagnificationMap.MouseRightButtonUp += OnMagnificationMapMouseRightClick;
                this.MagnificationMap.MouseRightButtonDown += OnMagnificationMapMouseRightClick;

               
                this.MouseMove += new MouseEventHandler(GeoMapMagnifier_MouseMove);
            }
           
           // UpdateParts();
        }

        void MagnificationMapMapVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            this.MagnificationMap.Visibility = this.MagnificationMap.Visibility == Visibility.Visible
                                                   ? Visibility.Collapsed
                                                   : Visibility.Visible;
        }

        void GeoMapMagnifier_MouseMove(object sender, MouseEventArgs e)
        {
   
        }

        void OnMagnificationMapMouseRightClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

         
        void MagnificationMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        protected MagnificationMapUpdateMode MapInteractionMode = MagnificationMapUpdateMode.Hovering;

        void OnGeographicMapMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            //this.Visibility = Visibility.Visible;
            MapInteractionMode = MagnificationMapUpdateMode.RightClickAndDragging;
            //OnWindowStartMoving(this.MapControl, e);
            //e.Handled = true;
        }
        void OnGeographicMapMouseRightUp(object sender, MouseButtonEventArgs e)
        {
            MapInteractionMode = MagnificationMapUpdateMode.Hovering;
            //OnWindowStopMoving(this.MapControl, e);
            //e.Handled = true;
            //this.Visibility = Visibility.Collapsed;
        }

        void OnGeographicMapMouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            MapInteractionMode = MagnificationMapUpdateMode.Hovering;
        }
        void OnGeographicMapMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            MapInteractionMode = MagnificationMapUpdateMode.LeftClickAndDragging;
        }
        private void OnGeographicMapMouseMove(object sender, MouseEventArgs e)
        {
            var refreshMagnificationMap = false;
            if (this.MagnificationMapUpdateMode == this.MapInteractionMode) // RightClickAndDragging
            {
                refreshMagnificationMap = true; // MagnificationMapUpdateMode
            }
            //if (this.MagnificationMapUpdateMode == this.MapInteractionMode)
            //{
            //    refreshMagnificationMap = true;
            //}
            if (this.MagnificationMapUpdateMode == MagnificationMapUpdateMode.Hovering)
            {
                refreshMagnificationMap = true;
            }
            if (refreshMagnificationMap)
            {
                var mousePosition = e.GetPosition(this.MapControl);
                UpdateMagnificationMap(mousePosition);
                //this.Move(mousePosition.X, mousePosition.Y);
                //OnWindowIsMoving(this.MapControl, e);
            }
        }
        private void UpdateMagnificationMap(Point mousePosition)
        {
            MapGeoLocation = this.MapControl.GetGeographicPoint(mousePosition);
            UpdateMagnificationMap();
        }
        private void UpdateMagnificationMap()
        {
            //if (this.MangificationMapScaleMode == MagnificationMapScaleMode.ScaledMagnificationMap)
            //{
            //    if (this.MapControl != null)
            //    {
            //        var mapScale = System.Math.Max(this.MapControl.ActualWindowScaleVertical,
            //                                       this.MapControl.ActualWindowScaleHorizontal);
            //        //MagnificationMapScale = mapScale * this.MagnificationMapScale;
            //    }
            //}
          
            if (this.MagnificationMap != null && this.MapControl != null)
            {
                var mousePosition = this.MapControl.GetMapPosition(MapGeoLocation);
                this.MagnificationMap.ZoomMapToLocation(this.MapControl, mousePosition, this.MagnificationMapScale);
                //this.MagnificationMap.ZoomMapToLocation(geoLocation, MagnificationMapScale);
            }
        }
        protected Point MapGeoLocation = new Point(0, 51);

        #region Properties
        public const string IsMapImageryViewSynchronizedPropertyName = "IsMapImageryViewSynchronized";
        public static readonly DependencyProperty IsMapImageryViewSynchronizedProperty = DependencyProperty.Register(
            IsMapImageryViewSynchronizedPropertyName, typeof(bool), typeof(GeoMapMagnifier), new PropertyMetadata(true, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(IsMapImageryViewSynchronizedPropertyName));
        })); 

        /// <summary>
        /// Gets or sets the IsMapImageryViewSynchronized property 
        /// </summary>
        public bool IsMapImageryViewSynchronized
        {
            get { return (bool)GetValue(IsMapImageryViewSynchronizedProperty); }
            set { SetValue(IsMapImageryViewSynchronizedProperty, value); }
        }
        public const string MagnificationMapImageryViewPropertyName = "MagnificationMapImageryView";
        public static readonly DependencyProperty MagnificationMapImageryViewProperty = DependencyProperty.Register(
            MagnificationMapImageryViewPropertyName, typeof(GeoImageryViewModel), typeof(GeoMapMagnifier), new PropertyMetadata(null, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(MagnificationMapImageryViewPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the MagnificationMapImageryView property 
        /// </summary>
        public GeoImageryViewModel MagnificationMapImageryView
        {
            get { return (GeoImageryViewModel)GetValue(MagnificationMapImageryViewProperty); }
            set { SetValue(MagnificationMapImageryViewProperty, value); }
        }
        public const string MagnificationMapPropertyName = "MagnificationMap";
        public static readonly DependencyProperty MagnificationMapProperty = DependencyProperty.Register(
            MagnificationMapPropertyName, typeof(XamGeographicMap), typeof(GeoMapMagnifier), new PropertyMetadata(null, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(MagnificationMapPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the MagnificationMap property 
        /// </summary>
        public XamGeographicMap MagnificationMap
        {
            get { return (XamGeographicMap)GetValue(MagnificationMapProperty); }
            set { SetValue(MagnificationMapProperty, value); }
        }
        public const string MagnificationMapUpdateModePropertyName = "MagnificationMapUpdateMode";
        public static readonly DependencyProperty MagnificationMapUpdateModeProperty = DependencyProperty.Register(
            MagnificationMapUpdateModePropertyName, typeof(MagnificationMapUpdateMode), typeof(GeoMapMagnifier),
            new PropertyMetadata(MagnificationMapUpdateMode.RightClickAndDragging, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(MagnificationMapUpdateModePropertyName));
        }));
        /// <summary>
        /// Gets or sets the MagnificationMapUpdateMode property 
        /// </summary>
        public MagnificationMapUpdateMode MagnificationMapUpdateMode
        {
            get { return (MagnificationMapUpdateMode)GetValue(MagnificationMapUpdateModeProperty); }
            set { SetValue(MagnificationMapUpdateModeProperty, value); }
        }
        public const string MangificationMapScaleModePropertyName = "MangificationMapScaleMode";
        public static readonly DependencyProperty MangificationMapScaleModeProperty = DependencyProperty.Register(
            MangificationMapScaleModePropertyName, typeof(MagnificationMapScaleMode), typeof(GeoMapMagnifier),
            new PropertyMetadata(MagnificationMapScaleMode.ScaledMagnificationMap, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(MangificationMapScaleModePropertyName));
        }));
        /// <summary>
        /// Gets or sets the MangificationMapScaleMode property 
        /// </summary>
        public MagnificationMapScaleMode MangificationMapScaleMode
        {
            get { return (MagnificationMapScaleMode)GetValue(MangificationMapScaleModeProperty); }
            set { SetValue(MangificationMapScaleModeProperty, value); }
        }
        public const string MagnificationMapScalePropertyName = "MagnificationMapScale";
        public static readonly DependencyProperty MagnificationMapScaleProperty = DependencyProperty.Register(
            MagnificationMapScalePropertyName, typeof(double), typeof(GeoMapMagnifier), new PropertyMetadata(2.0, (sender, e) =>
        {
            (sender as GeoMapMagnifier).OnPropertyChanged(new PropertyChangedEventArgs(MagnificationMapScalePropertyName));
        }));
        /// <summary>
        /// Gets or sets the MagnificationMapScale property 
        /// </summary>
        public double MagnificationMapScale
        {
            get { return (double)GetValue(MagnificationMapScaleProperty); }
            set { SetValue(MagnificationMapScaleProperty, value); }
        } 
        #endregion
        #endregion
        public override void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (this.IsUpdating) return;

            switch (ea.PropertyName)
            {
                case MapControlPropertyName:
                    {
                        if (this.MapControl != null)
                        {
                            
                            this.MapControl.MouseMove += OnGeographicMapMouseMove;
                            this.MapControl.MouseLeftButtonDown += OnGeographicMapMouseLeftDown;
                            this.MapControl.MouseLeftButtonUp += OnGeographicMapMouseLeftUp;
                            this.MapControl.MouseRightButtonDown += OnGeographicMapMouseRightDown;
                            this.MapControl.MouseRightButtonUp += OnGeographicMapMouseRightUp;
                            this.MapControl.PropertyChanged += OnGeographicMapPropertyChanged;
                            //this.MapControl.Series.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Series_CollectionChanged);
                        }
                
                        break;
                    }
                case MagnificationMapImageryViewPropertyName:
                    {
                        if (!this.IsMapImageryViewSynchronized)
                            UpdateImageryView(this.MagnificationMapImageryView);

                        //UpdateImageryView(this.MagnificationMapImageryView);
                     
                        //if (this.MapControl != null)
                        //    this.MapControl.MouseMove += OnGeographicMapMouseMove;
                        break;
                    }
                case MagnificationMapScalePropertyName:
                    {
                        UpdateMagnificationMap();
                        //if (this.MapControl != null)
                        //    this.MapControl.MouseMove += OnGeographicMapMouseMove;
                        break;
                    }
                //case MapCoordinateDisplayModePropertyName:
                //    {
                //        break;
                //    }
             

            }
            base.OnPropertyChanged(ea);
        }

        void Series_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                foreach (var series in e.NewItems) //this.MapControl.Series)
                {
                    Series sc = null;

                    if(series is GeographicShapeSeries)
                    {
                               //sc = (GeographicShapeSeries)CloneObject(series as GeographicShapeSeries);
                               //sc = series.XamlClone<GeographicShapeSeries>();
                        //sc = series.Clone<GeographicShapeSeries>();
                        //sc = Clone(series) as GeographicShapeSeries;
                        sc = (series as GeographicShapeSeries).Duplicate();

                    }
                    if (series is GeographicProportionalSymbolSeries)
                        sc = (series as GeographicProportionalSymbolSeries).Duplicate();

                        //sc = (GeographicProportionalSymbolSeries)CloneObject(series as GeographicProportionalSymbolSeries);

                    if (sc != null)
                        this.MagnificationMap.Series.Add(sc);
                    // var sc = (Series)CloneObject(series);
                    //sc.SeriesViewer = 
                }
            }
            //this.MagnificationMap.Series.Clear();
            //foreach (var series in this.MapControl.Series)
            //{
            //    this.MagnificationMap.Series.Add(series);
            //}
        }

         private void UpdateImageryView(GeoImageryViewModel geoImageryView)
        {
            if (this.MagnificationMap != null && geoImageryView != null) 
                this.MagnificationMap.LoadGeoImagery(geoImageryView);
        }

        void OnGeographicMapPropertyChanged(object sender, PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case "BackgroundContent":
                    {
                        //if (this.MagnificationMap == null || this.MapControl == null) return;
                        if (this.MapControl == null || !this.IsMapImageryViewSynchronized) return;
                        //var breakHere=true;
                        //var geoImagery = this.MapControl.BackgroundContent;
                        var geoImageryView = this.MapControl.GetGeoImageryViewModel();
                        this.MagnificationMap.LoadGeoImagery(geoImageryView);
                        
                        //this.MagnificationMapImageryView = geoImageryView;
                        //UpdateImageryView(geoImageryView);
                        //
                        break;
            
                    }
                case "ActualWindowRect":
                {
                    //update mag map
                    UpdateMagnificationMap();
                    break;

                }
                //case "Series":
                //{
                //    this.MagnificationMap.Series.Clear();
                //    foreach (var series in this.MapControl.Series)
                //    {
                //        this.MagnificationMap.Series.Add(series);
                //    }
                //    break;
                //}
            }
        }
 
    
    }
    public enum MagnificationMapScaleMode
    {
        FixedMagnificationMap,
        ScaledMagnificationMap
    }
    //public enum MagnificationMapUpdate
    //{
    //    OnRightClickAndDrag,
    //    OnLeftClickAndDrag,
    //    OnHover
    //}
    public enum MagnificationMapUpdateMode
    {
        RightClickAndDragging,
        LeftClickAndDragging,
        Hovering,
        
    }
    public class MagnificationMapUpdateModes :List<MagnificationMapUpdateMode>
    {
        public MagnificationMapUpdateModes()
        {
            this.Add(MagnificationMapUpdateMode.RightClickAndDragging);
            this.Add(MagnificationMapUpdateMode.LeftClickAndDragging);
            this.Add(MagnificationMapUpdateMode.Hovering);
      
        }
    }
    public static class ExtensionMethods
    {
        //http://blogs.microsoft.co.il/blogs/tamir/archive/2008/05/06/drawingbrush-and-deep-clone-in-silverlight.aspx
        //public static T CloneObject<T>(this T source) where T : DependencyObject
        //{
        //    Type t = source.GetType();
        //    T no = (T)Activator.CreateInstance(t);
        //    Type wt = t;
        //    while (wt.BaseType != typeof(DependencyObject))
        //    {
        //        FieldInfo[] fi = wt.GetFields(BindingFlags.Static | BindingFlags.Public);
        //        for (int n = 0; n < fi.Length; n++)
        //        {
        //            {
        //                DependencyProperty dp = fi[n].GetValue(source) as DependencyProperty;
        //                if (dp != null && fi[n].Name != "NameProperty")
        //                {
        //                    DependencyObject obj = source.GetValue(dp) as DependencyObject;
        //                    if (obj != null)
        //                    {
        //                        object o = obj.CloneObject();
        //                        no.SetValue(dp, o);
        //                    }

        //                    else
        //                    {
        //                        if (fi[n].Name != "CountProperty" &&
        //                            fi[n].Name != "GeometryTransformProperty" &&
        //                            fi[n].Name != "ActualWidthProperty" &&
        //                            fi[n].Name != "ActualHeightProperty" &&
        //                            fi[n].Name != "MaxWidthProperty" &&
        //                            fi[n].Name != "MaxHeightProperty" &&
        //                            fi[n].Name != "StyleProperty")
        //                        {
        //                            no.SetValue(dp, source.GetValue(dp));
        //                        }
        //                    }
        //                    wt = wt.BaseType;
        //                    PropertyInfo[] pis = t.GetProperties();

        //                    #region MyRegion

        //                    for (int i = 0; i < pis.Length; i++)
        //                    {

        //                        if (
        //                            pis[i].Name != "Name" &&
        //                            pis[i].Name != "Parent" &&
        //                            pis[i].CanRead && pis[i].CanWrite &&
        //                            !pis[i].PropertyType.IsArray &&
        //                            !pis[i].PropertyType.IsSubclassOf(typeof(DependencyObject)) &&
        //                            pis[i].GetIndexParameters().Length == 0 &&
        //                            pis[i].GetValue(source, null) != null &&
        //                            pis[i].GetValue(source, null) == (object)default(int) &&
        //                            pis[i].GetValue(source, null) == (object)default(double) &&
        //                            pis[i].GetValue(source, null) == (object)default(float)
        //                            )
        //                            pis[i].SetValue(no, pis[i].GetValue(source, null), null);

        //                        else if (pis[i].PropertyType.GetInterface("IList", true) != null)
        //                        {
        //                            int cnt =
        //                                (int)
        //                                pis[i].PropertyType.InvokeMember("get_Count", BindingFlags.InvokeMethod, null,
        //                                                                 pis[i].GetValue(source, null), null);
        //                            for (int c = 0; c < cnt; c++)
        //                            {
        //                                object val = pis[i].PropertyType.InvokeMember("get_Item",
        //                                                                              BindingFlags.InvokeMethod, null,
        //                                                                              pis[i].GetValue(source, null),
        //                                                                              new object[] { c });

        //                                object nVal = val;
        //                                DependencyObject v = val as DependencyObject;
        //                                if (v != null)
        //                                    nVal = v.CloneObject();

        //                                pis[i].PropertyType.InvokeMember("Add", BindingFlags.InvokeMethod, null,
        //                                                                 pis[i].GetValue(no, null), new object[] { nVal });
        //                            }
        //                        }

        //                    }

        //                    #endregion
        //                }
        //            }
        //        }
        //    }
        //}
        private static object CloneObject2(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p;
        }
       
    
    }
}