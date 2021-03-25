using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Newtonsoft.Json;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class CachedGetAllClientStatesRequest :
        IPipelineNode<
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract>
    {
        private readonly IDistributedCache _distributedCache;
        
        private readonly IPipelineNode<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract> _nextNode;

        public CachedGetAllClientStatesRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllClientStatesRequestContract, 
                IGetAllClientStatesResultContract> nextNode)
        {
            _distributedCache = distributedCache;
            _nextNode = nextNode;
        }

        public async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input)
        {
            try
            {
                IEnumerable<IClientStateViewModel> result = null;
                string serializedResult;
                
                var encodedResult = await _distributedCache.GetAsync("ClientStates");
                
                if (encodedResult != null)
                {
                    serializedResult = Encoding.UTF8.GetString(encodedResult);
                    result = JsonConvert.DeserializeObject<List<IClientStateViewModel>>(serializedResult);
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllClientStatesSuccessResultContract success:
                            result = success.Data;
                            var cacheEntryOptions = new DistributedCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                                SlidingExpiration = TimeSpan.FromSeconds(30)
                            };
                            await _distributedCache.SetAsync("ClientStates", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(success.Data)), cacheEntryOptions);
                            break;
                        case IGetAllClientStatesErrorResultContract error:
                            return error;
                    }
                }

                return new GetAllClientStatesSuccessResultContract()
                {
                    Data = result
                };
            }
            catch
            {
                return new GetAllClientStatesErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}

