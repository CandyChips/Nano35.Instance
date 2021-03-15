using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class ValidatedCreatePaymentOfComingRequest:
        PipeNodeBase<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        public ValidatedCreatePaymentOfComingRequest(
            IPipeNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract> next) :
            base(next) {}

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input)
        {
            return await DoNext(input);
        }
    }
}