using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class GetAllUnitsByTypeRequest :
        MasstransitRequest
        <IGetAllUnitsByTypeRequestContract,
            IGetAllUnitsByTypeResultContract,
            IGetAllUnitsByTypeSuccessResultContract,
            IGetAllUnitsByTypeErrorResultContract>
    {
        public GetAllUnitsByTypeRequest(IBus bus, IGetAllUnitsByTypeRequestContract request) : base(bus, request) {}
    }
}