using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DojoActivityNew.Models
{
    public class Activities
    {
        [Key]
        public int ActivityId {get; set;}

        [Required]
        [MinLength(2)]
        public string Title {get; set;}

        [Required]
        public DateTime Date{get; set;} = DateTime.Now;

        [Required]
        [MinLength(10)]
        public string Description {get;set;}
        [Required]
        public int DurationFirst {get; set;}

        [Required]
        public string DurationSecond {get;set;}

        
        public int UserId {get;set;}
        public User User {get;set;}

        public List<Guest> Guest{get;set;}

    }
}