using System;

namespace Infragistics.Framework.Converters
{
    public class CodeStyles
    {
        public CodeStyles()
        {
            Background = Paint.FromString("#FF15171A");
            Whitespace = Background;
            Default = Paints.White;
            Preprocessor = Paints.Gray;
            Keyword = Paint.FromString("#FF4B98D7");
            Type = Paint.FromString("#FF5FC8A4");
            Symbol = Paints.White;
            Property = Paints.White;
            String = Paint.FromString("#FFBB7F58");
            Comment = Paint.FromString("#FF34D06A");
        }
        public Paint Background { get; set; }

        public Paint Whitespace { get; set; }
        public Paint Default { get; set; }
        public Paint Keyword { get; set; }
        public Paint Preprocessor { get; set; }
        public Paint Symbol { get; set; }
        public Paint Type { get; set; }
        public Paint Property { get; set; }
        public Paint String { get; set; }
        public Paint Comment { get; set; }
    }
}