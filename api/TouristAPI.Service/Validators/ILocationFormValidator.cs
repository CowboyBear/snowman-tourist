using System;
using Microsoft.AspNetCore.Http;

namespace TouristAPI.Service.Validators
{  
  public interface ILocationFormValidator
  {
    bool isValid(IFormCollection form);
      
  }
}