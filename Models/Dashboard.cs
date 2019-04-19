using System.Collections.Generic;

namespace DojoActivityNew.Models
{
  public class Dashboard
  {
    public List<Activities> Activities { get; set; }
    public List<Guest> Guests { get; set; }
    public User User { get; set; }
  }

  public class One
  {
    public Activities Activity{get ;set;}
    public User User { get; set; }
    public List<Guest> Guests { get; set; }

  }

}