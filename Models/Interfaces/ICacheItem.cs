using System;

namespace RentApp.Models.Interfaces
{
    public interface ICacheItem
    {
       Guid Id { get; set; }
       string Email { get; set; }
       string Phonenumber { get; set; }
       string Firstname { get; set; }
       string Lastname { get; set; }
       string Password { get; set; }
       Guid? ProfileImageId { get; set; }
       Guid ActivationCode { get; set; }
       bool IsAlive { get; set; }
       bool IsActivated { get; set; }
       DateTime CreateDateTime { get; set; }
       DateTime LastOnlineDateTime { get; set; }
    }
}