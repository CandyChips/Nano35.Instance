using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class GetInstanceByIdResultViewModel :
        IGetInstanceByIdSuccessResultContract,
        IGetInstanceByIdNotFoundResultContract
    {
        public string Error { get; set; }
        public IInstanceViewModel Data { get; set; }
    }
    public class GetInstanceByIdQuery : 
        IGetInstanceByIdRequestContract, 
        IQueryRequest<GetInstanceByIdResultViewModel>
    {
        public Guid InstanceId { get; set; }
    }

    public class GetInstanceByIdHandler 
        : IRequestHandler<GetInstanceByIdQuery, GetInstanceByIdResultViewModel>
    {
        private readonly ILogger<GetInstanceByIdHandler> _logger;
        private readonly IBus _bus;
        public GetInstanceByIdHandler(
            IBus bus, 
            ILogger<GetInstanceByIdHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GetInstanceByIdResultViewModel> Handle(
            GetInstanceByIdQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetInstanceByIdResultViewModel();
            var client = _bus.CreateRequestClient<IGetInstanceByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetInstanceByIdSuccessResultContract, IGetInstanceByIdNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetInstanceByIdSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetInstanceByIdNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}