using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests
{
    public class GetAllRegionsQuery : 
        IGetAllRegionsRequestContract, 
        IQueryRequest<IGetAllRegionsResultContract>
    {
        private class GetAllRegionsSuccessResultContract : 
            IGetAllRegionsSuccessResultContract
        {
            public IEnumerable<IRegionViewModel> Data { get; set; }
        }

        public class GetAllRegionsHandler 
            : IRequestHandler<GetAllRegionsQuery, IGetAllRegionsResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllRegionsHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllRegionsResultContract> Handle(
                GetAllRegionsQuery message,
                CancellationToken cancellationToken)
            {
                var result = await this._context.Regions
                    .MapAllToAsync<IRegionViewModel>();
                return new GetAllRegionsSuccessResultContract() {Data = result};
            }
        }
    }
}