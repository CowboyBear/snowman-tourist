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
  public class LocationServiceTest
  {
    private Mock<ILocationRepository> mockRepository;
    private Mock<ILocationFormValidator> mockFormValidator;
    private LocationService service;

    public LocationServiceTest()
    {

      mockRepository = new Mock<ILocationRepository>();
      mockFormValidator = new Mock<ILocationFormValidator>();

      service = new LocationService(mockRepository.Object, mockFormValidator.Object);
    }

    [Fact]
    public void FindAll_ShouldCallTheRepository_WhenCalled()
    {
      service.FindAll();
      mockRepository.Verify(repository => repository.FindAll(), Times.Once);
    }

    [Fact]
    public void Save_ShouldSaveOnDb_GivenAllRequiredFieldsArePresentInForm()
    {
      Dictionary<string, StringValues> fields = new Dictionary<string, StringValues>();
      FormCollection form = new FormCollection(fields);

      mockFormValidator.Setup(validator => validator.isValid(form)).Returns(true);

      service.Save(form);

      mockRepository.Verify(repository => repository.Save(It.IsAny<Location>()), Times.Once);
    }
  }
}
