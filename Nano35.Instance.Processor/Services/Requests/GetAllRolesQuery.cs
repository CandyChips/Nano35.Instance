using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.Requests
{
    public class GetAllRolesQuery : 
        IGetAllRolesRequestContract, 
        IQueryRequest<IGetAllRegionsResultContract>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }

        public class GetAllRegionsResultContract : 
            IGetAllRegionsResultContract
        {
            public IEnumerable<IRegionViewModel> Data { get; set; }
        }

        public class GetAllRolesHandler 
            : IRequestHandler<GetAllRolesQuery, 
                IGetAllRegionsResultContract>
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
                return new GetAllRegionsResultContract() {Data = result};
            }
        }
    }
}