﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Initializers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Newtonsoft.Json;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class CachedGetAllInstanceTypesRequest :
        IPipelineNode<
            IGetAllInstanceTypesRequestContract, 
            IGetAllInstanceTypesResultContract>
    {
        private readonly IDistributedCache _distributedCache;
        
        private readonly IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract> _nextNode;

        public CachedGetAllInstanceTypesRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllInstanceTypesRequestContract, 
                IGetAllInstanceTypesResultContract> nextNode)
        {
            _distributedCache = distributedCache;
            _nextNode = nextNode;
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input)
        {
            try
            {
                IEnumerable<IInstanceTypeViewModel> result = null;
                string serializedResult;
                
                var encodedResult = await _distributedCache.GetAsync("InstanceTypes");
                
                if (encodedResult != null)
                {
                    serializedResult = Encoding.UTF8.GetString(encodedResult);
                    result = JsonConvert.DeserializeObject<List<InstanceTypeViewModel>>(serializedResult);
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllInstanceTypesSuccessResultContract success:
                            result = success.Data;
                            var cacheEntryOptions = new DistributedCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                                SlidingExpiration = TimeSpan.FromSeconds(30)
                            };
                            await _distributedCache.SetAsync("InstanceTypes", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(success.Data)), cacheEntryOptions);
                            break;
                        case IGetAllInstanceTypesErrorResultContract error:
                            return error;
                    }
                }

                return new GetAllInstanceTypesSuccessResultContract()
                {
                    Data = result
                };
            }
            catch
            {
                return new GetAllInstanceTypesErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}

