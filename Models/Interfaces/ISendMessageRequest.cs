using System;

namespace RentApp.Models.Interfaces
{
    public interface IMessage
    {
        Guid Id { get; set; }
        Guid UserIdTo { get; set; }
        Guid UserIdFrom { get; set; }
        string Body { get; set; }
        DateTime CreateDateTime { get; set; }
        bool IsRead { get; set; }
    }
}