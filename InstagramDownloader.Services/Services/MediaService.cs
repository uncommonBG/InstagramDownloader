using InstagramDownloader.Services.Interfaces;
using InstagramDownloader.Models.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using InstagramDownloader.Common;
using Microsoft.Extensions.Options;
using System.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using InstagramDownloader.Models.Enums;
using System.Linq;

namespace InstagramDownloader.Services.Services
{
    public class MediaService : ServiceBase, IMediaService
    {
        public MediaService(IOptions<AppSettings> settings) : base(settings) { }

        static HttpClient client = new HttpClient();

        public async Task<Media> GetAsync(string shortCode)
        {
            Media media = null;

            using (HttpResponseMessage response = await client.GetAsync($"{MediaBaseUrl}/shortcode/{shortCode}?access_token={AppKey}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Note: reading the response as a stream instead of loading the whole json into memory for better performance(less allocations this way)
                    using (Stream stream    = await response.Content.ReadAsStreamAsync()) 
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonReader   = new JsonTextReader(streamReader))
                    {
                        var serializer     = new JsonSerializer();
                        var responseResult = serializer.Deserialize<dynamic>(jsonReader); // can deserialize to concrete types, but It was too much not needed boilerplate code.
                        media = new Media(responseResult);
                    }
                }

                return media;
            }
        }

        public async Task<Models.Models.File> DownloadAsync(string mediaDownloadUrl)
        {
            using (Stream fileStream = await client.GetStreamAsync(mediaDownloadUrl))
            using (var memoryStream  = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);

                return new Models.Models.File
                {
                    Name    = "Instagram-640x800",
                    Content = memoryStream.ToArray()
                };
            }
        }

        public async Task<Models.Models.File> ZipCarouselAsync(IEnumerable<MediaFile> mediaFiles)
        {
            // Note: I'm so glad I found a way to do eveything in memory and by using the cache I don't have to save anything on the server. 
            // This way I don't have to maintain the files myself.

            var random = new Random();
            var file   = new Models.Models.File();

            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (MediaFile mediaFile in mediaFiles)
                    {
                        ZipArchiveEntry archiveEntry;
                        if (mediaFile.Type == MediaType.Image)
                        {
                            archiveEntry = archive.CreateEntry($"Instagram{random.Next()}.jpg", CompressionLevel.Optimal);
                        }
                        else
                        {
                            archiveEntry = archive.CreateEntry($"Instagram{random.Next()}.mp4", CompressionLevel.Optimal);
                        }
                        using (Stream entryStream   = archiveEntry.Open())
                        using (Stream contentStream = await client.GetStreamAsync(mediaFile.StandartResolutionURL))
                        {
                            await contentStream.CopyToAsync(entryStream);
                        }
                    }
                }

                file.Content = archiveStream.ToArray();
                file.Name    = $"Carousel{random.Next()}";
            }
           
            return file;
        }
    }
}
