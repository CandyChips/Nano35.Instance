using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdRequest :
        MasstransitRequest
        <IGetInstanceByIdRequestContract,
            IGetInstanceByIdResultContract,
            IGetInstanceByIdSuccessResultContract,
            IGetInstanceByIdErrorResultContract>
    {
        public GetInstanceByIdRequest(IBus bus, IGetInstanceByIdRequestContract request) : base(bus, request) {}
    }
}