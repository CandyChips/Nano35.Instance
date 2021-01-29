using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.Requests
{
    public class GetAllWorkersQuery : 
        IGetAllWorkersRequestContract,
        IQueryRequest<IGetAllWorkersResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid WorkersRoleId { get; set; }

        public class GetAllWorkersResultContract : IGetAllWorkersSuccessResultContract
        {
            public IEnumerable<IWorkerViewModel> Data { get; set; }
        }

        public class GetAllWorkersHandler 
            : IRequestHandler<GetAllWorkersQuery, IGetAllWorkersResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            public GetAllWorkersHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }

            private class GetUserResult : IGetUserByIdRequestContract
            {
                public Guid UserId { get; set; }
            }
            
            public async Task<IGetAllWorkersResultContract> Handle(
                GetAllWorkersQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (this._context.Workers
                    .MapAllToAsync<IWorkerViewModel>());
                foreach (var item in result)
                {
                    var client = _bus.CreateRequestClient<IGetUserByIdRequestContract>(TimeSpan.FromSeconds(10));
                    var response = await client
                        .GetResponse<IGetUserByIdSuccessResultContract, IGetUserByIdErrorResultContract>( new GetUserResult(){UserId = item.Id}, cancellationToken);

                    if (response.Is(out Response<IGetUserByIdSuccessResultContract> successResponse))
                    {
                        var tmp = successResponse.Message;
                        if (tmp.Data != null)
                        {
                            item.Name = tmp.Data.Name;
                            item.Email = tmp.Data.Email;
                            item.Phone = tmp.Data.Phone;
                        }
                    }
            
                    if (response.Is(out Response<IGetUserByIdErrorResultContract> errorResponse))
                    {
                        throw new Exception();
                    }
                }
                return new GetAllWorkersResultContract() {Data = result};
            }
        }

    }
}