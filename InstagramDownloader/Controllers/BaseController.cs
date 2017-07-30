using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InstagramDownloader.Controllers
{
    public class BaseController : Controller
    {
        protected IHostingEnvironment HostingEnvironment { get; }

        public BaseController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        protected void AddModelError(string key, string errorMessage)
        {
            ModelState.AddModelError(key, errorMessage);
        }
    }
}

