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
    public class GetInstanceByIdQuery : 
        IGetInstanceByIdRequestContract,
        IQueryRequest<IGetInstanceByIdResultContract>
    {
        public Guid InstanceId { get; set; }

        public GetInstanceByIdQuery(IGetInstanceByIdRequestContract request)
        {
            InstanceId = request.InstanceId;
        }

        public class GetInstanceByIdResultContract : IGetInstanceByIdSuccessResultContract
        {
            public IInstanceViewModel Data { get; set; }
        }

        public class GetInstanceByIdHandler 
            : IRequestHandler<GetInstanceByIdQuery, IGetInstanceByIdResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetInstanceByIdHandler(
                ApplicationContext context)
            {
                _context = context;
            }

            public async Task<IGetInstanceByIdResultContract> Handle(
                GetInstanceByIdQuery message,
                CancellationToken cancellationToken)
            {
                var result = this._context.Instances
                        .FirstOrDefault(f => f.Id == message.InstanceId)
                        .MapTo<IInstanceViewModel>();
                return new GetInstanceByIdResultContract() {Data = result};
            }
        }
    }
}