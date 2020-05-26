using TouristAPI.Model;

namespace TouristAPI.Service
{
  public interface IUserService
  {
    User Save(User user);
    User FindById(string id);
  }
}