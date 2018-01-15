using System;
using System.Collections.Generic;
using System.Numerics;
using GeoLayout.BL.Containers.Base;

namespace GeoLayout.BL.Containers
{
    internal class FourPerCellContainer : ParallelogramContainerBase 
    {

        public FourPerCellContainer(Vector2 rowsSide, Vector2 columnsSide, int rows, int columns)
            :base(rowsSide, columnsSide, rows, columns) { }

        

        protected override TriangleLocation OnGetLocation(Vector2[] sortedvertexes)
        {

            Vector2 vectorTest = sortedvertexes[0]; //start with the shortest vector
            int col = -1;
            int row = -1;
            if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest + _rowsSide, 1) > 0 ||
                Array.IndexOf<Vector2>(sortedvertexes, vectorTest + DiagonalMainHalfVector, 1) > 0 )
            { //this is triangle 11

                col =  (int)((_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix);



                row =  (int)((_columnsSide.Y * vectorTest.X -
                                            _columnsSide.X * vectorTest.Y) / DetMatrix);                
            }
            else if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest + _columnsSide, 1) > 0 &&
                Array.IndexOf<Vector2>(sortedvertexes, vectorTest + DiagonalMainHalfVector, 1) > 0)
            {// this is triangle 12
                col =  (int)((_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix);
                row =  (int)((_columnsSide.Y * vectorTest.X -
                                            _columnsSide.X * vectorTest.Y) / DetMatrix);
                return new TriangleLocation(col, row);

            }
            else
            { // this is either triangle 22 or 21
                vectorTest = sortedvertexes[2]; // use tha farthest vertex
                if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest - _rowsSide, 1) > 0)
                {
                    col = (int)((_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix);
                    row = (int)((_columnsSide.Y * vectorTest.X -
                                                _columnsSide.X * vectorTest.Y) / DetMatrix);
                }
                else if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest - _columnsSide, 1) > 0)
                {
                    col = (int)((_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix);
                    row = (int)((_columnsSide.Y * vectorTest.X -
                                                _columnsSide.X * vectorTest.Y) / DetMatrix);
                }                
            }
            return new TriangleLocation(row, col);
        }

        protected override Triangle OnGetTriangle(int row, int column)
        {
            var colRem = column % 2;
            var colEffective = column - colRem;
            var rowRem = row % 2;
            var rowEffective = row - rowRem;
            var vectorStart = Vector2.Multiply(rowEffective, _rowsSide) +
                                                               Vector2.Multiply(colEffective, _columnsSide);
            var vectorDiag1 = DiagonalMainHalfVector;
            var vectorDiag2 = DiagonalAltHalfVector;
            Vector2 vectorA = vectorStart + vectorDiag1;
            Vector2 vectorB;
            Vector2 vectorC;
            if (rowRem == 0)
            {
                if (colRem == 0)
                {
                    vectorC = vectorStart;
                    vectorB = vectorA + vectorDiag2;
                }
                else
                {
                    vectorB = vectorStart;
                    vectorC = vectorB + _columnsSide;
                }
            }
            else
            {
                if (colRem == 0)
                {
                    vectorB = vectorA + vectorDiag1;
                    vectorC = vectorA + vectorDiag2;
                }
                else
                {
                    vectorB = vectorA - vectorDiag2;
                    vectorC = vectorA + vectorDiag1;
                }
            }
            return new Triangle(vectorA, vectorB, vectorC);
        }


        


        protected override void OnAddGridCellLineSegments(List<GridLineSegment> segments)
        {
            AddDiagonalLineSegments(segments, Rows, Columns, _rowsSide, _columnsSide, true);
            AddDiagonalLineSegments(segments, Rows, Columns, _rowsSide, _columnsSide, false);
        }

        public override TrianglePattern Pattern => TrianglePattern.FourPerCell;

    }
}
