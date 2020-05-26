using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TouristAPI.Api.Controllers;
using TouristAPI.Model;
using TouristAPI.Service;
using TouristAPI.Service.Exceptions;
using Xunit;

namespace TouristAPI.Controller.Tests
{
  public class UserControllerTest
  {
    private UserController controller;
    private Mock<IUserService> mockUserService;

    public UserControllerTest()
    {
      mockUserService = new Mock<IUserService>();
      controller = new UserController(mockUserService.Object);
    }

    [Fact]
    public void Get_ShouldGetAllLocations_WhenCalled()
    {
      string userId = "Any user id";
      controller.Get(userId);
      mockUserService.Verify(service => service.FindById(userId), Times.Once);
    }

    [Fact]
    public void Post_ShouldReturnHttp201_GivenASuccessfulOperation()
    {
      mockUserService.Setup(service => service.Save(It.IsAny<User>())).Returns(new User());

      IActionResult result = controller.Post(new Mock<User>().Object);

      Assert.True(result.GetType().Equals(typeof(CreatedResult)));
    }

    [Fact]    
    public void Post_ShouldReturnHttp500_GivenAnErrorOccurs()
    {
      mockUserService.Setup(service => service.Save(It.IsAny<User>())).Throws(new Exception());

      IActionResult result = controller.Post(new Mock<User>().Object);

      Assert.True(result.GetType().Equals(typeof(ObjectResult)));
    }
  }
}
