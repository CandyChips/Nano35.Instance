using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Newtonsoft.Json;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class CachedGetAllClientTypesRequest :
        IPipelineNode<
            IGetAllClientTypesRequestContract, 
            IGetAllClientTypesResultContract>
    {
        private readonly IDistributedCache _distributedCache;
        
        private readonly IPipelineNode<
            IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract> _nextNode;

        public CachedGetAllClientTypesRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllClientTypesRequestContract, 
                IGetAllClientTypesResultContract> nextNode)
        {
            _distributedCache = distributedCache;
            _nextNode = nextNode;
        }

        public async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input)
        {
            try
            {
                IEnumerable<IClientTypeViewModel> result = null;
                string serializedResult;
                
                var encodedResult = await _distributedCache.GetAsync("ClientTypes");
                
                if (encodedResult != null)
                {
                    serializedResult = Encoding.UTF8.GetString(encodedResult);
                    result = JsonConvert.DeserializeObject<List<ClientTypeViewModel>>(serializedResult);
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllClientTypesSuccessResultContract success:
                            result = success.Data;
                            var cacheEntryOptions = new DistributedCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                                SlidingExpiration = TimeSpan.FromSeconds(30)
                            };
                            await _distributedCache.SetAsync("ClientTypes", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(success.Data)), cacheEntryOptions);
                            break;
                        case IGetAllClientTypesErrorResultContract error:
                            return error;
                    }
                }

                return new GetAllClientTypesSuccessResultContract()
                {
                    Data = result
                };
            }
            catch
            {
                return new GetAllClientTypesErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}

