using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class UpdateClientsNameUseCase : UseCaseEndPointNodeBase<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsNameUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateClientsNameResultContract>> Ask(IUpdateClientsNameRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(_bus, input).GetResponse();
        }
    }
}