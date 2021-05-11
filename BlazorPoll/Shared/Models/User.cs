using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Username must have 3 characters at least")]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must have 6 characters at least")]
        [AllowNull]
        public string Password { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
