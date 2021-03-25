using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneRequest :
        MasstransitRequest
        <IUpdateUnitsPhoneRequestContract,
            IUpdateUnitsPhoneResultContract,
            IUpdateUnitsPhoneSuccessResultContract,
            IUpdateUnitsPhoneErrorResultContract>
    {
        public UpdateUnitsPhoneRequest(IBus bus, IUpdateUnitsPhoneRequestContract request) : base(bus, request) {}
    }
}