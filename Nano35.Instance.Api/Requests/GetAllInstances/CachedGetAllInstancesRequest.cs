using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class CachedGetAllInstancesRequest :
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ICashedData<
            IEnumerable<IInstanceViewModel>, 
            List<InstanceViewModel>> _cashedData;
        
        private readonly IPipelineNode<
            IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract> _nextNode;

        public CachedGetAllInstancesRequest(
            IDistributedCache distributedCache,
            IPipelineNode<
                IGetAllInstancesRequestContract,
                IGetAllInstancesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _cashedData = new CashedData<
                IEnumerable<IInstanceViewModel>, 
                List<InstanceViewModel>>("Instances", distributedCache);
        }

        public async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input)
        {
            try
            {
                IEnumerable<IInstanceViewModel> result = await _cashedData.TryGet();
                if (result != null)
                {
                    return new GetAllInstancesSuccessResultContract()
                    {
                        Data = result
                    };
                }
                else
                {
                    switch (await _nextNode.Ask(input))
                    {
                        case IGetAllInstancesSuccessResultContract success:
                            result = success.Data;
                            await _cashedData.Set(success.Data);
                            break;
                        case IGetAllInstancesErrorResultContract error:
                            return error;
                    }

                    return new GetAllInstancesSuccessResultContract()
                    {
                        Data = result
                    };
                }
            }
            catch
            {
                return new GetAllInstancesErrorResultContract() {Message = "Сервер не отвечает повторите попытку позже"};
            }
        }
    }
}