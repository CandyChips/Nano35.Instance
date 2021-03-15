using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class ValidatedCreateWorkerRequest:
        PipeNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        public ValidatedCreateWorkerRequest(
            IPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> next) :
            base(next) {}

        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            return await DoNext(input);
        }
    }
}