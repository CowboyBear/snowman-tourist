using System;
using Moq;
using TouristAPI.Database.Repository;
using Xunit;

namespace TouristAPI.Service.Tests
{
  public class LocationServiceTest
  {
    private Mock<ILocationRepository> mockRepository;
    private LocationService service;
  
    public LocationServiceTest(){

      mockRepository = new Mock<ILocationRepository>();
      service = new LocationService(mockRepository.Object);
    }

    [Fact]
    public void FindAll_ShouldCallTheRepository_WhenCalled()
    {
      service.FindAll();
      mockRepository.Verify(repository => repository.FindAll(), Times.Once);
    }
  }
}
