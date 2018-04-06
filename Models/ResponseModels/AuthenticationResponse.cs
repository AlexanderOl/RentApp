
using RentApp.Models.Interfaces;
using System;

namespace RentApp.Models.ResponseModels
{
    public class AuthenticationResponse : BaseResponse, IAuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime LastOnlineDateTime { get; set; }

        public string Name { get; set; }
        public string ProfileImageURL { get; set; }
    }
}
