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


  }
}
