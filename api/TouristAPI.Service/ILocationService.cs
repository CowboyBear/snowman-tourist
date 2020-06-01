using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TouristAPI.Model;

namespace TouristAPI.Service
{
  public interface ILocationService
  {
    Location Save(IFormCollection formData);
    IList<Location> FindAll();
    IList<Location> FindNearby(double latitude, double longitude, int radius);
  }
}
