using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class UpdateClientsSelleRequest:
        MasstransitRequest
        <IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract,
            IUpdateClientsSelleSuccessResultContract,
            IUpdateClientsSelleErrorResultContract>
    {
        public UpdateClientsSelleRequest(IBus bus, IUpdateClientsSelleRequestContract request) : base(bus, request) {}
    }
}