using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitRequest :
        MasstransitRequest
        <IGetAvailableCashOfUnitRequestContract,
            IGetAvailableCashOfUnitResultContract,
            IGetAvailableCashOfUnitSuccessResultContract,
            IGetAvailableCashOfUnitErrorResultContract>
    {
        public GetAvailableCashOfUnitRequest(IBus bus, IGetAvailableCashOfUnitRequestContract request) : base(bus, request) {}
    }
}