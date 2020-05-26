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
  public class UserController : ControllerBase
  {
    private IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]    
    [Route("{userId}")]
    public User Get(string userId)
    {
      return _userService.FindById(userId);
    }

    [HttpPost]    
    public IActionResult Post(User user)
    {
      try
      {
        return Created(string.Empty, _userService.Save(user));
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
      }
    }
  }
}
