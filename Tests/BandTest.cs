using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTrackerTest")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void DB_DatabaseEmptyAtFirst()
    {
      //Arrange
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_ObjectsEqualEachOther()
    {
      //Arrange
      Band bandOne = new Band("Parappa the Rappa");
      Band bandTwo = new Band("Parappa the Rappa");

      //Assert
      Assert.Equal(bandOne, bandTwo);
    }

    [Fact]
    public void Save_ObjectAddedToDatabase()
    {
      //Arrange
      Band testBand = new Band("Parappa the Rappa");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Venue.DeleteAllFromJoin();
    }
  }
}
