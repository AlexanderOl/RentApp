
using AutoMapper;
using RentApp.Models.Cache;
using RentApp.Models.DbModels;
using RentApp.Models.RequestModels;
using RentApp.Models.ResponseModels;
using RentApp.Models.Structs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentApp.Utilities
{
    public static class AutoMapperUtility
    {

        private static IMapper _iMapper;
        public static IMapper IMapper => _iMapper ?? (_iMapper = PrepareMapper());

        private static IMapper PrepareMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserRequest, User>();
                cfg.CreateMap<User, UserCacheItem>();
                cfg.CreateMap<UserCacheItem, User>();
                cfg.CreateMap<UserCacheItem, AuthenticationResponse>()
                    .ForMember(dest => dest.Name,
                                map => map.MapFrom(source => source.Firstname + " " + source.Lastname))
                    .ForMember(dest => dest.ProfileImageURL,
                                map => map.MapFrom(source => GetUploadedImageUrl(source.ProfileImageId)));

                cfg.CreateMap<CreateOfferRequest, Offer>();
                cfg.CreateMap<Offer, OfferFilterResponse>()
                    .ForMember(dest => dest.PhotoURLs,
                                map => map.MapFrom(source => source.PropertyPhotos.Select(p => p.Url).ToList()));

                cfg.CreateMap<Message, SendMessageRequest>();
                cfg.CreateMap<MessageResponse, Message>();

                





            });
            return config.CreateMapper();
        }

        private static string GetUploadedImageUrl(Guid? profileImageId)
        {
            var imageUtility = new ImageUtility(PhotoType.Profile);
            return imageUtility.GetUploadedImageUrl(profileImageId);
        }


    }
}
