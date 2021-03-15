using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public class CreateUnitRequest :
        EndPointNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreateUnitRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
        {
            input.CreatorId = _auth.CurrentUserId;
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);

            var client = _bus.CreateRequestClient<ICreateUnitRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<ICreateUnitSuccessResultContract, ICreateUnitErrorResultContract>(input);
            if (response.Is(out Response<ICreateUnitSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<ICreateUnitErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}