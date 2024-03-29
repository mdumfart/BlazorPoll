﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class UserDetailsDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must have 3 characters at least")]
        public string UserName { get; set; }

        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{0,}$", ErrorMessage = "Password must contain at least 1 alphanumeric character and 1 upper case letter")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must have 8 characters at least")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [CompareProperty("Password", ErrorMessage = "Your passwords must match")]
        public string PasswordConfirm { get; set; }
    }
}
