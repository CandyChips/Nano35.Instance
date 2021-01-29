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
    public class GetAllClientStatesQuery : 
        IGetAllClientStatesRequestContract, 
        IQueryRequest<IGetAllClientStatesResultContract>
    {
        
        public class GetAllClientStatesHandler 
            : IRequestHandler<GetAllClientStatesQuery, IGetAllClientStatesResultContract>
        {
            private readonly IBus _bus;
            public GetAllClientStatesHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllClientStatesResultContract> Handle(
                GetAllClientStatesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllClientStatesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllClientStatesSuccessResultContract, IGetAllClientStatesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllClientStatesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllClientStatesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}