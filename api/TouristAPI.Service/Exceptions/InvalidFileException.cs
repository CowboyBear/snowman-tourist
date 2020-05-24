using System;

namespace TouristAPI.Service.Exceptions
{
  [Serializable]
  public class InvalidFileException : Exception
  {
    public InvalidFileException() { }

    public InvalidFileException(string message) : base(message) { }

  }
}