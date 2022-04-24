﻿using System.ComponentModel.DataAnnotations;

namespace TaskWebAPI.Models
{
    public class User
    {
        //email, password, confirmPassword, name, surname, address, phone
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public Token Token { get; set; } ??
    }
}
