using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitTypes
{
    public class GetAllUnitTypesRequest :
        MasstransitRequest
        <IGetAllUnitTypesRequestContract,
            IGetAllUnitTypesResultContract,
            IGetAllUnitTypesSuccessResultContract,
            IGetAllUnitTypesErrorResultContract>
    {
        public GetAllUnitTypesRequest(IBus bus, IGetAllUnitTypesRequestContract request) : base(bus, request) {}
    }
}