using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneRequest :
        MasstransitRequest
        <IUpdateInstancePhoneRequestContract,
            IUpdateInstancePhoneResultContract,
            IUpdateInstancePhoneSuccessResultContract,
            IUpdateInstancePhoneErrorResultContract>
    {
        public UpdateInstancePhoneRequest(IBus bus, IUpdateInstancePhoneRequestContract request) : base(bus, request) {}
    }
}