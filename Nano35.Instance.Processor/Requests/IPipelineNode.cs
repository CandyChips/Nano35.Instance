using System.Threading.Tasks;

namespace Nano35.Instance.Api.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }
}