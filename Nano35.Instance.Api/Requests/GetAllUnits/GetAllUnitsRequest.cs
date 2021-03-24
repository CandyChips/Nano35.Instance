using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class GetAllUnitsRequest :
        MasstransitRequest
        <IGetAllUnitsRequestContract,
            IGetAllUnitsResultContract,
            IGetAllUnitsSuccessResultContract,
            IGetAllUnitsErrorResultContract>
    {
        public GetAllUnitsRequest(IBus bus, IGetAllUnitsRequestContract request) : base(bus, request) {}
    }
}