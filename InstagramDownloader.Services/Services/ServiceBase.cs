using InstagramDownloader.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InstagramDownloader.Services.Services
{
    public class ServiceBase
    {
        private AppSettings Settings { get; }

        public ServiceBase(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;
        }

        protected string AppKey
        {
            get => Settings.AppKey;
        }

        protected string MediaBaseUrl
        {
            get => "https://api.instagram.com/v1/media"; // So C# 7, much wow.
        }
    }
}
