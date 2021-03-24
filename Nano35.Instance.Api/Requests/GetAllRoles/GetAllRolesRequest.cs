using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesRequest:
        MasstransitRequest
        <IGetAllRolesRequestContract,
            IGetAllRolesResultContract,
            IGetAllRolesSuccessResultContract,
            IGetAllRolesErrorResultContract>
    {
        public GetAllRolesRequest(IBus bus, IGetAllRolesRequestContract request) : base(bus, request) {}
    }
}