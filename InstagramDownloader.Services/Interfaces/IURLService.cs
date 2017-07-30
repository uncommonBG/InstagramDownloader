using InstagramDownloader.Models.ServiceResults;

namespace InstagramDownloader.Services.Interfaces
{
    public interface IURLService
    {
        ServiceResult<string> ExtractShortCodeFromURL(string url);
    }
}
