using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersName
{
    public class UpdateWorkersNameUseCase : UseCaseEndPointNodeBase<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateWorkersNameUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateWorkersNameResultContract>> Ask(IUpdateWorkersNameRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(_bus, input)
                .GetResponse();

        }
    }
}