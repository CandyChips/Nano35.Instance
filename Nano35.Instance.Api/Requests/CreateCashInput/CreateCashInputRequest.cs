using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateCashInput
{
    public class CreateCashInputRequest : 
        MasstransitRequest
        <ICreateCashInputRequestContract, 
            ICreateCashInputResultContract,
            ICreateCashInputSuccessResultContract, 
            ICreateCashInputErrorResultContract>
    {
        public CreateCashInputRequest(IBus bus, ICreateCashInputRequestContract request) : base(bus, request) {}
    }
}