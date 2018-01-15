using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoLayout.WebAPI.Services;
using GeoLayout.WebAPI.Models;
using GeoLayout.BL;


namespace GeoLayout.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TrianglesController : Controller
    {
        private readonly LayoutContainerService  _contSvc;
        public TrianglesController(LayoutContainerService contSvc) {_contSvc = contSvc;}

        // returns all triangle objects for the first container
        // GET api/triangles 
        [HttpGet]
        public ActionResult Get() => Get(0);
        

        // returns all triangle objects for the container with index containerIdx
        // GET api/triangles/1
        [HttpGet("{containerIdx}")]
        public ActionResult Get(int containerIdx)
        {
            try
            {
                return Ok(
                    from t in 
                    _contSvc.GetContainer(containerIdx)
                    .GetAllTriangles()
                    select new TriangleInfo(t, containerIdx)
                    );
            }
            catch (Exception ex) {
               return NotFound(ex.Message); 
            }              
            
        }


        // returns a single triangle object for the container with index containerIdx and given address
        // GET api/triangles/5/C3
        [HttpGet("{containerIdx}/{address}")]
        public ActionResult Get(int containerIdx, string address)
        {
            try
            {
                var cont = _contSvc.GetContainer(containerIdx);
                return Ok(cont.GetTriangleAt(
                    cont.CreateLocation(address)));
            }
            catch (Exception ex) {
               return NotFound(ex.Message); 
            }    
        }      


    }
}
