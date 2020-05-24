using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using TouristAPI.Service.Exceptions;

namespace TouristAPI.Service.Validators
{
  public class LocationFormValidator : ILocationFormValidator
  {

    private static List<string> SUPPORTED_FILE_FORMATS = new List<string>() { "jpg", "png" };
    private static List<string> REQUIRED_FORM_KEYS = new List<string>() { "Name", "Address", "Lat", "Lng", "UserId" };
    private const string INVALID_COORDINATES_EXCEPTION_ERROR = "Latitude or Longitude invalid. Latitude range is -90 to 90; Longitude range is -180 to 180";
    private const string REQUIRED_FIELDS_MISSING_ERROR = "Payload missing required fields, please check the documentation.";
    private const string INVALID_FILE_FORMAT_ERROR = "Invalid file format. Accepted formats are: .jpg and .png";
    private const string FILE_TOO_BIG_ERROR = "File is too big, please upload images up to 6MB";
    private const int SIX_MEGABYTES_IN_BYTES = 6000000;
    private const string COORDINATES_PARSE_ERROR = "Latitude or Longitude invalid. Expected values must be double";

    public bool isValid(IFormCollection form)
    {
      validateRequiredKeys(form);
      validateLatLng(form);
      validateFile(form);

      return true;
    }

    private void validateFile(IFormCollection form)
    {
      if (form.Files.Count > 0)
      {
        IFormFile file = form.Files[0];
        validateFileExtension(file);
        validateFileSize(file);
      }
    }

    private void validateFileSize(IFormFile file)
    {
      if (file.Length > SIX_MEGABYTES_IN_BYTES)
      {
        throw new InvalidFileException(FILE_TOO_BIG_ERROR);
      }
    }

    private void validateFileExtension(IFormFile file)
    {
      string[] split = file.FileName.Split('.');

      if (split.Length > 1)
      {
        string fileExtension = split[1];

        if (SUPPORTED_FILE_FORMATS.Contains(fileExtension))
        {
          return;
        }
      }

      throw new InvalidFileException(INVALID_FILE_FORMAT_ERROR);
    }

    private void validateLatLng(IFormCollection form)
    {
      double lat = 0;
      double lng = 0;

      if (!double.TryParse(form["Lat"], out lat) || !double.TryParse(form["Lng"], out lng))
      {
        throw new InvalidLocationException(COORDINATES_PARSE_ERROR);
      }
      else
      {
        try
        {
          GeoCoordinate coordinate = new GeoCoordinate(lat, lng);
        }
        catch (ArgumentOutOfRangeException)
        {
          throw new InvalidLocationException(INVALID_COORDINATES_EXCEPTION_ERROR);
        }
      }
    }

    private void validateRequiredKeys(IFormCollection form)
    {
      foreach (string key in REQUIRED_FORM_KEYS)
      {
        if (!form.ContainsKey(key))
        {
          throw new InvalidLocationException(REQUIRED_FIELDS_MISSING_ERROR);
        }
      }
    }
  }
}