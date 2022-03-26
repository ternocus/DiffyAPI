﻿using DiffyAPI.Core.Model;

namespace DiffyAPI.UserAPI.Core.Model
{
    public class UploadUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public Privileges Privilege { get; set; }
    }
}
