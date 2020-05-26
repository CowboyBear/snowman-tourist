using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using TouristAPI.Database.Repository;
using TouristAPI.Model;
using TouristAPI.Service.Exceptions;
using TouristAPI.Service.Validators;
using Xunit;

namespace TouristAPI.Service.Tests
{
  public class UserServiceTest
  {
    private Mock<IUserRepository> mockRepository;    
    private UserService service;

    public UserServiceTest()
    {

      mockRepository = new Mock<IUserRepository>();      

      service = new UserService(mockRepository.Object);
    }

    [Fact]
    public void FindById_ShouldCallTheRepository_WhenCalled()
    {
      string userId = "any user id";
      service.FindById(userId);
      mockRepository.Verify(repository => repository.FindById(userId), Times.Once);
    }

    [Fact]
    public void Save_ShouldSaveOnDb_WhenCalled()
    {
      service.Save(new User());

      mockRepository.Verify(repository => repository.Save(It.IsAny<User>()), Times.Once);
    }
  }
}
