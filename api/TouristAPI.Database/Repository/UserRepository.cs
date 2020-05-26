using System.Collections.Generic;
using System.Linq;
using TouristAPI.Database.Context;
using TouristAPI.Model;

namespace TouristAPI.Database.Repository
{
  public class UserRepository : IUserRepository
  {
    private IDatabaseContext _dbContext;

    public UserRepository(IDatabaseContext dbContext)
    {
      this._dbContext = dbContext;
    }

    public User Save(User user)
    {
      this._dbContext.Users.Add(user);
      this._dbContext.SaveChanges();
      return user;
    }

    public User FindById(string id)
    {
      return this._dbContext.Users.Where(user => user.Id == id).FirstOrDefault();
    }

  }
}