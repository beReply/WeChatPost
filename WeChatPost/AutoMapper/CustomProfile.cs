using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChatPost.DataProvider.EFCore.Entities;
using WeChatPost.WeChat.Param;

namespace WeChatPost.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<ReceiveMessage, ReceiveMessageEntity>();
        }
    }
}
