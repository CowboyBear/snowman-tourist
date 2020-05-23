using System;
using Microsoft.EntityFrameworkCore;
using TouristAPI.Model;

namespace TouristAPI.Database.Context
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<User> Users { get; set; }
  }
}
