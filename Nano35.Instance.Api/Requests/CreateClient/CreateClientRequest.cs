using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientRequest :
        EndPointNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreateClientRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            
            var client = _bus.CreateRequestClient<ICreateClientRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<
                    ICreateClientSuccessResultContract, 
                    ICreateClientErrorResultContract>(input);
            if (response.Is(out Response<ICreateClientSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<ICreateClientErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}