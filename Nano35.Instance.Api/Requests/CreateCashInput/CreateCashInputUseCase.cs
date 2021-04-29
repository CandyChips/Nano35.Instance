using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateCashInput
{
    public class CreateCashInputUseCase :
        EndPointNodeBase<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreateCashInputUseCase(
            IBus bus,
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }

        public override async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input)
        {
            input.WorkerId = _auth.CurrentUserId;
            
            return (await new CreateCashInputRequest(_bus, input).GetResponse());
        }
    }
}