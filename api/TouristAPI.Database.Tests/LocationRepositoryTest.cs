using System;
using TouristAPI.Database.Context;
using TouristAPI.Database.Repository;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using TouristAPI.Model;
using Microsoft.EntityFrameworkCore;
using TouristAPI.Database.Tests.Utils;

namespace TouristAPI.Database.Tests
{
  public class LocationRepositoryTest
  {
    private Mock<IDatabaseContext> dbContext;
    private LocationRepository repository;
    private List<Location> mockLocationsList = new List<Location>();

    public LocationRepositoryTest()
    {
      dbContext = new Mock<IDatabaseContext>();

      Mock<DbSet<Location>> locationsMock = DbContextTestUtils.CreateDbSetMock(mockLocationsList);
      dbContext.Setup(context => context.Locations).Returns(locationsMock.Object);

      repository = new LocationRepository(dbContext.Object);
    }

    [Fact]
    public void FindAll_ShouldGetAllLocations_WhenCalled()
    {
      IList<Location> locations = repository.FindAll();
      Assert.Equal(locations, mockLocationsList);
    }

    [Fact]
    public void Save_ShouldAddANewLocation_WhenCalled()
    {
      Location toBeSaved = new Location() { Name = "This will be saved" };

      repository.Save(toBeSaved);

      dbContext.Verify(context => context.Locations.Add(toBeSaved), Times.Once);
      dbContext.Verify(context => context.SaveChanges(), Times.Once);
    }
  }
}
