using System;
using GeoLayout.BL;


namespace GeoLayout.WebAPI.Models
{
    public class GrdSegment {
        public GrdSegment():base() {}        

        internal GrdSegment(GridLineSegment segment):base() {
            Ax = segment.PointA.X ;
            Ay = segment.PointA.Y ;
            Bx = segment.PointB.X ;
            By = segment.PointB.Y ;
            IsCellSegment = segment.IsCellSegment;
        }   

        public float Ax {get; set;}     
        public float Ay {get; set;} 

        public float Bx {get; set;}     
        public float By {get; set;} 

        public bool IsCellSegment { get; set;}

    }
}