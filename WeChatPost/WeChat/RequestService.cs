using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeChatPost.WeChat
{
    public class RequestService : IRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> SendAsync(HttpMethod httpMethod, string path, string body)
        {
            Console.WriteLine(path);
            Console.WriteLine(body);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(body, Encoding.UTF8);
            var request = new HttpRequestMessage(httpMethod, path) { Content = content };
            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<string> GetAsync(string url, Dictionary<string, string> bodyDic)
        {
            var path = url + "?";
            foreach (var body in bodyDic)
            {
                path += $"{body.Key}={body.Value}&";
            }

            Console.WriteLine($"请求路径:{path}");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(path);

            return await response.Content.ReadAsStringAsync();

        }
    }
}
