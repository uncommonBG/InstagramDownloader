using InstagramDownloader.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InstagramDownloader.Models.Models;
using InstagramDownloader.Models.ViewModels;
using System;
using InstagramDownloader.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using InstagramDownloader.Models.ServiceResults;

namespace InstagramDownloader.Controllers
{
    public class MediaController : BaseController
    {
        private IMediaService MediaService { get; }
        private IURLService URLService { get; }
        private ICachingService CachingService { get; }

        public MediaController(IMediaService mediaService, IURLService urlService, IHostingEnvironment hostingEnvironment, ICachingService cache) : base(hostingEnvironment)
        {
            MediaService   = mediaService;
            URLService     = urlService;
            CachingService = cache;
        }

        [HttpPost, ActionName("Index"), ValidateModel]
        public async Task<IActionResult> MediaDetails(MediaViewModel model)
        {
            ServiceResult<string> urlExtractResult = URLService.ExtractShortCodeFromURL(model.URL);
            Media media = null;

            if (urlExtractResult.Success)
            {
                media = await MediaService.GetAsync(urlExtractResult.Result);
            }
            else
            {
                AddModelError("InvalidURL", "The URL given is invalid.");
                return View();
            }

            if (media == null)
            {
                AddModelError("ImageNotFound", "Unfortunately, we weren't able to find anything on that link. :(");
                return View();
            }

            return View("Details", media); // no need for ViewModel since taking only the data needed from the API.
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Download(string url, MediaType type)
        {
            File media = await MediaService.DownloadAsync(url);

            if (type == MediaType.Image)
            {
                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{media.Name}.jpg\"");
                return File(media.Content, "image/jpeg");
            }

            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{media.Name}.mp4\"");
            return File(media.Content, "video/mp4");
        }

        [HttpPost, ValidateModel]
        public async Task<IActionResult> SaveCarousel([FromBody]CarouselViewModel model)
        {
            File zip = await MediaService.ZipCarouselAsync(model.MediaFiles);
            CachingService.Set<File>(zip.Name, zip);
            return Ok(zip);
        }

        [HttpGet]
        public IActionResult DownloadCarousel(string name)
        {
            File zip = CachingService.Get<File>(name);
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{zip.Name}.zip\"");
            return File(zip.Content, "application/zip");
        }
    }
}
