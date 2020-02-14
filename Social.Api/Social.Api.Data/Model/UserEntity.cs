﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace Social.Api.Data.Model
{
    [Table("Users")]
    public class UserEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}