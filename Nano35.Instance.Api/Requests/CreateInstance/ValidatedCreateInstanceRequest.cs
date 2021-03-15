using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    
    public class ValidatedCreateInstanceRequest:
        PipeNodeBase<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        public ValidatedCreateInstanceRequest(
            IPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> next) :
            base(next) {}

        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            return await DoNext(input);
        }
    }
}