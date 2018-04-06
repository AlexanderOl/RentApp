using RentApp.Models.Interfaces;
using System;

namespace RentApp.Models.ResponseModels
{
    public class MessageResponse : BaseResponse, IMessage
    {
        public Guid Id { get; set; }
        public Guid UserIdTo { get; set; }
        public Guid UserIdFrom { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDateTime { get; set; }

    }
}
