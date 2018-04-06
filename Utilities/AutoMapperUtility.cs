
using AutoMapper;
using RentApp.Models.DbModels;

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

            });

            return config.CreateMapper();
        }

        //var source = new AuthorModel();

        //var destination = iMapper.Map<AuthorModel, AuthorDTO>(source);

    }
}
