//using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices; 

namespace Infragistics.Framework
{
    public class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != "LastPropChanged")
                LastPropChanged = propertyName;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _LastPropChanged = "";
        public string LastPropChanged
        {
            get { return _LastPropChanged; }
            set
            {
                if (_LastPropChanged == value) return;
                _LastPropChanged = value;
                OnPropertyChanged();
            }
        }

    }


    //public class AnnoViewModel : ObservableModel
    //{
    //    private double _Thickness = 2.0;
    //    public double Thickness
    //    {
    //        get { return _Thickness; }
    //        set { if (_Thickness == value) return; _Thickness = value; OnPropertyChanged(); }
    //    }


    //    private double _Angle = 0.0;
    //    public double TextAngle
    //    {
    //        get { return _Angle; }
    //        set { if (_Angle == value) return; _Angle = value; OnPropertyChanged(); }
    //    }

    //    private double _VerticalMargin = 5.0;
    //    public double TextVerticalMargin
    //    {
    //        get { return _VerticalMargin; }
    //        set { if (_VerticalMargin == value) return; _VerticalMargin = value; OnPropertyChanged(); }
    //    }

    //    private double _HorizontalMargin = 5.0;
    //    public double TextHorizontalMargin
    //    {
    //        get { return _HorizontalMargin; }
    //        set { if (_HorizontalMargin == value) return; _HorizontalMargin = value; OnPropertyChanged(); }
    //    }

    //    private double _TextFontSize = 15.0;
    //    public double TextFontSize
    //    {
    //        get { return _TextFontSize; }
    //        set { if (_TextFontSize == value) return; _TextFontSize = value; OnPropertyChanged(); }
    //    }

    //    private TextLocation _TextLocation = OverlayTextLocation.InsideBottomLeft;
    //    public TextLocation TextLocation
    //    {
    //        get { return _TextLocation; }
    //        set { if (_TextLocation == value) return; _TextLocation = value; OnPropertyChanged(); }
    //    }

    //    private string _TextFontFamily = "Roboto Medium";
    //    public string TextFontFamily
    //    {
    //        get { return _TextFontFamily; }
    //        set { if (_TextFontFamily == value) return; _TextFontFamily = value; OnPropertyChanged(); }
    //    }

    //    private FontWeight _TextFontWeight = FontWeights.Normal;
    //    public FontWeight TextFontWeight
    //    {
    //        get { return _TextFontWeight; }
    //        set { if (_TextFontWeight == value) return; _TextFontWeight = value; OnPropertyChanged(); }
    //    }

    //    private FontStyle _TextFontStyle = FontStyles.Normal;
    //    public FontStyle TextFontStyle
    //    {
    //        get { return _TextFontStyle; }
    //        set { if (_TextFontStyle == value) return; _TextFontStyle = value; OnPropertyChanged(); }
    //    }

    //    private FontStretch _TextFontStretch = FontStretches.Normal;
    //    public FontStretch TextFontStretch
    //    {
    //        get { return _TextFontStretch; }
    //        set { if (_TextFontStretch == value) return; _TextFontStretch = value; OnPropertyChanged(); }
    //    }


    //}

}
