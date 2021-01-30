using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests
{
    public class GetAllRolesQuery : 
        IGetAllRolesRequestContract, 
        IQueryRequest<IGetAllRegionsResultContract>
    {
        private class GetAllRegionsSuccessResultContract : 
            IGetAllRegionsResultContract
        {
            public IEnumerable<IRegionViewModel> Data { get; set; }
        }

        public class GetAllRolesHandler 
            : IRequestHandler<GetAllRolesQuery, IGetAllRegionsResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllRolesHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllRegionsResultContract> Handle(
                GetAllRolesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await this._context.Regions
                    .MapAllToAsync<IRegionViewModel>();
                return new GetAllRegionsSuccessResultContract() {Data = result};
            }
        }
    }
}