using System;
using System.Numerics;
using System.Drawing;
using GeoLayout.BL.Containers.Base;

namespace GeoLayout.BL
{
    public enum TrianglePattern { TwoPerColumn, TwoPerRow, FourPerCell }
    public struct Triangle
    {
        private readonly PointF _vxA;
        private readonly PointF _vxB;
        private readonly PointF _vxC;
        private readonly TriangleLocation _location;       

        

        public Triangle(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC): 
                                this(vertexA, vertexB, vertexC, TriangleLocation.Empty) {}

        internal Triangle(Vector2 vertexA, 
                            Vector2 vertexB, 
                            Vector2 vertexC, 
                            TriangleLocation location)
        {
            _vxA = vertexA.ToPoint();
            _vxB = vertexB.ToPoint();
            _vxC = vertexC.ToPoint();
            _location = location;
        }

        internal Triangle(Triangle source, 
                          TriangleLocation location)
        {
            _vxA = source._vxA;
            _vxB = source._vxB;
            _vxC = source._vxC;
            _location = location;
        }


        public PointF vertexA => _vxA;
        public PointF vertexB => _vxB;
        public PointF vertexC => _vxC;

        public TriangleLocation Location => _location;


        
        public bool IsEmpty => _vxA.X == 0 && _vxA.Y == 0 && _vxB.X == 0 && _vxB.Y == 0 && _vxC.X == 0 && _vxC.Y == 0;


        public override bool Equals(object obj)
        {
            if (obj != null && obj is Triangle)
            {
                return (Triangle)obj == this;
            }
            return false;
        }

        public override string ToString() => $"{_vxA.ToString()},{_vxB.ToString()},{_vxC.ToString()}";

        public override int GetHashCode() => ToString().GetHashCode();

        public static bool operator ==(Triangle t1, Triangle t2)
                                => t1._vxA == t2._vxA && t1._vxB == t2._vxB && t1._vxC == t2._vxC;
        public static bool operator !=(Triangle t1, Triangle t2)
            => t1._vxA != t2._vxA || t1._vxB != t2._vxB || t1._vxC != t2._vxC;

    }    
    public struct TriangleLocation
    {
        private readonly string _address;
        private readonly int _row;
        private readonly int _column;
        public TriangleLocation(int row, int column): this(row, column, null) { }
        internal TriangleLocation(int row, int column, string address)
                        { _row = row; _column = column; _address = address;}
        internal TriangleLocation(TriangleLocation source, string address): 
                                    this(source._row, source._column, address) {}

        public static TriangleLocation Empty => new TriangleLocation(-1, -1);
        public int Row => _row;
        public int Column => _column;
        public bool IsValid => _row > -1 && _column > -1;


        public string Address => _address;


        public override bool Equals(object obj)
        {
            if (obj != null && obj is TriangleLocation) {
                return (TriangleLocation)obj == this;
            }
            return false;
        }

        public override string ToString() => $"{_row},{_column}";        

        public override int GetHashCode() => ToString().GetHashCode();

        public static bool operator ==(TriangleLocation t1, TriangleLocation t2) 
                                => t1._row == t2._row && t1._column == t2._column;
        public static bool operator !=(TriangleLocation t1, TriangleLocation t2)
            => t1._row != t2._row || t1._column != t2._column;        
        
    }


    public struct GridLineSegment {
        private readonly PointF _A;
        private readonly PointF _B;
        private readonly bool _IsCellSegment;
        public GridLineSegment(PointF a, PointF b, bool isCellSegment) {
            _A = a;
            _B = b;
            _IsCellSegment = isCellSegment;
        }

        public GridLineSegment(Vector2 a, Vector2 b, bool isCellSegment) :
                                this(a.ToPoint(), b.ToPoint(), isCellSegment) { }

        public PointF PointA => _A;
        public PointF PointB => _B;

        /// <summary>
        /// Indicates if this line segment crosses at least one grid cell
        /// </summary>
        public bool IsCellSegment => _IsCellSegment;


        public bool IsEmpty => _A.X == 0 && _A.Y == 0 && _B.X == 0 && _B.Y == 0;
        public override string ToString() => $"{_A.X},{_A.Y};{_B.X},{_B.Y}";
        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj) {
            if (obj != null && obj is GridLineSegment)
                return (GridLineSegment)obj == this;
            return false;
        }


        public static bool operator ==(GridLineSegment s1, GridLineSegment s2) 
                                            => s1._A == s2._A && s1._B == s2._B;

        public static bool operator !=(GridLineSegment s1, GridLineSegment s2) 
                                            => s1._A != s2._A || s1._B != s2._B;
    }


}
