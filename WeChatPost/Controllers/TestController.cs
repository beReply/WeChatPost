using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using WeChatPost.Logger;
using WeChatPost.WeChat.WeChatServices;
using WeChatPost.WeChat.Param;

namespace WeChatPost.Controllers
{
    public class TestController : Controller
    {
        private readonly IWeChatService _baseSupport;
        private readonly ILog4Provider _logger;

        public TestController(IWeChatService baseSupport, ILog4Provider logger)
        {
            _baseSupport = baseSupport;
            _logger = logger;
        }

        [HttpGet("/Test")]
        public void Test()
        {
            _logger.Info(typeof(ReceiveMessage), "测试");
        }

        [HttpPost("/GetAccessToken")]
        public async Task<string> GetAccessTokenAsync()
        {
            return await _baseSupport.GetAccessTokenAsync();
        }

        [HttpPost("/GetServerIp")]
        public async Task<string> GetServerIpAsync(string accessToken)
        {
            return await _baseSupport.GetServerIpAsync(accessToken);
        }

        [HttpGet("/Message")]
        public string WeChatMessage(string signature, string timestamp, string nonce, string echostr)
        {
            return echostr;
        }


        [HttpPost("/Message")]
        public async Task WeChatMessageAsync()
        {
            var body = Request.Body;
            using var streamReader = new StreamReader(body);
            var requestString = await streamReader.ReadToEndAsync();
        }
    }
}
