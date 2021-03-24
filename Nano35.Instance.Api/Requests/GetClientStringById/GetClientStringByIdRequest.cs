using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientStringById
{
    public class GetClientStringByIdRequest :
        MasstransitRequest
        <IGetClientStringByIdRequestContract,
            IGetClientStringByIdResultContract,
            IGetClientStringByIdSuccessResultContract,
            IGetClientStringByIdErrorResultContract>
    {
        public GetClientStringByIdRequest(IBus bus, IGetClientStringByIdRequestContract request) : base(bus, request) {}
    }
}