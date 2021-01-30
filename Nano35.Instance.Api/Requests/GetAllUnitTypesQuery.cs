using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class GetAllUnitTypesQuery : 
        IGetAllUnitTypesRequestContract, 
        IQueryRequest<IGetAllUnitTypesResultContract>
    {
        public class GetAllUnitTypesHandler 
            : IRequestHandler<GetAllUnitTypesQuery, IGetAllUnitTypesResultContract>
        {
            private readonly IBus _bus;
            public GetAllUnitTypesHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllUnitTypesResultContract> Handle(
                GetAllUnitTypesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllUnitTypesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllUnitTypesSuccessResultContract, IGetAllUnitTypesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllUnitTypesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllUnitTypesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }

    }
}