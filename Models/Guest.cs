using System;
using System.Collections.Generic;

namespace DojoActivityNew.Models
{
    public class Guest
    {
        public int GuestId {get;set;}
        public int UserId {get; set;}
        public int ActivityId {get;set;}
        public User User {get;set;}
        public Activities Activity {get;set;}

        public Guest(int userId, int activityId)
        {
            UserId = userId;
            ActivityId = activityId;
        }
  
    }
}