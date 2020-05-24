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
  public class LocationControllerTest
  {
    private LocationController controller;
    private Mock<ILocationService> mockLocationService;

    public LocationControllerTest()
    {
      mockLocationService = new Mock<ILocationService>();
      controller = new LocationController(mockLocationService.Object);
    }

    [Fact]
    public void Get_ShouldGetAllLocations_WhenCalled()
    {
      controller.Get();
      mockLocationService.Verify(service => service.FindAll(), Times.Once);
    }

    [Fact]
    public void Post_ShouldReturnHttp201_GivenASuccessfulOperation()
    {
      mockLocationService.Setup(service => service.Save(It.IsAny<IFormCollection>())).Returns(new Location());

      IActionResult result = controller.Post(new Mock<IFormCollection>().Object);

      Assert.True(result.GetType().Equals(typeof(CreatedResult)));
    }

    [Fact]    
    public void Post_ShouldReturnHttp400_GivenAnInvalidFormIsSent()
    {
      mockLocationService.Setup(service => service.Save(It.IsAny<IFormCollection>())).Throws(new InvalidLocationException());

      IActionResult result = controller.Post(new Mock<IFormCollection>().Object);

      Assert.True(result.GetType().Equals(typeof(BadRequestObjectResult)));
    }

    [Fact]    
    public void Post_ShouldReturnHttp400_GivenAnInvalidFileIsSent()
    {
      mockLocationService.Setup(service => service.Save(It.IsAny<IFormCollection>())).Throws(new InvalidFileException());

      IActionResult result = controller.Post(new Mock<IFormCollection>().Object);

      Assert.True(result.GetType().Equals(typeof(BadRequestObjectResult)));
    }

    [Fact]    
    public void Post_ShouldReturnHttp500_GivenAnErrorOccurs()
    {
      mockLocationService.Setup(service => service.Save(It.IsAny<IFormCollection>())).Throws(new Exception());

      IActionResult result = controller.Post(new Mock<IFormCollection>().Object);

      Assert.True(result.GetType().Equals(typeof(ObjectResult)));
    }
  }
}
