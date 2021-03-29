using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class GetAllWorkersUseCase :
        EndPointNodeBase<
            IGetAllWorkersRequestContract, 
            IGetAllWorkersResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;

        public GetAllWorkersUseCase(
            ApplicationContext context, 
            IBus bus)
        {
            _context = context;
            _bus = bus;
        }
        
        private class GetAllWorkersSuccessResultContract : 
            IGetAllWorkersSuccessResultContract
        {
            public IEnumerable<IWorkerViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }
        
        private class GetUserResult : IGetUserByIdRequestContract
        {
            public Guid UserId { get; set; }
        }

        public override async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Workers
                .Where(c => c.InstanceId == input.InstanceId)
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
            return new GetAllWorkersSuccessResultContract() {Data = result};
        }
    }
}