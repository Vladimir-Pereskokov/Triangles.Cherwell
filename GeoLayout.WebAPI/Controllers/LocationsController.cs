using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoLayout.WebAPI.Services;
using GeoLayout.WebAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GeoLayout.BL;
using System.Numerics;

namespace GeoLayout.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly LayoutContainerService  _contSvc;
        public LocationsController(LayoutContainerService contSvc) {_contSvc = contSvc;}

        // GET api/locations
        [HttpGet]
        public IActionResult Get() => Get(0);

        // GET api/locations/1
        [HttpGet("{containerIdx}")]
        public IActionResult Get(int containerIdx) =>
            Ok(_contSvc.GetContainer(containerIdx).GetAllLocations()
                .Select((l) => new LocationInfo(l)));

        [HttpGet("{containerIdx}/triangle/{Ax}/{Ay}/{Bx}/{By}/{Cx}/{Cy}")]   
        public IActionResult Get(
            [FromRoute] TriangleInfo triangle)  {            
            var triangleAPI = new Triangle(new Vector2(triangle.Ax, triangle.Ay), 
            new Vector2(triangle.Bx, triangle.By),
            new Vector2(triangle.Cx, triangle.Cy));
            return Ok(new LocationInfo(
                _contSvc.GetContainer(triangle.ContainerIdx).GetTriangleLocation(triangleAPI)
                )
                )
                ;
        }
    }
}