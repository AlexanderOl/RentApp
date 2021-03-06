﻿using RentApp.Models.ResponseModels;
using System;
using RentApp.Models.Interfaces;
using RentApp.Models.DbModels;

namespace RentApp.Models.Cache
{
    public class UserCacheItem : IEmailItem, ICacheItem, IAuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public Guid? ProfileImageId { get; set; }
        public Guid ActivationCode { get; set; }
        public bool IsAlive { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LastOnlineDateTime { get; set; }

        //Not Mapped
        public string ConnectionId { get; set; }
    }
}