using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class UpdateClientsStateRequest :
        MasstransitRequest
        <IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract,
            IUpdateClientsStateSuccessResultContract,
            IUpdateClientsStateErrorResultContract>
    {
        public UpdateClientsStateRequest(IBus bus, IUpdateClientsStateRequestContract request) : base(bus, request) {}
    }
}