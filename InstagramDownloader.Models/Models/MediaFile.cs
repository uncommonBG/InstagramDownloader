using System;
using System.Collections.Generic;
using System.Text;
using InstagramDownloader.Models.Enums;

namespace InstagramDownloader.Models.Models
{
    public class MediaFile
    {
        public string LowResolutionURL { get; set; }

        public string StandartResolutionURL { get; set; }

        public string ThumbnailURL { get; set; }

        public MediaType Type { get; set; }
    }
}
