using MediatR;

namespace Nano35.Instance.Processor.Requests.Behaviours
{
    public interface IQueryRequest<TOut> :
        IRequest<TOut>
    {
        
    }
    public interface ICommandRequest<TOut> :
        IRequest<TOut>
    {
        
    }
}