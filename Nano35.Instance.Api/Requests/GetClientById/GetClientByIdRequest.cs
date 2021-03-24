using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class GetClientByIdRequest :
        MasstransitRequest
        <IGetClientByIdRequestContract,
            IGetClientByIdResultContract,
            IGetClientByIdSuccessResultContract,
            IGetClientByIdErrorResultContract>
    {
        public GetClientByIdRequest(IBus bus, IGetClientByIdRequestContract request) : base(bus, request) {}
    }
}