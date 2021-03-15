using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerRequest :
        EndPointNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ICustomAuthStateProvider _auth;
        private readonly IBus _bus;
        
        public CreateWorkerRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);

            var client = _bus.CreateRequestClient<ICreateWorkerRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<ICreateWorkerSuccessResultContract, ICreateWorkerErrorResultContract>(input);
            if (response.Is(out Response<ICreateWorkerSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<ICreateWorkerErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}