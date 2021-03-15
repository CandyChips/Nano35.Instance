using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class CreatePaymentOfComingRequest :
        EndPointNodeBase<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreatePaymentOfComingRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreatePaymentOfComingRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<ICreatePaymentOfComingSuccessResultContract, ICreatePaymentOfComingErrorResultContract>(input);
            if (response.Is(out Response<ICreatePaymentOfComingSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<ICreatePaymentOfComingErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}