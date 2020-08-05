using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChatPost.DataProvider.Entities;
using WeChatPost.DataProvider.Enums;
using WeChatPost.WeChat.Param;

namespace WeChatPost.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<ReceiveMessage, ReceiveMessageEntity>().ForMember(x => x.MsgType, 
                opt => opt.MapFrom(s => s.MsgType.ToMsgType()));
        }
    }
}
