using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Auth
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public DateTime Expiry { get; set; }
    }
}
