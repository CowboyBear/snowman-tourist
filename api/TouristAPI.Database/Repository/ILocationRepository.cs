using System.Collections.Generic;
using GeoCoordinatePortable;
using TouristAPI.Model;

namespace TouristAPI.Database.Repository
{
  public interface ILocationRepository
  {
    Location Save(Location location);
    IList<Location> FindAll();
    IList<Location> FindNearby(GeoCoordinate coordinate, int radius);
  }
}