using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeRequest :
        MasstransitRequest
        <IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract,
            IUpdateUnitsTypeSuccessResultContract,
            IUpdateUnitsTypeErrorResultContract>
    {
        public UpdateUnitsTypeRequest(IBus bus, IUpdateUnitsTypeRequestContract request) : base(bus, request) {}
    }
}