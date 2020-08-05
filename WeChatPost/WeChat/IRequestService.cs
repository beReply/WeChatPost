using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeChatPost.WeChat
{
    public interface IRequestService
    {
        Task<string> SendAsync(HttpMethod httpMethod, string path, string body);

        Task<string> GetAsync(string url, Dictionary<string, string> bodyDic);
    }
}
