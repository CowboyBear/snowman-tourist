using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TouristAPI.Database.Repository;
using TouristAPI.Model;
using TouristAPI.Service.Validators;

namespace TouristAPI.Service
{
  public class UserService : IUserService
  {

    private IUserRepository _repository;    

    public UserService(IUserRepository repository)
    {
      _repository = repository;      
    }

    public User FindById(string userId)
    {
      return _repository.FindById(userId);
    }    

    public User Save(User user)
    {
      return _repository.Save(user);
    }
  }
}
