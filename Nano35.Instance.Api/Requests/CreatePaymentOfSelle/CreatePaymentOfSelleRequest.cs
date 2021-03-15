using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleRequest :
        EndPointNodeBase<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreatePaymentOfSelleRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreatePaymentOfSelleRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<ICreatePaymentOfSelleSuccessResultContract, ICreatePaymentOfSelleErrorResultContract>(input);
            if (response.Is(out Response<ICreatePaymentOfSelleSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<ICreatePaymentOfSelleErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}