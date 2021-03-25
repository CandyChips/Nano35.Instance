using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsPhone
{
    public class UpdateClientsPhoneRequest :
        MasstransitRequest
        <IUpdateClientsPhoneRequestContract,
            IUpdateClientsPhoneResultContract,
            IUpdateClientsPhoneSuccessResultContract,
            IUpdateClientsPhoneErrorResultContract>
    {
        public UpdateClientsPhoneRequest(IBus bus, IUpdateClientsPhoneRequestContract request) : base(bus, request) {}
    }
}