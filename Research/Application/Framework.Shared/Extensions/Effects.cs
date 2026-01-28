using System;
using Xamarin.Forms;

namespace Infragistics.Framework
{ 
    public class EntryEffect : RoutingEffect
    { 
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color HintTextColor { get; set; }
        public Color HighlightColor { get; set; }
         
        public EntryEffect() : base("Infragistics.Framework.EntryEffect")
        { 
            BackgroundColor = Color.FromHex("#FFD9D9D9");
            TextColor = Color.FromHex("#FF575656");
            HintTextColor = Color.FromHex("#FF008DFF");
            HighlightColor = Color.FromHex("#FF008DFF");
        }
    }

    public class SliderEffect : RoutingEffect
    {  
        public Color ThumbColor { get; set; }
        public Color LineColor { get; set; }

        public SliderEffect() : base("Infragistics.Framework.SliderEffect")
        {  
            ThumbColor = Color.FromHex("#FF008DFF"); 
            LineColor = Color.FromHex("#FF008DFF");
        }
    }
    
}
