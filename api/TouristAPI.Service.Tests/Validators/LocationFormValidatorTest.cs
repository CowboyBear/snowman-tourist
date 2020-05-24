using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using TouristAPI.Service.Exceptions;
using Xunit;

namespace TouristAPI.Service.Validators.Tests
{
  public class LocationFormValidatorTest
  {
    private LocationFormValidator validator;
    public LocationFormValidatorTest()
    {
      validator = new LocationFormValidator();
    }

    [Fact]
    public void isValid_ShouldReturnTrue_GivenAValidForm()
    {
      Dictionary<string, StringValues> fields = CreateValidFormFields();

      FormCollection form = new FormCollection(fields);

      Assert.True(validator.isValid(form));
    }

    [Fact]
    public void isValid_ShouldThrowAnInvalidLocationException_GivenMissingRequiredFieldsInForm()
    {
      Dictionary<string, StringValues> fields = new Dictionary<string, StringValues>();      

      FormCollection form = new FormCollection(fields);

      InvalidLocationException exception = Assert.Throws<InvalidLocationException>(() => validator.isValid(form));
      Assert.Equal("Payload missing required fields, please check the documentation.", exception.Message);
    }

    [Theory]
    [InlineData("91", "181", "Latitude or Longitude invalid. Latitude range is -90 to 90; Longitude range is -180 to 180")]
    [InlineData("abc", "def", "Latitude or Longitude invalid. Expected values must be double")]
    public void isValid_ShouldThrowAnInvalidLocationException_GivenInvalidCoordinatesAreSent(string lat, string lng, string expectedExceptionMessage)
    {
      Dictionary<string, StringValues> fields = CreateValidFormFields();

      fields["Lat"] = new StringValues(lat);
      fields["Lng"] = new StringValues(lng);

      FormCollection form = new FormCollection(fields);

      InvalidLocationException exception = Assert.Throws<InvalidLocationException>(() => validator.isValid(form));
      Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void isValid_ShouldThrowAnInvalidFileException_GivenAnUnsupportedFileFormatIsSentInForm()
    {
      Dictionary<string, StringValues> fields = CreateValidFormFields();

      Mock<IFormFile> mockFormFile = new Mock<IFormFile>();
      mockFormFile.Setup(file => file.FileName).Returns("file.txt");
      IFormFileCollection formFiles = new FormFileCollection() { mockFormFile.Object };

      FormCollection form = new FormCollection(fields, formFiles);

      InvalidFileException exception = Assert.Throws<InvalidFileException>(() => validator.isValid(form));
      Assert.Equal("Invalid file format. Accepted formats are: .jpg and .png", exception.Message);
    }

    [Fact]
    public void isValid_ShouldThrowAnInvalidFileException_GivenFileInFormHasMoreThan5MB()
    {
      Dictionary<string, StringValues> fields = CreateValidFormFields();

      Mock<IFormFile> mockFormFile = new Mock<IFormFile>();
      mockFormFile.Setup(file => file.FileName).Returns("file.jpg");
      mockFormFile.Setup(file => file.Length).Returns(6000001);
      IFormFileCollection formFiles = new FormFileCollection() { mockFormFile.Object };

      FormCollection form = new FormCollection(fields, formFiles);

      InvalidFileException exception = Assert.Throws<InvalidFileException>(() => validator.isValid(form));
      Assert.Equal("File is too big, please upload images up to 6MB", exception.Message);
    }

    private Dictionary<string, StringValues> CreateValidFormFields()
    {
      Dictionary<string, StringValues> fields = new Dictionary<string, StringValues>();

      fields.Add("Name", new StringValues("Test"));
      fields.Add("Address", new StringValues("R. Test"));
      fields.Add("Lat", new StringValues("90"));
      fields.Add("Lng", new StringValues("90"));
      fields.Add("UserId", new StringValues("Test"));

      return fields;
    }
  }
}