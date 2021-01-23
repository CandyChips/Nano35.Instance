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
    public class GetAllInstancesResultViewModel :
        IGetAllInstancesResultContract,
        IGetAllInstancesNotFoundResultContract
    {
        public string Error { get; set; }
        public IEnumerable<IInstanceViewModel> Data { get; set; }
    }
    public class GetAllInstancesQuery : 
        IGetAllInstancesRequestContract, 
        IQueryRequest<GetAllInstancesResultViewModel>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }
    }

    public class GetAllInstancesHandler 
        : IRequestHandler<GetAllInstancesQuery, GetAllInstancesResultViewModel>
    {
        private readonly ILogger<GetAllInstancesHandler> _logger;
        private readonly IBus _bus;
        public GetAllInstancesHandler(
            IBus bus, 
            ILogger<GetAllInstancesHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GetAllInstancesResultViewModel> Handle(
            GetAllInstancesQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetAllInstancesResultViewModel();
            var client = _bus.CreateRequestClient<IGetAllInstancesRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllInstancesResultContract, IGetAllInstancesNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllInstancesResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetAllInstancesNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}