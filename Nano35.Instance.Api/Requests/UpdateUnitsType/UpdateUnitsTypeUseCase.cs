using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeUseCase : UseCaseEndPointNodeBase<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsTypeUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateUnitsTypeResultContract>> Ask(IUpdateUnitsTypeRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(_bus, input).GetResponse();
        }
    }
}