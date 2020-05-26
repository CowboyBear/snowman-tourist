using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using TouristAPI.Database.Context;
using TouristAPI.Database.Repository;
using TouristAPI.Database.Tests.Utils;
using TouristAPI.Model;
using Xunit;

namespace TouristAPI.Database.Tests
{
  public class UserRepositoryTest
  {
    private IUserRepository repository;
    private Mock<IDatabaseContext> dbContext;
    private List<User> mockUsersList;

    public UserRepositoryTest()
    {
      dbContext = new Mock<IDatabaseContext>();

      mockUsersList = populateMockUsersList();

      Mock<DbSet<User>> usersMock = DbContextTestUtils.CreateDbSetMock(mockUsersList);
      dbContext.Setup(context => context.Users).Returns(usersMock.Object);

      repository = new UserRepository(dbContext.Object);
    }

    private List<User> populateMockUsersList()
    {
      List<User> list = new List<User>();

      list.Add(new User()
      {
        Id = "1",
        Name = "User 1"
      });

      list.Add(new User()
      {
        Id = "2",
        Name = "User 2"
      });

      return list;
    }

    [Fact]
    public void FindById_ShouldQueryUsersById_WhenCalled()
    {
      User result = repository.FindById("2");

      Assert.Equal(mockUsersList[1], result);
    }

    [Fact]
    public void Save_ShouldAddANewUser_WhenCalled()
    {
      User toBeSaved = new User() { Name = "This will be saved" };

      repository.Save(toBeSaved);

      dbContext.Verify(context => context.Users.Add(toBeSaved), Times.Once);
      dbContext.Verify(context => context.SaveChanges(), Times.Once);
    }
  }

}