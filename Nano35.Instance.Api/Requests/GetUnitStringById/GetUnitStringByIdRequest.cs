using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitStringById
{
    public class GetUnitStringByIdRequest: 
        MasstransitRequest
        <IGetUnitStringByIdRequestContract,
            IGetUnitStringByIdResultContract,
            IGetUnitStringByIdSuccessResultContract,
            IGetUnitStringByIdErrorResultContract>
    {
        public GetUnitStringByIdRequest(IBus bus, IGetUnitStringByIdRequestContract request) : base(bus, request) {}
    }
}