using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class UpdateClientsNameRequest :
        MasstransitRequest
        <IUpdateClientsNameRequestContract,
            IUpdateClientsNameResultContract,
            IUpdateClientsNameSuccessResultContract,
            IUpdateClientsNameErrorResultContract>
    {
        public UpdateClientsNameRequest(IBus bus, IUpdateClientsNameRequestContract request) : base(bus, request) {}
    }
}