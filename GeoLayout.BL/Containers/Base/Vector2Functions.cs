using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;


namespace GeoLayout.BL.Containers.Base
{
    internal static class Vector2Functions {
        /// <summary>
        /// Checks if this vector is parallel to a given other vector
        /// </summary>
        /// <param name="vector">this vector to check</param>
        /// <param name="other">other vector to check this vector to</param>
        /// <returns>True if this vector is parallel to other vector</returns>
        public static bool ParallelTo(this Vector2 vector, Vector2 other)
        {
            if (vector == Vector2.Zero || other == Vector2.Zero) return true;            
            var normVector = Vector2.Normalize(vector);
            var normOther = Vector2.Normalize(other);
            return normVector == normOther || normVector == Vector2.Negate(normOther);
        }
        
        /// <summary>
        /// Returns two vectors that may be used as two adjacent sides of a parallelogram.
        /// </summary>
        /// <param name="side1">this side vector to be used as a side 1</param>
        /// <param name="side2">side 2 of the parallelogram</param>
        /// <returns>key value pair for side 1 (key) and side 2 (value) of a parallelogram</returns>
        public static KeyValuePair<Vector2, Vector2> ParallelogramSides(this Vector2 side1, Vector2 side2) {
            var minX = Math.Min(side1.X, side2.X);
            var minY = Math.Min(side1.Y, side2.Y);
            var v1 = new Vector2(minX < 0 ? side1.X - minX : side1.X, 
                                    minY < 0 ? side1.Y - minY : side1.Y);
            var v2 = new Vector2(minX < 0 ? side2.X - minX : side2.X,
                                    minY < 0 ? side2.Y - minY : side2.Y);
            return new KeyValuePair<Vector2, Vector2>(v1, v2);
        }


        public static PointF ToPoint(this Vector2 vector) => new PointF(vector.X, vector.Y);
        public static Vector2 ToVector(this PointF point) => new Vector2(point.X, point.Y);


    }
   
}
