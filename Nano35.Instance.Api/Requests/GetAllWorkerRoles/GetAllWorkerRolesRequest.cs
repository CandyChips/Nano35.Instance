using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class GetAllWorkerRolesRequest :
        MasstransitRequest
        <IGetAllWorkerRolesRequestContract,
            IGetAllWorkerRolesResultContract,
            IGetAllWorkerRolesSuccessResultContract,
            IGetAllWorkerRolesErrorResultContract>
    {
        public GetAllWorkerRolesRequest(IBus bus, IGetAllWorkerRolesRequestContract request) : base(bus, request) {}
    }
}