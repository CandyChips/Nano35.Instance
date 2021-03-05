using System;
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
        private readonly ICashedData<
            IEnumerable<IInstanceTypeViewModel>, 
            List<InstanceTypeViewModel>> _cashedData;
        
        private readonly IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract> _nextNode;

        public CachedGetAllInstanceTypesRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllInstanceTypesRequestContract, 
                IGetAllInstanceTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _cashedData = new CashedData<
                IEnumerable<IInstanceTypeViewModel>, 
                List<InstanceTypeViewModel>>("InstanceTypes", distributedCache);
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input)
        {
            try
            {
                IEnumerable<IInstanceTypeViewModel> result = await _cashedData.TryGet();
                if (result != null)
                {
                    return new GetAllInstanceTypesSuccessResultContract()
                    {
                        Data = result
                    };
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllInstanceTypesSuccessResultContract success:
                            result = success.Data;
                            await _cashedData.Set(success.Data);
                            break;
                        case IGetAllInstanceTypesErrorResultContract error:
                            return error;
                    }

                    return new GetAllInstanceTypesSuccessResultContract()
                    {
                        Data = result
                    };
                }
            }
            catch
            {
                return new GetAllInstanceTypesErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}

