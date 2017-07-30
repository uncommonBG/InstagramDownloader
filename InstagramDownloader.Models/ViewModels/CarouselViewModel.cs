using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using InstagramDownloader.Models.Models;

namespace InstagramDownloader.Models.ViewModels
{
    public class CarouselViewModel
    {
        [Required(ErrorMessage = "Please provide images.")]
        public List<MediaFile> MediaFiles { get; set; }
    }
}
