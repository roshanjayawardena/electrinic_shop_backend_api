﻿namespace Electronic_Application.Models.Auth
{
    public class RegisterUserModel
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;

    }
}
