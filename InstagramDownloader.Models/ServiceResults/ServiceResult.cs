using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramDownloader.Models.ServiceResults
{
    public class ServiceResult<TResult>
    {
        public bool Success { get; set; }

        public TResult Result { get; set; }
    }
}
