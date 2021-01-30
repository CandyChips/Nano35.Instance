using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class GetAllUnitsQuery : 
        IGetAllUnitsRequestContract, 
        IQueryRequest<IGetAllUnitsResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid UnitTypeId { get; set; }
        
        public class GetAllUnitsHandler 
            : IRequestHandler<GetAllUnitsQuery, IGetAllUnitsResultContract>
        {
            private readonly IBus _bus;
            public GetAllUnitsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllUnitsResultContract> Handle(
                GetAllUnitsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllUnitsRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllUnitsSuccessResultContract, IGetAllUnitsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllUnitsSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllUnitsErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }

    }
}