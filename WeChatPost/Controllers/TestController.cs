using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using WeChatPost.DataProvider.EFCore.Entities;
using WeChatPost.Logger;
using WeChatPost.WeChat.Extensions;
using WeChatPost.WeChat.WeChatServices;
using WeChatPost.WeChat.Param;

namespace WeChatPost.Controllers
{
    public class TestController : Controller
    {
        private readonly IWeChatService _weChatService;
        private readonly ILog4Provider _logger;
        private readonly IMapper _mapper;

        public TestController(IWeChatService weChatService, ILog4Provider logger, IMapper mapper)
        {
            _weChatService = weChatService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("/Test")]
        public ReceiveMessageEntity Test()
        {
            var rr = new ReceiveMessage
            {
                ToUserName = "测试",
                FromUserName = "测试",
                CreateTime = "测试",
                MsgType = "测试",
                MsgId = "测试",
                Content = "测试",
                MediaId = "测试",
                PicUrl = "测试",
                Format = "测试"
            };

            var res = _mapper.Map<ReceiveMessageEntity>(rr);

            return res;
        }

        [HttpPost("/GetAccessToken")]
        public async Task<string> GetAccessTokenAsync()
        {
            return await _weChatService.GetAccessTokenAsync();
        }

        [HttpPost("/GetServerIp")]
        public async Task<string> GetServerIpAsync(string accessToken)
        {
            return await _weChatService.GetServerIpAsync(accessToken);
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

            _logger.Info(typeof(ReceiveMessage), $"微信消息:{requestString}");

            _weChatService.ReceiveMessageProcessing(requestString);
        }

    }
}
