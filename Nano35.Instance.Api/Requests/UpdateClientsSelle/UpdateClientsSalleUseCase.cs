using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class UpdateClientsSelleUseCase : UseCaseEndPointNodeBase<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsSelleUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateClientsSelleResultContract>> Ask(IUpdateClientsSelleRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(_bus, input).GetResponse();
        }
    }
}