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
  }
}
