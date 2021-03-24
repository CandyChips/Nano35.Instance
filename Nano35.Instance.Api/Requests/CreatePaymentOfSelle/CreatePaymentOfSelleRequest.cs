using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleRequest :
        MasstransitRequest
        <ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract,
            ICreatePaymentOfSelleSuccessResultContract,
            ICreatePaymentOfSelleErrorResultContract>
    {
        public CreatePaymentOfSelleRequest(IBus bus, ICreatePaymentOfSelleRequestContract request) : base(bus, request) {}
    }
}