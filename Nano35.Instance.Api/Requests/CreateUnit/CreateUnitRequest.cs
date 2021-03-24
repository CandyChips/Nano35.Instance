using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public class CreateUnitRequest :
        MasstransitRequest
        <ICreateUnitRequestContract,
            ICreateUnitResultContract,
            ICreateUnitSuccessResultContract,
            ICreateUnitErrorResultContract>
    {
        public CreateUnitRequest(IBus bus, ICreateUnitRequestContract request) : base(bus, request) {}
    }
}