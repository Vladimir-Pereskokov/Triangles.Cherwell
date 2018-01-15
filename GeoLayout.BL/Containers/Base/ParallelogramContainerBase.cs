using System;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeoLayout.BL.Containers.Base
{

    

    /// <summary>
    /// Defines a parallelogram containing triangles with given number of triangle rows and columns
    /// </summary>
    internal abstract class ParallelogramContainerBase :IParallelogramContainer {
        protected readonly Vector2 _rowsSide;
        protected readonly Vector2 _columnsSide;
        private readonly int _rows;
        private readonly int _columns;

        protected const float C_MAXROUNDING_ERROR = 0.01F;
        /// <summary>
        /// Constructs parellelogram shape object
        /// </summary>
        /// <param name="rowsSide">Rows side vector</param>
        /// <param name="columnsSide">Columns side vector</param>
        /// <param name="rows">number of triangle rows</param>
        /// <param name="columns">number of triangle columns</param>
        protected ParallelogramContainerBase(Vector2 rowsSide, Vector2 columnsSide, 
            int rows, int columns): base() {
            if (rowsSide.ParallelTo(columnsSide)) throw new ArgumentException("Adjacent sides of a parallelogramm may not be parallel");
            if (rows <= 0) throw new ArgumentOutOfRangeException("Number of rows must be greater than zero");
            if (columns <= 0) throw new ArgumentOutOfRangeException("Number of columns must be greater than zero");

            if (columns % 2 != 0 && Pattern != TrianglePattern.TwoPerRow)
                throw new ArgumentException("Number of triangle columns must be an even number");
            if (rows % 2 != 0 && Pattern != TrianglePattern.TwoPerColumn)
                throw new ArgumentException("Number of triangle rows must be an even number");
            _rows = rows;
            _columns = columns;
            var kv = rowsSide.ParallelogramSides(columnsSide);
            _rowsSide = kv.Key;
            _columnsSide = kv.Value;
        }


        public SizeF Size {
            get
            {
                var rowSIDE = Vector2.Multiply(Rows, _rowsSide);
                var colSIDE = Vector2.Multiply(Columns, _columnsSide);

                return new System.Drawing.SizeF(
                        Math.Abs(rowSIDE.Y) +
                        Math.Abs(colSIDE.Y),
                        Math.Abs(rowSIDE.X) +
                        Math.Abs(colSIDE.X));
            }
        }
        



        public int Rows => _rows;
        public int Columns => _columns;


        public int TriangleRows => (Pattern == TrianglePattern.TwoPerColumn) ? Rows : 2 * Rows;
        public int TriangleColumns => (Pattern == TrianglePattern.TwoPerRow) ? Columns : 2 * Columns;

        public TriangleLocation CreateLocation(string address) => OnParseLocation(address);

        public Triangle GetTriangleAt(string address) => GetTriangleAt(OnParseLocation(address));

        public Triangle GetTriangleAt(TriangleLocation location)
        {
            var row = location.Row;
            var column = location.Column;
            if (row < 0 || row >= TriangleRows)
                throw new ArgumentOutOfRangeException($"Row parameter can not be greater than {TriangleRows - 1} and less than 0.");
            if (column < 0 || column >= TriangleColumns)
                throw new ArgumentOutOfRangeException($"Row parameter can not be greater than {TriangleColumns - 1} and less than 0.");

            var t =  OnGetTriangle(row, column);
            if (!t.IsEmpty) t = new Triangle(t, 
                new TriangleLocation(row, column, GetAddress(row, column)));
            return t;
        }

        public TriangleLocation GetTriangleLocation(Triangle triangle) {            
            var arrVs = new Vector2[] {
                triangle.vertexA.ToVector(),
                triangle.vertexB.ToVector(),
                triangle.vertexC.ToVector()
            };
            Array.Sort<Vector2>(arrVs, (v1, v2) => v1.Length().CompareTo(v2.Length()));
            var loc = OnGetLocation(arrVs);
            if (loc.IsValid) loc = new TriangleLocation(loc, GetAddress(loc.Row, loc.Column));
            return loc;
        }


        protected abstract Triangle OnGetTriangle(int row, int column);
        protected abstract TriangleLocation OnGetLocation(Vector2[] sortedvertexes);

        public abstract TrianglePattern Pattern { get; }

        public virtual string Name {
            get
            {
                var sb = new StringBuilder();
                if (Vector2.Multiply(_rowsSide, _columnsSide).Length() == 0)
                    sb.Append(Rows == Columns ? "Square " : "Rectangle ");
                else
                    sb.Append("Parallelogram ");

                sb.Append(Rows.ToString());
                sb.Append('x');
                sb.Append(Columns.ToString());
                sb.Append(" (");
                sb.Append(Pattern.ToString());
                sb.Append(')');
                return sb.ToString();
            }
        }

        private ReadOnlyCollection<Triangle> _allTriangles;
        public IEnumerable<Triangle> GetAllTriangles() {
            if (_allTriangles == null) {
                try
                {
                    var lst = new List<Triangle>();
                    for (int idxRow = 0; idxRow < TriangleRows; idxRow++)
                    {
                        for (int idxCol = 0;
                            idxCol < TriangleColumns;
                            idxCol++) lst.Add(
                                GetTriangleAt(new TriangleLocation(idxRow, idxCol)));
                    }
                    _allTriangles = lst.AsReadOnly();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                }               
            }
            return _allTriangles;
        }

        public IEnumerable<TriangleLocation> GetAllLocations() => GetAllTriangles().Select(t => t.Location);

        private float _detMatrix = 0;

        /// <summary>
        /// Returns the parallelogram's row and column matrix determinant value
        /// </summary>
        protected float DetMatrix {
            get
            {
                if (_detMatrix == 0) _detMatrix = 
                        _rowsSide.X * _columnsSide.Y - _rowsSide.Y * _columnsSide.X;
                return _detMatrix;
            }
        }

        private Vector2 _diagonalMainVector;
        protected Vector2 DiagonalMainVector {
            get
            {
                if (_diagonalMainVector.X == 0 && _diagonalMainVector.Y == 0)
                       _diagonalMainVector = _rowsSide + _columnsSide;
                return _diagonalMainVector;
            }
        }


        private Vector2 _diagonalAltVector;
        protected Vector2 DiagonalAltVector
        {
            get
            {
                if (_diagonalAltVector.X == 0 && _diagonalAltVector.Y == 0)
                    _diagonalAltVector = _rowsSide - _columnsSide;
                return _diagonalAltVector;
            }
        }


        private Vector2 _diagonalMainHalfVector;
        protected Vector2 DiagonalMainHalfVector
        {
            get
            {
                if (_diagonalMainHalfVector.X == 0 && _diagonalMainHalfVector.Y == 0)
                        _diagonalMainHalfVector = Vector2.Multiply(0.5F , DiagonalMainVector);
                return _diagonalMainHalfVector;
            }
        }


        private Vector2 _diagonalAltHalfVector;
        protected Vector2 DiagonalAltHalfVector
        {
            get
            {
                if (_diagonalAltHalfVector.X == 0 && _diagonalAltHalfVector.Y == 0)
                        _diagonalAltHalfVector = Vector2.Multiply(0.5F, DiagonalAltVector);
                return _diagonalAltHalfVector;
            }
        }



        protected virtual TriangleLocation OnParseLocation(string value) {
            if (!string.IsNullOrWhiteSpace(value)) {
                value = value.Trim();
                var firstNonDigit = ((IEnumerable<char>)value).FirstOrDefault(c => !char.IsDigit(c));
                if (firstNonDigit != char.MinValue) {
                    var sArr = value.Split(firstNonDigit);
                    if (sArr != null && sArr.Length == 2) {
                        if (int.TryParse(sArr[0].Trim(), out var row) &&
                            int.TryParse(sArr[1].Trim(), out var col)) {
                            return new TriangleLocation(row -1, col -1, value);
                        }
                    }
                }
            }
            return TriangleLocation.Empty; 
        }


        protected virtual string GetAddress(int row, int column) => $"{row + 1},{column + 1}";

        public IEnumerable<GridLineSegment> GetGridSegments()
        {
            var lst = new List<GridLineSegment>();
            //add row segments
            var vectorCOLUMNS = Vector2.Multiply(Columns, _columnsSide);
            for (int idxRows = 0; idxRows <= Rows; idxRows++)
            {
                var vectorA = Vector2.Multiply(idxRows, _rowsSide);
                var vectorB = vectorA + vectorCOLUMNS;
                lst.Add(new GridLineSegment(vectorA, vectorB, false));
            }
            var vectorROWS = Vector2.Multiply(Rows, _rowsSide);
            for (int idxCols = 0; idxCols <= Columns; idxCols++)
            {
                var vectorA = Vector2.Multiply(idxCols, _columnsSide);
                var vectorB = vectorA + vectorROWS;
                lst.Add(new GridLineSegment(vectorA, vectorB, false));
            }
            OnAddGridCellLineSegments(lst);
            return lst.AsReadOnly();
        }
        
        protected abstract void OnAddGridCellLineSegments(List<GridLineSegment> segments);


        /// <summary>
        /// Adds diagonal line segments to a given list
        /// </summary>
        /// <param name="segments">list to add diagonal segments to</param>
        /// <param name="rows">number of rows in parallelogram</param>
        /// <param name="columns">number of columns in parallelogram</param>
        /// <param name="rowSide">row side vector</param>
        /// <param name="columnSide">column side vector</param>
        /// <param name="southWestToNorthEast">direction of diagonals south-west to north-east?</param>
        protected static void AddDiagonalLineSegments(
            List<GridLineSegment> segments, int rows, int columns,
            Vector2 rowSide, Vector2 columnSide, bool southWestToNorthEast)
        {
            if (segments != null)
            {
                var intTotalLines = rows + columns - 1;
                var idxStopGrowAt = (rows == 2) ? 0 : Math.Min(rows, columns) - 1;
                var idxStartShrinkAt = (rows == 2) ? intTotalLines : intTotalLines - idxStopGrowAt;
                var movingUpAlongRowsSide = true;
                int idxCol = 0;
                var vectorDiag = Vector2.Zero;
                if (!southWestToNorthEast) //north-west to south-east
                {   
                    var vectorDiagNorm = (rows == 2) ? Vector2.Multiply(2, rowSide + columnSide) : rowSide + columnSide;                    
                    
                    for (int idxLine = 0; idxLine < intTotalLines; idxLine++)
                    {
                        Vector2 vectorA =
                         (movingUpAlongRowsSide) ? Vector2.Multiply(rows - idxLine - 1, rowSide) :
                         Vector2.Multiply(idxCol, columnSide);

                        vectorDiag = (idxLine <= idxStopGrowAt) ?
                            vectorDiag + vectorDiagNorm :
                            (idxLine>= idxStartShrinkAt)? vectorDiag - vectorDiagNorm: vectorDiag;
                        var vectorB = vectorA + vectorDiag;
                        segments.Add(new GridLineSegment(vectorA, vectorB, true));
                        if (movingUpAlongRowsSide)
                            if (vectorA == rowSide) movingUpAlongRowsSide = false;
                            else idxCol++;                        
                    }
                }
                else // south-west to north east
                {   
                    var vectorDiagNorm = (rows == 2) ? Vector2.Multiply(2,  columnSide - rowSide) : columnSide - rowSide;                    
                    var maxRowSideVector = Vector2.Multiply(rows, rowSide);
                    for (int idxLine = 0; idxLine < intTotalLines; idxLine++)
                    {
                        var vectorA = movingUpAlongRowsSide ? Vector2.Multiply(1 + idxLine, rowSide) :
                            maxRowSideVector + Vector2.Multiply(idxCol, columnSide);

                        vectorDiag = (idxLine <= idxStopGrowAt) ?
                            vectorDiag + vectorDiagNorm :
                            (idxLine >= idxStartShrinkAt) ? vectorDiag - vectorDiagNorm : vectorDiag;
                        var vectorB = vectorA + vectorDiag;
                        segments.Add(new GridLineSegment(vectorA, vectorB, true));

                        if (movingUpAlongRowsSide)
                            if (vectorA == maxRowSideVector) movingUpAlongRowsSide = false;
                            else idxCol++;
                    }
                }                
            }
        }

        #region "Overrides"
        public override string ToString()=> Name;

        public static bool operator ==(ParallelogramContainerBase p1, ParallelogramContainerBase p2) {
            return p1.Pattern == p2.Pattern && p1.Columns == p2.Columns
                    && p1.Rows == p2.Rows && p1._rowsSide == p2._rowsSide
                    && p1._columnsSide == p2._columnsSide;
        }

        public static bool operator !=(ParallelogramContainerBase p1, ParallelogramContainerBase p2)
        {
            return p1.Pattern != p2.Pattern || p1.Columns != p2.Columns
                    || p1.Rows != p2.Rows || p1._rowsSide != p2._rowsSide
                    || p1._columnsSide != p2._columnsSide;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ParallelogramContainerBase)
                            return this == (ParallelogramContainerBase)obj;
            return false;
        }

        public override int GetHashCode()=> Name.GetHashCode();
    }
        #endregion



 }

