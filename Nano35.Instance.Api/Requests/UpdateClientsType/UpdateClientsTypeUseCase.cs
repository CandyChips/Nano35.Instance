using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeUseCase : UseCaseEndPointNodeBase<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsTypeUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateClientsTypeResultContract>> Ask(IUpdateClientsTypeRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(_bus, input)
                .GetResponse();
        }
    }
}