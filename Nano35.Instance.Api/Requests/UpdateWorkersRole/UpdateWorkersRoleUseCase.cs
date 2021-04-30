using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class UpdateWorkersRoleUseCase : EndPointNodeBase<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateWorkersRoleUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<IUpdateWorkersRoleResultContract> Ask(IUpdateWorkersRoleRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitRequest<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract, IUpdateWorkersRoleSuccessResultContract, IUpdateWorkersRoleErrorResultContract>(_bus, input)
                .GetResponse();
        }
    }
}