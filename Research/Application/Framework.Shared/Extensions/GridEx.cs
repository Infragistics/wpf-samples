namespace Xamarin.Forms
{
    public static class GridEx 
    {
        public static void AddRow(this Grid grid, RowDefinition row)
        {
            grid.RowDefinitions.Add(row);
        }
        public static void AddColumn(this Grid grid, ColumnDefinition column)
        {
            grid.ColumnDefinitions.Add(column);
        }
        /// <summary>
        /// Add a <see cref="RowDefinition"/> created from specified size and unit type of a row
        /// </summary>
        public static void AddRow(this Grid grid, GridUnitType type = GridUnitType.Star, double height = 1.0)
        {
            var row = CreateRow(height, type);
            grid.RowDefinitions.Add(row);
        }
        /// <summary>
        /// Add a <see cref="ColumnDefinition"/> created from specified size and unit type of a column
        /// </summary>
        public static void AddColumn(this Grid grid, GridUnitType type = GridUnitType.Star, double width = 1.0)
        {
            var column = CreateColumn(width, type);
            grid.ColumnDefinitions.Add(column);
        }
       
        /// <summary>
        /// Creates a <see cref="RowDefinition"/> from specified size and unit type of a row
        /// </summary>
        public static RowDefinition CreateRow(double height, GridUnitType type)
        {
            var size = new GridLength(height, type);
            return size.CreateRow();
        }
         
        /// <summary>
        /// Creates a <see cref="ColumnDefinition"/> from specified size and unit type of a column
        /// </summary>
        public static ColumnDefinition CreateColumn(double width, GridUnitType type)
        {
            var size = new GridLength(width, type);
            return size.CreateColumn();
        }
    }

    public static class GridLengthEx
    {
        /// <summary>
        /// Creates a <see cref="RowDefinition"/> from this <see cref="GridLength"/> object
        /// </summary>
        public static RowDefinition CreateRow(this GridLength length)
        {
            var row = new RowDefinition { Height = length };
            return row;
        }

        /// <summary>
        /// Creates a <see cref="ColumnDefinition"/> from this <see cref="GridLength"/> object
        /// </summary>
        public static ColumnDefinition CreateColumn(this GridLength length)
        {
            var row = new ColumnDefinition { Width = length };
            return row;
        }
    }
}