using System;
using Moq;
using TouristAPI.Api.Controllers;
using TouristAPI.Service;
using Xunit;

namespace TouristAPI.Controller.Tests
{
  public class LocationControllerTest
  {
    private LocationController controller;
    private Mock<ILocationService> mockLocationService;

    public LocationControllerTest(){
      mockLocationService = new Mock<ILocationService>();
      controller = new LocationController(mockLocationService.Object);
    }

    [Fact]
    public void Get_ShouldGetAllLocations_WhenCalled()
    {
      controller.Get();
      mockLocationService.Verify(service => service.FindAll(), Times.Once);
    }
  }
}
