using System.Collections.Generic;
using AutoMapper;
using DiSConnected.Sitecore.Web.Common.Classes.Interfaces;
using DiSConnected.Sitecore.Web.Common.Classes.Util;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;

namespace DiSConnected.Sitecore.Web.Common.Classes.Dtos
{
    public class Mappings
    {
        public class UserDtoMapping : IDtoMapping
        {
            public void CreateMappings()
            {
                Mapper.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.ProfilePortrait, opt => opt.MapFrom(src => RestUtil.ResolveSitecoreIcon(src.Profile.Portrait)));
            }
        }

        public class ArticleDtomapping : IDtoMapping
        {
            public void CreateMappings()
            {
                Mapper.CreateMap<Item, ArticleDto>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DisplayName))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Fields["Text"].ToString()))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => RestUtil.GetComponentCodeName(src)))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID.ToGuid()))
                    .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => "Summary - " + src.DisplayName))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Fields["Author"].ToString()))
                    .ForMember(dest => dest.Tags, opt => opt.UseValue(new List<string>()))
                    .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => RestUtil.ResolveSitecoreIcon(src.Template.Icon)));
            }
        }
    }
}