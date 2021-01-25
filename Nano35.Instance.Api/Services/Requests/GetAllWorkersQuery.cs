using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class GetAllWorkersQuery : 
        IGetAllWorkersRequestContract, 
        IQueryRequest<IGetAllWorkersResultContract>
    {
        public Guid InstanceTypeId { get; set; }

        public class GetAllWorkersHandler 
            : IRequestHandler<GetAllWorkersQuery, IGetAllWorkersResultContract>
        {
            private readonly IBus _bus;
            public GetAllWorkersHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllWorkersResultContract> Handle(
                GetAllWorkersQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllWorkersRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetAllWorkersSuccessResultContract, IGetAllWorkersErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllWorkersSuccessResultContract> responseA))
                {
                    return responseA.Message;
                }
                else if (response.Is(out Response<IGetAllWorkersErrorResultContract> responseB))
                {
                    throw new Exception();
                }
                throw new InvalidOperationException();
            }
        }
    }
}