using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class UpdateWorkersRoleRequest :
        MasstransitRequest
        <IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract,
            IUpdateWorkersRoleSuccessResultContract,
            IUpdateWorkersRoleErrorResultContract>
    {
        public UpdateWorkersRoleRequest(IBus bus, IUpdateWorkersRoleRequestContract request) : base(bus, request) {}
    }
}