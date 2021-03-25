using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Newtonsoft.Json;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class CachedGetAllRegionsRequest :
        IPipelineNode<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract>
    {
        private readonly IDistributedCache _distributedCache;
        
        private readonly IPipelineNode<
            IGetAllRegionsRequestContract,
            IGetAllRegionsResultContract> _nextNode;

        public CachedGetAllRegionsRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllRegionsRequestContract, 
                IGetAllRegionsResultContract> nextNode)
        {
            _distributedCache = distributedCache;
            _nextNode = nextNode;
        }

        public async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input)
        {
            try
            {
                IEnumerable<IRegionViewModel> result = null;
                string serializedResult;
                
                var encodedResult = await _distributedCache.GetAsync("Regions");
                
                if (encodedResult != null)
                {
                    serializedResult = Encoding.UTF8.GetString(encodedResult);
                    result = JsonConvert.DeserializeObject<List<IRegionViewModel>>(serializedResult);
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllRegionsSuccessResultContract success:
                            result = success.Data;
                            var cacheEntryOptions = new DistributedCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                                SlidingExpiration = TimeSpan.FromSeconds(30)
                            };
                            await _distributedCache.SetAsync("Regions", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(success.Data)), cacheEntryOptions);
                            break;
                        case IGetAllRegionsErrorResultContract error:
                            return error;
                    }
                }

                return new GetAllRegionsSuccessResultContract()
                {
                    Data = result
                };
            }
            catch
            {
                return new GetAllRegionsErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}

