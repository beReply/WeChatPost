using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeChatPost.Configuration;
using WeChatPost.Logger;
using WeChatPost.WeChat.Extensions;
using WeChatPost.WeChat.Param;

namespace WeChatPost.WeChat.WeChatServices
{
    public class WeChatService : IWeChatService
    {
        private readonly WeChatParam _weChatParam;
        private readonly IRequestService _requestService;
        private readonly ILog4Provider _logger;

        public WeChatService(WeChatParam weChatParam, IRequestService requestService, ILog4Provider logger)
        {
            _weChatParam = weChatParam;
            _requestService = requestService;
            _logger = logger;
        }

        #region 基础支持

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            var path = _weChatParam.Url + "/token";

            var body = new Dictionary<string, string>
            {
                {"grant_type", "client_credential"},
                {"appid", _weChatParam.AppId},
                {"secret", _weChatParam.AppSecret}
            };

            return await _requestService.GetAsync(path, body);
        }

        /// <summary>
        /// 获取微信服务器IP地址
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<string> GetServerIpAsync(string accessToken)
        {
            var path = _weChatParam.Url + "/getcallbackip";
            var body = new Dictionary<string, string>
            {
                {"access_token", accessToken}
            };

            return await _requestService.GetAsync(path, body);
        }

        #endregion


        #region 接收消息

        public void ReceiveMessageProcessing(string xmlMessage)
        {
            var receiveMessage = xmlMessage.XmlToObj<ReceiveMessage>();
            _logger.Info(typeof(ReceiveMessage), $"{JsonConvert.SerializeObject(receiveMessage)}");
        }

        #endregion

    }
}
