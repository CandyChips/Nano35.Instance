using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class GetAllInstanceTypesResultViewModel :
        IGetAllInstanceTypesResultContract,
        IGetAllInstanceTypesNotFoundResultContract
    {
        public string Error { get; set; }
        public IEnumerable<IInstanceTypeViewModel> Data { get; set; }
    }
    public class GetAllInstanceTypesQuery : 
        IGetAllInstanceTypesRequestContract, 
        IQueryRequest<GetAllInstanceTypesResultViewModel>
    {
        public Guid InstanceId { get; set; }
    }

    public class GetAllInstanceTypesHandler 
        : IRequestHandler<GetAllInstanceTypesQuery, GetAllInstanceTypesResultViewModel>
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

        public async Task<GetAllInstanceTypesResultViewModel> Handle(
            GetAllInstanceTypesQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetAllInstanceTypesResultViewModel();
            var client = _bus.CreateRequestClient<IGetAllInstanceTypesRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllInstanceTypesResultContract, IGetAllInstanceTypesNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllInstanceTypesResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetAllInstanceTypesNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}