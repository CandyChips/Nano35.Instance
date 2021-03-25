using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class UpdateUnitsAddressRequest :
        MasstransitRequest
        <IUpdateUnitsAddressRequestContract,
            IUpdateUnitsAddressResultContract,
            IUpdateUnitsAddressSuccessResultContract,
            IUpdateUnitsAddressErrorResultContract>
    {
        public UpdateUnitsAddressRequest(IBus bus, IUpdateUnitsAddressRequestContract request) : base(bus, request) {}
    }
}