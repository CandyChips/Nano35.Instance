using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class UpdateClientsStateUseCase : UseCaseEndPointNodeBase<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsStateUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        public override async Task<UseCaseResponse<IUpdateClientsStateResultContract>> Ask(IUpdateClientsStateRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(_bus, input)
                .GetResponse();
        }
    }
}