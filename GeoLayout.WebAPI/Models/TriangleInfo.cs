using System;
using GeoLayout.BL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoLayout.WebAPI.Services;
using GeoLayout.WebAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GeoLayout.WebAPI.Models
{


    internal static class floatConv {
            internal static int ToInt(this float n) => (int)Math.Round(n, 0);

    }


    public class TriangleInfo {
        public TriangleInfo(): base() {}
        internal TriangleInfo(Triangle triangle, int containerIDX) {
            Ax = triangle.vertexA.X ;
            Ay = triangle.vertexA.Y ;
            Bx = triangle.vertexB.X ;
            By = triangle.vertexB.Y ;
            Cx = triangle.vertexC.X ;
            Cy = triangle.vertexC.Y ;            
            Address = triangle.Location.Address;
            ContainerIdx = containerIDX;
        }

        public int ContainerIdx { get; set;}
        public string Address { get; set;}

        public float Ax {get; set;}     
        public float Ay {get; set;} 

        public float Bx {get; set;}     
        public float By {get; set;} 

        public float Cx {get; set;}     
        public float Cy {get; set;}

        

    }


    


}