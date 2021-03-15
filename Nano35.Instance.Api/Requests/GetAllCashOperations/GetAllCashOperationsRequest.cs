using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class GetAllCashOperationsRequest : 
        MasstransitRequest
        <IGetAllCashOperationsRequestContract, 
            IGetAllCashOperationsResultContract,
            IGetAllCashOperationsSuccessResultContract, 
            IGetAllCashOperationsErrorResultContract>
    {
        public GetAllCashOperationsRequest(IBus bus, IGetAllCashOperationsRequestContract request) : base(bus, request) {}
    }
}