using RentApp.Models.Interfaces;
using System;

namespace RentApp.Models.RequestModels
{
    public class SendMessageRequest : IMessage
    {
        public Guid Id { get; set; }
        public Guid UserIdTo { get; set; }
        public Guid UserIdFrom { get; set; }
        public string Body { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
