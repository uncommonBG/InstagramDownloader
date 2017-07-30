using System.Collections.Generic;
using InstagramDownloader.Models.Enums;

namespace InstagramDownloader.Models.Models
{
    public class Media
    {
        public Media(dynamic json)
        {
            MapJsonData(json);
        }

        private void MapJsonData(dynamic json)
        {
            Caption = json.data.caption.text;
            Likes   = json.data.likes.count;
            Type    = AssignMediaType(json);
            if(Type == MediaType.Image)
            {
                MediaFile = new MediaFile
                {
                    ThumbnailURL          = json.data.images.thumbnail.url,
                    StandartResolutionURL = json.data.images.standard_resolution.url,
                    LowResolutionURL      = json.data.images.low_resolution.url,
                    Type                  = Type
                };
            }
            else if(Type == MediaType.Carousel)
            {
                Carousel = new List<MediaFile>();

                foreach (var carouselMedia in json.data.carousel_media)
                {
                    if(carouselMedia.type == "image")
                    {
                        Carousel.Add(new MediaFile
                        {
                            ThumbnailURL          = carouselMedia.images.thumbnail.url,
                            StandartResolutionURL = carouselMedia.images.standard_resolution.url,
                            LowResolutionURL      = carouselMedia.images.low_resolution.url,
                            Type                  = MediaType.Image
                        });
                    }
                    else if(carouselMedia.type == "video")
                    {
                        Carousel.Add(new MediaFile
                        {
                            StandartResolutionURL = carouselMedia.videos.standard_resolution.url,
                            LowResolutionURL      = carouselMedia.videos.low_resolution.url,
                            Type                  = MediaType.Video
                        });
                    }
                }
            }
            else
            {
                MediaFile = new MediaFile
                {
                    StandartResolutionURL = json.data.videos.standard_resolution.url,
                    LowResolutionURL      = json.data.videos.low_resolution.url
                };
            }
        }

        private MediaType AssignMediaType(dynamic json)
        {
            MediaType mediaType = MediaType.Image;

            if (json.data.type == "video")
            {
                mediaType = MediaType.Video;
            }
            else if (json.data.type == "carousel")
            {
                mediaType = MediaType.Carousel;
            }

            return mediaType;
        }

        public string Caption { get; set; }

        public int Likes { get; set; }

        public MediaFile MediaFile { get; set; }

        public List<MediaFile> Carousel { get; set; }

        public MediaType Type { get; set; }
    }
}
