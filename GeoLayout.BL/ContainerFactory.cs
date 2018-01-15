using System;
using System.Collections.Generic;
using System.Numerics;
using GeoLayout.BL.Containers;
using GeoLayout.BL.Containers.Base;


namespace GeoLayout.BL
{
    public static class ContainerFactory
    {

        public static IParallelogramContainer CreateSquare(int columns, float cellWidth)
                                => CreateSquare(columns, cellWidth, TrianglePattern.TwoPerColumn);

        public static IParallelogramContainer CreateSquare(int columns, float cellWidth, TrianglePattern pattern)
        {
            switch (pattern)
            {
                case TrianglePattern.TwoPerRow:
                    return new TwoPerRowContainer(new Vector2(cellWidth, 0), new Vector2(0, cellWidth), columns, columns);
                case TrianglePattern.FourPerCell:
                    return new FourPerCellContainer(new Vector2(cellWidth, 0), new Vector2(0, cellWidth), columns, columns);
                default: return new TwoPerColumnContainer(new Vector2(cellWidth, 0), new Vector2(0, cellWidth), columns, columns);
            }
        }

        public static IParallelogramContainer CreateParallelogram(int angleDegrees, int columns, float cellWidth)
                    => CreateParallelogram(angleDegrees, columns, cellWidth, TrianglePattern.TwoPerColumn);

        public static IParallelogramContainer CreateParallelogram(int angleDegrees, int columns,
                                                                    float cellWidth, TrianglePattern pattern)
            => CreateParallelogram(angleDegrees, columns, cellWidth, columns, cellWidth, pattern);


        public static IParallelogramContainer CreateParallelogram(int angleDegrees, int columns, float cellWidth,
                                                                    int rows, float cellHeight, TrianglePattern pattern)
        {
            if (angleDegrees % 180 == 0) throw new ArgumentOutOfRangeException("Parallelogram's angle can't be a multiple of 180°");

            var vectorCols = new Vector2(0, cellWidth);
            var vectorRows = new Vector2((float)(cellHeight * Math.Sin((double)angleDegrees / 180)),
                                         (float)(cellHeight * Math.Cos((double)angleDegrees / 180)));
            return CreateParallelogram(vectorRows, vectorCols, rows, columns, pattern);
        }

        public static IParallelogramContainer CreateParallelogram(Vector2 rowsV, Vector2 columnsV,
                                                        int rows, int columns, TrianglePattern pattern)
        {
            switch (pattern)
            {
                case TrianglePattern.TwoPerRow:
                    return new TwoPerRowContainer(rowsV, columnsV, rows, columns);
                case TrianglePattern.FourPerCell:
                    return new FourPerCellContainer(rowsV, columnsV, rows, columns);
                default: return new TwoPerColumnContainer(rowsV, columnsV, rows, columns);
            }
        }


    }
}
