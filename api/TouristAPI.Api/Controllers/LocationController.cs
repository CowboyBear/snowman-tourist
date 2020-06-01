using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouristAPI.Model;
using TouristAPI.Service;
using TouristAPI.Service.Exceptions;

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
    public IActionResult Get(double latitude, double longitude, int radius)
    {
      try
      {
        if (radius == 0)
        {
          return Ok(_locationService.FindAll());
        }
        else
        {
          return Ok(_locationService.FindNearby(latitude, longitude, radius));
        }
      }
      catch (Exception ex)
      {
        return HandleExceptions(ex);
      }
    }

    [HttpPost]
    public IActionResult Post(IFormCollection formData)
    {
      try
      {
        return Created(string.Empty, _locationService.Save(formData));
      }
      catch (Exception ex)
      {
        return HandleExceptions(ex);
      }
    }

    private IActionResult HandleExceptions(Exception ex)
    {
      if (ex is InvalidLocationException || ex is InvalidFileException)
      {
        return BadRequest(string.Format("{0}: {1}", ex.GetType(), ex.Message));
      }
      else
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
      }
    }
  }
}
