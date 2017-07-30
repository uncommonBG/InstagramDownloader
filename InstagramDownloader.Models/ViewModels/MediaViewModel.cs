using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InstagramDownloader.Models.Models;

namespace InstagramDownloader.Models.ViewModels
{
    public class MediaViewModel
    {
        [Required(ErrorMessage = "Please enter a URL.")]
        [Url(ErrorMessage      = "Please enter a valid URL.")]
        public string URL { get; set; }
    }
}
