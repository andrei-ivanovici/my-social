using AutoMapper;
using Social.Api.Contracts;
using Social.Api.Data.Model;

namespace Social.Api.MapProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<CreateUser, UserEntity>();
            CreateMap<UserEntity, User>();
        }
    }
}