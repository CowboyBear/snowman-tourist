using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TouristAPI.Database.Repository;
using TouristAPI.Model;
using TouristAPI.Service.Validators;

namespace TouristAPI.Service
{
  public class LocationService : ILocationService
  {

    private ILocationRepository _repository;
    private ILocationFormValidator _formValidator;

    public LocationService(ILocationRepository repository, ILocationFormValidator formValidator)
    {
      _repository = repository;
      _formValidator = formValidator;
    }

    public IList<Location> FindAll()
    {
      return _repository.FindAll();
    }

    public Location Save(IFormCollection formData)
    {
      if (_formValidator.isValid(formData))
      {
        return _repository.Save(parseToLocation(formData));
      }

      return null;
    }

    private Location parseToLocation(IFormCollection formData)
    {
      return new Location()
      {
        Name = formData["Name"],
        Address = formData["Address"],
        Lat = Convert.ToDouble(formData["Lat"]),
        Lng = Convert.ToDouble(formData["Lng"]),
        UserId = formData["UserId"]
      };
    }
  }
}
