using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Venue(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }
    /////////////////////////////// NOT NORMAL STUFF ///////////////////////////////

    public static void Merge()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("MERGE INTO venues AS TARGET USING venues_source AS SOURCE ON (TARGET.id = SOURCE.id) WHEN MATCHED AND TARGET.name <> SOURCE.name THEN UPDATE SET TARGET.name = SOURCE.name WHEN NOT MATCHED BY TARGET THEN INSERT (name) VALUES (SOURCE.name) WHEN NOT MATCHED BY SOURCE THEN DELETE OUTPUT $action, DELETED.id AS TargetId, DELETED.name AS TargetName, INSERTED.id AS SourceId, INSERTED.name AS SourceName;", conn);
      cmd.ExecuteNonQuery();

      conn.Close();
    }


    public static List<Venue> GetAllFromSource()
    {
      List<Venue> allVenues = new List<Venue>();
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues_source;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        rdr.Close();
      }
      return allVenues;
    }

    public static void AddToSourceTable(List<Venue> allVenues)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues_source;", conn);
      cmd.ExecuteNonQuery();


      foreach (Venue venue in allVenues)
      {
        if (venue.Name == "")
        {
          return;
        }
        cmd = new SqlCommand("SET IDENTITY_INSERT venues_source ON; INSERT INTO venues_source (id, name) OUTPUT INSERTED.id VALUES (@Id, @Name); SET IDENTITY_INSERT venues_source OFF;", conn);

        cmd.Parameters.Add(new SqlParameter("@Name", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(venue.Name.ToLower())));
        cmd.Parameters.Add(new SqlParameter("@Id", venue.Id));

        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          venue.Id = rdr.GetInt32(0);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
      }

      if (conn != null)
      {
        conn.Close();
      }
    }


/////////////////////////////// NORMAL STUFF ///////////////////////////////
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id=@VenueId; DELETE FROM bands_venues WHERE venue_id=@VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name=@Name OUTPUT INSERTED.name WHERE id=@VenueId;", conn);

      cmd.Parameters.Add(new SqlParameter("@Name", newName));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id=@VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT DISTINCT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Band> bands = new List<Band>();

      while (rdr.Read())
      {
        int thisBandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band foundBand = new Band(bandName, thisBandId);
        bands.Add(foundBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

      cmd.Parameters.Add(new SqlParameter("@BandId", newBand.Id));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));
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


      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

      cmd.Parameters.Add(new SqlParameter("@Name", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(this.Name.ToLower())));
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

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>();
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        rdr.Close();
      }
      return allVenues;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquals = (this.Id == newVenue.Id);
        bool nameEquals = (this.Name == newVenue.Name);
        return (idEquals && nameEquals);
      }
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static void DeleteAllFromJoin()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands_venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    //Turn to Drop TABLE later
    public static void DeleteAllFromSource()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues_source;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
