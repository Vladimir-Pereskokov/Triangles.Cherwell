using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoLayout.WebAPI.Services;
using GeoLayout.WebAPI.Models;

namespace GeoLayout.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContainersController : Controller
    {
        private readonly LayoutContainerService  _contSvc;
        public ContainersController(LayoutContainerService contSvc) {_contSvc = contSvc;}

        // GET api/triangles
        [HttpGet]
        public IActionResult Get() =>
            Ok(_contSvc.GetActiveContainers()                
                .Select((c, Index) => new ContainerInfo(c, Index)));
    }
}
