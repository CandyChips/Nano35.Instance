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
    public class GetAllRegionsResultViewModel :
        IGetAllRegionsResultContract,
        IGetAllRegionsNotFoundResultContract
    {
        public string Error { get; set; }
        public IEnumerable<IRegionViewModel> Data { get; set; }
    }
    public class GetAllRegionsQuery : 
        IGetAllRegionsRequestContract, 
        IQueryRequest<GetAllRegionsResultViewModel>
    {
        public Guid InstanceId { get; set; }
    }

    public class GetAllRegionsHandler 
        : IRequestHandler<GetAllRegionsQuery, GetAllRegionsResultViewModel>
    {
        private readonly ILogger<GetAllRegionsHandler> _logger;
        private readonly IBus _bus;
        public GetAllRegionsHandler(
            IBus bus, 
            ILogger<GetAllRegionsHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GetAllRegionsResultViewModel> Handle(
            GetAllRegionsQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetAllRegionsResultViewModel();
            var client = _bus.CreateRequestClient<IGetAllRegionsRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllRegionsResultContract, IGetAllRegionsNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllRegionsResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetAllRegionsNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}