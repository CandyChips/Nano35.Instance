using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class GetAllInstanceTypesQuery : 
        IGetAllInstanceTypesRequestContract, 
        IQueryRequest<IGetAllInstanceTypesResultContract>
    {
        public class GetAllInstanceTypesHandler 
            : IRequestHandler<GetAllInstanceTypesQuery, IGetAllInstanceTypesResultContract>
        {
            private readonly ILogger<GetAllInstanceTypesHandler> _logger;
            private readonly IBus _bus;
            public GetAllInstanceTypesHandler(
                IBus bus, 
                ILogger<GetAllInstanceTypesHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetAllInstanceTypesResultContract> Handle(
                GetAllInstanceTypesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllInstanceTypesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllInstanceTypesSuccessResultContract, IGetAllInstanceTypesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllInstanceTypesSuccessResultContract> successResponse))
                    return successResponse.Message;
            
                if (response.Is(out Response<IGetAllInstanceTypesErrorResultContract> errorResponse))
                    return errorResponse.Message;
            
                throw new InvalidOperationException();
            }
        }
    }
}