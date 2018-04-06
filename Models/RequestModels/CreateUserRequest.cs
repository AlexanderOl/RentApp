using RentApp.Models.Interfaces;
using System;

namespace RentApp.Models.DbModels
{
    public class CreateUserRequest : IUserRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public Guid ActivationCode { get; set; } = Guid.NewGuid();

    }
}