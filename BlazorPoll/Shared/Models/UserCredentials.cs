using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class UserCredentials
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must have 3 characters at least")]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "Password must have 6 characters at least")]
        public string Password { get; set; }
    }
}
