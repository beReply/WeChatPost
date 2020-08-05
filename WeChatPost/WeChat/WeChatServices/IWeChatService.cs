using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChatPost.WeChat.WeChatServices
{
    public interface IWeChatService
    {
        Task<string> GetAccessTokenAsync();

        Task<string> GetServerIpAsync(string accessToken);
    }
}
