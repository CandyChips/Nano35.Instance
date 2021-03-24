using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class CreatePaymentofCommingRequest : 
        MasstransitRequest
        <ICreatePaymentOfComingRequestContract,
            ICreatePaymentOfComingResultContract,
            ICreatePaymentOfComingSuccessResultContract,
            ICreatePaymentOfComingErrorResultContract>
    {
        public CreatePaymentofCommingRequest(IBus bus, ICreatePaymentOfComingRequestContract request) : base(bus, request) {}
    }
}