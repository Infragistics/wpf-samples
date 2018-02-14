using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgWord.Infrastructure
{
    public enum LineSpacingTypes
    {
        [Description("Single")]
        Single,
        [Description("1.5 lines")]
        OnePointFiveLines,
        [Description("Double")]
        Double,
        [Description("At least")]
        AtLeast,
        [Description("Exactly")]
        Exactly,
        [Description("Multiple")]
        Multiple
    }

    public enum BulletListStyles
    {
        None,
        Bullet,
        Bullet_Check,
        BulletArrow,
        BulletCircle,
        BulletDiamond,
        BulletSquare
    }
    public enum NumberListStyles
    {
        None,
        Decimal,
        DecimalConcatenated,
        DecimalParenthesis,
        LowerLetter,
        LowerLetterParenthesis,
        LowerRoman,
        UpperLetter,
        UpperRoman
    }

    public enum FontStyle
    {
        Regular, Italic, Bold, BoldItalic
    }
    public enum FirstLineType
    {
        None, FirstLine, Hanging
    }
}
