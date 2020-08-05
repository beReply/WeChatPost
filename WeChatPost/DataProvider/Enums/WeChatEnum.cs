using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChatPost.DataProvider.Enums
{
    public class WeChatEnum
    {
        public enum MsgType
        {
            text = 1, //文本消息
            image = 2, //图片消息
            voice = 3, //语音消息
            video = 4, //视频消息
            shortvideo = 5, //小视频消息
            location = 6, //地理位置消息
            link = 7, //链接消息
            unknown = 8 //未知类型
        }
    }
}
