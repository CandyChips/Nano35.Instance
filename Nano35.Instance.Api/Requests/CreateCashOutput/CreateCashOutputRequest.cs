using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class CreateCashOutputRequest : 
        MasstransitRequest
        <ICreateCashOutputRequestContract, 
            ICreateCashOutputResultContract,
            ICreateCashOutputSuccessResultContract, 
            ICreateCashOutputErrorResultContract>
    {
        public CreateCashOutputRequest(IBus bus, ICreateCashOutputRequestContract request) : base(bus, request) {}
    }
}