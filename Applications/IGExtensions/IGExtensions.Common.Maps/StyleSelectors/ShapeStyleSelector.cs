using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using Infragistics;
using Infragistics.Controls.Charts;
#if WPF
//using StyleSelectorNamespace = System.Windows.Controls;
using System.Windows.Controls;
#endif

namespace IGExtensions.Common.Maps.StyleSelectors
{
    public class ShapeStyleSettings : ObservableModel
    {
        public ShapeStyleSettings()
        {
            this.ShapeStyleSelectorType = StyleSelectorType.SingleShapeStyle;
            this.ShapeStyleSelector = GetShapeStyleSelector();
            this.PropertyChanged += OnPropertyChanged;
            var path = new Path();
            //path.StrokeThickness
        }

        public new void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs ea)
        {
            //base()
            switch (ea.PropertyName)
            {
                case "StyleSelectorType":
                    {
                        this.ShapeStyleSelector = GetShapeStyleSelector();
                        this.ShapeStyle = GetShapeStyle();
                        break;
                    }
                case "ShapeBrushes":
                    {
                        if (this.ShapeStyleSelectorType == StyleSelectorType.RandomShapeStyle)
                            this.ShapeStyleSelector = GetShapeStyleSelector();
                        break;
                    }
                case "ShapeFill":
                    {
                        if (this.ShapeStyleSelectorType == StyleSelectorType.SingleShapeStyle)
                            this.ShapeStyleSelector = GetShapeStyleSelector();
                        break;
                    }

            }
        }
        public Style GetShapeStyle()
        {
            if (this.ShapeStyleSelectorType == StyleSelectorType.SingleShapeStyle)
            {
                if (this.ShapeStyleTarget == ShapeStyleTarget.ShapePath)
                {
                    var stylePath = new Style(typeof(Path));
                    stylePath.Setters.Add(new Setter(Path.FillProperty, this.ShapeFill));
                    stylePath.Setters.Add(new Setter(Path.StrokeProperty, this.ShapeStroke));
                    stylePath.Setters.Add(new Setter(Path.StrokeThicknessProperty, this.ShapeStrokeThickness));
                    return stylePath;
                }
                else if (this.ShapeStyleTarget == ShapeStyleTarget.ShapeControl)
                {
                    var styleControl = new Style(typeof(ShapeControl));
                    styleControl.Setters.Add(new Setter(ShapeControl.BackgroundProperty, this.ShapeFill));
                    styleControl.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, this.ShapeStroke));
                    styleControl.Setters.Add(new Setter(ShapeControl.BorderThicknessProperty, this.ShapeStrokeThickness));
                    return styleControl;
                }

                //return new RandomFillStyleSelector { Brushes = new BrushCollection { this.ShapeFill } };
            }
            //else if (this.ShapeStyleSelectorType == StyleSelectorType.RandomShapeStyle)
            //{
            //    return new RandomFillStyleSelector { Brushes = this.ShapeBrushes };
            //}

            var style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, this.ShapeFill));
            style.Setters.Add(new Setter(Path.StrokeProperty, this.ShapeStroke));
            style.Setters.Add(new Setter(Path.StrokeThicknessProperty, this.ShapeStrokeThickness));
            return style;
           
        }

        public StyleSelector GetShapeStyleSelector()
        {
            if (this.ShapeStyleSelectorType == StyleSelectorType.SingleShapeStyle)
            {
                return new RandomFillStyleSelector { Brushes = this.ShapeBrushes };
          //      return new RandomFillStyleSelector { Brushes = new BrushCollection { this.ShapeFill } };
            }
            else if (this.ShapeStyleSelectorType == StyleSelectorType.RandomShapeStyle)
            {
                return new RandomFillStyleSelector { Brushes = this.ShapeBrushes };
            }
            else if (this.ShapeStyleSelectorType == StyleSelectorType.RangeShapeStyle)
            {
                if (string.IsNullOrEmpty(this.ShapeFillMemberPath))
                {
                    return new RandomFillStyleSelector { Brushes = new BrushCollection { this.ShapeFill } };
                }
                var shapeSelector = new RangeConditionalStyleSelector(); 
                

                return new RandomFillStyleSelector { Brushes = this.ShapeBrushes };
            }
            //else if (this.StyleSelectorType == StyleSelectorType.RandomShapeStyle)
            //{
            //    var shapeSelector = new RandomSelectStyleSelector(); 
            //    foreach (var shapeBrush in ShapeBrushes)
            //    {
            //        var style = new Style(typeof (Path));
            //        style.Setters.Add(new Setter(Path.FillProperty, shapeBrush));
            //        style.Setters.Add(new Setter(Path.StrokeProperty, this.ShapeStroke));
            //        style.Setters.Add(new Setter(Path.StrokeThicknessProperty, this.ShapeThickness));
            //        shapeSelector.Styles.Add(style);
            //    }
            //    return shapeSelector;
            //}

            return new RandomFillStyleSelector { Brushes = new BrushCollection { this.ShapeFill } };
        }

        #region Properties
        private string _shapeFillMemberPath;
        /// <summary>
        /// Gets or sets ShapeFillMemberPath property 
        /// </summary>
        public string ShapeFillMemberPath
        {
            get {  return _shapeFillMemberPath; }
            set { if (_shapeFillMemberPath == value) return; _shapeFillMemberPath = value; OnPropertyChanged("ShapeFillMemberPath"); }
        }

        private StyleSelector _styleSelector;
        /// <summary>
        /// Gets or sets StyleSelector property 
        /// </summary>
        public StyleSelector ShapeStyleSelector
        {
            get { return _styleSelector; }
            set { if (_styleSelector == value) return; _styleSelector = value; OnPropertyChanged("ShapeStyleSelector"); }
        }

        private StyleSelectorType _styleSelectorType = StyleSelectorType.SingleShapeStyle;
        /// <summary>
        /// Gets or sets ShapeStyleSelectorType property 
        /// </summary>
        public StyleSelectorType ShapeStyleSelectorType
        {
            get { return _styleSelectorType; }
            set { if (_styleSelectorType == value) return; _styleSelectorType = value; OnPropertyChanged("ShapeStyleSelectorType"); }
        }

        private Style _shapeStyle;
        /// <summary>
        /// Gets or sets ShapeStyle property 
        /// </summary>
        public Style ShapeStyle
        {
            get {  return _shapeStyle; }
            set { if (_shapeStyle == value) return; _shapeStyle = value; OnPropertyChanged("ShapeStyle"); }
        }

        private ShapeStyleTarget _shapeStyleTarget = ShapeStyleTarget.ShapePath;
        /// <summary>
        /// Gets or sets ShapeStyleTarget property 
        /// </summary>
        public ShapeStyleTarget ShapeStyleTarget
        {
            get { return _shapeStyleTarget; }
            set { if (_shapeStyleTarget == value) return; _shapeStyleTarget = value; OnPropertyChanged("ShapeStyleTarget"); }
        }

        private BrushCollection _shapeBrushes = NamedColors.Collections.RedColors.ToBrushCollection();
        /// <summary>
        /// Gets or sets ShapeBrushes property 
        /// </summary>
        public BrushCollection ShapeBrushes
        {
            get {  return _shapeBrushes; }
            set { if (_shapeBrushes == value) return; _shapeBrushes = value; OnPropertyChanged("ShapeBrushes"); }
        }

        private Brush _shapeFill = NamedColors.DodgerBlue.BrushOpacity(0.5);
        /// <summary>
        /// Gets or sets ShapeFill property 
        /// </summary>
        public Brush ShapeFill
        {
            get { return _shapeFill; }
            set
            {
                if (_shapeFill == value) return;
                value.Opacity = this.ShapeFillOpacity;
                _shapeFill = value; OnPropertyChanged("ShapeFill");
            }
        }
        /// <summary>
        /// Gets or sets opacity of ShapeFill property 
        /// </summary>
        public double ShapeFillOpacity
        {
            get { return this.ShapeFill.Opacity; }
            set { if (this.ShapeFill.Opacity == value) return; this.ShapeFill.Opacity = value; OnPropertyChanged("ShapeFillOpacity"); }
        }
        private Brush _shapeStroke = NamedColors.White.Brush;
        /// <summary>
        /// Gets or sets ShapeStroke property 
        /// </summary>
        public Brush ShapeStroke
        {
            get { return _shapeStroke; }
            set 
            { 
                if (_shapeStroke == value) return;
                value.Opacity = this.ShapeStrokeOpacity;
                _shapeStroke = value; OnPropertyChanged("ShapeStroke"); 
            }
        }
        /// <summary>
        /// Gets or sets opacity of ShapeStroke property 
        /// </summary>
        public double ShapeStrokeOpacity
        {
            get { return this.ShapeStroke.Opacity; }
            set { if (this.ShapeStroke.Opacity == value) return; this.ShapeStroke.Opacity = value; OnPropertyChanged("ShapeStrokeOpacity"); }
        }
        private double _shapeStrokeThickness = 0.75;
        /// <summary>
        /// Gets or sets ShapeStrokeThickness property 
        /// </summary>
        public double ShapeStrokeThickness
        {
            get { return _shapeStrokeThickness; }
            set { if (_shapeStrokeThickness == value) return; _shapeStrokeThickness = value; OnPropertyChanged("ShapeStrokeThickness"); }
        } 
        #endregion
    }
    public enum StyleSelectorType
    {
        //SolidShapeStyle,
        //GradientShapeStyle,
        SingleShapeStyle,
        RandomShapeStyle,
        RangeShapeStyle,

    }
    public enum ShapeStyleTarget
    {
        ShapePath,
        ShapeControl,
    }

    public class StyleSelectorList : List<StyleSelectorItem>
    { }
    public class StyleSelectorItem
    {
        public StyleSelector StyleSelector { get; set; }
        public string StyleName { get; set; }
    }
}