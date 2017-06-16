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
      Venue venueOne = new Venue("Master Onion's Dojo");
      Venue venueTwo = new Venue("Master Onion's Dojo");

      //Assert
      Assert.Equal(venueOne, venueTwo);
    }

    [Fact]
    public void Save_ObjectAddedToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void AddBand_AddBandToVenue()
    {
      //arrange
      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      Band testBand = new Band("Parappa the Rappa");
      testBand.Save();

      testVenue.AddBand(testBand);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand};
      //assert
      Assert.Equal(result, testList);
    }

    [Fact]
    public void GetBands_ReturnsAllBands()
    {
      //arrange
      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      Band testBand1 = new Band("Parappa the Rappa");
      testBand1.Save();

      Band testBand2 = new Band("Mega Parappa the Rappa");
      testBand2.Save();

      testVenue.AddBand(testBand1);
      testVenue.AddBand(testBand2);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1, testBand2};

      //Assert
      Assert.Equal(testList, result);
      //assert
    }

    [Fact]
    public void Find_FindsVenueInDB()
    {
      //arrange
      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.Id);
      //assert
      Assert.Equal(foundVenue, testVenue);
    }

    [Fact]
    public void Update_UpdatesInfoInDB()
    {
      //arrange
      string newName = "Club Fun";
      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      testVenue.Update(newName);
      //assert
      Assert.Equal(newName, testVenue.Name);
    }

    [Fact]
    public void Delete_DeletesVenueAssociationsFromDatatbase_VenueList()
    {
      //arrange
      Band testBand = new Band("Parappa the Rappa");
      testBand.Save();

      Venue testVenue = new Venue("Master Onion's Dojo");
      testVenue.Save();

      //act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //assert
      Assert.Equal(testBandVenues, resultBandVenues);
    }

    ///////////////////// NOT NORMAL STUFF //////////////////////////////////

    [Fact]
    public void AddToSourceTable_PopulatesSourceTable()
    {
      //Arrange
      Venue venueOne = new Venue("Master Onion's Dojo");
      Venue venueTwo = new Venue("Club Fun");
      List<Venue> allVenues = new List<Venue>{venueOne, venueTwo};

      //Act
      Venue.AddToSourceTable(allVenues);
      List<Venue> result = Venue.GetAllFromSource();

      //Assert
      Assert.Equal(allVenues, result);
    }

    [Fact]
    public void Merge_InsertsDataThroughMerge()
    {
      Venue venueOne = new Venue("Master Onion's Dojo");
      Venue venueTwo = new Venue("Club Fun");
      List<Venue> allVenues = new List<Venue>{venueOne, venueTwo};

      //Act
      Venue.AddToSourceTable(allVenues);
      Venue.Merge();
      List<Venue> result = Venue.GetAll();

      //Assert
      Assert.Equal(allVenues, result);
    }


    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Venue.DeleteAllFromJoin();
      Venue.DeleteAllFromSource();
    }
  }
}
