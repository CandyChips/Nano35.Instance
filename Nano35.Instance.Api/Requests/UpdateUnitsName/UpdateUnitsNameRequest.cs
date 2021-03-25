using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameRequest :
        MasstransitRequest
        <IUpdateUnitsNameRequestContract,
            IUpdateUnitsNameResultContract,
            IUpdateUnitsNameSuccessResultContract,
            IUpdateUnitsNameErrorResultContract>
    {
        public UpdateUnitsNameRequest(IBus bus, IUpdateUnitsNameRequestContract request) : base(bus, request) {}
    }
}