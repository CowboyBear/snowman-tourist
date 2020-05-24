using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TouristAPI;
using TouristAPI.Database.Repository;
using TouristAPI.Model;
using TouristAPI.Service;

namespace TouristAPI.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class LocationController : ControllerBase
  {    
    private ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {      
      _locationService = locationService;
    }

    [HttpGet]
    public IList<Location> Get()
    {
      return _locationService.FindAll();
    }

    [HttpPost]
    public Location Post(IFormCollection formData)
    {
      return _locationService.Save(formData);
    }
  }
}
