using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Band
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Band(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }

    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT DISTINCT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", this.Id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Venue> venues = new List<Venue>();

      while (rdr.Read())
      {
        int thisVenueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue foundVenue = new Venue(venueName, thisVenueId);
        venues.Add(foundVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venues;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

      cmd.Parameters.Add(new SqlParameter("@VenueId", newVenue.Id));
      cmd.Parameters.Add(new SqlParameter("@BandId", this.Id));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Save()
    {
      if (this.Name == "")
      {
        return;
      }
      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

      cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>();
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        rdr.Close();
      }
      return allBands;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquals = (this.Id == newBand.Id);
        bool nameEquals = (this.Name == newBand.Name);
        return (idEquals && nameEquals);
      }
    }



    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
