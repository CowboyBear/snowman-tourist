using Microsoft.EntityFrameworkCore;
using TouristAPI.Model;

namespace TouristAPI.Database.Context
{
  public interface IDatabaseContext
  {
    DbSet<Location> Locations { get; set; }
    DbSet<User> Users { get; set; }
    int SaveChanges();
  }
}