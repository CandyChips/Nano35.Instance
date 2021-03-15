using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public class ValidatedCreateUnitRequest:
        PipeNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        public ValidatedCreateUnitRequest(
            IPipeNode<ICreateUnitRequestContract, ICreateUnitResultContract> next) :
            base(next) {}

        public override async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
        {
            return await DoNext(input);
        }
    }
}