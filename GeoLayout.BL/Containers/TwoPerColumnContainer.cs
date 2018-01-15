using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;
using GeoLayout.BL.Containers.Base;


namespace GeoLayout.BL.Containers
{
    internal  class TwoPerColumnContainer  : ParallelogramContainerBase
    {
        public TwoPerColumnContainer(Vector2 rowsSide, Vector2 columnsSide, int rows, int columns)
            :base(rowsSide, columnsSide, rows, columns) { }



        protected override TriangleLocation OnGetLocation(Vector2[] sortedvertexes)
        {
            var vectorShortest = sortedvertexes[0];
            var colD = (_rowsSide.X * vectorShortest.Y -
                                        _rowsSide.Y * vectorShortest.X) / DetMatrix;
            var col = (int)Math.Round(colD, 0);
            int row = -1;
            if (col >= 0 && Math.Abs(colD - col) <= C_MAXROUNDING_ERROR)
            {
                var vectorA = vectorShortest + _columnsSide;
                if (Array.IndexOf<Vector2>(sortedvertexes, vectorA, 1) > -1) col = 2 * (col + 1) - 1;
                else col++; //Is this the column 2 within the cell?
                var rowD = (_columnsSide.Y * vectorShortest.X -
                                                _columnsSide.X * vectorShortest.Y) / DetMatrix;

                row = (int)Math.Round(rowD, 0);
                if (Math.Abs(rowD - row) > C_MAXROUNDING_ERROR) row = -1;
            }
            else col = -1;            
            return new TriangleLocation(row, col);
        }



        protected override Triangle OnGetTriangle(int row, int column)
        {
            var colRem = column % 2;
            var colEffective = colRem == 0 ? column / 2: (column - colRem) / 2;
            var vectorStart = Vector2.Multiply(colEffective, _columnsSide) +
                                                                Vector2.Multiply(row, _rowsSide);
            Vector2 vectorA;
            Vector2 vectorB;
            Vector2 vectorC;

            if (colRem == 0)
            {
                vectorB = vectorStart;
                vectorA = vectorB + _rowsSide;
                vectorC = vectorA + _columnsSide;
            }
            else
            {
                vectorC = vectorStart;
                vectorA = vectorC + _columnsSide;
                vectorB = vectorA + _rowsSide;
            }
            return new Triangle(vectorA, vectorB, vectorC);
        }


        


        public override TrianglePattern Pattern => TrianglePattern.TwoPerColumn;
        
        protected override TriangleLocation OnParseLocation(string value)
        {           
            if (!string.IsNullOrWhiteSpace(value)) {
                value = value.Trim();
                var letterFirst = value[0];
                if (char.IsLetter(letterFirst)) {
                    var digitFirst = ((IEnumerable<char>)value).FirstOrDefault(c => char.IsDigit(c));
                    if (digitFirst != char.MinValue) {
                        var digitIDX = value.IndexOf(digitFirst);
                        if (int.TryParse(value.Substring(digitIDX), out var col) &&
                            value.Substring(0, digitIDX).TryDecryptAZ(out var row))
                        {
                            return new TriangleLocation(row, col - 1, value);
                        }                        
                    }
                }
            }
            return base.OnParseLocation(value);
        }

        protected override string GetAddress(int row, int column)
        {            
            if (row > -1 && column > -1) return $"{row.EncryptAZ()}{column + 1}";
            return base.GetAddress(row, column);
        }

        protected override void OnAddGridCellLineSegments(List<GridLineSegment> segments) 
             => AddDiagonalLineSegments(segments, Rows, Columns, _rowsSide, _columnsSide, false);
     

        
    }
}
