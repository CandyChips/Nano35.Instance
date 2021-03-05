using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Newtonsoft.Json;

namespace Nano35.Instance.Api
{
    public interface ICashedData<TData, TSaved>
    {
        Task<TSaved> TryGet();
        Task Set(TData data);
    }

    public class CashedData<TData, TSaved> :
        ICashedData<TData, TSaved> 
        where TData : class
        where TSaved : class
    {
        private readonly IDistributedCache _distributedCache;
        private string _key;

        public CashedData(string key, IDistributedCache distributedCache)
        {
            _key = key;
            _distributedCache = distributedCache;
        }

        public async Task<TSaved> TryGet()
        {
            var result = await _distributedCache.GetStringAsync(_key);
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<TSaved>(result);
        }

        public async Task Set(TData data)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };

            var result =  JsonConvert.SerializeObject(data);

            await _distributedCache.SetStringAsync(_key, result, cacheEntryOptions);
        }
    }
}