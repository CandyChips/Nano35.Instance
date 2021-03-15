using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class ValidatedCreateClientRequest:
        PipeNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        public ValidatedCreateClientRequest(
            IPipeNode<ICreateClientRequestContract, ICreateClientResultContract> next) :
            base(next) {}

        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            return await DoNext(input);
        }
    }
}