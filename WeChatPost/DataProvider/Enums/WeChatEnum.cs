using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChatPost.DataProvider.Enums
{
    public static class WeChatEnum
    {
        #region 方法

        public static MsgType ToMsgType(this string str)
        {
            var msgTypeArray = Enum.GetValues(typeof(MsgType)) as MsgType[];

            foreach (var msgType in msgTypeArray)
            {
                if (msgType.ToString() == str)
                {
                    return msgType;
                }
            }
            return MsgType.unknown;
        }

        #endregion



        #region 枚举

        public enum MsgType
        {
            text = 1,
            image = 2,
            voice = 3,
            video = 4,
            shortvideo = 5,
            location = 6,
            link = 7,
            unknown = 8
        }

        #endregion


    }
}
