using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class UpdateClientsEmailRequest :
        MasstransitRequest
        <IUpdateClientsEmailRequestContract,
            IUpdateClientsEmailResultContract,
            IUpdateClientsEmailSuccessResultContract,
            IUpdateClientsEmailErrorResultContract>
    {
        public UpdateClientsEmailRequest(IBus bus, IUpdateClientsEmailRequestContract request) : base(bus, request) {}
    }
}