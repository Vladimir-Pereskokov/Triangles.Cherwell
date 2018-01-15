using System;
using GeoLayout.BL;


namespace GeoLayout.WebAPI.Models
{
    public class  LocationInfo {

        public LocationInfo(): base() {}

        internal LocationInfo(TriangleLocation location): base() {
            Row = location.Row;
            Column  = location.Column;
            Address = location.Address;
        }


        public int Row { get; set;}
        public int Column { get; set;}
        public string Address { get; set;}
    }




    
}