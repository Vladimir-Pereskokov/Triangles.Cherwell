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
    public class GridSegmentsController : Controller
    {
        private readonly LayoutContainerService  _contSvc;
        public GridSegmentsController(LayoutContainerService contSvc) {_contSvc = contSvc;}


        // GET api/triangles
        [HttpGet]
        public ActionResult Get() => Get(0);
        


        [HttpGet("{containerIdx}")]
        public ActionResult Get(int containerIdx)
        {
            try
            {
                return Ok(
                    from t in 
                    _contSvc.GetContainer(containerIdx)
                    .GetGridSegments()
                    .Where ( t => !t.IsCellSegment)
                    select new GrdSegment(t)
                    );
            }
            catch (Exception ex) {
               return NotFound(ex.Message); 
            }              
            
        }       
          
        



    }
}
