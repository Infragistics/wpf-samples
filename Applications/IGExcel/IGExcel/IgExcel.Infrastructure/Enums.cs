
namespace IgExcel.Infrastructure
{
    public enum FontStylesCustom { Regular, Italic, Bold, BoldItalic }

    public enum ExcelBorders
    {
        BottomBorder,
        TopBorder,
        LeftBorder,
        RightBorder,
        NoBorder,
        AllBorders,
        OutsideBorder,
        ThickBoxBorder,
        BottomDoubleBorder,
        ThickBottomBorder,
        TopAndBottomBorder,
        TopAndThickBottomBorder,
        TopAndDoubleBottomBorder,
    }

    public enum MatrixModes
    {
        OneCell, TwoCellsHorizontal, TwoCellsVertical, FourCells, None
    }

    public enum BorderType
    {
        Top, Bottom, Left, Right, DiagonalDown, DiagonalUp, Horizontal, Vertical
    }
}
