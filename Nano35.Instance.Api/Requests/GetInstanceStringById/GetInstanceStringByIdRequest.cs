using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceStringById
{
    public class GetInstanceStringByIdRequest : 
        MasstransitRequest
        <IGetInstanceStringByIdRequestContract,
            IGetInstanceStringByIdResultContract,
            IGetInstanceStringByIdSuccessResultContract,
            IGetInstanceStringByIdErrorResultContract>
    {
        public GetInstanceStringByIdRequest(IBus bus, IGetInstanceStringByIdRequestContract request) : base(bus, request) {}
    }
}