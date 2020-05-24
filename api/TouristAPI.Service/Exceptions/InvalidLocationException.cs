using System;

namespace TouristAPI.Service.Exceptions
{
  [Serializable]
  public class InvalidLocationException : Exception
  {
    public InvalidLocationException() { }

    public InvalidLocationException(string message) : base(message) { }

  }
}