using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Win32.SafeHandles;
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
    private Mock<IHostingEnvironment> mockHostingEnvironment;
    private Mock<IFileSystem> mockFileSystem;
    private LocationService service;

    public LocationServiceTest()
    {

      mockRepository = new Mock<ILocationRepository>();
      mockFormValidator = new Mock<ILocationFormValidator>();
      mockHostingEnvironment = new Mock<IHostingEnvironment>();
      mockFileSystem = new Mock<IFileSystem>();

      service = new LocationService(
        mockRepository.Object,
        mockFormValidator.Object,
        mockHostingEnvironment.Object,
        mockFileSystem.Object
      );
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

    [Fact]
    public void Save_ShouldSaveFilesToSpecificFolderInWebRootPath_GivenAValidFilePresentInForm()
    {
      Mock<IFormFile> mockFile = new Mock<IFormFile>();
      Dictionary<string, StringValues> fields = new Dictionary<string, StringValues>();
      FormCollection form = new FormCollection(fields, new FormFileCollection() { (mockFile.Object) });
      Mock<IFile> iFileMock = new Mock<IFile>();
      FileStream fileStream = null;

      mockFormValidator.Setup(validator => validator.isValid(form)).Returns(true);
      mockFile.Setup(mock => mock.FileName).Returns("FileName.jpg");
      iFileMock.Setup(fileMock => fileMock.Create(It.IsAny<string>())).Returns(fileStream);
      mockFileSystem.Setup(fileSystem => fileSystem.File).Returns(iFileMock.Object);
      mockHostingEnvironment.Setup(environment => environment.WebRootPath).Returns("/wwwroot");

      service.Save(form);

      iFileMock.Verify(mock => mock.Create(It.Is<string>(str => str.StartsWith("/wwwroot/uploadedfiles/"))));
      mockFile.Verify(mock => mock.CopyTo(fileStream), Times.Once);
    }

    [Fact]
    public void Save_ShouldSaveOnDbWithNullPicturePath_GivenNoFilesPresentInForm()
    {
      Dictionary<string, StringValues> fields = new Dictionary<string, StringValues>();
      FormCollection form = new FormCollection(fields);

      mockFormValidator.Setup(validator => validator.isValid(form)).Returns(true);

      service.Save(form);

      mockRepository.Verify(repository => repository.Save(It.Is<Location>(location => location.PicturePath == null)), Times.Once);
    }

    [Fact]
    public void FindNearby_ShouldReturnAListOfNearbyLocation_GivenCoordinatesAndARadius()
    {
      List<Location> mockList = new List<Location>();
      mockRepository.Setup(repository => repository.FindNearby(It.IsAny<GeoCoordinate>(), It.IsAny<int>())).Returns(mockList);

      IList<Location> result = service.FindNearby(90, -90, 500);

      Assert.Equal(mockList, result);
    }

    [Fact]
    public void FindNearby_ShouldParseCoordinatesAndCallRepository_GivenCoordinatesAndARadius()
    {
      GeoCoordinate expectedCoordinate = new GeoCoordinate(-90, 90);
      int expectedRadius = 500;

      service.FindNearby(expectedCoordinate.Latitude, expectedCoordinate.Longitude, expectedRadius);

      mockRepository.Verify(repository => repository.FindNearby(
        It.Is<GeoCoordinate>(
          coordinate => coordinate.Latitude == expectedCoordinate.Latitude
          && coordinate.Longitude == expectedCoordinate.Longitude
        ),
        expectedRadius
      ), Times.Once);
    }

    [Fact]        
    public void FindNearby_ShouldThrowAnInvalidLocationException_GivenInvalidCoordinates()
    {      
      string expectedExceptionMessage = "Latitude or Longitude invalid. Latitude range is -90 to 90; Longitude range is -180 to 180";

      InvalidLocationException exception = Assert.Throws<InvalidLocationException>(() => service.FindNearby(91, 181, 500));

      Assert.Equal(expectedExceptionMessage, exception.Message);
    }
  }
}
