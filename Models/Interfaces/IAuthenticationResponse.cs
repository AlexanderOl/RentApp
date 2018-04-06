using System;

namespace RentApp.Models.Interfaces
{
    internal interface IAuthenticationResponse
    {
        Guid Id { get; set; }
        string Phonenumber { get; set; }
        string Email { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        DateTime LastOnlineDateTime { get; set; }

    }
}