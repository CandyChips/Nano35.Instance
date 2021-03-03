using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsRequest :
        IPipelineNode<
            IGetAllRegionsRequestContract,
            IGetAllRegionsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRegionsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllRegionsSuccessResultContract : 
            IGetAllRegionsSuccessResultContract
        {
            public IEnumerable<IRegionViewModel> Data { get; set; }
        }

        public async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await this._context.Regions
                .MapAllToAsync<IRegionViewModel>();
            return new GetAllRegionsSuccessResultContract() {Data = result};
        }
    }
}