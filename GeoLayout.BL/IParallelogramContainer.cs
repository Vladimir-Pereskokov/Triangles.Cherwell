using System;
using System.Drawing;
using System.Collections.Generic;



namespace GeoLayout.BL
{
    public interface IParallelogramContainer
    {
        string Name { get; }
        int Rows { get; }
        int Columns { get; }

        int TriangleRows { get; }
        int TriangleColumns { get; }

        TriangleLocation CreateLocation(string address);
        Triangle GetTriangleAt(TriangleLocation location);
        TriangleLocation GetTriangleLocation(Triangle triangle);
        TrianglePattern Pattern { get; }
        IEnumerable<GridLineSegment> GetGridSegments();
        IEnumerable<Triangle> GetAllTriangles();
        IEnumerable<TriangleLocation> GetAllLocations();
        SizeF Size { get; }
    }
}
