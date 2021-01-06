using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class User
    {
        public string Username { get; set; }
        [AllowNull]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
