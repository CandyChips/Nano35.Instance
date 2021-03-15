using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfSelle
{
    public class ValidatedCreatePaymentOfSelleRequest:
        PipeNodeBase<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract>
    {
        public ValidatedCreatePaymentOfSelleRequest(
            IPipeNode<ICreatePaymentOfSelleRequestContract, ICreatePaymentOfSelleResultContract> next) :
            base(next) {}

        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input)
        {
            return await DoNext(input);
        }
    }
}