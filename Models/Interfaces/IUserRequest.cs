using System;

namespace RentApp.Models.Interfaces
{
    public interface IUserRequest
    {
        Guid Id { get; set; }
        string Email { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Password { get; set; }
        Guid ActivationCode { get; set; }
    }
}