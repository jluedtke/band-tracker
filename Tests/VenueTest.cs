using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTrackerTest")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void DB_DatabaseEmptyAtFirst()
    {
      //Arrange
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_ObjectsEqualEachOther()
    {
      //Arrange
      Venue venueOne = new Venue("The Knitting Factory");
      Venue venueTwo = new Venue("The Knitting Factory");

      //Assert
      Assert.Equal(venueOne, venueTwo);
    }


    public void Dispose()
    {
      Venue.DeleteAll();
      Venue.DeleteAllFromJoin();
    }
  }
}
