using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Numerics;
using GeoLayout.BL.Containers.Base;

namespace GeoLayout.BL.Containers
{
    internal class TwoPerRowContainer : ParallelogramContainerBase
    {
        public TwoPerRowContainer(Vector2 rowsSide, Vector2 columnsSide, int rows, int columns)
            : base(rowsSide, columnsSide, rows, columns) { }



        protected override TriangleLocation OnGetLocation(Vector2[] sortedvertexes)
        {
            var vectorTest = sortedvertexes[0]; //use the shortest vector
            var vectorA = vectorTest + _columnsSide;
            int col = -1;
            int row = -1;
            if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest + _columnsSide, 1) > -1 &&
                Array.IndexOf<Vector2>(sortedvertexes, vectorTest + _rowsSide, 1) > -1)
            { //This is row 1 within the cell

                var colD = (_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix;
                col = (int)Math.Round(colD, 0);
                if (Math.Abs(colD - col) <= C_MAXROUNDING_ERROR)
                {
                    var rowD = (_columnsSide.Y * vectorTest.X -
                                        _columnsSide.X * vectorTest.Y) / DetMatrix;
                    row = (int)Math.Round(rowD, 0);
                    if (Math.Abs(rowD - row) > C_MAXROUNDING_ERROR) row = -1;
                }
                else col = -1;               
                
            }
            else
            { //this is row 2 within a cell

                vectorTest = sortedvertexes[2]; //use the longest vector

                if (Array.IndexOf<Vector2>(sortedvertexes, vectorTest - _columnsSide, 0, 2) > -1 &&
                    Array.IndexOf<Vector2>(sortedvertexes, vectorTest - _rowsSide, 0, 2) > -1)
                {
                    var colD = (_rowsSide.X * vectorTest.Y -
                                        _rowsSide.Y * vectorTest.X) / DetMatrix - 1;
                    col = (int)Math.Round(colD, 0);
                    if (Math.Abs(colD - col) <= C_MAXROUNDING_ERROR)
                    {
                        var rowD = (_columnsSide.Y * vectorTest.X -
                                            _columnsSide.X * vectorTest.Y) / DetMatrix -1;
                        row = (int)Math.Round(rowD, 0);
                        if (Math.Abs(rowD - row) > C_MAXROUNDING_ERROR) row = -1;
                    }
                    else col = -1;
                }
            }
            return new TriangleLocation(row, col);
        }



        protected override Triangle OnGetTriangle(int row, int column)
        {
            var rowRem = row % 2;
            var rowEffective = rowRem == 0 ? row / 2:  (row - rowRem) / 2;
            var vectorStart = Vector2.Multiply(rowEffective, _rowsSide) +
                                                Vector2.Multiply(column, _columnsSide);
            Vector2 vectorA;
            Vector2 vectorB;
            Vector2 vectorC;

            if (rowRem == 0)
            {
                vectorA = vectorStart;
                vectorB = vectorA + _columnsSide;
                vectorC = vectorA + _rowsSide;
            }
            else
            {
                vectorC = vectorStart + _columnsSide;
                vectorA = vectorC + _rowsSide;
                vectorB = vectorA - _columnsSide;
            }
            return new Triangle(vectorA, vectorB, vectorC);
        }      

        


        public override TrianglePattern Pattern => TrianglePattern.TwoPerRow;


        protected override TriangleLocation OnParseLocation(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                var digitFirst = value[0];
                if (char.IsDigit(digitFirst))
                {
                    var letterFirst = ((IEnumerable<char>)value).FirstOrDefault(c => char.IsLetter(c));
                    if (letterFirst != char.MinValue)
                    {
                        var letterIDX = value.IndexOf(letterFirst);
                        if (value.Substring(letterIDX).TryDecryptAZ(out var col) &&
                            int.TryParse(value.Substring(0, letterIDX),
                             out var row))
                        {
                            return new TriangleLocation(row - 1, col, value);
                        }
                    }
                }
            }
            return base.OnParseLocation(value);
        }

        protected override string GetAddress(int row, int column)
        {
            if (row > -1 && column > -1) return $"{row + 1}{column.EncryptAZ()}";
            return base.GetAddress(row, column);
        }


        protected override void OnAddGridCellLineSegments(List<GridLineSegment> segments)
                  => AddDiagonalLineSegments(segments, Rows, Columns, _rowsSide, _columnsSide, true);


    }
}
