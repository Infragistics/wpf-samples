namespace Infragistics.Framework 
{ 
    /// <summary>
    /// Determines how to distribute data points if they fall between grid points
    /// </summary>
    /// <remarks>
    /// - G is grid point
    /// - P is data point  
    /// - L is snapped  location of data point to one of grid points
    /// - O is diagonal location opposite to L
    /// 
    ///  DATA POINT BETWEEN 4 GRID POINTS 
    ///    G-------+-------G  G-------+-------G  G-------+-------G  G-------+-------G
    ///    |       |       |  |       |       |  |       |       |  |       |       |
    ///    |   L   |       |  |   O   |       |  |       |   O   |  |       |   L   | 
    ///    |       |       |  |       |       |  |       |       |  |       |       | 
    ///    +-------P-------+  +-------P-------+  +-------P-------+  +-------P-------+ 
    ///    |       |       |  |       |       |  |       |       |  |       |       | 
    ///    |       |   O   |  |       |   L   |  |   L   |       |  |   O   |       | 
    ///    |       |       |  |       |       |  |       |       |  |       |       | 
    ///    G-------+-------G  G-------+-------G  G-------+-------G  G-------+-------G 
    ///   1.0     1.5     2.0
    ///              
    ///  DATA POINT BETWEEN VERTICAL 2 GRID POINTS:
    ///    G-------+-------G  G-------+-------G   
    ///    |       |       |  |       |       |   
    ///    |   O   |       |  |       |   L   |   
    ///    |       |       |  |       |       |    
    ///    P-------+-------+  +-------+-------P    
    ///    |       |       |  |       |       |    
    ///    |   L   |       |  |       |   O   |   
    ///    |       |       |  |       |       |   
    ///    G-------+-------G  G-------+-------G   
    ///   1.0     1.5     2.0
    ///                
    ///  DATA POINT BETWEEN HORIZONTAL 2 GRID POINTS:
    ///    G-------P-------G  G-------+-------G   
    ///    |       |       |  |       |       |   
    ///    |   O   |   L   |  |       |       |   
    ///    |       |       |  |       |       |   
    ///    +-------+-------+  +-------+-------+   
    ///    |       |       |  |       |       |   
    ///    |       |       |  |   L   |   O   |    
    ///    |       |       |  |       |       |    
    ///    G-------+-------G  G-------P-------G    
    ///   1.0     1.5     2.0
    ///  
    /// </remarks>
    public enum DataSplitMode
    {
        /// <summary>
        /// Specifies full split of Z value to all grid points 
        /// by distributing 100% of Z value to grid points
        /// </summary>
        FullSplit,
        /// <summary>
        /// Specifies even split of Z value among each grid point
        /// by distributing portion of Z value to grid points
        /// </summary>
        EvenSplit,
        /// <summary>
        /// Specifies no split of Z value between grid points
        /// by excluding data points that fall between grid points 
        /// </summary>
        NoSplit,
    }
}