using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeRequest :
        MasstransitRequest
        <IUpdateClientsTypeRequestContract,
            IUpdateClientsTypeResultContract,
            IUpdateClientsTypeSuccessResultContract,
            IUpdateClientsTypeErrorResultContract>
    {
        public UpdateClientsTypeRequest(IBus bus, IUpdateClientsTypeRequestContract request) : base(bus, request) {}
    }
}