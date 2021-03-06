﻿using Microsoft.AspNetCore.SignalR;
using RentApp.Cache;
using RentApp.Hubs;
using RentApp.Models.Cache;
using RentApp.Models.DbModels;
using RentApp.Models.RequestModels;
using RentApp.Models.ResponseModels;
using RentApp.Repositories;
using RentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentApp.Managers
{
    public class ProfileManager
    { 
        MessageRepository _messageRepository;
        IHubContext<MainHub> _hubContext;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public ProfileManager(MessageRepository messageRepository, IHubContext<MainHub> hubContext)
        {
            _messageRepository = messageRepository;
            _hubContext = hubContext;
        }

        internal UserMessagesResponse GetUserMessages(Guid userId)
        {
            var messageList = MessageCache.CachedItems.Values
                .Where(w => w.UserIdFrom == userId || w.UserIdTo == userId)
                .OrderBy(o => o.CreateDateTime)
                .Select(x => AutoMapperUtility.IMapper.Map<MessageResponse>(x))
                .ToList();


            var distinctUserIds = messageList
                .Select(s => new[] { s.UserIdFrom, s.UserIdTo })
                .SelectMany(s => s)
                .Where(w => w != userId)
                .Distinct();

            var userList = new List<AuthenticationResponse>();
            foreach (var item in distinctUserIds)
                if (UserCache.CachedItems.ContainsKey(item))
                    userList.Add(AutoMapperUtility.IMapper.Map<AuthenticationResponse>(UserCache.CachedItems[item]));

            return new UserMessagesResponse(messageList, userList);
        }

        internal async Task<BaseResponse> SendChatMessage(SendMessageRequest messageRequest)
        {
            var message = AutoMapperUtility.IMapper.Map<Message>(messageRequest);
            _messageRepository.Create(message);

            UserCache.CachedItems.TryGetValue(message.UserIdTo, out UserCacheItem result);

            if (result != null && !string.IsNullOrEmpty(result?.ConnectionId))
                await _hubContext.Clients.Client(result?.ConnectionId.ToString())
                     .InvokeAsync("messageSent", AutoMapperUtility.IMapper.Map<MessageResponse>(message));

            return new BaseResponse();
        }

        internal void UpdateOnlineStatus(Guid value)
        {
            if (UserCache.CachedItems.ContainsKey(value))
            {
                var now = DateTime.Now;
                UserCache.CachedItems[value].LastOnlineDateTime = now;
                Task.Factory.StartNew(() => UpdateLastOnlineAsync(now));
            }
        }

        private async Task UpdateLastOnlineAsync(DateTime now)
        {
            var minute = 60000;
            await semaphoreSlim.WaitAsync(minute);
            try
            {
                var result = UserCache.CachedItems.Values
                    .Where(w => w.LastOnlineDateTime > now.AddMilliseconds(-minute))
                    .ToDictionary(x => x.Id, x => x.LastOnlineDateTime);

                await _hubContext.Clients.All.InvokeAsync("onlineStatusUpdated", result);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
