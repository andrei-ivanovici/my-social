using System;
using AutoMapper;
using Microsoft.VisualBasic;
using Social.Api.Contracts;
using Social.Api.Contracts.Posts;
using Social.Api.Data.Model;

namespace Social.Api.MapProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<CreateUser, UserEntity>();
            CreateMap<UserEntity, User>();
            CreateMap<UserEntity, EntryOwner>();
            CreateMap<Post, PostEntity>()
                .ForMember(entity => entity.CreatedOn,
                    opt =>
                        opt.AddTransform(dt => dt == default ? DateTime.UtcNow : dt))
                .ReverseMap();
        }
    }
}