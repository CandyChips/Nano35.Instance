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
    public class GetAllWorkersQuery : 
        IGetAllWorkersRequestContract,
        IQueryRequest<IGetAllWorkersResultContract>
    {
        public Guid InstanceTypeId { get; set; }

        public class GetAllWorkersResultContract : IGetAllWorkersSuccessResultContract
        {
            public IEnumerable<IWorkerViewModel> Data { get; set; }
        }

        public class GetAllWorkersHandler 
            : IRequestHandler<GetAllWorkersQuery, IGetAllWorkersResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllWorkersHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetAllWorkersResultContract> Handle(
                GetAllWorkersQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (this._context.Workers
                    .MapAllToAsync<IWorkerViewModel>());
                return new GetAllWorkersResultContract() {Data = result};
            }
        }
    }
}