﻿namespace RegistracijaVozila.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles{ get; set; }
    }
}
