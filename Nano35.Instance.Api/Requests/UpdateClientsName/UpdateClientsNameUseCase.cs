using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class UpdateClientsNameUseCase : EndPointNodeBase<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsNameUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<IUpdateClientsNameResultContract> Ask(IUpdateClientsNameRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitRequest<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract, IUpdateClientsNameSuccessResultContract, IUpdateClientsNameErrorResultContract>(_bus, input)
                .GetResponse();
        }
    }
}