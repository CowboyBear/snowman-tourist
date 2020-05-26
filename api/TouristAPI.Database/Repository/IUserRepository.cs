using TouristAPI.Model;

namespace TouristAPI.Database.Repository
{
  public interface IUserRepository
  {
    User Save(User user);
    User FindById(string id);
    
  }
}