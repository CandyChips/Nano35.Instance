using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.Requests
{
    public class GetAllInstanceTypesQuery : 
        IGetAllInstanceTypesRequestContract, 
        IQueryRequest<IGetAllInstanceTypesSuccessResultContract>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }

        public class GetAllInstanceTypesResultContract : 
            IGetAllInstanceTypesSuccessResultContract
        {
            public IEnumerable<IInstanceTypeViewModel> Data { get; set; }
        }

        public class GetAllInstancesHandler 
            : IRequestHandler<GetAllInstanceTypesQuery, 
                IGetAllInstanceTypesSuccessResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllInstancesHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllInstanceTypesSuccessResultContract> Handle(
                GetAllInstanceTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await this._context.InstanceTypes
                    .MapAllToAsync<IInstanceTypeViewModel>();
                return new GetAllInstanceTypesResultContract() {Data = result};
            }
        }
    }
}