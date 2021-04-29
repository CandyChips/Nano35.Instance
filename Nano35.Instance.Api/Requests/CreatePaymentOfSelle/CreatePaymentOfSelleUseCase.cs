using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleUseCase :
        EndPointNodeBase<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreatePaymentOfSelleUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input) =>
            (await (new CreatePaymentOfSelleRequest(_bus, input)).GetResponse());
        
    }
}