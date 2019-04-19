using System.ComponentModel.DataAnnotations;

namespace DojoActivityNew.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        public string LogEmail {get; set;}
        
        [Required]
        [MinLength(8)]
        public string LogPassword { get; set; }
    }
}