﻿using System.ComponentModel.DataAnnotations;

namespace CurdApplication.Models.Account
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long? Mobile { get; set; }
        public bool IsActive { get; set; }
        
        
    }
}
