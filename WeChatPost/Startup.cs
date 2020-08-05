using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeChatPost.Configuration;
using WeChatPost.DataProvider.EFCore;
using WeChatPost.Logger;
using WeChatPost.WeChat;
using WeChatPost.WeChat.WeChatServices;

namespace WeChatPost
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static ILoggerRepository LoggerRepository { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient();

            services.AddAutoMapper(typeof(Startup));

            #region sql server connect

            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<WeChatParam>(new WeChatParam
            {
                AppId = _configuration["WeChat:AppId"],
                AppSecret = _configuration["WeChat:AppSecret"],
                Url = _configuration["WeChat:Url"]
            });

            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IWeChatService, WeChatService>();
            services.AddSingleton<ILog4Provider, Log4Provider>();
            //services.AddTransient<ILog4Provider, Log4Provider>();

            LoggerRepository = LogManager.CreateRepository("WeChatPost");
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
