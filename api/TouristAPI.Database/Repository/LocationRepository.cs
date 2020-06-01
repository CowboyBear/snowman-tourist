using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using TouristAPI.Database.Context;
using TouristAPI.Model;

namespace TouristAPI.Database.Repository
{
  public class LocationRepository : ILocationRepository
  {
    private IDatabaseContext _dbContext;

    public LocationRepository(IDatabaseContext dbContext)
    {
      this._dbContext = dbContext;
    }

    public Location Save(Location location)
    {
      this._dbContext.Locations.Add(location);
      this._dbContext.SaveChanges();
      return location;
    }

    public IList<Location> FindAll()
    {
      return this._dbContext.Locations.ToList();
    }

    public IList<Location> FindNearby(GeoCoordinate coordinate, int radius) {
      
      return this._dbContext.Locations
        .ToList()
        .Where(location => 
          new GeoCoordinate(location.Lat, location.Lng).GetDistanceTo(coordinate) < radius
        )
        .ToList();
    }

  }
}