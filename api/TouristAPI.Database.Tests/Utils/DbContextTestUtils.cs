using System;
using TouristAPI.Database.Context;
using TouristAPI.Database.Repository;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using TouristAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TouristAPI.Database.Tests.Utils
{
  public class DbContextTestUtils
  {   
    public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
    {
      var elementsAsQueryable = elements.AsQueryable();
      var dbSetMock = new Mock<DbSet<T>>();

      dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
      dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
      dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
      dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

      return dbSetMock;
    }
  }
}
