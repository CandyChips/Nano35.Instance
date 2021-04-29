using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class CreatePaymentOfCommingUseCase :
        EndPointNodeBase<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreatePaymentOfCommingUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input) =>
            (await (new CreatePaymentofCommingRequest(_bus, input)).GetResponse());
    }
}