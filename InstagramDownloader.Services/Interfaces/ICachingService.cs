using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramDownloader.Services.Interfaces
{
    public interface ICachingService
    {
        TResult Get<TResult>(string key);
        void Set<T>(string key, T item);
    }
}
