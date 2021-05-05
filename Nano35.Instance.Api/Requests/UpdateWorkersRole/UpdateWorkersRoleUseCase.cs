using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class UpdateWorkersRoleUseCase : UseCaseEndPointNodeBase<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateWorkersRoleUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateWorkersRoleResultContract>> Ask(IUpdateWorkersRoleRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(_bus, input)
                .GetResponse();
        }
    }
}