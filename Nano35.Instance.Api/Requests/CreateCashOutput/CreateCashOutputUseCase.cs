using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class CreateCashOutputUseCase :
        EndPointNodeBase<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreateCashOutputUseCase(
            IBus bus,
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input)
        {
            input.WorkerId = _auth.CurrentUserId;
            var request = new CreateCashOutputRequest(_bus, input);
            return (await request.GetResponse());
        }
    }
}