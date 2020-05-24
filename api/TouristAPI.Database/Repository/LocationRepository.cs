using System.Collections.Generic;
using System.Linq;
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

  }
}