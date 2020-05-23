using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TouristAPI.Database.Repository;
using TouristAPI.Model;

namespace TouristAPI.Service
{
  public class LocationService : ILocationService
  {

    private ILocationRepository _repository;

    public LocationService(ILocationRepository repository){
      _repository = repository;
    }

    public IList<Location> FindAll()
    {
      return _repository.FindAll();
    }

    public Location Save(IFormCollection formData)
    {      
      throw new NotImplementedException("Too be implemented soon");
    }
  }
}
