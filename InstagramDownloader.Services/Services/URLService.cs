using System;
using InstagramDownloader.Models.ServiceResults;
using InstagramDownloader.Services.Interfaces;

namespace InstagramDownloader.Services.Services
{
    public class URLService : IURLService
    {
        public ServiceResult<string> ExtractShortCodeFromURL(string url)
        {
            var extractResult = new ServiceResult<string>();

            try
            {
                string segment        = new UriBuilder(url).Uri.Segments[2];
                extractResult.Success = true;
                extractResult.Result  = segment.Substring(0, segment.Length - 1);
            }
            catch (Exception)
            {
                extractResult.Success = false;
                extractResult.Result  = String.Empty;
            }

            return extractResult;
        }
    }
}
