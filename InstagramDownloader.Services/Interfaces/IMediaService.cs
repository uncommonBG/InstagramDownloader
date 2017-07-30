using InstagramDownloader.Models.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InstagramDownloader.Services.Interfaces
{
    public interface IMediaService
    {
        Task<Media> GetAsync(string url);
        Task<File> DownloadAsync(string url);
        Task<File> ZipCarouselAsync(IEnumerable<MediaFile> mediaFiles);
    }
}
