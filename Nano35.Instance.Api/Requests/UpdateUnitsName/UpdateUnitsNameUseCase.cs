using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameUseCase : UseCaseEndPointNodeBase<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsNameUseCase(IBus bus, ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        public override async Task<UseCaseResponse<IUpdateUnitsNameResultContract>> Ask(IUpdateUnitsNameRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(_bus, input)
                .GetResponse();

        }
    }
}