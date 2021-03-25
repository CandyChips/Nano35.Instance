using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersName
{
    public class UpdateWorkersNameRequest :
        MasstransitRequest
        <IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract,
            IUpdateWorkersNameSuccessResultContract,
            IUpdateWorkersNameErrorResultContract>
    {
        public UpdateWorkersNameRequest(IBus bus, IUpdateWorkersNameRequestContract request) : base(bus, request) {}
    }
}