using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TouristAPI.Database.Repository;
using TouristAPI.Model;
using TouristAPI.Service.Exceptions;
using TouristAPI.Service.Validators;

namespace TouristAPI.Service
{
  public class LocationService : ILocationService
  {

    private ILocationRepository _repository;
    private ILocationFormValidator _formValidator;
    private IHostingEnvironment _hostingEnvironment;
    private IFileSystem _fileSystem;

    public LocationService(
      ILocationRepository repository, 
      ILocationFormValidator formValidator, 
      IHostingEnvironment hostingEnvironment,
      IFileSystem fileSystem
      )
    {
      _repository = repository;
      _formValidator = formValidator;
      _hostingEnvironment = hostingEnvironment;
      _fileSystem = fileSystem;
    }

    public IList<Location> FindAll()
    {
      return _repository.FindAll();
    }

    public Location Save(IFormCollection formData)
    {
      if (_formValidator.isValid(formData))
      {
        return _repository.Save(parseToLocation(formData, SavePicture(formData)));
      }

      return null;
    }

    private string SavePicture(IFormCollection formData)
    {
      if (formData.Files.Count > 0)
      {
        IFormFile file = formData.Files[0];
        string newFileName = getNewFileName(file.FileName);
        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploadedfiles", newFileName);

        using (var stream = _fileSystem.File.Create(filePath))
        {
          file.CopyTo(stream);
        }

        return newFileName;
      }

      return null;
    }

    private string getNewFileName(string fileName)
    {
      string fileExtension = fileName.Split('.')[1];
      return string.Format("{0}.{1}", Guid.NewGuid().ToString(), fileExtension);
    }

    private Location parseToLocation(IFormCollection formData, string fileName)
    {
      return new Location()
      {
        Name = formData["Name"],
        Address = formData["Address"],
        Lat = Convert.ToDouble(formData["Lat"]),
        Lng = Convert.ToDouble(formData["Lng"]),
        UserId = formData["UserId"],
        PicturePath = fileName
      };
    }

    public IList<Location> FindNearby(double latitude, double longitude, int radius)
    {
      try {
        GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
        return _repository.FindNearby(coordinate, radius);
      } catch (ArgumentOutOfRangeException ex) {
        throw new InvalidLocationException("Latitude or Longitude invalid. Latitude range is -90 to 90; Longitude range is -180 to 180");
      }
    }
  }
}
