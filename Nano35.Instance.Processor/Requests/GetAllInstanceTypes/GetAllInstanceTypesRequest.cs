using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesRequest :
        IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllInstanceTypesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllInstanceTypesSuccessResultContract : 
            IGetAllInstanceTypesSuccessResultContract
        {
            public IEnumerable<IInstanceTypeViewModel> Data { get; set; }
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.InstanceTypes
                .MapAllToAsync<IInstanceTypeViewModel>();
            return new GetAllInstanceTypesSuccessResultContract() {Data = result};
        }
    }
}